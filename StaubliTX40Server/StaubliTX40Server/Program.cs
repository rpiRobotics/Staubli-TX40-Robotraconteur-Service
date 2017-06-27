using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotRaconteur;
using Staubli.Robotics.Soap.Proxies.ServerV0;
using Staubli.Robotics.Soap.Proxies.ServerV1;
using Staubli.Robotics.Soap.Proxies.ServerV3;
using System.ServiceModel;
using System.Globalization;
using StaubliSoapClient;

namespace StaubliTX40Server
{
    class Program
    {
        static StaubliTX40 robot;
        static void Main(string[] args)
        {

            RobotRaconteurNode.s.NodeName = "StaubliTX40Server";
            RobotRaconteurNode.s.NodeID = new NodeID("241c:2283:cc37:764f:b784:999f:a7d7:e3e8");

            RobotRaconteurNode.s.RegisterServiceType(new StaubliTX40_interface.StaubliTX40_interfaceFactory());
            TcpChannel t = new RobotRaconteur.TcpChannel();
            t.StartServer(4445);

            RobotRaconteurNode.s.RegisterChannel(t);
           robot= new StaubliTX40();
            robot.Connect(/*"127.0.0.1"*/ "10.10.90.1", 5653);
 
            //robot.Connect("127.0.0.1", 5653);

            //server.Connect(...)

             RobotRaconteurNode.s.RegisterService("StaubliTX40","StaubliTX40_interface",robot);

             Console.WriteLine("Server started");
             Console.ReadLine();

             RobotRaconteurNode.s.Shutdown();


        }


    }


    


    public class StaubliTX40 : StaubliTX40_interface.StaubliTX40
    {

        private string m_address;
        private Robot m_currentRobot = null;
        private int m_sessionId = -1;
        private CS8ServerV0PortTypeClient m_cs8ServerV0;
        private CS8ServerV1PortTypeClient m_cs8ServerV1;
        private CS8ServerV3PortTypeClient m_cs8ServerV3;
        private double[] m_jointPosition;


        private const string m_applicationName = "soapSample.pjx";

        public bool Connected { get { return m_Connected; } }
        protected bool m_Connected = false;

        public void Connect(string address, int port)
        {

            // Create an instance of the soap server V0
            m_address = "http://" + address + ":" + port;

            m_cs8ServerV0 = null;


            //CS8ServerV0(m_txtBoxIpAdress.Text, m_txtBoxPortNumber.Text);
            m_cs8ServerV0 = new CS8ServerV0PortTypeClient();
            m_cs8ServerV0.Endpoint.Address = new System.ServiceModel.EndpointAddress(m_address);

            m_cs8ServerV3 = new CS8ServerV3PortTypeClient();
            m_cs8ServerV3.Endpoint.Address = new System.ServiceModel.EndpointAddress(this.m_address + "/CS8ServerV3");

            // If soap server is created, enable others tests
            if (m_cs8ServerV0 == null) throw new Exception("Could not connect to robot");
            // will generate an error if cannot connect to server
            SoapServerVersion l_soapVersion = m_cs8ServerV0.getSoapServerVersion("me", "0");
            double l_version = Convert.ToDouble(l_soapVersion.version, CultureInfo.GetCultureInfo("en-US").NumberFormat);

            m_cs8ServerV0.login("default", "", out m_sessionId);



            m_currentRobot = m_cs8ServerV0.getRobots(m_sessionId)[0];



            m_cs8ServerV3.setSchedulingMode(m_sessionId, SchedulingMode.SCHEDULINGINTERNAL);

            m_Connected = true;
        }

        private Frame getAsIdentity()
        {
            Frame l_ret = new Frame();
            Tools.setRxRyRzCoord(0, 0, 0, out l_ret);
            l_ret.px = 0;
            l_ret.py = 0;
            l_ret.pz = 0;
            return l_ret;
        }

        public void Close()
        {
            if (!Connected) return;
            m_cs8ServerV0.Close();
        }

        //start test cartesian code
        public StaubliTX40_interface.Frame ActualCartesianPos
        {
            get
            {
               
                //Cartesian Pos forwardKin(int sessionId, int robotIndex, double[] jointPos, out Config config)
                double[] jointPos = m_cs8ServerV0.getRobotJointPos(m_sessionId, 0);
                Config config = new Config();
                Frame temp = new Frame();
                temp = m_cs8ServerV3.forwardKin(m_sessionId, 0, jointPos, out config);

                
                double[] position = { temp.px, temp.py, temp.pz };
                RobotRaconteur.MultiDimArray a = new RobotRaconteur.MultiDimArray();
                a.DimCount = 2;
                a.Dims = new int[] { 3, 3 }; 
                a.Complex = false;
                //a.Imag = null;
               
                double[] rotation = { temp.nx, temp.ny, temp.nz, temp.ox, temp.oy, temp.oz, temp.ax, temp.ay, temp.az };
               
                a.Real = rotation;
                StaubliTX40_interface.Frame f = new StaubliTX40_interface.Frame();
                f.Position = position;
                f.Rotation = a;
                return f;
            }

            set
            {
                //double[] rotation = (double[])a.Real;
                throw new NotImplementedException("Property is read only");

            }
        }
        StaubliTX40_interface.Frame c_des = null; //cartesian position requires position vector and rotation matrix

        bool firstset = true;

        public StaubliTX40_interface.Frame DesiredCartesianPos
        {
            get
            {
                return c_des;
            }
            set
            {
                

                if (value.Rotation.DimCount != 2) throw new InvalidOperationException("Rotation matrix must be 3x3");
                if (value.Rotation.Dims[0] != 3 && c_des.Rotation.Dims[1] !=3) throw new InvalidOperationException("Rotation matrix must be 3x3");

                
                //copied mdesc code
                MotionDesc l_mdesc = new MotionDesc();
                l_mdesc.acc = 2.25;
                l_mdesc.dec = 2.25;
                l_mdesc.vel = 1.50;
                l_mdesc.transVel = 99999;
                l_mdesc.rotVel = 99999;
                l_mdesc.freq = 1.0;
                l_mdesc.absRel = MoveType.ABSOLUTEMOVE;
                l_mdesc.config = new Config();
                l_mdesc.config.Item = new AnthroConfig();
                ((AnthroConfig)l_mdesc.config.Item).shoulder = ShoulderConfig.SFREE;
                ((AnthroConfig)l_mdesc.config.Item).elbow = PositiveNegativeConfig.PNFREE;
                ((AnthroConfig)l_mdesc.config.Item).wrist = PositiveNegativeConfig.PNFREE;
                l_mdesc.frame = getAsIdentity();
                l_mdesc.tool = getAsIdentity();
                //l_mdesc.blendType = BlendType.BLENDOFF;
                l_mdesc.blendType = BlendType.BLENDJOINT;
                l_mdesc.distBlendPrev = (1 / 1000.0);		// in meter
                l_mdesc.distBlendNext = (1/ 1000.0);		// in meter
                //
                MotionReturnCode l_rc;
                Frame r_frame = new Frame();
                //position:
                r_frame.px = value.Position[0];
                r_frame.py = value.Position[1];
                r_frame.pz = value.Position[2];
                
                //rotation:
                double[] r = (double[])value.Rotation.Real;
                r_frame.nx = r[0];
                r_frame.ny = r[1];
                r_frame.nz = r[2];
                r_frame.ox = r[3];
                r_frame.oy = r[4];
                r_frame.oz = r[5];
                r_frame.ax = r[6];
                r_frame.ay = r[7];
                r_frame.az = r[8];

                //Config l_cfg = new Config();
                //l_cfg.Item = new AnthroConfig();
                //((AnthroConfig)l_cfg.Item).shoulder = ShoulderConfig.SFREE;
                //((AnthroConfig)l_cfg.Item).elbow = PositiveNegativeConfig.PNFREE;
                //((AnthroConfig)l_cfg.Item).wrist = PositiveNegativeConfig.PNFREE;

               // m_cs8ServerV3.reverseKin(m_sessionId,0,ActualJointAngles,r_frame,

                //getting timeout error
               // m_cs8ServerV3.resetMotion(m_sessionId);

                if (c_des == null) c_des = value;

                StaubliTX40_interface.Frame currentpos = ActualCartesianPos;

                double dist_sq=Math.Pow(currentpos.Position[0] -c_des.Position[0],2 ) +Math.Pow(currentpos.Position[1] -c_des.Position[1],2 )
                    +Math.Pow(currentpos.Position[2] -c_des.Position[2],2 );
                double dist=Math.Sqrt(dist_sq);

                double dist_mv_sq = Math.Pow(value.Position[0] - c_des.Position[0], 2) + Math.Pow(value.Position[1] - c_des.Position[1], 2)
                    + Math.Pow(value.Position[2] - c_des.Position[2], 2);
                double dist_mv = Math.Sqrt(dist_mv_sq);

                //Console.WriteLine("dist={0} dist_mv={1}", dist, dist_mv);

                if ( (dist< .005 && dist_mv > .007) || firstset)
                {
                    m_cs8ServerV3.moveJC(m_sessionId, 0, r_frame, l_mdesc, out l_rc); //sends move command to robot
                    c_des = value;
                    //Console.WriteLine("Move!");
                    firstset = false;
                }
            }
        }
        //end test cartesian code

        public double[] ActualJointAngles
        {
            get
            {
                return m_cs8ServerV0.getRobotJointPos(m_sessionId, 0);
            }

            set
            {
            }
        }
        
        private double[] m_des = new double[6];
        public double[] DesiredJointAngles
        {
            get
            {
                return m_des;
            }

            set
            {
                if (value.Length != 6) throw new InvalidOperationException("Joint position vector must have size of 6");

                MotionDesc l_mdesc = new MotionDesc();
                l_mdesc.acc = 2.25;
                l_mdesc.dec = 2.25;
                l_mdesc.vel = 1.50;
                l_mdesc.transVel = 99999;
                l_mdesc.rotVel = 99999;
                l_mdesc.freq = 1.0;
                l_mdesc.absRel = MoveType.ABSOLUTEMOVE;
                l_mdesc.config = new Config();
                l_mdesc.config.Item = new AnthroConfig();
                ((AnthroConfig)l_mdesc.config.Item).shoulder = ShoulderConfig.SSAME;
                ((AnthroConfig)l_mdesc.config.Item).elbow = PositiveNegativeConfig.PNSAME;
                ((AnthroConfig)l_mdesc.config.Item).wrist = PositiveNegativeConfig.PNSAME;
                l_mdesc.frame = getAsIdentity();
                l_mdesc.tool = getAsIdentity();
                //l_mdesc.blendType = BlendType.BLENDOFF;
                l_mdesc.blendType = BlendType.BLENDJOINT;
                l_mdesc.distBlendPrev = (1/ 1000.0);		// in meter
                l_mdesc.distBlendNext = (1/ 1000.0);		// in meter

                MotionReturnCode l_rc;
                m_cs8ServerV3.resetMotion(m_sessionId);
                m_cs8ServerV3.moveJJ(m_sessionId, 0, value, l_mdesc, out l_rc);
                m_des = value;
            }

        }

    }

}

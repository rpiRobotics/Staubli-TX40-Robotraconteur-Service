classdef RobotRaconteurPipeEndpoint < handle
    properties
       robotraconteurstubid=[];
       robotraconteurpipeendpointid=[];
       
       Available;
    end
    
    methods
        
        function count=get.Available(c)
           count=RobotRaconteurMex('PipeAvailable',c.robotraconteurstubid,c.robotraconteurpipeendpointid);
            
        end
        
        function Close(c)
           RobotRaconteurMex('ClosePipe',c.robotraconteurstubid,c.robotraconteurpipeendpointid);
            
        end
        
        function packet=ReceivePacket(c)
            packet=RobotRaconteurMex('PipeReceivePacket',c.robotraconteurstubid,c.robotraconteurpipeendpointid);
        end
        
        function SendPacket(c,packet)
            RobotRaconteurMex('PipeSendPacket',c.robotraconteurstubid,c.robotraconteurpipeendpointid,packet);
        end
        
        
        
    end
    
    events
       PacketReceived; 
        
    end
    
end
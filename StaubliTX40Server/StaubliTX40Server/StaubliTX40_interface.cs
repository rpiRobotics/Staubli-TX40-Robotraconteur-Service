using System;
using RobotRaconteur;
using System.Collections.Generic;
namespace StaubliTX40_interface{

public class StaubliTX40_interfaceFactory : ServiceFactory {
    public override string DefString() {
    const string d=@"service StaubliTX40_interface


struct Frame
    field double[*] Rotation
    field double[3] Position
end struct


object StaubliTX40
    property double[6] ActualJointAngles
    property double[6] DesiredJointAngles
    property Frame ActualCartesianPos
    property Frame DesiredCartesianPos
end object


";
    return d;
    }
    public override string GetServiceName() {return "StaubliTX40_interface";}
    public Frame_stub Frame_stubentry;
    public StaubliTX40_interfaceFactory() {
    Frame_stubentry=new Frame_stub(this);
    }
    public override IStructureStub FindStructureStub(string objecttype) {
    string objshort=RemovePath(objecttype);
    switch (objshort) {
    case "Frame":
    return  Frame_stubentry;
    }
    throw new DataTypeException("Cannot find appropriate structure stub");
    }
    public override MessageElementStructure PackStructure(Object s) {
    if (s==null) return null;
    string objtype=s.GetType().ToString();
    if (objtype.Split(new char[] {'.'})[0] == "StaubliTX40_interface") {
    string objshort=RemovePath(objtype);
    switch (objshort) {
    case "Frame":
    return  Frame_stubentry.PackStructure(s);
    }
    } else {
    return RobotRaconteurNode.s.PackStructure(s);
    }
    throw new Exception();
    }
    public override T UnpackStructure<T>(MessageElementStructure l) {
    if (l==null) return default(T);
    if (l.Type.Split(new char[] {'.'})[0] == "StaubliTX40_interface") {
    string objshort=RemovePath(l.Type);
    switch (objshort) {
    case "Frame":
    return  Frame_stubentry.UnpackStructure<T>(l);
    }
    } else {
    return RobotRaconteurNode.s.UnpackStructure<T>(l);
    }
    throw new DataTypeException("Could not unpack structure");
    }
    public override ServiceStub CreateStub(string objecttype, string path, ClientContext context) {
    if (objecttype.Split(new char[] {'.'})[0] == "StaubliTX40_interface") {
    string objshort=RemovePath(objecttype);
    switch (objshort) {
    case "StaubliTX40":
    return new StaubliTX40_stub(path,context);
    }
    } else {
    string ext_service_type=objecttype.Split(new char[] {'.'})[0];
    return RobotRaconteurNode.s.GetServiceFactory(ext_service_type).CreateStub(objecttype,path,context);
    }
    throw new ServiceException("Could not create stub");
    }
    public override ServiceSkel CreateSkel(string path,object obj,ServerContext context) {
    string objtype=ServerContext.FindParentInterface(obj.GetType()).ToString();
    if (objtype.ToString().Split(new char[] {'.'})[0] == "StaubliTX40_interface") {
    string sobjtype=RemovePath(objtype);
    switch(sobjtype) {
    case "StaubliTX40":
    return new StaubliTX40_skel(path,(StaubliTX40)obj,context);
    }
    } else {
    string ext_service_type=objtype.ToString().Split(new char[] {'.'})[0];
    return RobotRaconteurNode.s.GetServiceFactory(ext_service_type).CreateSkel(path,obj,context);
    }
    throw new ServiceException("Could not create skel");
    }
}

public class Frame {
    public MultiDimArray Rotation;
    public double[] Position;
}

[RobotRaconteurServiceObjectInterface()]
public interface StaubliTX40  {
    double[] ActualJointAngles { get; set;}
    double[] DesiredJointAngles { get; set;}
    Frame ActualCartesianPos { get; set;}
    Frame DesiredCartesianPos { get; set;}




}
}

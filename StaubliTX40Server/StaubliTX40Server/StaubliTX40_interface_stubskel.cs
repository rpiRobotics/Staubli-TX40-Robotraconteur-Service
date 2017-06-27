using System;
using RobotRaconteur;
using System.Collections.Generic;
namespace StaubliTX40_interface{

public class Frame_stub : IStructureStub {
    public Frame_stub(StaubliTX40_interfaceFactory d) {def=d;}
    private StaubliTX40_interfaceFactory def;
    public MessageElementStructure PackStructure(Object s1) {
    List<MessageElement> m=new List<MessageElement>();
    if (s1 ==null) return null;
    Frame s = (Frame)s1;
    m.Add(new MessageElement("Rotation",RobotRaconteurNode.s.PackMultiDimArray((MultiDimArray)s.Rotation)));
    m.Add(new MessageElement("Position",s.Position));
    return new MessageElementStructure("StaubliTX40_interface.Frame",m);
    }
    public T UnpackStructure<T>(MessageElementStructure m) {
    if (m == null ) return default(T);
    Frame s=new Frame();
    s.Rotation=RobotRaconteurNode.s.UnpackMultiDimArray((MessageElementMultiDimArray)MessageElement.FindElement(m.Elements,"Rotation").Data);
    s.Position=(double[])MessageElement.FindElement(m.Elements,"Position").Data;
    T st; try {st=(T)((object)s);} catch (InvalidCastException e) {throw new DataTypeMismatchException("Wrong structuretype");}
    return st;
    }
}

public class StaubliTX40_stub : ServiceStub , StaubliTX40 {
    public StaubliTX40_stub(string path,ClientContext c) : base(path,c) {
    }
    public double[] ActualJointAngles {
        get {
        MessageEntry m = new MessageEntry(MessageEntryType.PropertyGetReq, "ActualJointAngles");
        MessageEntry mr=ProcessTransaction(m);
        MessageElement me=mr.FindElement("value");
        return (double[])me.Data;
        }
        set {
        MessageEntry m=new MessageEntry(MessageEntryType.PropertySetReq,"ActualJointAngles");
        m.AddElement(new MessageElement("value",value));
        MessageEntry mr=ProcessTransaction(m);
        }
    }
    public double[] DesiredJointAngles {
        get {
        MessageEntry m = new MessageEntry(MessageEntryType.PropertyGetReq, "DesiredJointAngles");
        MessageEntry mr=ProcessTransaction(m);
        MessageElement me=mr.FindElement("value");
        return (double[])me.Data;
        }
        set {
        MessageEntry m=new MessageEntry(MessageEntryType.PropertySetReq,"DesiredJointAngles");
        m.AddElement(new MessageElement("value",value));
        MessageEntry mr=ProcessTransaction(m);
        }
    }
    public Frame ActualCartesianPos {
        get {
        MessageEntry m = new MessageEntry(MessageEntryType.PropertyGetReq, "ActualCartesianPos");
        MessageEntry mr=ProcessTransaction(m);
        MessageElement me=mr.FindElement("value");
        return context.UnpackStructure<Frame>((MessageElementStructure)me.Data);
        }
        set {
        MessageEntry m=new MessageEntry(MessageEntryType.PropertySetReq,"ActualCartesianPos");
        m.AddElement(new MessageElement("value",context.PackStructure(value)));
        MessageEntry mr=ProcessTransaction(m);
        }
    }
    public Frame DesiredCartesianPos {
        get {
        MessageEntry m = new MessageEntry(MessageEntryType.PropertyGetReq, "DesiredCartesianPos");
        MessageEntry mr=ProcessTransaction(m);
        MessageElement me=mr.FindElement("value");
        return context.UnpackStructure<Frame>((MessageElementStructure)me.Data);
        }
        set {
        MessageEntry m=new MessageEntry(MessageEntryType.PropertySetReq,"DesiredCartesianPos");
        m.AddElement(new MessageElement("value",context.PackStructure(value)));
        MessageEntry mr=ProcessTransaction(m);
        }
    }



    public override void DispatchEvent(MessageEntry m) {
    string ename=m.MemberName;
    switch (ename) {
    }
    }
    public override void DispatchPipeMessage(MessageEntry m)
    {
    switch (m.MemberName) {
    default:
    throw new Exception();
    }
    }
}
public class StaubliTX40_skel : ServiceSkel {
    StaubliTX40 obj { get { return (StaubliTX40)uncastobj;}}
    public StaubliTX40_skel(string p,StaubliTX40 o,ServerContext c) : base(p,o,c) {
    uncastobj=o;
    }
    public override MessageEntry CallGetProperty(MessageEntry m) {
    string ename=m.MemberName;
    MessageEntry mr=new MessageEntry(MessageEntryType.PropertyGetRes, ename);
    switch (ename) {
    case "ActualJointAngles": {
    Object value=obj.ActualJointAngles;
    mr.AddElement(new MessageElement("value",value));
    break;
    }
    case "DesiredJointAngles": {
    Object value=obj.DesiredJointAngles;
    mr.AddElement(new MessageElement("value",value));
    break;
    }
    case "ActualCartesianPos": {
    Object value=obj.ActualCartesianPos;
    mr.AddElement(new MessageElement("value",context.PackStructure(value)));
    break;
    }
    case "DesiredCartesianPos": {
    Object value=obj.DesiredCartesianPos;
    mr.AddElement(new MessageElement("value",context.PackStructure(value)));
    break;
    }
    default:
    throw new MemberNotFoundException("Member not found");
    }
    return mr;
    }

    public override MessageEntry CallSetProperty(MessageEntry m) {
    string ename=m.MemberName;
    MessageElement me=m.FindElement("value");
    MessageEntry mr=new MessageEntry(MessageEntryType.PropertySetRes, ename);
    switch (ename) {
    case "ActualJointAngles": {
    obj.ActualJointAngles=(double[])me.Data;
    break;
    }
    case "DesiredJointAngles": {
    obj.DesiredJointAngles=(double[])me.Data;
    break;
    }
    case "ActualCartesianPos": {
    obj.ActualCartesianPos=context.UnpackStructure<Frame>((MessageElementStructure)me.Data);
    break;
    }
    case "DesiredCartesianPos": {
    obj.DesiredCartesianPos=context.UnpackStructure<Frame>((MessageElementStructure)me.Data);
    break;
    }
    default:
    throw new MemberNotFoundException("Member not found");
    }
    return mr;
    }

    public override MessageEntry CallFunction(MessageEntry m) {
    string ename=m.MemberName;
    MessageEntry mr=new MessageEntry(MessageEntryType.FunctionCallRes, ename);
    switch (ename) {
    default:
    throw new MemberNotFoundException("Member not found");
    }
    return mr;
    }
    public override void ReleaseCastObject() {}


    public override void RegisterEvents(Object obj1) {
    StaubliTX40 obj = obj1 as StaubliTX40;
    base.RegisterEvents(obj1);

    }
    public override void UnregisterEvents(Object obj1) {
    StaubliTX40 obj = obj1 as StaubliTX40;
    base.UnregisterEvents(obj1);

    }

    public override Object GetSubObj(string name, string ind) {
    switch (name) {
    default:
    throw new MemberNotFoundException("Member not found");
    }
    }
    private bool m_InitPipeServersRun=false;
    public override void InitPipeServers(object o) {
    if (m_InitPipeServersRun) return;
    m_InitPipeServersRun=true;
    StaubliTX40 castobj=(StaubliTX40)o;
    }
    public override void DispatchPipeMessage(MessageEntry m, Endpoint e)
    {
    switch (m.MemberName) {
    default:
    throw new MemberNotFoundException("Member not found");
    }
    }
    public override MessageEntry CallPipeFunction(MessageEntry m,Endpoint e) {
    string ename=m.MemberName;
    switch (ename) {
    default:
    throw new MemberNotFoundException("Member not found");
    }
    }
}

}

function RobotRaconteurServiceRegisterEvents(skelid,obj)

e=events(obj);

n=length(e);

for i=1:n
   callb=eval(['@(h,eventdat) RobotRaconteurMex(''ServiceDispatchEvent'',int32(' num2str(skelid) '),eventdat)']);
   
   addlistener(obj,e{i},callb);
   
end
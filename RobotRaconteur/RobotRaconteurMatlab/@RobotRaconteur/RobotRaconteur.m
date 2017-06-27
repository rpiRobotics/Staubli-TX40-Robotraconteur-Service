classdef RobotRaconteur
         
    methods
        function a=RobotRaconteur(c)
           error('Valid commands are Connect, Disconnect, EnableEvents, and DisableEvents'); 
        end
    end
    
    methods(Static=true)
        function ret=Connect(url)
           ret=RobotRaconteurMex('Connect',url); 
        end
        
        function Disconnect(objref)
           RobotRaconteurMex('Disconnect',objref.robotraconteurstubid);
        end
        
        function EnableEvents(objref)
           RobotRaconteurMex('SetEnableEvents',objref.robotraconteurstubid,int32(1));
        end
        
        function DisableEvents(objref)
           RobotRaconteurMex('SetEnableEvents',objref.robotraconteurstubid,int32(0));
        end
    end
    
end


classdef RobotRaconteur
         
    methods
        function a=RobotRaconteur(c)
           error('Valid commands are Connect, Disconnect, EnableEvents, and DisableEvents'); 
        end
    end
    
    methods(Static=true)
        function ret=Connect(url,username,credentials)
			if nargin ==1
				ret=RobotRaconteurMex('Connect',url); 
			else
				ret=RobotRaconteurMex('Connect',url,username,credentials); 
		    end
        end
		
		
        
        function Disconnect(objref)
           RobotRaconteurMex('Disconnect',objref.robotraconteurstubid);
        end
        
        function EnableEvents(objref)
           if ( strcmp(class(objref),'RobotRaconteurPipeEndpoint'))
               RobotRaconteurMex('PipeSetEnableEvents',objref.robotraconteurstubid,objref.robotraconteurpipeendpointid,int32(1));
           else
               RobotRaconteurMex('SetEnableEvents',objref.robotraconteurstubid,int32(1));
           end
        end
        
        function DisableEvents(objref)
           if ( strcmp(class(a),'RobotRaconteurPipeEndpoint'))
               RobotRaconteurMex('SetEnablePipeEvents',objref.robotraconteurstubid,objref.robotraconteurpipeendpointid,int32(0));
           else
               RobotRaconteurMex('SetEnableEvents',objref.robotraconteurstubid,int32(0));
           end
        end
        
        function s=FindService(name)
           s=RobotRaconteurMex('FindService',name); 
        end
        
        function DispatchEvents()
           
           RobotRaconteurMex('DispatchEvents');
        end
        
        function RequestObjectLock(objref)
           RobotRaconteurMex('RequestObjectLock',objref.robotraconteurstubid); 
        end
        
        function ReleaseObjectLock(objref)
           RobotRaconteurMex('ReleaseObjectLock',objref.robotraconteurstubid); 
        end
        
    end
    
end


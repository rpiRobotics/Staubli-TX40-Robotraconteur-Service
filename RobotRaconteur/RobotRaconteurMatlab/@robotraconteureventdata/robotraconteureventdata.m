%  Robot Raconteur - A communication library for robotics and automation systems
%  Copyright (C) 2011  John Wason <wasonj@alum.rpi.edu>
%                      Center for Automation Technologies and Systems
%                      Rensselaer Polytechnic Institute
%
%  This program is free software; you can redistribute it and/or
%  modify it under the terms of the Lesser GNU General Public License
%  as published by the Free Software Foundation; either version 3
%  of the License, or (at your option) any later version.
%
%  This program is distributed in the hope that it will be useful,
%  but WITHOUT ANY WARRANTY; without even the implied warranty of
%  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
%  Lesser GNU General Public License for more details.
%
%  You should have received a copy of the Lesser GNU General Public License
%  along with Robot Raconteur.  If not, see <http:%www.gnu.org/licenses/>.

classdef robotraconteureventdata < event.EventData
   properties
      Parameters
   end

   methods
      function data = robotraconteureventdata(newParameters)
         data.Parameters = newParameters;
      end
   end
end
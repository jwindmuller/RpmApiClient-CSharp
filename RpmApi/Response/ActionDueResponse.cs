using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ActionDueResponse : Abstract.Response
	{
		public int ActionsDue { get; set; }
		public int ActionsDueHigh { get; set; }
		public int ActionsDueNormal { get; set; }
		public string _Process { get; set; }
		public string Process { 
			get {
				if (_Process == null)
				{
					_Process = "";
				}
				return _Process;
			}
			set { _Process = value; } 
		}
		public int ProcessID { get; set; }
	}
}

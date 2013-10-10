using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ProcActionDueResponse : Abstract.Response
	{
		private List<ActionDueResponse> _Due { get; set; }
		public List<ActionDueResponse> Due { 
			get {
				if (_Due == null)
				{
					_Due = new List<ActionDueResponse>();
				}
				return _Due;
			}
			set { _Due = value; } 
		}

		private string _Staff { get; set; }
		public string Staff { 
			get {
				if (_Staff == null)
				{
					_Staff = "";
				}
				return _Staff;
			}
			set { _Staff = value; } 
		}

		public int StaffID { get; set; }


	}
}

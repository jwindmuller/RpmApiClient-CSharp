using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.Api.Response
{
	public class RepResponse : Abstract.Response
	{
		public bool Logon { get; set; }
		public string Rep { get; set; }
		public int RepID { get; set; }
		public string Type { get; set; }
		public string Username { get; set; }

		// Non-standar
		public string Agency { get; set; }
		public int AgencyID { get; set; }

		public override bool Equals(object obj)
		{
			RepResponse other = (RepResponse)obj;
			return
				this.Logon == other.Logon &&
				this.Rep == other.Rep &&
				this.RepID == other.RepID &&
				this.Type == other.Type &&
				this.Username == other.Username &&
				this.Agency == other.Agency &&
				this.AgencyID == other.AgencyID;
		}
	}
}

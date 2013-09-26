using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.Api.Response
{
	public class RepResponse : Abstract.Response
	{
		public string Agency;
		public int AgencyID;
		//List<AssignmentCode> AssignmentCodes;
		public string Rep;
		public int RepID;

		public override bool Equals(object obj)
		{
			RepResponse other = (RepResponse)obj;
			return
				this.Agency == other.Agency &&
				this.AgencyID == other.AgencyID &&
				this.Rep == other.Rep &&
				this.RepID == other.RepID;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommAgencyResponse : Abstract.Response
	{
		private string _Agency { get; set; }
		public string Agency
		{
			get
			{
				if (_Agency == null)
				{
					_Agency = "";
				}
				return _Agency;
			}
			set { _Agency = value; }
		}

		public int AgencyID { get; set; }
		public int Value { get; set; }
		

		public override bool Equals(object obj)
		{
			CommAgencyResponse other = obj as CommAgencyResponse;
			return
				this.Agency.Equals(other.Agency) &&
				this.AgencyID == other.AgencyID &&
				this.Value == other.Value;
		}
	}
}

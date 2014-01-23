using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommAgencyPayment : Abstract.Response
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

		public double Payout { get; set; }

		public bool Paid { get; set; }
	}
}

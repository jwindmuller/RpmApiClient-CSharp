using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class PhoneNumberResponse : Abstract.Response
	{
		public string PhoneNumber { get; set; }
		public int PhoneNumberID { get; set; }
		public int Type { get; set; }

		public override bool Equals(object obj)
		{
			PhoneNumberResponse other = (PhoneNumberResponse)obj;
			return
				this.PhoneNumber == other.PhoneNumber &&
				this.PhoneNumberID == other.PhoneNumberID &&
				this.Type == other.Type;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class PhoneNumberResponse : Abstract.Response
	{
		public enum NumberType
		{
			Business=1, Home, Fax, Other=6
		};
		public string Number { get; set; }
		public int PhoneNumberID { get; set; }
		public NumberType Type { get; set; }

		public override bool Equals(object obj)
		{
			PhoneNumberResponse other = (PhoneNumberResponse)obj;
			return
				this.Number == other.Number &&
				this.PhoneNumberID == other.PhoneNumberID &&
				this.Type == other.Type;
		}
	}
}

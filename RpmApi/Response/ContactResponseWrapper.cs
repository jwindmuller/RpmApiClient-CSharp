using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ContactResponseWrapper : Abstract.Response
	{
		public ContactResponse Contact { get; set; }
		public Boolean IsPrimary { get; set; }

		public override bool Equals(object obj)
		{
			ContactResponseWrapper other = (ContactResponseWrapper)obj;
			return
				this.Contact.Equals(other.Contact) &&
				this.IsPrimary == other.IsPrimary;
		}
	}
}

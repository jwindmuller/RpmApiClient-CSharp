using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ContactResponse : Abstract.Response
	{
		public int ContactID { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		private List<PhoneNumberResponse> _PhoneNumbers { get; set; }
		public List<PhoneNumberResponse> PhoneNumbers {
			get {
				if (_PhoneNumbers == null)
				{
					_PhoneNumbers = new List<PhoneNumberResponse>();
				}
				return _PhoneNumbers;
			}
			set { _PhoneNumbers = value; } 
		}
		public string Salutation { get; set; }
		public string Title { get; set; }

		public override bool Equals(object obj)
		{
			ContactResponse other = (ContactResponse)obj;
			return
				this.ContactID == other.ContactID &&
				this.Email == other.Email &&
				this.FirstName == other.FirstName &&
				this.CollectionsAreEqual(this.PhoneNumbers, other.PhoneNumbers) &&
				this.Salutation == other.Salutation &&
				this.Title == other.Title;
		}
	}
}

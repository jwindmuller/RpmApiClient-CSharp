using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ContactResponse : Abstract.Response
	{
		public int ContactID { get; set; }
		private string _Email { get; set; }
		public string Email 
		{ 
			get {
				if (_Email == null)
				{
					_Email = "";
				}
				return _Email;
			}
			set { _Email = value; } 
		}
		private string _FirstName { get; set; }
		public string FirstName
		{
			get
			{
				if (_FirstName == null)
				{
					_FirstName = "";
				}
				return _FirstName;
			}
			set { _FirstName = value; }
		}
		private string _LastName { get; set; }
		public string LastName
		{
			get
			{
				if (_LastName == null)
				{
					_LastName = "";
				}
				return _LastName;
			}
			set { _LastName = value; }
		}
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
		private string _Salutation { get; set; }
		public string Salutation
		{
			get
			{
				if (_Salutation == null)
				{
					_Salutation = "";
				}
				return _Salutation;
			}
			set { _Salutation = value; }
		}
		private string _Title { get; set; }
		public string Title
		{
			get
			{
				if (_Title == null)
				{
					_Title = "";
				}
				return _Title;
			}
			set { _Title = value; }
		}

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class UserResponse : Abstract.Response
	{
		public DateTime Added { get; set; }

		private ContactResponse _Contact { get; set; }
		public ContactResponse Contact
		{
			get
			{
				if (_Contact == null)
				{
					_Contact = new ContactResponse();
				}
				return _Contact;
			}
			set { _Contact = value; }
		}

		public List<FieldResponse> _Fields;
		public List<FieldResponse> Fields
		{
			get
			{
				if (_Fields == null)
				{
					_Fields = new List<FieldResponse>();
				}
				return _Fields;
			}
			set
			{
				_Fields = value;
			}
		}

		private LastLogonInfo _LastLogon { get; set; }
		public LastLogonInfo LastLogon
		{
			get
			{
				if (_LastLogon == null)
				{
					_LastLogon = new LastLogonInfo();
				}
				return _LastLogon;
			}
			set { _LastLogon = value; }
		}

		public bool Locked { get; set; }
		public bool Logon { get; set; }
		public DateTime Modified { get; set; }
		public bool PasswordCheck { get; set; }

		private string _Role { get; set; }
		public string Role
		{
			get
			{
				if (_Role == null)
				{
					_Role = "";
				}
				return _Role;
			}
			set { _Role = value; }
		}

		public int RoleID { get; set; }
		public int StaffID { get; set; }

		private string _Username { get; set; }
		public string Username
		{
			get
			{
				if (_Username == null)
				{
					_Username = "";
				}
				return _Username;
			}
			set { _Username = value; }
		}

		public override bool Equals(object obj)
		{
			UserResponse other = obj as UserResponse;
			return
				this.Added == other.Added &&
				this.Contact.Equals(other.Contact) &&
				this.CollectionsAreEqual(this.Fields, other.Fields) &&
				this.LastLogon.Equals(other.LastLogon) &&
				this.Locked == other.Locked &&
				this.Logon == other.Logon &&
				this.Modified == other.Modified &&
				this.PasswordCheck == other.PasswordCheck &&
				this.Role.Equals(other.Role) &&
				this.RoleID == other.RoleID &&
				this.StaffID == other.StaffID &&
				this.Username.Equals(other.Username);
		}
	}
}

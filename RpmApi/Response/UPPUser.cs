using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class UPPUser : Abstract.Response
	{
		public int UserID { get; set; }

		private string _Fullname { get; set; }
		public string Fullname
		{
			get
			{
				if (_Fullname == null)
				{
					_Fullname = "";
				}
				return _Fullname;
			}
			set { _Fullname = value; }
		}

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

		private string _Email { get; set; }
		public string Email
		{
			get
			{
				if (_Email == null)
				{
					_Email = "";
				}
				return _Email;
			}
			set { _Email = value; }
		}

		public bool Logon { get; set; }

		public override bool Equals(object obj)
		{
			UPPUser other = obj as UPPUser;
			return
				this.UserID == other.UserID &&
				this.Fullname.Equals(other.Fullname) &&
				this.Role.Equals(other.Role) &&
				this.Email.Equals(other.Email) &&
				this.Logon == other.Logon;
		}

	}
}

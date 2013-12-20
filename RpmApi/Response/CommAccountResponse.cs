using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommAccountResponse : Abstract.Response
	{
		private string _Account { get; set; }
		public string Account
		{
			get
			{
				if (_Account == null)
				{
					_Account = "";
				}
				return _Account;
			}
			set { _Account = value; }
		}

		public int AccountID { get; set; }
		public int Value { get; set; }

		public override bool Equals(object obj)
		{
			CommAccountResponse other = obj as CommAccountResponse;
			return
				this.Account.Equals(other.Account) &&
				this.AccountID == other.AccountID &&
				this.Value == other.Value;
		}
	}
}

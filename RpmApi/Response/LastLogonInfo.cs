using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class LastLogonInfo : Abstract.Response
	{
		private string _Browser { get; set; }
		public string Browser
		{
			get
			{
				if (_Browser == null)
				{
					_Browser = "";
				}
				return _Browser;
			}
			set { _Browser = value; }
		}

		private string _IP { get; set; }
		public string IP
		{
			get
			{
				if (_IP == null)
				{
					_IP = "";
				}
				return _IP;
			}
			set { _IP = value; }
		}

		public DateTime When { get; set; }

		public override bool Equals(object obj)
		{
			LastLogonInfo other = obj as LastLogonInfo;
			return
				this.Browser.Equals(other.Browser) &&
				this.IP.Equals(other.IP) &&
				this.When == other.When;
		}
	}
}

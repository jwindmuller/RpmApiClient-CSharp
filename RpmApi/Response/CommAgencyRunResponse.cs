using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommAgencyRunResponse : Abstract.Response
	{
		private string _Run { get; set; }
		public string Run
		{
			get
			{
				if (_Run == null)
				{
					_Run = "";
				}
				return _Run;
			}
			set { _Run = value; }
		}

		public int Value { get; set; }

		public override bool Equals(object obj)
		{
			CommAgencyRunResponse other = obj as CommAgencyRunResponse;
			return
				this.Run.Equals(other.Run) &&
				this.Value == other.Value;
		}
	}
}

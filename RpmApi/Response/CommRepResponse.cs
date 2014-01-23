using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommRepResponse : Abstract.Response
	{
		private string _Rep { get; set; }
		public string Rep
		{
			get
			{
				if (_Rep == null)
				{
					_Rep = "";
				}
				return _Rep;
			}
			set { _Rep = value; }
		}

		public int RepID { get; set; }

		public double Value {get;set;}

		public override bool Equals(object obj)
		{
			CommRepResponse other = obj as CommRepResponse;
			return
				this.Rep.Equals(other.Rep) &&
				this.RepID == other.RepID &&
				this.Value == other.Value;
		}
	}
}

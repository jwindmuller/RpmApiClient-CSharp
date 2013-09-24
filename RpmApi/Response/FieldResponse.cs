using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.Api.Response
{
	public class FieldResponse : Abstract.Response
	{
		public string Field { get; set; }
		public string Value { get; set; }

		public override bool Equals(object obj)
		{
			FieldResponse other = (FieldResponse)obj;
			return
				this.Field == other.Field &&
				this.Value == other.Value;
		}
	}
}

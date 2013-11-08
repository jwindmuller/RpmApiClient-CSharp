using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public object Clone()
		{
			FieldResponse clone = new FieldResponse();
			clone.Field = (string)this.Field.Clone();
			clone.Value = (string)this.Value.Clone();
			return clone;
		}

	}
}

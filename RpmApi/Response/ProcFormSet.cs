using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ProcFormSet: Abstract.Response
	{
		public int FormID { get; set; }

		private List<FieldResponse> _Fields { get; set; }
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
			set { _Fields = value; }
		}

		public override bool Equals(object obj)
		{
			ProcFormSet other = (ProcFormSet)obj;
			return
				this.FormID == other.FormID &&
				this.CollectionsAreEqual(this.Fields, other.Fields);
		}

		public object Clone()
		{
			ProcFormSet clone = new ProcFormSet();
			clone.FormID = this.FormID;
			clone.Fields = this.CloneCollection<FieldResponse>(this.Fields);
			return clone;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommRepReport : Abstract.Response
	{
		private string _Val { get; set; }
		public string Val
		{
			get
			{
				if (_Val == null)
				{
					_Val = "";
				}
				return _Val;
			}
			set { _Val = value; }
		}

		private List<RunValuePairResponse> _Values { get; set; }
		public List<RunValuePairResponse> Values
		{
			get
			{
				if (_Values == null)
				{
					_Values = new List<RunValuePairResponse>();
				}
				return _Values;
			}
			set { _Values = value; }
		}


		public override bool Equals(object obj)
		{
			CommRepReport other = obj as CommRepReport;
			return
				this.Val.Equals(other.Val) &&
				this.CollectionsAreEqual(this.Values, other.Values);
		}
	}
}

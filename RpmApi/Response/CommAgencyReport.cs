using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommAgencyReport : Abstract.Response
	{
		private string _Val { get; set; }
		public string Val {
			get {
				if (_Val == null)
				{
					_Val = "";
				}
				return _Val;
			}
			set { _Val = value; }
		}

		private List<CommAgencyRunResponse> _Values { get; set; }
		public List<CommAgencyRunResponse> Values
		{
			get
			{
				if (_Values == null)
				{
					_Values = new List<CommAgencyRunResponse>();
				}
				return _Values;
			}
			set { _Values = value; }
		}


		public override bool Equals(object obj)
		{
			CommAgencyReport other = obj as CommAgencyReport;
			return
				this.Val.Equals(other.Val) &&
				this.CollectionsAreEqual(this.Values, other.Values);
		}


	}
}

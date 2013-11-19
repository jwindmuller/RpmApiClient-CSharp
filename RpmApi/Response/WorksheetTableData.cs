using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class WorksheetTableData : Abstract.Response
	{
		public int ColID { get; set; }

		private string _RefName { get; set; }
		public string RefName
		{
			get
			{
				if (_RefName == null)
				{
					_RefName = "";
				}
				return _RefName;
			}
			set { _RefName = value; }
		}

		public int Row { get; set; }

		private string _Value { get; set; }
		public string Value
		{
			get
			{
				if (_Value == null)
				{
					_Value = "";
				}
				return _Value;
			}
			set { _Value = value; }
		}
	}
}

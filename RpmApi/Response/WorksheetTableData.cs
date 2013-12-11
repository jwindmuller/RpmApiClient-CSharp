using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class WorksheetTableData : Abstract.Response
	{
		private int _ID { get; set; }
		public int ColID
		{
			get;
			set;
		}
		public int ID {
			get { return _ID; }
			set { _ID = value; }
		}

		private int _ColIndex = -1;
		public int ColIndex {
			get { return _ColIndex; }
			set { _ColIndex = value; }
		}

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

		public override bool Equals(object obj)
		{
			WorksheetTableData other = obj as WorksheetTableData;
			return
				this.ColID == other.ColID &&
				this.ColIndex == other.ColIndex &&
				this.RefName.Equals(other.RefName) &&
				this.Row == other.Row &&
				this.Value.Equals(other.Value);
		}
	}
}

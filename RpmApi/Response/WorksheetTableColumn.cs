using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class WorksheetTableColumn : Abstract.Response
	{

		public int ColType { get; set; }

		private string _FixedValue { get; set; }
		public string FixedValue
		{
			get
			{
				if (_FixedValue == null)
				{
					_FixedValue = "";
				}
				return _FixedValue;
			}
			set { _FixedValue = value; }
		}

		public int ID { get; set; }
		public bool IsFixed { get; set; }
		public bool IsHidden { get; set; }

		private string _Name { get; set; }
		public string Name
		{
			get
			{
				if (_Name == null)
				{
					_Name = "";
				}
				return _Name;
			}
			set { _Name = value; }
		}

		public int Order { get; set; }
		public int RefType { get; set; }

		private string _RefTypeName { get; set; }
		public string RefTypeName
		{
			get
			{
				if (_RefTypeName == null)
				{
					_RefTypeName = "";
				}
				return _RefTypeName;
			}
			set { _RefTypeName = value; }
		}

		public override bool Equals(object obj)
		{
			WorksheetTableColumn other = obj as WorksheetTableColumn;
			return
				this.ColType == other.ColType &&
				this.FixedValue.Equals(other.FixedValue) &&
				this.ID == other.ID &&
				this.IsFixed == other.IsFixed &&
				this.IsHidden == other.IsHidden &&
				this.Name.Equals(other.Name) &&
				this.Order == other.Order &&
				this.RefType == other.RefType &&
				this.RefTypeName.Equals(other.RefTypeName);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPM.Api;

namespace RPM.Api.Response
{
	public class WorksheetResponse : Abstract.Response
	{
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

		private int _ID { get; set; }
		public int ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}
		public int WorksheetID { 
			get { 
				return _ID;
			}
			set{
				_ID = value;
			}
		}

		public DateTime DateAdded { get; set; }
		public DateTime DateModified { get; set; }

		private List<WorksheetField> _Fields { get; set; }
		private List<WorksheetField> Fields
		{
			get
			{
				if (_Fields == null)
				{
					_Fields = new List<WorksheetField>();
				}
				return _Fields;
			}
			set { _Fields = value; }
		}

		public override bool Equals(object obj)
		{
			WorksheetResponse other = obj as WorksheetResponse;
			return
				this.Name.Equals(other.Name) &&
				this.WorksheetID == other.WorksheetID &&
				this.DateAdded == other.DateAdded &&
				this.DateModified == other.DateModified &&
				this.CollectionsAreEqual(this.Fields, other.Fields);
		}
	}
}

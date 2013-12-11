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
				// Prevent this setter from overwriting good data
				if (_ID != 0 && value == 0)
				{
					return;
				}
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

		private string _Header { get; set; }
		public string Header
		{
			get
			{
				if (_Header == null)
				{
					_Header = "";
				}
				return _Header;
			}
			set { _Header = value; }
		}
		private string _Footer { get; set; }
		public string Footer
		{
			get
			{
				if (_Footer == null)
				{
					_Footer = "";
				}
				return _Footer;
			}
			set { _Footer = value; }
		}

		public bool IsEnabled { get; set; }
		public int NumChildWorksheets { get; set; }
		public int NumFields { get; set; }
		public int NumTables { get; set; }
		public int OwnerID { get; set; }
		public int OwnerType { get; set; }

		private List<WorksheetTable> _Tables { get; set; }
		public List<WorksheetTable> Tables
		{
			get
			{
				if (_Tables == null)
				{
					_Tables = new List<WorksheetTable>();
				}
				return _Tables;
			}
			set { _Tables = value; }
		}

		public int WSTemplateID { get; set; }

		public override bool Equals(object obj)
		{
			WorksheetResponse other = obj as WorksheetResponse;
			return
				this.Name.Equals(other.Name) &&
				this.WorksheetID == other.WorksheetID &&
				this.DateAdded == other.DateAdded &&
				this.DateModified == other.DateModified &&
				this.CollectionsAreEqual(this.Fields, other.Fields) &&
				this.Header.Equals(other.Header) &&
				this.Footer.Equals(other.Footer) &&
				this.IsEnabled == other.IsEnabled &&
				this.NumChildWorksheets == other.NumChildWorksheets &&
				this.NumFields == other.NumFields &&
				this.NumTables == other.NumTables &&
				this.OwnerID == other.OwnerID &&
				this.OwnerType == other.OwnerType &&
				this.CollectionsAreEqual(this.Tables, other.Tables) &&
				this.WSTemplateID == other.WSTemplateID;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class WorksheetField : Abstract.Response
	{
		public int Column { get; set; }
		public int ID { get; set; }

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

		public int ObjectSpecificID { get; set; }
		public int ObjectType { get; set; }
		public int Order { get; set; }
		public int WorksheetID { get; set; }

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

		public bool IsEnabled { get; set; }
		public int NumChildWorksheets { get; set; }
		public int NumFields { get; set; }
		public int NumTables { get; set; }
		public int OwnerID { get; set; }
		public int OwnerType { get; set; }

		private List<WorksheetTable> _Tables { get; set; }
		private List<WorksheetTable> Tables
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

		private string _WSTemplateID { get; set; }
		public string WSTemplateID
		{
			get
			{
				if (_WSTemplateID == null)
				{
					_WSTemplateID = "";
				}
				return _WSTemplateID;
			}
			set { _WSTemplateID = value; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	class WorksheetTable : Abstract.Response
	{

		private List<WorksheetTableColumn> _Columns { get; set; }
		public List<WorksheetTableColumn> Columns
		{
			get
			{
				if (_Columns == null)
				{
					_Columns = new List<WorksheetTableColumn>();
				}
				return _Columns;
			}
			set { _Columns = value; }
		}

		private string _Comment { get; set; }
		public string Comment
		{
			get
			{
				if (_Comment == null)
				{
					_Comment = "";
				}
				return _Comment;
			}
			set { _Comment = value; }
		}

		private string _CommentName { get; set; }
		public string CommentName
		{
			get
			{
				if (_CommentName == null)
				{
					_CommentName = "";
				}
				return _CommentName;
			}
			set { _CommentName = value; }
		}

		//private List<??> _Data { get; set; }

		public int ID { get; set; }
		public bool IsEnabled { get; set; }
		public bool IsIncluded { get; set; }

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
		public bool PrefFixed { get; set; }
		public int RefID { get; set; }

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
		
		public int RefType { get; set; }
		public bool ShowComment { get; set; }
		public bool ShowFooter { get; set; }

		//private List<??> _SuperHeaders { get; set; }

		public int WSTemplateID { get; set; }

		public override bool Equals(object obj)
		{
			WorksheetTable other = obj as WorksheetTable;
			return
				this.CollectionsAreEqual(this.Columns, other.Columns) &&
				this.Comment.Equals(other.Comment) &&
				this.CommentName.Equals(other.CommentName) &&
				this.ID == other.ID &&
				this.IsEnabled == other.IsEnabled &&
				this.IsIncluded == other.IsIncluded &&
				this.Name.Equals(other.Name) &&
				this.Order == other.Order &&
				this.PrefFixed == other.PrefFixed &&
				this.RefID == other.RefID &&
				this.RefName.Equals(other.RefName) &&
				this.RefType == other.RefType &&
				this.ShowComment == other.ShowComment &&
				this.ShowFooter == other.ShowFooter &&
				this.WSTemplateID == other.WSTemplateID;
		}
	}
}

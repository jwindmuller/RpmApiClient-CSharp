using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.Api.Response
{
	public class RepResponse : Abstract.Response
	{
		public DateTime Added { get; set; }

		private List<string> _AssignmentCodeIDs { get; set; }
		public List<string> AssignmentCodeIDs { 
			get {
				return _AssignmentCodeIDs;
			} 
			set {
				_AssignmentCodeIDs = value;
				if (value == null)
				{
					return;
				}
				if (this.AssignmentCodes.Count < this._AssignmentCodeIDs.Count)
				{
					for (int i = 0; i < this.AssignmentCodeIDs.Count; i++)
					{
						AssignmentCodeResponse ac = new AssignmentCodeResponse();
						ac.AssignmentCode = Int32.Parse( _AssignmentCodeIDs[i] );
						this.AssignmentCodes.Add(ac);
					}
				}
			}
		}

		private List<AssignmentCodeResponse> _AssignmentCodes { get; set; }
		public List<AssignmentCodeResponse> AssignmentCodes
		{
			get
			{
				if (_AssignmentCodes == null)
				{
					_AssignmentCodes = new List<AssignmentCodeResponse>();
				}
				return _AssignmentCodes;
			}
			set { _AssignmentCodes = value; }
		}

		public bool CommissionsHidden { get; set; }

		private ContactResponse _Contact { get; set; }
		public ContactResponse Contact
		{
			get
			{
				if (_Contact == null)
				{
					_Contact = new ContactResponse();
				}
				return _Contact;
			}
			set { _Contact = value; }
		}

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


		public bool Logon { get; set; }
		public DateTime Modified { get; set; }

		private List<NoteResponse> _NotesForStaff { get; set; }
		public List<NoteResponse> NotesForStaff {
			get {
				if (_NotesForStaff == null)
				{
					_NotesForStaff = new List<NoteResponse>();
				}
				return _NotesForStaff;
			}
			set { _NotesForStaff = value; }
		}

		private string _Rep { get; set; }
		public string Rep
		{
			get
			{
				if (_Rep == null)
				{
					_Rep = "";
				}
				return _Rep;
			}
			set { _Rep = value; }
		}

		public int RepID { get; set; }
		private string _Type { get; set; }
		public string Type
		{
			get
			{
				if (_Type == null)
				{
					_Type = "";
				}
				return _Type;
			}
			set { _Type = value; }
		}

		private string _Username { get; set; }
		public string Username
		{
			get
			{
				if (_Username == null)
				{
					_Username = "";
				}
				return _Username;
			}
			set { _Username = value; }
		}

		// Non-standard
		private string _Agency { get; set; }
		public string Agency
		{
			get
			{
				if (_Agency == null)
				{
					_Agency = "";
				}
				return _Agency;
			}
			set { _Agency = value; }
		}
		public int AgencyID { get; set; }

		public override bool Equals(object obj)
		{
			RepResponse other = (RepResponse)obj;
			return
				this.CollectionsAreEqual(this.AssignmentCodes, other.AssignmentCodes) &&
				this.CommissionsHidden == other.CommissionsHidden &&
				this.Contact.Equals(other.Contact) &&
				this.CollectionsAreEqual(this.Fields, other.Fields) &&
				this.Logon == other.Logon &&
				this.Modified == other.Modified &&
				this.CollectionsAreEqual(this.NotesForStaff, other.NotesForStaff) &&
				this.Rep == other.Rep &&
				this.RepID == other.RepID &&
				this.Type == other.Type &&
				this.Username == other.Username &&
				this.Agency == other.Agency &&
				this.AgencyID == other.AgencyID;
		}
	}
}

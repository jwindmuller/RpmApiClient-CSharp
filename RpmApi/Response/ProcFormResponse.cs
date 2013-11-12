using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ProcFormResponse : ProcFormSet //Abstract.Response
    {

		public int ProcessID { get; set; }
		private string _ProcessName { get; set; }
		public string ProcessName
		{
			get
			{
				if (_ProcessName == null)
				{
					_ProcessName = "";
				}
				return _ProcessName;
			}
			set { _ProcessName = value; }
		}

		private string _Number { get; set; }
		public string Number
		{
			get
			{
				if (_Number == null)
				{
					_Number = "";
				}
				return _Number;
			}
			set { _Number = value; }
		}

		private string _Owner { get; set; }
		public string Owner
		{
			get
			{
				if (_Owner == null)
				{
					_Owner = "";
				}
				return _Owner;
			}
			set { _Owner = value; }
		}

		private List<ParticipantResponse> _Participants { get; set; }
		public List<ParticipantResponse> Participants
		{
			get
			{
				if (_Participants == null)
				{
					_Participants = new List<ParticipantResponse>();
				}
				return _Participants;
			}
			set { _Participants = value; }
		}

		private string _Status { get; set; }
		public string Status
		{
			get
			{
				if (_Status == null)
				{
					_Status = "";
				}
				return _Status;
			}
			set { _Status = value; }
		}

		private string _ApprovalResult { get; set; }
		public string ApprovalResult
		{
			get
			{
				if (_ApprovalResult == null)
				{
					_ApprovalResult = "";
				}
				return _ApprovalResult;
			}
			set { _ApprovalResult = value; }
		}

		private DateTime _Started { get; set; }
		public DateTime Started
		{
			get
			{
				if (_Started == null)
				{
					_Started = new DateTime();
				}
				return _Started;
			}
			set { _Started = value; }
		}

		private DateTime _Modified { get; set; }
        public DateTime Modified
		{
			get
			{
				if (_Modified == null)
				{
					_Modified = new DateTime();
				}
				return _Modified;
			}
			set { _Modified = value; }
		}

		private List<string> _Values { get; set; }
		public List<string> Values
		{
			get
			{
				if (_Values == null)
				{
					_Values = new List<string>();
				}
				return _Values;
			}
			set { _Values = value; }
		}

		private List<ProcFormSet> _Sets { get; set; }
		public List<ProcFormSet> Sets
		{
			get
			{
				if (_Sets == null)
				{
					_Sets = new List<ProcFormSet>();
				}
				return _Sets;
			}
			set { _Sets = value; }
		}

		private List<NoteResponse> _Notes { get; set; }
		public List<NoteResponse> Notes
		{
			get
			{
				if (_Notes == null)
				{
					_Notes = new List<NoteResponse>();
				}
				return _Notes;
			}
			set { _Notes = value; }
		}

		private List<NoteResponse> _NotesForStaff { get; set; }
		public List<NoteResponse> NotesForStaff
		{
			get
			{
				if (_NotesForStaff == null)
				{
					_NotesForStaff = new List<NoteResponse>();
				}
				return _NotesForStaff;
			}
			set { _NotesForStaff = value; }
		}

		private List<WorksheetResponse> _Worksheets { get; set; }
		public List<WorksheetResponse> Worksheets
		{
			get
			{
				if (_Worksheets == null)
				{
					_Worksheets = new List<WorksheetResponse>();
				}
				return _Worksheets;
			}
			set { _Worksheets = value; }
		}

        // TODO: rest of properties for form, see: http://rpmsoftware.wordpress.com/json-structure-for-forms/
		public override bool Equals(object obj)
		{
			ProcFormResponse other = (ProcFormResponse)obj;
			return
				base.Equals(obj) &&
				this.Number == other.Number &&
				this.Owner == other.Owner &&
				this.Status == other.Status &&
				this.ApprovalResult == other.ApprovalResult &&
				this.Started.Equals(other.Started) &&
				this.Modified.Equals(other.Modified) &&
				this.CollectionsAreEqual(this.Values, other.Values) &&
				this.CollectionsAreEqual(this.Sets, other.Sets) &&
				this.CollectionsAreEqual(this.Worksheets, other.Worksheets);
		}

        private Dictionary<string, string> fieldsByName;
        public string valueForField(string fieldName)
        {
            if (this.fieldsByName == null)
            {
                this.fieldsByName = new Dictionary<string, string>();
				foreach (FieldResponse field in this.Fields)
                {
                    this.fieldsByName.Add(field.Field, field.Value);
                }
            }
            if (this.fieldsByName.ContainsKey(fieldName))
            {
                return this.fieldsByName[fieldName];
            }
            throw new KeyNotFoundException(fieldName + " Not Found");
        }

		public object Clone()
		{
			ProcFormResponse clone = new ProcFormResponse();
			clone.FormID = this.FormID;
			clone.Fields = this.CloneCollection<FieldResponse>(this.Fields);
			clone.ProcessID = this.ProcessID;
			clone.ProcessName = (string)this.ProcessName.Clone();
			clone.Number = (string)this.Number.Clone();
			clone.Owner = (string)this.Owner.Clone();
			clone.Participants = this.CloneCollection<ParticipantResponse>(this.Participants);
			clone.Status = (string)this.Status.Clone();
			clone.ApprovalResult = (string)this.ApprovalResult.Clone();
			clone.Started = this.Started;
			clone.Modified = this.Modified;
			clone.Values = this.CloneCollection<string>(this.Values);
			clone.Sets = this.CloneCollection<ProcFormSet>(this.Sets);
			clone.Notes = this.CloneCollection<NoteResponse>(this.Notes);
			clone.NotesForStaff = this.CloneCollection<NoteResponse>(this.NotesForStaff);
			return clone;
		}
    }
}

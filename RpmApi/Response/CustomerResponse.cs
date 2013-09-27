using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CustomerResponse : Abstract.Response
	{
		public List<AccountResponse> _Accounts { get; set; }
		public List<AccountResponse> Accounts {
			get{
				if (_Accounts == null)
				{
					_Accounts = new List<AccountResponse>();
				}
				return _Accounts;
			} 
			set{ _Accounts = value; }
		}

		public DateTime Added { get; set; }

		//public List<string> Aliases { get; set; }

		public List<ContactResponse> _Contacts { get; set; }
		public List<ContactResponse> Contacts {
			get {
				if (_Contacts == null)
				{
					_Contacts = new List<ContactResponse>();
				}
				return _Contacts;
			}
			set { _Contacts = value; }
		}

		public int CustomerID { get; set; }

		public List<FieldResponse> _Fields { get; set; }
		public List<FieldResponse> Fields { 
			get {
				if (_Fields == null)
				{
					_Fields = new List<FieldResponse>();
				}
				return _Fields;
			}
			set { _Fields = value; } 
		}

		//public List<LocationResponse> _Locations { get; set; }

		public DateTime Modified { get; set; }
		public string Name { get; set; }

		public List<NoteResponse> _Notes { get; set; }
		public List<NoteResponse> Notes { 
			get {
				if (_Notes == null)
				{
					_Notes = new List<NoteResponse>();
				}
				return _Notes;
			}
			set { _Notes = value; } 
		}

		public List<NoteResponse> _NotesForStaff { get; set; }
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

		public string Website { get; set; }

		public override bool Equals(object obj)
		{
			CustomerResponse other = (CustomerResponse)obj;
			return
				this.CollectionsAreEqual(this.Accounts, other.Accounts) &&
				this.Added.Equals(other.Added) &&
				this.CollectionsAreEqual(this.Contacts, other.Contacts) &&
				this.CollectionsAreEqual(this.Fields, other.Fields) &&
				this.Modified.Equals(other.Modified) &&
				this.Name == other.Name &&
				this.CollectionsAreEqual(this.Fields, other.Fields) &&
				this.CollectionsAreEqual(this.Notes, other.Notes) &&
				this.CollectionsAreEqual(this.NotesForStaff, other.NotesForStaff) &&
				this.Website == other.Website;
		}
	}
}

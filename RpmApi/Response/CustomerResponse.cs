using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CustomerResponse : Abstract.Response
	{
		private List<AccountResponse> _Accounts { get; set; }
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

		private List<ContactResponseWrapper> _Contacts { get; set; }
		public List<ContactResponseWrapper> Contacts
		{
			get {
				if (_Contacts == null)
				{
					_Contacts = new List<ContactResponseWrapper>();
				}
				return _Contacts;
			}
			set { _Contacts = value; }
		}

		public int CustomerID { get; set; }

		private List<FieldResponse> _Fields { get; set; }
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

		private List<LocationResponse> _Locations { get; set; }
		public List<LocationResponse> Locations
		{
			get
			{
				if (_Locations == null)
				{
					_Locations = new List<LocationResponse>();
				}
				return _Locations;
			}
			set { _Locations = value; }
		}

		public DateTime Modified { get; set; }
		private string _Name;
		public string Name
		{
			get
			{
				if (_Name == null)
				{
					return "";
				}
				return _Name;
			}
			set
			{
				_Name = value;
			}
		}

		/// <summary>
		/// Alias for Name
		/// </summary>
		/// <value>
		/// The customer's Name.
		/// </value>
		public string Customer
		{
			get {
				return this.Name;
			}
			set { _Name = value; }
		}

		private List<NoteResponse> _Notes { get; set; }
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

		private string _Website;
		public string Website
		{
			get
			{
				if (_Website == null)
				{
					return "";
				}
				return _Website;
			}
			set
			{
				_Website = value;
			}
		}

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

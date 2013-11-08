using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class AgencyResponse
	{
		public DateTime Added { get; set; }

		public string Address { get; set; }
		public string Agency { get; set; }
		
		public int AgencyID { get; set; }
		public string City { get; set; }
		public ContactResponse Contact { get; set; }
		public string Country { get; set; }
		
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

		public DateTime Modified { get; set; }

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

		private List<RepResponse> _Reps { get; set; }
		public List<RepResponse> Reps { 
			get {
				if (_Reps == null)
				{
					_Reps = new List<RepResponse>();
				}
				return _Reps;
			}
			set { _Reps = value; }
		}

		private List<StaffAssignmentResponse> _StaffAssignments { get; set; }
		public List<StaffAssignmentResponse> StaffAssignments
		{
			get
			{
				if (_StaffAssignments == null)
				{
					_StaffAssignments = new List<StaffAssignmentResponse>();
				}
				return _StaffAssignments;
			}
			set { _StaffAssignments = value; }
		}

		public string StateProvince { get; set; }

		private List<SupplierExclusionResponse> _SupplierExclusions { get; set; }
		public List<SupplierExclusionResponse> SupplierExclusions
		{
			get
			{
				if (_SupplierExclusions == null)
				{
					_SupplierExclusions = new List<SupplierExclusionResponse>();
				}
				return _SupplierExclusions;
			}
			set { _SupplierExclusions = value; }
		}

		public string website { get; set; }
		public string ZipPostalCode { get; set; }
	}
}
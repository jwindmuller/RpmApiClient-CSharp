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
		public List<FieldResponse> Fields { get; set; }
		public DateTime Modified { get; set; }
		public List<NoteResponse> NotesForStaff { get; set; }
		public List<RepResponse> Reps { get; set; }
		public List<StaffAssignmentResponse> StaffAssignments { get; set; }
		public string StateProvince { get; set; }
		public List<SupplierExclusionResponse> SupplierExclusions { get; set; }
		public string website { get; set; }
		public string ZipPostalCode { get; set; }
	}
}
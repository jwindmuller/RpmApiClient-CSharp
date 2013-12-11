using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class AssignmentCodeResponse : Abstract.Response
	{
		public int AssignmentCode { get; set; }
		public string _Supplier { get; set; }
		public string Supplier { 
			get {
				if (_Supplier == null)
				{
					_Supplier = "";
				}
				return _Supplier;
			}
			set { _Supplier = value; }
		}
		public int SupplierID { get; set; }

		public override bool Equals(object obj)
		{
			AssignmentCodeResponse other = obj as AssignmentCodeResponse;
			return
				this.AssignmentCode == other.AssignmentCode &&
				this.Supplier.Equals(other.Supplier) &&
				this.SupplierID == other.SupplierID;
		}
	}
}

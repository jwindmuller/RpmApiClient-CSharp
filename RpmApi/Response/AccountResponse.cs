using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPM.Api.Response
{
	public class AccountResponse : Abstract.Response
	{
		public string Account { get; set; }
		public int AccountID { get; set; }
		public string AccountGroup { get; set; }
		public int AccountGroupID { get; set; }
		public DateTime Added { get; set; }
		public DateTime Modified {get; set; }
		public string Customer { get; set; }
		public int CustomerID { get; set; }
		public List<FieldResponse> _Fields;
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
			set
			{
				_Fields = value;
			}
		}
		private List<RepResponse> _Reps;
		public List<RepResponse> Reps
		{
			get
			{
				if (_Reps == null)
				{
					_Reps = new List<RepResponse>();
				}
				return _Reps;
			}
			set
			{
				_Reps = value;
			}
		}
		public string Supplier { get; set; }
		public int SupplierID { get; set; }

		public bool Equals(AccountResponse r)
		{
			bool areEquals =
				this.Account == r.Account &&
				this.AccountID == r.AccountID &&
				this.AccountGroup == r.AccountGroup &&
				this.AccountGroupID == r.AccountGroupID &&
				this.Added.Equals(r.Added) &&
				this.Customer == r.Customer &&
				this.CustomerID == r.CustomerID &&
				this.Supplier == r.Supplier &&
				this.SupplierID == r.SupplierID &&
				this.CollectionsAreEqual(this.Fields, r.Fields) &&
				this.CollectionsAreEqual(this.Reps, r.Reps);

			return areEquals;
		}


	}
}

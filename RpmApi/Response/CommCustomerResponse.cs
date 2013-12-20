using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommCustomerResponse: Abstract.Response
	{
		private string _Customer { get; set; }
		public string Customer
		{
			get
			{
				if (_Customer == null)
				{
					_Customer = "";
				}
				return _Customer;
			}
			set { _Customer = value; }
		}

		public int CustomerID { get; set; }
		public int Value { get; set; }

		public override bool Equals(object obj)
		{
			CommCustomerResponse other = obj as CommCustomerResponse;
			return
				this.Customer.Equals(other.Customer) &&
				this.CustomerID == other.CustomerID &&
				this.Value == other.Value;
		}
	}
}

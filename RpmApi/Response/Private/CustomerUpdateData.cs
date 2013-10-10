using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RPM.Api.Response;

namespace RPM.Api
{
	class CustomerUpdateData
	{

		public CustomerUpdateData(CustomerResponse data)
		{
			this.ID = data.CustomerID;
			this.Name = data.Name;
			this.Website = data.Website;

			foreach (LocationResponse l in data.Locations)
			{
				if (l.IsPrimary)
				{
					this.Address = l.Address;
					this.City = l.City;
					this.StateProvince = l.StateProvince;
					this.Country = l.Country;
					this.ZipPostalCode = l.ZipPostalCode;
					break;
				}
			}

			this.PrimaryContact = data.getPrimaryContact();
	
			this.Fields = data.Fields;
		}
	
		public int ID { get; set; }

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

		private string _Address { get; set; }
		public string Address
		{
			get
			{
				if (_Address == null)
				{
					_Address = "";
				}
				return _Address;
			}
			set { _Address = value; }
		}

		private string _City { get; set; }
		public string City
		{
			get
			{
				if (_City == null)
				{
					_City = "";
				}
				return _City;
			}
			set { _City = value; }
		}

		private string _StateProvince { get; set; }
		public string StateProvince
		{
			get
			{
				if (_StateProvince == null)
				{
					_StateProvince = "";
				}
				return _StateProvince;
			}
			set { _StateProvince = value; }
		}

		private string _Country { get; set; }
		public string Country
		{
			get
			{
				if (_Country == null)
				{
					_Country = "";
				}
				return _Country;
			}
			set { _Country = value; }
		}

		private string _ZipPostalCode { get; set; }
		public string ZipPostalCode
		{
			get
			{
				if (_ZipPostalCode == null)
				{
					_ZipPostalCode = "";
				}
				return _ZipPostalCode;
			}
			set { _ZipPostalCode = value; }
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

		private ContactResponse _PrimaryContact { get; set; }
		private ContactResponse PrimaryContact
		{
			get
			{
				if (_PrimaryContact == null)
				{
					_PrimaryContact = new ContactResponse();
				}
				return _PrimaryContact;
			}
			set { _PrimaryContact = value; }
		}
	}
}
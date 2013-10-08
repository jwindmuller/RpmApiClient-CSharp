using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class LocationResponse : Abstract.Response
	{
		public int CustomerLocationID { get; set; }

		public bool IsPrimary { get; set; }
		
		public int LocationID { get; set; }

		private string _Name { get; set; }
		public string Name { 
			get {
				if (_Name == null)
				{
					_Name = "";
				}
				return _Name;
			}
			set { _Name = value; }
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

		public override bool Equals(object obj)
		{
			LocationResponse other = (LocationResponse)obj;
			return
				this.IsPrimary == other.IsPrimary &&
				this.LocationID == other.LocationID &&
				this.Name == other.Name &&
				this.Address == other.Address &&
				this.City == other.City &&
				this.StateProvince == other.StateProvince &&
				this.Country == other.Country &&
				this.ZipPostalCode == other.ZipPostalCode;
		}
	}
}

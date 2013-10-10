using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RPM.Api;
using RPM.Api.Response;

namespace RpmApiTests
{
	[TestClass]
	public class TestRpmApi : TestBase
	{
		[TestMethod]
		public void TestIncorrectApiURL()
		{
			try
			{
				Client client = new Client("badurl", ApiSettings.key);
				Assert.Fail("Expected UriFormatException");
			}
			catch (Exception e)
			{
				Assert.AreEqual(e.GetType().Name, "UriFormatException");
			}
		}

		[TestMethod]
		public void TestIncorrectApiKey()
		{
			Client client = new Client(ApiSettings.url, "badkey");
			try
			{
				InfoResponse info = client.Info();
				Assert.Fail("Expected RPMApiError Exception");
			}
			catch (RPMApiError e)
			{
				Assert.AreEqual(e.Message, "Valid key required");
			}
		}

		[TestMethod]
		public void TestInfo()
		{
			Client client = getApiClient();
			InfoResponse info = client.Info();
			Assert.IsNotNull(info.Role);
			Assert.IsNotNull(info.Subscriber);
			Assert.IsNotNull(info.User);
			Assert.IsNotNull(info.RPM);
		}

		[TestMethod]
		public void TestAccount()
		{
			AccountResponse firstAccount = getFirstAccount();
			Client client = getApiClient();
			AccountResponse account = client.Account(firstAccount.Supplier, firstAccount.Account);

			// getFirstAccount uses the Accounts API call which does not return Reps
			firstAccount.Reps = account.Reps;
			Assert.IsTrue(firstAccount.Equals(account));
		}

		private AccountResponse getFirstAccount(SupplierResponse supplier = null)
		{
			if (supplier == null)
			{
				supplier = this.getFirstSupplier();	
			}

			Client client = getApiClient();
			List<AccountResponse> accounts = client.AccountsForSupplier(supplier.Supplier);

			if (accounts.Count == 0)
			{
				return null;
			}
			return accounts[0];
		}

		[TestMethod]
		public void TestAccounts()
		{
			SupplierResponse supplier = this.getFirstSupplier();
			AccountResponse account = getFirstAccount(supplier);
			if (account == null)
			{
				Assert.Inconclusive("No Accounts found for Supplier " + supplier.Supplier);
				return;
			}
			Assert.AreEqual(account.Supplier, supplier.Supplier);
			Assert.AreEqual(account.SupplierID, supplier.SupplierID);
		}

		[TestMethod]
		public void TestCustomer()
		{
			SupplierResponse supplier = this.getFirstSupplier();
			AccountResponse account = getFirstAccount(supplier);

			CustomerResponse customerByID = getApiClient().Customer(account.CustomerID);
			CustomerResponse customerByName = getApiClient().Customer(account.Customer);

			Assert.IsTrue(customerByID.Equals(customerByName));
		}

		[TestMethod]
		public void TestCustomerAdd()
		{
			Client client = getApiClient();
			CustomerResponse newCustomerData = new CustomerResponse();
			newCustomerData.Name = "Tester";
			CustomerResponse response = client.CustomerAdd(newCustomerData);

			newCustomerData.Added = response.Added;
			newCustomerData.Modified = response.Modified;
			newCustomerData.CustomerID = response.CustomerID;
			newCustomerData.Fields = response.Fields;

			Assert.IsTrue(newCustomerData.Equals(response));
		}

		[TestMethod]
		public void TestCustomerContactAdd()
		{
			int CustomerID = 77777777; // Fake ID for Mock testing
			Client client = getApiClient();
			ContactResponse contact = new ContactResponse();
			contact.FirstName = "Contact";
			contact.LastName = "Contactson";
			contact.Salutation = "Mr.";
			contact.Title = "Title";
			
			PhoneNumberResponse phone = new PhoneNumberResponse();
			phone.Number = "555-0035";
			phone.Type = PhoneNumberResponse.NumberType.Business;
			contact.PhoneNumbers.Add(phone);
			
			ContactResponse response = client.CustomerContactAdd(CustomerID, contact, true);

			foreach (PhoneNumberResponse responsePhone in response.PhoneNumbers)
			{
				if (responsePhone.Type == phone.Type)
				{
					phone.PhoneNumberID = responsePhone.PhoneNumberID;
					Assert.IsTrue(responsePhone.Equals(phone));
					break;
				}

			}

			// The response comes with the 4 phone number entries
			response.PhoneNumbers = contact.PhoneNumbers;

			contact.ContactID = response.ContactID;

			Assert.IsTrue(response.Equals(contact));
		}

		/// <summary>
		/// Tests the customer contact adding empty phone.
		/// The resulting phone number will be "none" instead of ""
		/// </summary>
		[TestMethod]
		public void TestCustomerContactAddEmptyPhone()
		{
			int CustomerID = 77777777; // Fake ID for Mock testing
			Client client = getApiClient();
			ContactResponse contact = new ContactResponse();
			contact.FirstName = "Contact";
			contact.LastName = "Contactson";
			contact.Salutation = "Mr.";
			contact.Title = "Title";

			PhoneNumberResponse phone = new PhoneNumberResponse();
			phone.Number = "";
			phone.Type = PhoneNumberResponse.NumberType.Business;
			contact.PhoneNumbers.Add(phone);

			ContactResponse response = client.CustomerContactAdd(CustomerID, contact, true);

			foreach (PhoneNumberResponse responsePhone in response.PhoneNumbers)
			{
				if (responsePhone.Type == phone.Type)
				{
					phone.PhoneNumberID = responsePhone.PhoneNumberID;
					Assert.AreEqual<string>(responsePhone.Number, "none");
					break;
				}
			}
		}

		[TestMethod]
		public void TestCustomerContactEdit()
		{
			int CustomerID = 77777777; // Fake ID for Mock testing
			Client client = getApiClient();

			CustomerResponse customer = client.Customer(CustomerID);
			ContactResponseWrapper original = customer.Contacts[0];

			ContactResponse contact = original.Contact;

			// Fax is set to 555-FAXS
			Assert.AreEqual(contact.getPhoneNumber(PhoneNumberResponse.NumberType.Fax).Number, "555-FAXS");
			Assert.AreNotEqual(contact.getPhoneNumber(PhoneNumberResponse.NumberType.Fax).PhoneNumberID, 0);

			// Home was not set
			Assert.AreEqual(contact.getPhoneNumber(PhoneNumberResponse.NumberType.Home).Number, "");
			Assert.AreEqual(contact.getPhoneNumber(PhoneNumberResponse.NumberType.Home).PhoneNumberID, 0);

			// Now let's update the contact phones
			contact.setPhoneNumber("1-800-1RPM", PhoneNumberResponse.NumberType.Business);
			contact.setPhoneNumber("", PhoneNumberResponse.NumberType.Fax);
			contact.setPhoneNumber("", PhoneNumberResponse.NumberType.Home);

			// And name
			contact.FirstName = "Name";
			contact.LastName = "Last";
			ContactResponse response = client.CustomerContactEdit(CustomerID, contact);

			Assert.AreEqual(response.getPhoneNumber(PhoneNumberResponse.NumberType.Business).Number, "1-800-1RPM");
			// Because The Fax Number was already set the value is now "none"
			Assert.AreEqual(response.getPhoneNumber(PhoneNumberResponse.NumberType.Fax).Number, "none");
			// Home didn't exist before so it's not set.
			Assert.AreEqual(response.getPhoneNumber(PhoneNumberResponse.NumberType.Home).Number, "");

			contact.PhoneNumbers = response.PhoneNumbers;

			Assert.AreEqual<ContactResponse>(response, contact);
		}

		[TestMethod]
		public void TestCustomerLocationAdd()
		{
			int CustomerID = 77777777; // Fake ID for Mock testing
			Client client = this.getApiClient();

			LocationResponse location = new LocationResponse();
			location.Name = "Home Office";
			location.Address = "205 - 5th Avenue SW";
			location.City = "Calgary";
			location.StateProvince = "Alberta";
			location.Country = "Canada";
			location.ZipPostalCode = "T2P 2V7";

			LocationResponse response = client.CustomerLocationAdd(CustomerID, location);

			Assert.IsTrue(response.CustomerLocationID > 0);

			LocationResponse emptyResponse = new LocationResponse();
			emptyResponse.CustomerLocationID = response.CustomerLocationID;
			Assert.AreEqual<LocationResponse>(emptyResponse, response);
		}

		[TestMethod]
		public void TestCustomerLocationEdit()
		{
			int CustomerID = 77777777; // Fake ID for Mock testing
			Client client = this.getApiClient();

			CustomerResponse customer = client.Customer(CustomerID);

			LocationResponse location = customer.Locations[0];
			location.Name = "Same Location, new name";

			LocationResponse response = client.CustomerLocationEdit(CustomerID, location);

			Assert.AreEqual(location.LocationID, response.CustomerLocationID);

			// The response only contains CustomerLocationID
			LocationResponse emptyResponse = new LocationResponse();
			emptyResponse.CustomerLocationID = response.CustomerLocationID;
			Assert.AreEqual<LocationResponse>(emptyResponse, response);
		}

		[TestMethod]
		public void TestCustomers()
		{
			Client client = this.getApiClient();

			List<CustomerResponse> customers = client.Customers();

			foreach (CustomerResponse customer in customers)
			{
				// Only these 2 fields are received
				Assert.IsNotNull(customer.Customer);
				Assert.AreEqual(customer.Name, customer.Customer);
				Assert.IsTrue(customer.CustomerID > 0);
			}
		}

		[TestMethod]
		public void TestCustomerUpdate()
		{
			Client client = this.getApiClient();
			CustomerResponse c = client.Customer(41801);
			c.Contacts[0].Contact.setPhoneNumber("abc", PhoneNumberResponse.NumberType.Business);
			c.Website = "joe";

			ContactResponse contact = c.getPrimaryContact();
			contact.FirstName = "Joe";
			contact.LastName = "Contact";
			contact.Email = "mail@joecontact";

			CustomerResponse response = client.CustomerUpdate(c);
			Assert.Inconclusive("Do not use CustomerUpdate");
		}

		[TestMethod]
		public void TestProcActionsDue()
		{
			Client client = this.getApiClient();

			List<ProcActionDueResponse> response = client.ProcActionsDue();

			foreach (ProcActionDueResponse procDue in response)
			{
				Assert.IsTrue(procDue.Due.Count >= 0);
				Assert.IsNotNull(procDue.Staff);
				Assert.IsTrue(procDue.StaffID > 0);
			}
		}

		[TestMethod]
		public void TestAgencies()
		{
			Client client = getApiClient();
			List<AgencyResponse> agencies = client.Agencies();
			AgencyResponse agency = agencies[0];
			Assert.IsNotNull(agency.Agency);
			Assert.IsTrue(agency.AgencyID > 0);
		}

		[TestMethod]
		public void TestAgency()
		{
			Client client = getApiClient();
			List<AgencyResponse> agencies = client.Agencies();
			AgencyResponse agency0 = agencies[0];

			AgencyResponse agency = client.Agency(agency0.AgencyID);
			
			// The Agencies call only returns these 2 fields
			Assert.AreEqual(agency0.Agency, agency.Agency);
			Assert.AreEqual(agency0.AgencyID, agency.AgencyID);
		}

		[TestMethod]
		public void TestSuppliers()
		{
			SupplierResponse supplier = getFirstSupplier();
			Assert.IsNotNull(supplier.Supplier);
			Assert.IsTrue(supplier.SupplierID > 0);
		}

		private SupplierResponse getFirstSupplier()
		{
			Client client = getApiClient();
			List<SupplierResponse> suppliers = client.Suppliers();

			return suppliers[0];
		}
	}
}

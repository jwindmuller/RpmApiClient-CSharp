using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RPM.Api;
using RPM.Api.Response;

using System.Collections.Generic;

namespace RpmApiTests
{
	[TestClass]
	public class TestRpmApi
	{
		[TestMethod]
		public void TestIncorrectApiURL()
		{
			try
			{
				Client client = new Client("badurl", "nokey");
			}
			catch (Exception e)
			{
				Assert.AreEqual(e.GetType().Name, "WebException");
			}
		}

		[TestMethod]
		public void TestIncorrectApiKey()
		{
			Client client = new Client(ApiSettings.url, "badkey");
			try
			{
				InfoResult info = client.info();
			}
			catch (RPMApiError e)
			{
				Assert.AreEqual(e.Message, "Valid key required");
			}

		}

		[TestMethod]
		public void TestInfo()
		{
			Client client = new Client(ApiSettings.url, ApiSettings.key);
			InfoResult info = client.info();
			Assert.IsNotNull(info.Role);
			Assert.IsNotNull(info.Subscriber);
			Assert.IsNotNull(info.User);
			Assert.IsNotNull(info.RPM);
		}

		[TestMethod]
		public void TestAccount()
		{
			AccountResponse firstAccount = getFirstAccount();
			Client client = new Client(ApiSettings.url, ApiSettings.key);
			AccountResponse account = client.Account(firstAccount.Supplier, Account: firstAccount.Account);

			Assert.IsTrue(firstAccount.Equals(account));
		}

		private AccountResponse getFirstAccount(SupplierResponse supplier = null)
		{
			if (supplier == null)
			{
				supplier = this.getFirstSupplier();	
			}

			Client client = new Client(ApiSettings.url, ApiSettings.key);
			List<AccountResponse> accounts = client.Accounts(supplier.Supplier);

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
		public void TestAgencies()
		{
			Client client = new Client(ApiSettings.url, ApiSettings.key);
			AgenciesResponse agencies = client.Agencies();
			AgencyResponse agency = agencies.Agencies[0];
			Assert.IsNotNull(agency.Agency);
			Assert.IsTrue(agency.AgencyID > 0);
		}

		[TestMethod]
		public void TestAgency()
		{
			Client client = new Client(ApiSettings.url, ApiSettings.key);
			AgenciesResponse agencies = client.Agencies();
			AgencyResponse agency0 = agencies.Agencies[0];

			AgencyResponse agency = client.Agency(null, agency0.AgencyID);

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
			Client client = new Client(ApiSettings.url, ApiSettings.key);
			List<SupplierResponse> suppliers = client.Suppliers();

			return suppliers[0];
		}
	}
}

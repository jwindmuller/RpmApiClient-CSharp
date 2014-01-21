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
			if (firstAccount == null)
			{
				Assert.Inconclusive("Could not find an Account");
				return;
			}
			Client client = getApiClient();
			AccountResponse account = client.Account(firstAccount.Supplier, firstAccount.Account);

			// getFirstAccount uses the Accounts API call which does not return Reps
			firstAccount.Reps = account.Reps;
			Assert.IsTrue(firstAccount.Equals(account));

			AccountResponse accountByID = client.Account(firstAccount.SupplierID, firstAccount.AccountID);
			firstAccount.Reps = accountByID.Reps;

			Assert.IsTrue(firstAccount.Equals(accountByID));
		}

		private AccountResponse getFirstAccount(SupplierResponse supplier = null)
		{
			Client client = getApiClient();
			List<AccountResponse> accounts = null;
			if (supplier == null)
			{
				List<SupplierResponse> suppliers = this.getSuppliers();
				foreach (SupplierResponse s in suppliers)
				{
					accounts = client.AccountsForSupplier(s.Supplier);
					if (accounts.Count > 0)
					{
						break;
					}
				}
			}
			else
			{
				accounts = client.AccountsForSupplier(supplier.Supplier);
			}
			if (accounts == null || accounts.Count == 0)
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
		public void TestCommAccounts()
		{
			Client client = this.getApiClient();
			List<CommAccountResponse> reports = client.CommAccounts(Client.Var.NetBilled, Client.Run._this); // Mocked
			List<CommAccountResponse> reportsYYYYMM = client.CommAccounts(Client.Var.NetBilled, "2013", "01"); // Mocked

			Assert.IsTrue(reports.Count == reportsYYYYMM.Count);
			for (int i = 0; i < reports.Count; i++)
			{
				CommAccountResponse r1 = reports[i];
				CommAccountResponse r2 = reportsYYYYMM[i];
				Assert.IsTrue(r1.Equals(r2));
			}
		}

		[TestMethod]
		public void TestCommAgencies()
		{
			Client client = this.getApiClient();
			List<CommAgencyResponse> reports = client.CommAgencies(Client.Var.NetBilled, Client.Run._this);
			List<CommAgencyResponse> reportsYYYYMM = client.CommAgencies(Client.Var.NetBilled, "2013", "01");

			Assert.IsTrue(reports.Count == reportsYYYYMM.Count);
			for (int i = 0; i < reports.Count; i++)
			{
				CommAgencyResponse r1 = reports[i];
				CommAgencyResponse r2 = reportsYYYYMM[i];
				Assert.IsTrue(r1.Equals(r2));
			}
		}

		[TestMethod]
		public void TestCommAgency()
		{
			Client client = this.getApiClient();

			int AgencyID = 1; // Mocked
			CommAgencyReport agencyReport = client.CommAgency(AgencyID, Client.Var.NetBilled);

			foreach (CommAgencyRunResponse runReport in agencyReport.Values)
			{
				List<CommAgencyResponse> allAgencies = client.CommAgencies(Client.Var.NetBilled, runReport.Run); // This works because Run will always be in YYYYMM format
				int total = 0;
				foreach (CommAgencyResponse agencyResult in allAgencies)
				{
					if (agencyResult.AgencyID == AgencyID)
					{
						total += agencyResult.Value;
					}
				}
				Assert.IsTrue(total == runReport.Value);
			}
		}

		[TestMethod]
		public void TestCommCustomers()
		{
			Client client = this.getApiClient();
			List<CommCustomerResponse> reports = client.CommCustomers(Client.Var.NetBilled, Client.Run._this); // Mocked
			List<CommCustomerResponse> reportsYYYYMM = client.CommCustomers(Client.Var.NetBilled, "2013", "01"); // Mocked

			Assert.IsTrue(reports.Count == reportsYYYYMM.Count);
			for (int i = 0; i < reports.Count; i++)
			{
				CommCustomerResponse r1 = reports[i];
				CommCustomerResponse r2 = reportsYYYYMM[i];
				Assert.IsTrue(r1.Equals(r2));
			}
		}

		[TestMethod]
		public void TestCommItem()
		{
			Client client = this.getApiClient();
			CommItemResponse itemInfo = client.CommItem(Client.CommItemType.Item, 60);
		}

		[TestMethod]
		public void TestCommPayments()
		{
			Client client = this.getApiClient();
			List<CommAgencyPayment> results = client.CommPayments("201012"); // Mocked
			Assert.IsTrue(results.Count >= 0);
			foreach (CommAgencyPayment payment in results)
			{
				Assert.IsTrue(payment.Agency != null);
				Assert.IsTrue(payment.AgencyID > 0);
				Assert.IsTrue(payment.Payout >= 0);
			}
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
			CustomerResponse c = client.Customer(77777777);
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
		public void TestProcForm()
		{
			Client client = this.getApiClient();
			List<ProcResponse> procs = client.Procs();

			ProcResponse procWithForms = this.getProcessWithForms();
			if (procWithForms == null)
			{
				Assert.Inconclusive("Could not find a Process with forms");
				return;
			}
			// ProcForms retrieves the data from a View, so it won't be 100% complete compared to ProcForm
			ProcFormsResponse allForms = client.ProcForms(procWithForms.ProcessID, 0);
			ProcFormResponse firstForm = allForms.Forms[0];

			ProcFormResponseWrapper byID = client.ProcForm(firstForm.FormID);
			ProcFormResponseWrapper byProcessID = client.ProcForm(procWithForms.ProcessID, firstForm.Number);

			Assert.AreEqual<ProcFormResponseWrapper>(byID, byProcessID);
			ProcFormResponseWrapper byProcessName = client.ProcForm(procWithForms.Process, firstForm.Number);
			Assert.AreEqual<ProcFormResponseWrapper>(byID, byProcessName);

			// ProcForm obtained via ProcForms will only have the data available on the selected View.
			if (firstForm.Equals(byID.Form))
			{
				Assert.Inconclusive("ProcForm obtained from ProcForms will not necessaryly be equal to the ones from ProcForm.");
			}
			else
			{
				Assert.Inconclusive("ProcForm obtained from ProcForms will not necessaryly be equal to the ones from ProcForm.");
			}
		}

		[TestMethod]
		public void TestProcFormAdd()
		{
			int ProcessID = 77777777;

			FieldResponse someField = new FieldResponse();
			someField.Field = "Non Rep";
			someField.Value = "The Value";

			ProcFormResponse FormData = new ProcFormResponse();
			FormData.Fields.Add(someField);
			Client client = this.getApiClient();
			ProcFormResponseWrapper response = client.ProcFormAdd(ProcessID, FormData);

			Assert.AreEqual(response.ProcessID, ProcessID);
			Assert.AreEqual(response.Form.Fields.Count, 1);
			FieldResponse SavedField = response.Form.Fields[0];
			Assert.AreEqual(someField, SavedField);
		}

		[TestMethod]
		public void TestProcFormEdit()
		{

			int MockFormID = 77777777;

			Client client = this.getApiClient();

			ProcFormResponseWrapper formInformation = client.ProcForm(MockFormID);
			ProcFormResponse formData = formInformation.Form;

			FieldResponse firstField = formData.Fields[0];
			firstField.Value = "Changed!";

			ProcFormResponseWrapper response = client.ProcFormEdit(formData);

			formData.Fields = new List<FieldResponse>();
			formData.Fields.Add( firstField );
			formData.Modified = response.Form.Modified;
			Assert.AreEqual<ProcFormResponse>(formData, response.Form);
		}

		[TestMethod]
		public void TestProcFormNoteAdd()
		{
			Client client = this.getApiClient();

			ProcFormResponseWrapper original = client.ProcForm(77777777);

			ProcFormResponseWrapper response = client.ProcFormNoteAdd(77777777, "Note", "NoteForStaff");
			NoteResponse note = response.Form.Notes[response.Form.Notes.Count - 1];
			original.Form.Notes.Add(note);
			Assert.AreEqual(note.Note, "Note");
			NoteResponse noteForStaff = response.Form.NotesForStaff[response.Form.NotesForStaff.Count - 1];
			original.Form.NotesForStaff.Add(noteForStaff);

			Assert.AreEqual(noteForStaff.Note, "NoteForStaff");
			Assert.AreEqual<ProcFormResponseWrapper>(original, response);
		}

		[TestMethod]
		public void TestProcFormParticipantAddByAgency()
		{
			AgencyResponse agency = this.getFirstAgencyWithReps();
			ProcFormResponse formBasicInfo = this.getFirstForm();
			int FormID = formBasicInfo.FormID;
			
			
			Client client = this.getApiClient();
			ProcFormResponse formBefore = client.ProcForm(FormID).Form;
			
			ProcFormResponseWrapper updatedFormInformation = client.ProcFormParticipantAdd(FormID, agency);

			Assert.IsTrue(updatedFormInformation.Form.Participants.Count == formBefore.Participants.Count + 1);

			ParticipantResponse newParticipant;
			foreach (ParticipantResponse participant in updatedFormInformation.Form.Participants)
			{
				ParticipantResponse found = formBefore.Participants.Find(delegate(ParticipantResponse p)
				{
					return p.Equals(participant);
				});
				if (found == null)
				{
					newParticipant = participant;
					Assert.AreEqual(agency.Reps[0].Rep, newParticipant.Name);
					break;
				}
			}
		}

		[TestMethod]
		public void TestProcForms()
		{
			ProcResponse procWithForms = this.getProcessWithForms();
			if (procWithForms == null)
			{
				Assert.Inconclusive("Could not find a Process with forms");
				return;
			}
			Client client = this.getApiClient();
			ProcFormsResponse byID = client.ProcForms(procWithForms.ProcessID);
			ProcFormsResponse byName = client.ProcForms(procWithForms.Process);

			Assert.AreEqual<ProcFormsResponse>(byID, byName);
		}

		[TestMethod]
		public void TestProcFormSetAdd()
		{
			Client client = getApiClient();

			ProcFormResponseWrapper original = client.ProcForm(77777777);

			List<FieldResponse> Fields = new List<FieldResponse>();
			FieldResponse CustomField1 = new FieldResponse();
			CustomField1.Field = "Field1";
			CustomField1.Value = "Value1";
			Fields.Add(CustomField1);
			ProcFormResponseWrapper response = client.ProcFormSetAdd(77777777, Fields);

			original.Form.Sets.Add(response.Form.Sets[response.Form.Sets.Count - 1]);
			Assert.AreEqual<ProcFormResponseWrapper>(original, response);
		}

		[TestMethod]
		public void TestProcFormUnexistantView()
		{
			ProcResponse procWithForms = this.getProcessWithForms();
			if (procWithForms == null)
			{
				Assert.Inconclusive("Could not find a Process with forms");
				return;
			}
			Client client = this.getApiClient();
			try
			{
				ProcFormsResponse byID = client.ProcForms(procWithForms.ProcessID, 77777777);
				Assert.Inconclusive("Expected 'View not found' error");
			}
			catch (RPMApiError error)
			{

				Assert.AreEqual(error.Message, "View not found");
			}
		}

		[TestMethod]
		public void TestProcFormWorksheet()
		{
			ProcFormResponse form = this.getFirstFormWithWorksheet();
			WorksheetResponse ws = form.Worksheets[0];
			Client client = this.getApiClient();
			WorksheetResponse wsInfo = client.ProcFormWorksheet(ws.WorksheetID);

			// They seem to be the same
			Assert.AreEqual(ws.Name, wsInfo.Name);
			Assert.AreEqual(ws.WorksheetID, wsInfo.WorksheetID);
			// getFirstFormWithWorksheet uses ProcForm which doesn't return the complete Worksheet Data Set.
			Assert.AreNotEqual(ws, wsInfo);
		}

		[TestMethod]

		public void TestProcFormWorksheetAdd()
		{

			ProcFormResponse form = this.getFirstFormWithWorksheet();
			if (form == null)
			{
				Assert.Inconclusive("No Forms With Worksheets Found");
			}

			Client client = this.getApiClient();
			WorksheetResponse ws = form.Worksheets[0];
			ws = client.ProcFormWorksheet(ws.WorksheetID);
			
			WorksheetResponse response = client.ProcFormWorksheetAdd(form.FormID, ws.WorksheetID);

			Assert.AreNotEqual(ws.WorksheetID, response.WorksheetID);
			ws.WorksheetID = response.WorksheetID;
			Assert.AreNotEqual(ws.Name, response.Name);
			Assert.AreEqual(response.Name.Substring(0, ws.Name.Length), ws.Name);
			response.Name = ws.Name;
			Assert.AreNotEqual(ws.DateAdded, response.DateAdded);
			response.DateAdded = ws.DateAdded;


			Assert.AreNotEqual(ws, response); // new IDs for the copy so not exactly equal
		}

		[TestMethod]
		public void TestProcFormWorksheetTableAdd()
		{

			ProcFormResponse form = this.getFirstFormWithWorksheet();
			if (form == null)
			{
				Assert.Inconclusive("No Forms With Worksheets Found");
			}

			Client client = this.getApiClient();
			WorksheetResponse ws = client.ProcFormWorksheet(form.Worksheets[0].WorksheetID);

			WorksheetResponse response = client.ProcFormWorksheetTableAdd(ws.WorksheetID, ws.Tables[0].ID);

			Assert.IsTrue(ws.WorksheetID == response.WorksheetID);
			ws.DateModified = response.DateModified;
			ws.Tables = response.Tables;

			Assert.IsTrue(ws.NumTables == response.NumTables - 1);
			ws.NumTables = response.NumTables;
			Assert.AreEqual(ws, response);
		}

		[TestMethod]
		public void TestSuppliers()
		{
			SupplierResponse supplier = getFirstSupplier();
			Assert.IsNotNull(supplier.Supplier);
			Assert.IsTrue(supplier.SupplierID > 0);
		}

		[TestMethod]
		public void TestProcFormWorksheetTableDataEdit()
		{
			Client client = this.getApiClient();
			WorksheetResponse orig = client.ProcFormWorksheet(77777777); // Mock ID

			WorksheetTable tableInfo = orig.Tables[0];
			tableInfo.Data = new List<WorksheetTableData>();

			WorksheetTableData cell1 = new WorksheetTableData();
			cell1.Value = "Something";
			cell1.ColIndex = tableInfo.getColumnIndex("Column 1");
			cell1.Row = 0;
			WorksheetTableData cell2 = new WorksheetTableData();
			cell2.Value = "10";
			cell2.ColIndex = tableInfo.getColumnIndex("Qty.");
			cell2.Row = 0;
			WorksheetTableData cell3 = new WorksheetTableData();
			cell3.Value = "10";
			cell3.ColIndex = tableInfo.getColumnIndex("Unit Cost");
			cell3.Row = 0;

			tableInfo.Data.Add(cell1);
			tableInfo.Data.Add(cell2);
			tableInfo.Data.Add(cell3);

			WorksheetResponse ws = client.ProcFormWorksheetTableDataEdit(tableInfo, true);

			WorksheetTable modifiedTable = ws.Tables[0];
			cell1.ColID = modifiedTable.Data[modifiedTable.getColumnIndex("Column 1")].ColID;
			cell2.ColID = modifiedTable.Data[modifiedTable.getColumnIndex("Qty.")].ColID;
			cell3.ColID = modifiedTable.Data[modifiedTable.getColumnIndex("Unit Cost")].ColID;

			Assert.AreEqual(ws, orig);
		}

		[TestMethod]
		public void TestRep()
		{
			Client client = this.getApiClient();
			RepResponse rep = this.getFirstRep();
			if (rep == null)
			{
				Assert.Inconclusive("No Reps Found");
				return;
			}

			RepResponse byID = client.Rep(rep.RepID);
			Assert.AreEqual(rep, byID);

			// Get Rep by Agency
			RepResponse byAgencyID = client.RepByName(rep.Rep, rep.AgencyID);
			Assert.AreEqual(rep, byAgencyID);
			RepResponse byAgencyName = client.RepByName(rep.Rep, rep.Agency);
			Assert.AreEqual(rep, byAgencyName);


			// By assignment code + supplier
			if (rep.AssignmentCodes.Count > 0)
			{
				AssignmentCodeResponse r = rep.AssignmentCodes[0];
				RepResponse byAssignmentCodeAndSupplierID = client.RepByAssignmentCode(r.AssignmentCode, r.SupplierID);
				Assert.AreEqual(rep, byAssignmentCodeAndSupplierID);
				RepResponse byAssignmentCodeAndSupplier = client.RepByAssignmentCode(r.AssignmentCode, r.Supplier);
				Assert.AreEqual(rep, byAssignmentCodeAndSupplier);
			}
		}

		[TestMethod]
		public void TestUserPasswordEdit()
		{
			string username = "Joaquin";
			string password = "LePass1245";

			Client client = this.getApiClient();
			client.UserPasswordEdit(username, password);
		}

		#region Helper Functions
		private List<SupplierResponse> getSuppliers()
		{
			Client client = getApiClient();
			List<SupplierResponse> suppliers = client.Suppliers();
			return suppliers;
		}
		private SupplierResponse getFirstSupplier()
		{
			List<SupplierResponse> suppliers = this.getSuppliers();
			return suppliers[0];
		}

		private ProcResponse getProcessWithFunc(Func<ProcResponse, bool> check)
		{
			Client client = this.getApiClient();
			List<ProcResponse> procs = client.Procs();

			ProcResponse procWithForms = null;
			foreach (ProcResponse proc in procs)
			{
				if (check(proc))
				{
					procWithForms = proc;
					break;
				}
			}
			return procWithForms;
		}

		private ProcResponse getProcessWithForms()
		{
			Func<ProcResponse, bool> f = (proc) =>
			{
				return proc.Forms > 0;
			};
			return this.getProcessWithFunc(f);
		}

		private ProcResponse getProcessWithFormsHavingWorksheets()
		{
			Client client = this.getApiClient();
			Func<ProcResponse, bool> f = (proc) =>
			{
				if (proc.Forms == 0)
				{
					return false;
				}
				ProcFormsResponse forms = client.ProcForms(proc.ProcessID);
				foreach (ProcFormResponse formInfo in forms.Forms)
				{
					ProcFormResponseWrapper form = client.ProcForm(formInfo.FormID);
					if (form.Form.Worksheets.Count > 1)
					{
						return true;
					}
				}
				return false;
			};
			return this.getProcessWithFunc(f);
		}

		private ProcFormResponse getFirstForm()
		{
			ProcResponse proc = this.getProcessWithForms();
			Client client = this.getApiClient();
			ProcFormsResponse formsInformation = client.ProcForms(proc.ProcessID);
			return formsInformation.Forms[0];
		}

		private ProcFormResponse getFirstFormWithWorksheet()
		{
			ProcResponse proc = this.getProcessWithFormsHavingWorksheets();
			Client client = this.getApiClient();
			ProcFormsResponse formsInformation = client.ProcForms(proc.ProcessID);
			foreach (ProcFormResponse formInfo in formsInformation.Forms)
			{
				ProcFormResponseWrapper form = client.ProcForm(formInfo.FormID);
				if (form.Form.Worksheets.Count > 0)
				{
					return form.Form;
				}
			}
			return null;
		}

		private AgencyResponse getFirstAgencyWithReps()
		{
			Client client = this.getApiClient();
			
			List<AgencyResponse> agencies = client.Agencies();
			foreach (AgencyResponse agencyInfo in agencies)
			{
				AgencyResponse agency = client.Agency(agencyInfo.AgencyID);
				if (agency.Reps.Count > 0)
				{
					return agency;
				}
			}
			return null;
		}

		private RepResponse getFirstRep()
		{
			Client client = this.getApiClient();
			List<AgencyResponse> agencies = client.Agencies();
			foreach (AgencyResponse agency in agencies)
			{
				AgencyResponse fullInfo = client.Agency(agency.AgencyID);
				if (fullInfo.Reps.Count > 0)
				{
					RepResponse rep = client.Rep(fullInfo.Reps[0].RepID);
					return rep;
				}
			}
			return null;
		}
		#endregion
	}
}

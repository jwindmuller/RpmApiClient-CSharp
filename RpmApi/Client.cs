using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Dynamic; // ExpandoObject
using System.Net; //WebException
using RestSharp;
using RestSharp.Deserializers;
using RPM.Api.Response;

namespace RPM.Api
{
    public class Client
    {
		private string url;
        private string key;
        private RestClient client;


		/// <summary>
		/// Initializes a new instance of the <see cref="Client" /> class.
		/// To obtain the API Url and Key, please see RPM's Documentation site at:
		/// http://rpmsoftware.wordpress.com/getting-started/
		/// </summary>
		/// <param name="apiURL">The API URL.</param>
		/// <param name="apiKey">The API key.</param>
		/// <exception cref="System.UriFormatException">Invalid URL</exception>
		public Client(string apiURL, string apiKey, RestClient client = null)
        {
            this.url = apiURL;
            if (!this.url.EndsWith("/"))
            {
                this.url += "/";
            }
			string url = this.url + "Api2.svc";
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
			{
				throw new UriFormatException("Invalid URL: " + this.url);
			}
			if (client == null)
			{
				this.client = new RestClient(url);
			}
			else
			{
				this.client = client;
			}
            
            this.key = apiKey;
        }

		/// <summary>
		/// Execute the "Account" API Endpoint.
		/// http://rpmsoftware.wordpress.com/api/account/
		/// </summary>
		/// <param name="SupplierID">The supplier identifier.</param>
		/// <param name="AccountID">The account identifier.</param>
		/// <returns>AccountResponse object with the response data</returns>
		public AccountResponse Account(int SupplierID, int AccountID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.SupplierID = SupplierID;
			apiParameters.AccountID = AccountID;
			return this.Account(apiParameters);
		}

		/// <summary>
		/// Execute the "Account" API Endpoint.
		/// http://rpmsoftware.wordpress.com/api/account/
		/// </summary>
		/// <param name="SupplierID">The supplier identifier.</param>
		/// <param name="AccountName">Name of the account.</param>
		/// <returns>AccountResponse object with the response data</returns>
		public AccountResponse Account(int SupplierID, string AccountName)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.SupplierID = SupplierID;
			apiParameters.Account = AccountName;
			return this.Account(apiParameters);
		}

		/// <summary>
		/// Execute the "Account" API Endpoint.
		/// http://rpmsoftware.wordpress.com/api/account/
		/// </summary>
		/// <param name="SupplierName">Name of the supplier.</param>
		/// <param name="AccountID">The account identifier.</param>
		/// <returns>AccountResponse object with the response data</returns>
		public AccountResponse Account(string SupplierName, int AccountID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Supplier = SupplierName;
			apiParameters.AccountID = AccountID;
			return this.Account(apiParameters);
		}

		/// <summary>
		/// Execute the "Account" API Endpoint.
		/// http://rpmsoftware.wordpress.com/api/account/
		/// </summary>
		/// <param name="SupplierName">Name of the supplier.</param>
		/// <param name="AccountName">Name of the account.</param>
		/// <returns>AccountResponse object with the response data</returns>
		public AccountResponse Account(string SupplierName, string AccountName)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Supplier = SupplierName;
			apiParameters.Account = AccountName;
			return this.Account(apiParameters);
		}

		/// <summary>
		/// Execute the "Account" API Endpoint.
		/// http://rpmsoftware.wordpress.com/api/Account/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>AccountResponse object with the response data</returns>
		private AccountResponse Account(dynamic apiParameters)
		{
			return this.sendRequest<AccountResponse>("Account", apiParameters);
		}

		/// <summary>
		/// <para>Return a list of Accounts for a Supplier ID.</para>
		/// <remarks>Note: Expect AccountResponse.Rep to be null. Use Account call to get Account's Reps.</remarks>
		/// <para>Executes the "Accounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Accounts/ </para>
		/// </summary>
		/// <param name="SupplierID">The supplier identifier.</param>
		/// <returns>List of AccountResponse object with the response data</returns>
		public List<AccountResponse> AccountsForSupplier(int SupplierID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.SupplierID = SupplierID;
			return this.Accounts(apiParameters);
		}

		/// <summary>
		/// <para>Return a list of Accounts for a Supplier ID.</para>
		/// <remarks>Note: Expect AccountResponse.Rep to be null. Use Account call to get Account's Reps.</remarks>
		/// <para>Executes the "Accounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Accounts/ </para>
		/// </summary>
		/// <param name="SupplierID">The supplier identifier.</param>
		/// <returns>List of AccountResponse object with the response data</returns>
		public List<AccountResponse> AccountsForSupplier(string SupplierName)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Supplier = SupplierName;
			return this.Accounts(apiParameters);
		}

		/// <summary>
		/// <para>Return a list of Accounts for a Supplier ID.</para>
		/// <remarks>Note: Expect AccountResponse.Rep to be null. Use Account call to get Account's Reps.</remarks>
		/// <para>Executes the "Accounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Accounts/ </para>
		/// </summary>
		/// <param name="CustomerID">The customer ID.</param>
		/// <returns>List of AccountResponse object with the response data</returns>
		public List<AccountResponse> AccountsForCustomer(int CustomerID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.CustomerID = CustomerID;
			return this.Accounts(apiParameters);
		}

		/// <summary>
		/// <para>Return a list of Accounts for a Supplier ID.</para>
		/// <remarks>Note: Expect AccountResponse.Rep to be null. Use Account call to get Account's Reps.</remarks>
		/// <para>Executes the "Accounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Accounts/ </para>
		/// </summary>
		/// <param name="CustomerName">Name of the customer.</param>
		public List<AccountResponse> AccountsForCustomer(string CustomerName)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Customer = CustomerName;
			return this.Accounts(apiParameters);
		}


		/// <summary>
		/// Execute the "Accounts" API endpoint
		/// <para>
		/// http://rpmsoftware.wordpress.com/api/Accounts/
		/// </para>
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>List of AccountResponse object with the response data</returns>
		private List<AccountResponse> Accounts(dynamic apiParameters)
		{
			Dictionary<String, List<AccountResponse>> response =
				this.sendRequest<Dictionary<String, List<AccountResponse>>>("Accounts", apiParameters);
			return response["Accounts"];
		}

		/// <summary>
		/// Execute the "Agencies" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Agencies/
		/// <para>
		/// The resulting Agencies only contain Agency ID and Name
		/// </para>
		/// </summary>
		/// <returns>List of AgencyResponse object with the response data</returns>
		public List<AgencyResponse> Agencies()
		{
			Dictionary<String, List<AgencyResponse>> response =
				this.sendRequest<Dictionary<String, List<AgencyResponse>>>("Agencies");
			List<AgencyResponse> agencies = response["Agencies"];
			return agencies;
		}

		/// <summary>
		/// Execute the "Agency" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Agency/
		/// </summary>
		/// <param name="AgencyID">The agency ID.</param>
		/// <returns>AgencyResponse object with the response data</returns>
		public AgencyResponse Agency(int AgencyID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.AgencyID = AgencyID;
			return this.Agency(apiParameters);
		}

		/// <summary>
		/// Execute the "Agency" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Agency/
		/// </summary>
		/// <param name="AgencyName">The Agency Name.</param>
		/// <returns>AgencyResponse object with the response data</returns>
		public AgencyResponse Agency(string AgencyName)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Agency = AgencyName;
			return this.Agency(apiParameters);
		}

		/// <summary>
		/// Execute the "Agency" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Agency/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>AgencyResponse object with the response data</returns>
		private AgencyResponse Agency(dynamic apiParameters)
		{
			return this.sendRequest<AgencyResponse>("Agency", apiParameters);
		}

		/// <summary>
		/// User facing Enum to provide the Variable to report on.
		/// </summary>
		/// <see cref="CommAccounts"/>
		public enum Var {NetBilled, GrossComm, AgentComm, GrossProfit, Referral, ContractVal, Margin, Wholesale};

		/// <summary>
		/// Private translation of the Var Enum into string
		/// </summary>
		/// <see cref="CommAccounts"/>
		private string[] VarStr = { "NetBilled", "GrossComm", "AgentComm", "GrossProfit", "Referral", "ContractVal", "Margin", "Wholesale" };

		/// <summary>
		/// User facing Enum to provide the Run types that can be reported on.
		/// </summary>
		/// <see cref="CommAccounts"/>
		public enum Run {
			/// <summary>Include all runs in the report</summary>
			all,
			/// <summary>Limit the report to only the current run</summary>
			_this,
			/// <summary>Limit the report to only the previous run</summary>
			prev,
			/// <summary>Limit the report to only the most recently closed run</summary>
			closed };

		/// <summary>
		/// Private translation of the Run Enum into string
		/// </summary>
		/// <see cref="CommAccounts"/>
		public string[] RunStr = {"all", "this", "prev", "closed" };

		/// <summary>
		/// Execute the "CommAccounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommAccounts/
		/// </summary>
		/// <param name="variable">The variable to report.</param>
		/// <param name="run">Which run to report on.</param>
		/// <returns>List of CommAccountResponse containing the reponse data.</returns>
		public List<CommAccountResponse> CommAccounts(Var variable, Run run)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Var = VarStr[(int)variable];
			apiParameters.Run = RunStr[(int)run];
			return this.CommAccounts(apiParameters);
		}

		/// <summary>
		/// Execute the "CommAccounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommAccounts/
		/// </summary>
		/// <param name="variable">The variable.</param>
		/// <param name="yyyy">Year in yyyy format.</param>
		/// <param name="mm">Month in mm format.</param>
		/// <returns>List of CommAccountResponse containing the reponse data.</returns>
		public List<CommAccountResponse> CommAccounts(Var variable, string yyyy, string mm = "")
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Var = VarStr[(int)variable];
			apiParameters.Run = yyyy + mm;
			return this.CommAccounts(apiParameters);
		}

		/// <summary>
		/// Execute the "CommAccounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommAccounts/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>List of CommAccountResponse containing the reponse data.</returns>
		private List<CommAccountResponse> CommAccounts(dynamic apiParameters)
		{
			Dictionary<String, List<CommAccountResponse>> response =
				this.sendRequest<Dictionary<String, List<CommAccountResponse>>>("CommAccounts", apiParameters);

			return response["Accounts"];
		}

		/// <summary>
		///   <para>Return Customer information by providing their Name.</para>
		///   <para>Executes the "Customer" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Customer/ </para>
		/// </summary>
		/// <param name="CustomerName">The customer Name.</param>
		/// <returns>
		/// CustomerResponse containing the reponse data.
		/// </returns>
		public CustomerResponse Customer(string CustomerName)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Customer = CustomerName;
			return this.Customer(apiParameters);
		}

		/// <summary>
		///   <para>Return Customer information by providing their ID.</para>
		///   <para>Executes the "Customer" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Customer/ </para>
		/// </summary>
		/// <param name="CustomerID">The customer ID.</param>
		/// <returns>
		/// CustomerResponse containing the reponse data.
		/// </returns>
		public CustomerResponse Customer(int CustomerID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.CustomerID = CustomerID;
			return this.Customer(apiParameters);
		}

		/// <summary>
		///   <para>Return Customer information by providing their ID.</para>
		///   <para>Executes the "Customer" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Customer/ </para>
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>
		/// CustomerResponse containing the reponse data.
		/// </returns>
		private CustomerResponse Customer(dynamic apiParameters)
		{
			return this.sendRequest<CustomerResponse>("Customer", apiParameters);
		}

		/// <summary>
		///   <para>Add Customer information.</para>
		///   <para>Executes the "CustomerAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerAdd/ </para>
		/// </summary>
		/// <returns>CustomerResponse containing the newly created customer.</returns>
		public CustomerResponse CustomerAdd(CustomerResponse newCustomerData)
		{
			newCustomerData.Contacts.Clear();
			dynamic apiParameters = this.apiParameters();
			apiParameters.Customer = newCustomerData;
			return this.CustomerAdd(apiParameters);
		}

		/// <summary>
		///   <para>Create Customer information by providing their ID.</para>
		///   <para>Executes the "Customer" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerAdd/ </para>
		/// </summary>
		/// <returns>CustomerResponse containing the newly created customer.</returns>
		private CustomerResponse CustomerAdd(dynamic apiParameters)
		{
			return this.sendRequest<CustomerResponse>("CustomerAdd", apiParameters);
		}

		/// <summary>
		///   <para>Add Contact Information for an existing Customer.</para>
		///   <para>Executes the "CustomerContactAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerContactAdd/ </para>
		/// </summary>
		/// <param name="CustomerID">The customer identifier.</param>
		/// <param name="ContactData">The contact data.</param>
		/// <param name="MakePrimary">if set to <c>true</c> [make primary contact].</param>
		/// <returns>CustomerResponse containing the customer information including the Contact Information.</returns>
		public ContactResponse CustomerContactAdd(int CustomerID, ContactResponse ContactData, Boolean MakePrimary = false)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.CustomerID = CustomerID;
			apiParameters.Contact = ContactData;
			apiParameters.IsPrimary = MakePrimary;
			return this.CustomerContactAdd(apiParameters);
		}

		/// <summary>
		///   <para>Add Contact Information for an existing Customer.</para>
		///   <para>Executes the "CustomerContactAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerContactAdd/ </para>
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>CustomerResponse containing the customer information including the Contact Information.</returns>
		private ContactResponse CustomerContactAdd(dynamic apiParameters)
		{
			Dictionary<string, ContactResponse> response =
				this.sendRequest<Dictionary<string, ContactResponse>>("CustomerContactAdd", apiParameters);
			return response["Contact"];
		}

		/// <summary>
		///   <para>Edit Contact Information for an existing Customer.</para>
		///   <para>Executes the "CustomerContactEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerContactEdit/ </para>
		/// </summary>
		/// <param name="CustomerID">The customer identifier.</param>
		/// <param name="ContactData">The contact data.</param>
		/// <param name="MakePrimary">if set to <c>true</c> [make primary].</param>
		/// <returns>CustomerResponse containing the customer information including the Contact Information.</returns>
		public ContactResponse CustomerContactEdit(int CustomerID, ContactResponse ContactData, Boolean MakePrimary = false)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.CustomerID = CustomerID;
			apiParameters.Contact = ContactData;
			apiParameters.IsPrimary = MakePrimary;
			return this.CustomerContactEdit(apiParameters);
		}

		/// <summary>
		///   <para>Edit Contact Information for an existing Customer.</para>
		///   <para>Executes the "CustomerContactEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerContactEdit/ </para>
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>CustomerResponse containing the customer information including the Contact Information.</returns>
		private ContactResponse CustomerContactEdit(dynamic apiParameters)
		{
			ContactResponse Contact = apiParameters.Contact;
			// Remove any phone that has no ID and was left empty
			Contact.PhoneNumbers.RemoveAll(phone => phone.Number == "" && phone.PhoneNumberID == 0);

			Dictionary<string, ContactResponse> response =
				this.sendRequest<Dictionary<string, ContactResponse>>("CustomerContactEdit", apiParameters);
			return response["Contact"];
		}

		/// <summary>
		///   <para>Add Location Information for an existing Customer.</para>
		///   <para>Executes the "CustomerLocationAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerLocationAdd/ </para>
		/// </summary>
		/// <param name="CustomerID">The customer identifier.</param>
		/// <param name="Location">The location data.</param>
		/// <returns>LocationResponse with only the newly created ID in CustomerLocationID.</returns>
		public LocationResponse CustomerLocationAdd(int CustomerID, LocationResponse Location)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.CustomerID = CustomerID;
			apiParameters.Location = Location;
			return this.CustomerLocationAdd(apiParameters);
		}

		/// <summary>
		///   <para>Add Location Information for an existing Customer.</para>
		///   <para>Executes the "CustomerLocationAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerLocationAdd/ </para>
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>LocationResponse with only the newly created ID in CustomerLocationID.</returns>
		private LocationResponse CustomerLocationAdd(dynamic apiParameters)
		{
			return this.sendRequest<LocationResponse>("CustomerLocationAdd", apiParameters);
		}

		/// <summary>
		///   <para>Edit Current Location Information for an existing Customer.</para>
		///   <para>Executes the "CustomerLocationEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerLocationEdit/ </para>
		/// </summary>
		/// <param name="CustomerID">The customer identifier.</param>
		/// <param name="Location">The location data.</param>
		/// <returns>LocationResponse with only the newly created ID in CustomerLocationID.</returns>
		public LocationResponse CustomerLocationEdit(int CustomerID, LocationResponse Location)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.CustomerID = CustomerID;
			apiParameters.Location = Location;
			return this.CustomerLocationEdit(apiParameters);
		}

		/// <summary>
		///   <para>Edit Current Location Information for an existing Customer.</para>
		///   <para>Executes the "CustomerLocationEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CustomerLocationEdit/ </para>
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>LocationResponse with only the newly created ID in CustomerLocationID.</returns>
		private LocationResponse CustomerLocationEdit(dynamic apiParameters)
		{
			return this.sendRequest<LocationResponse>("CustomerLocationEdit", apiParameters);
		}

		/// <summary>
		/// <para>Returns a list of all the Customers registered (Customer Name and ID only)</para>
		///   <para>Executes the "Customers" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Customers/ </para>
		/// </summary>
		/// <returns>List of CustomerResponse. Note: only CustomerID and Customer fields will be filled in</returns>
		public List<CustomerResponse> Customers()
		{
			Dictionary<String, List<CustomerResponse>> response = this.sendRequest<Dictionary<String, List<CustomerResponse>>>("Customers");
			List<CustomerResponse> customers = response["Customers"];
			return customers;
		}

		/// <summary>
		/// <para>Update limited Customer Data.</para>
		///   <para>Executes the "Customers" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Customers/ </para>
		/// <para>It's not recommended to use this method as it is very limited and will be removed in the future.</para>
		/// </summary>
		/// <param name="CustomerData">The customer data.</param>
		/// <returns></returns>
		[Obsolete("Use CustomerEdit, CustomerLocationEdit and CustomerContactEdit")]
		public CustomerResponse CustomerUpdate(CustomerResponse CustomerData)
		{
			CustomerUpdateData data = new CustomerUpdateData(CustomerData);
			dynamic apiParameters = this.apiParameters();
			apiParameters.Customer = data;
			return this.sendRequest<CustomerResponse>("CustomerUpdate", apiParameters);
		}

		/// <summary>
		/// Execute the "Info" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/info/
		/// </summary>
		/// <returns>InfoResponse containing the response data.</returns>
        public InfoResponse Info() {
            InfoResponse result = this.sendRequest<InfoResponse>("Info");
            return result;
        }

		public List<ProcActionDueResponse> ProcActionsDue()
		{
			Dictionary<string, List<ProcActionDueResponse>> response =
				this.sendRequest<Dictionary<string, List<ProcActionDueResponse>>>("ProcActionsDue");
			return response["Procs"];
		}

		/// <summary>
		/// Execute the "ProcForm" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcForm/
		/// </summary>
		/// <param name="FormID">The FormID.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
		public ProcFormResponseWrapper ProcForm(int FormID)
		{
			dynamic parameters = this.apiParameters();
			parameters.FormID = FormID;
			return this.sendRequest<ProcFormResponseWrapper>("ProcForm", parameters);
		}

		/// <summary>
		/// Execute the "ProcForm" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcForm/
		/// </summary>
		/// <param name="ProcessID">The ProcessID.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
		public ProcFormResponseWrapper ProcForm(int ProcessID, string FormNumber)
		{
			dynamic parameters = this.apiParameters();
			parameters.ProcessID = ProcessID;
			parameters.FormNumber = FormNumber;
			return this.ProcForm(parameters);
		}

		/// <summary>
		/// Execute the "ProcForm" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcForm/
		/// </summary>
		/// <param name="ProcessName">Name of the process.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
		public ProcFormResponseWrapper ProcForm(string ProcessName, string FormNumber)
		{
			dynamic parameters = this.apiParameters();
			parameters.Process = ProcessName;
			parameters.FormNumber = FormNumber;
			return this.ProcForm(parameters);
		}

		/// <summary>
		/// Execute the "ProcForm" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcForm/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
		private ProcFormResponseWrapper ProcForm(dynamic apiParameters)
		{
			return this.sendRequest<ProcFormResponseWrapper>("ProcForm", apiParameters);
		}

		/// <summary>
		/// Execute the "ProcForms" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcForms/
		/// </summary>
		/// <param name="ProcessName">Name of the process.</param>
		/// <param name="ViewID">The View ID to use.</param>
		/// <returns>ProcFormsResult containing the response data.</returns>
		public ProcFormsResponse ProcForms(string ProcessName, int ViewID = 0)
        {
			dynamic apiParameters = this.apiParameters();
			apiParameters.Process = ProcessName;
			if (ViewID > 0)
			{
				apiParameters.ViewID = ViewID;
			}
            return this.sendRequest<ProcFormsResponse>("ProcForms", apiParameters);
        }

		/// <summary>
		/// <para>Add values to a set of repeating fields.</para>
		///   <para>Executes the "ProcFormSetAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormSetAdd/ </para>
		/// </summary>
		/// <param name="FormID">The form identifier.</param>
		/// <param name="Fields">The fields to add.</param>
		/// <returns>ProcFormResponse with the entire Form Data.</returns>
		public ProcFormResponseWrapper ProcFormSetAdd(int FormID, List<FieldResponse> Fields)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Form = new
			{
				FormID = FormID,
				Fields = Fields
			};
			return this.ProcFormSetAdd(apiParameters);
		}

		/// <summary>
		/// <para>Add values to a set of repeating fields.</para>
		///   <para>Executes the "ProcFormSetAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormSetAdd/ </para>
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>ProcFormResponse with the entire Form Data.</returns>
		private ProcFormResponseWrapper ProcFormSetAdd(dynamic apiParameters)
		{
			return this.sendRequest<ProcFormResponseWrapper>("ProcFormSetAdd", apiParameters);
		}

		/// <summary>
		/// Execute the "ProcForms" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcForms/
		/// </summary>
		/// <param name="ProcessID">The Process ID.</param>
		/// <param name="ViewID">The view identifier.</param>
		/// <returns>ProcFormsResult containing the response data.</returns>
		public ProcFormsResponse ProcForms(int ProcessID, int ViewID = 0)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.ProcessID = ProcessID;
			if (ViewID > 0)
			{
				apiParameters.ViewID = ViewID;
			}
			return this.ProcForms(apiParameters);
		}

		/// <summary>
		/// Execute the "ProcForms" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcForms/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns></returns>
		private ProcFormsResponse ProcForms(dynamic apiParameters)
		{
			return this.sendRequest<ProcFormsResponse>("ProcForms", apiParameters);
		}

		/// <summary>
		/// Execute the "ProcFormAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormAdd/
		/// </summary>
		/// <param name="ProcessID">The process identifier.</param>
		/// <param name="FormData">The form data.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
        public ProcFormResponseWrapper ProcFormAdd(int ProcessID, ProcFormResponse FormData)
        {
            dynamic parameters = this.apiParameters();
			parameters.ProcessID = ProcessID;
			parameters.Form = FormData;
            return this.sendRequest<ProcFormResponseWrapper>("ProcFormAdd", parameters);
        }

		/// <summary>
		/// Execute the "ProcForm" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormEdit/
		/// </summary>
		/// <param name="FormID">The form identifier.</param>
		/// <param name="FormData">The form data.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
		public ProcFormResponseWrapper ProcFormEdit(ProcFormResponse FormData = null)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Form = FormData;
			return this.ProcFormEdit(apiParameters);
		}

		/// <summary>
		/// Execute the "ProcForm" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormEdit/
		/// </summary>
		/// <param name="ProcessID">The process ID.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <param name="FormData">The form data to modify, any field missing will not be modified.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
		public ProcFormResponseWrapper ProcFormEdit(int ProcessID, string FormNumber, ProcFormResponse FormData = null)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.ProcessID = ProcessID;
			FormData.Number = FormNumber;
			apiParameters.Form = FormData;
			return this.ProcFormEdit(apiParameters);
		}

		/// <summary>
		/// Execute the "ProcForm" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormEdit/
		/// </summary>
		/// <param name="ProcessName">Name of the process.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <param name="FormData">The form data to modify, any field missing will not be modified.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
		public ProcFormResponseWrapper ProcFormEdit(string ProcessName, string FormNumber, ProcFormResponse FormData = null)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Process = ProcessName;
			FormData.Number = FormNumber;
			apiParameters.Form = FormData;
			return this.ProcFormEdit(apiParameters);
		}

		/// <summary>
		/// Execute the "ProcForm" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormEdit/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>ProcFormResponse containing the response data.</returns>
		private ProcFormResponseWrapper ProcFormEdit(dynamic apiParameters)
		{
			ProcFormResponse FormData = apiParameters.Form;
			apiParameters.Form = FormData.Clone();
			apiParameters.Form.Fields = null;
			apiParameters.Form.Values = null;
			apiParameters.Form.Participants = null;
			apiParameters.Form.Sets = null;
			return this.sendRequest<ProcFormResponseWrapper>("ProcFormEdit", apiParameters);
		}

		public ProcFormResponseWrapper ProcFormNoteAdd(string ProcessName, string FormNumber, string Note, string NoteForStaff)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Process = ProcessName;
			apiParameters.Form = new
			{
				Number = FormNumber,
				Note = Note,
				NoteForStaff = NoteForStaff
			};
			return this.ProcFormNoteAdd(apiParameters);
		}

		public ProcFormResponseWrapper ProcFormNoteAdd(int ProcessID, string FormNumber, string Note, string NoteForStaff)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.ProcesID = ProcessID;
			apiParameters.Form = new
			{
				Number = FormNumber,
				Note = Note,
				NoteForStaff = NoteForStaff
			};
			return this.ProcFormNoteAdd(apiParameters);
		}

		public ProcFormResponseWrapper ProcFormNoteAdd(int FormID, string Note, string NoteForStaff)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Form = new
			{
				FormID = FormID,
				Note = Note,
				NoteForStaff = NoteForStaff
			};
			return this.ProcFormNoteAdd(apiParameters);
		}

		private ProcFormResponseWrapper ProcFormNoteAdd(dynamic apiParameters)
		{
			return this.sendRequest<ProcFormResponseWrapper>("ProcFormNoteAdd", apiParameters);
		}

		#region ProcFormParticipantAdd FormID
		/// <summary>
		/// <para>Add a participant to a Form (Form ID)
		/// Execute the "ProcFormParticipantAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormParticipantAdd/
		/// </summary>
		/// <param name="FormID">The form identifier.</param>
		/// <param name="ParticipantUsername">The participant username.</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
		public ProcFormResponseWrapper ProcFormParticipantAdd(int FormID, string ParticipantUsername)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Form = new {FormID = FormID};
			apiParameters.Username = ParticipantUsername;
			return this.ProcFormParticipantAdd(apiParameters);
		}

		/// <summary>
		/// <para>Add a participant to a Form (Form ID)
		/// Execute the "ProcFormParticipantAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormParticipantAdd/
		/// </summary>
		/// <param name="FormID">The form identifier.</param>
		/// <param name="agency">Identify the new participant by agency (first manager if available, or rep).</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
		public ProcFormResponseWrapper ProcFormParticipantAdd(int FormID, AgencyResponse agency)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Form = new { FormID = FormID };
			if (agency.AgencyID > 0)
			{
				apiParameters.AgencyID = agency.AgencyID;
			}
			else
			{
				apiParameters.Agency = agency.Agency;
			}
			return this.ProcFormParticipantAdd(apiParameters);
		}
		#endregion

		#region ProcFormParticipantAdd ProcessID + FormNumber
		/// <summary>
		/// <para>Add a participant to a Form (Process ID + Form Number)
		/// Execute the "ProcFormParticipantAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormParticipantAdd/
		/// </summary>
		/// <param name="ProcessID">The process identifier.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <param name="ParticipantUsername">The participant username.</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
		public ProcFormResponseWrapper ProcFormParticipantAdd(int ProcessID, string FormNumber, string ParticipantUsername)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.ProcessID = ProcessID;
			apiParameters.Form = new { FormNumber = FormNumber };
			apiParameters.ParticipantUsername = ParticipantUsername;
			return this.ProcFormParticipantAdd(apiParameters);
		}

		/// <summary>
		/// <para>Add a participant to a Form (Process ID + Form Number)
		/// Execute the "ProcFormParticipantAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormParticipantAdd/
		/// </summary>
		/// <param name="ProcessID">The process identifier.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <param name="agency">Identify the new participant by agency (first manager if available, or rep).</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
		public ProcFormResponseWrapper ProcFormParticipantAdd(int ProcessID, string FormNumber, AgencyResponse agency)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.ProcessID = ProcessID;
			apiParameters.Form = new { FormNumber = FormNumber };
			if (agency.AgencyID > 0)
			{
				apiParameters.AgencyID = agency.AgencyID;
			}
			else
			{
				apiParameters.Agency = agency.Agency;
			}
			return this.ProcFormParticipantAdd(apiParameters);
		}
		#endregion

		#region ProcFormParticipantAdd ProcessName + FormNumber
		/// <summary>
		/// <para>Add a participant to a Form (Process Name + Form Number)
		/// Execute the "ProcFormParticipantAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormParticipantAdd/
		/// </summary>
		/// <param name="ProcessName">Name of the process.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <param name="ParticipantUsername">The participant username.</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
		public ProcFormResponseWrapper ProcFormParticipantAdd(string ProcessName, string FormNumber, string ParticipantUsername)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.ProcessName = ProcessName;
			apiParameters.Form = new { FormNumber = FormNumber };
			apiParameters.ParticipantUsername = ParticipantUsername;
			return this.ProcFormParticipantAdd(apiParameters);
		}

		/// <summary>
		/// <para>Add a participant to a Form (Process Name + Form Number)</para>
		/// Execute the "ProcFormParticipantAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormParticipantAdd/
		/// </summary>
		/// <param name="ProcessName">Name of the process.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <param name="agency">Identify the new participant by agency (first manager if available, or rep).</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
		public ProcFormResponseWrapper ProcFormParticipantAdd(string ProcessName, string FormNumber, AgencyResponse agency)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.ProcessName = ProcessName;
			apiParameters.Form = new { FormNumber = FormNumber };
			if (agency.AgencyID > 0)
			{
				apiParameters.AgencyID = agency.AgencyID;
			}
			else
			{
				apiParameters.Agency = agency.Agency;
			}
			return this.ProcFormParticipantAdd(apiParameters);
		}
		#endregion

		/// <summary>
		/// <para>Add a participant to a Form</para>
		/// Execute the "ProcFormParticipantAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormParticipantAdd/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>ProcFormResponseWrapper</returns>
		private ProcFormResponseWrapper ProcFormParticipantAdd(dynamic apiParameters)
		{
			return this.sendRequest<ProcFormResponseWrapper>("ProcFormParticipantAdd", apiParameters);
		}

		/// <summary>
		/// Execute the "Procs" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/procs/
		/// </summary>
		/// <returns>List of ProcResponse containing the response data.</returns>
		public List<ProcResponse> Procs()
		{
			Dictionary<String, List<ProcResponse>> response =
				this.sendRequest<Dictionary<String, List<ProcResponse>>>("Procs");

			return response["Procs"];
		}

		/// <summary>
		/// Execute the "Suppliers" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Suppliers/
		/// </summary>
		/// <returns>SupplierResponse containing the response data.</returns>
		public List<SupplierResponse> Suppliers()
		{
			Dictionary<String, List<SupplierResponse>> response =
				this.sendRequest<Dictionary<String, List<SupplierResponse>>>("Suppliers");
			return response["Suppliers"];
		}

		/// <summary>
		/// Sends a Request to the RPM API.
		/// http://rpmsoftware.wordpress.com/api/
		/// </summary>
		/// <typeparam name="T">Generic Return Type</typeparam>
		/// <param name="endpoint">The Endpoint to call. </param>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>Generic return type dependant on the specific endpoint.</returns>
		/// <exception cref="System.Net.WebException">If the request returns HTTP errors (Not Found, Bad Request).</exception>
		/// <exception cref="RPM.Api.Response.RPMApiError">An RPM API Error occurred.</exception>
        private T sendRequest<T>(string endpoint, dynamic apiParameters = null)
        {
            if (apiParameters == null)
            {
                apiParameters = this.apiParameters();
            }

            RestRequest request = new RestRequest(endpoint, Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(apiParameters);

            RestResponse response = (RestResponse)this.client.Execute(request);
            string statusDescription = response.StatusDescription.ToString();
            if (statusDescription == "Not Found" || statusDescription == "Bad Request")
            {
                throw new WebException(statusDescription);
            }
            JsonDeserializer js = new JsonDeserializer();
            response.Content = response.Content.Replace("{\"Result\":", "");
            response.Content = response.Content.Substring(0, response.Content.Length - 1);
				
            if (response.Content.StartsWith("{\"Error\""))
            {
                RPMApiError parsedError = js.Deserialize<RPMApiError>(response);
                throw parsedError;
            }
            T parsedResponse = js.Deserialize<T>(response);
            return parsedResponse;
        }

		/// <summary>
		/// Builds the basic API parameters object.
		/// </summary>
		/// <returns>Dynamic ExpandoObject with the API key</returns>
        private dynamic apiParameters()
        {
            dynamic apiParameters = new ExpandoObject();
            apiParameters.Key = this.key;
            return apiParameters;
        }
    }
}

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
		#region Client
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
            this.url = url;
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
		#endregion

		#region Account
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
			// FIX: The Account endpoint 
			Func<string, string> FixResponse = delegate(string response)
			{
				return response.Replace("AssignmentCodes", "AssignmentCodeIDs");
			};
			return this.sendRequest<AccountResponse>("Account", apiParameters, FixResponse);
		}
		#endregion

		#region TODO:AccountAdd
		#endregion

		#region Accounts
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
		#endregion

		#region Agencies
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
		#endregion

		#region Agency
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
		#endregion

		#region CommReports
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
		public enum Run
		{
			/// <summary>Include all runs in the report</summary>
			all,
			/// <summary>Limit the report to only the current run</summary>
			_this,
			/// <summary>Limit the report to only the previous run</summary>
			prev,
			/// <summary>Limit the report to only the most recently closed run</summary>
			closed
		};

		/// <summary>
		/// Private translation of the Run Enum into string
		/// </summary>
		/// <see cref="CommAccounts"/>
		public string[] RunStr = {"all", "this", "prev", "closed" };
		private List<T> CommReport<T>(string name, Var variable, Run run)
		{
			string Var = VarStr[(int)variable];
			string Run = RunStr[(int)run];
			return this.CommReport<T>(name, Var, Run);
		}
		private List<T> CommReport<T>(string name, string Var, string Run)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Var = Var;
			apiParameters.Run = Run;

			string endpoint = "Comm" + name;
			Dictionary<String, List<T>> response =
				this.sendRequest<Dictionary<String, List<T>>>(endpoint, apiParameters);
			return response[name];
		}
		#region CommAccounts
		/// <summary>
		/// <para>Commission report by Accounts</para>
		/// Execute the "CommAccounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommAccounts/
		/// </summary>
		/// <param name="variable">The variable to report.</param>
		/// <param name="run">Which run to report on.</param>
		/// <returns>List of CommAccountResponse containing the reponse data.</returns>
		public List<CommAccountResponse> CommAccounts(Var variable, Run run)
		{
			return this.CommReport<CommAccountResponse>("Accounts", variable, run);
		}

		/// <summary>
		/// <para>Commission report by Accounts</para>
		/// Execute the "CommAccounts" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommAccounts/
		/// </summary>
		/// <param name="variable">The variable.</param>
		/// <param name="yyyy">Year in yyyy format.</param>
		/// <param name="mm">Month in mm format.</param>
		/// <returns>List of CommAccountResponse containing the reponse data.</returns>
		public List<CommAccountResponse> CommAccounts(Var variable, string yyyy, string mm = "")
		{
			string Var = VarStr[(int)variable];
			string Run = yyyy + mm;
			return this.CommReport<CommAccountResponse>("Accounts", Var, Run);
		}
		#endregion

		#region CommAgencies
		/// <summary>
		/// <para>Commission report by Agencies</para>
		/// Execute the "CommAgencies" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommAgencies/
		/// </summary>
		/// <param name="variable">The variable to report on.</param>
		/// <param name="run">Which run report on.</param>
		/// <returns>List of CommAgencyResponse containing the results.</returns>
		public List<CommAgencyResponse> CommAgencies(Var variable, Run run)
		{
			return this.CommReport<CommAgencyResponse>("Agencies", variable, run);
		}
		/// <summary>
		///   <para>Commission report by Agencies for a specific month and year</para>
		/// Execute the "CommAgencies" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommAgencies/
		/// </summary>
		/// <param name="variable">The variable to report on.</param>
		/// <param name="yyyy">Year in yyyy format.</param>
		/// <param name="mm">Month in mm format.</param>
		/// <returns>List of CommAgencyResponse containing the results.</returns>
		public List<CommAgencyResponse> CommAgencies(Var variable, string yyyy, string mm = "")
		{
			string Var = VarStr[(int)variable];
			string Run = yyyy + mm;
			return this.CommReport<CommAgencyResponse>("Agencies", Var, Run);
		}
		#endregion

		#region CommAgency
		/// <summary>
		///   <para>Get a commission metric report for an Agency for all commission months.</para>
		/// Execute the "CommAgency" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommAgency/
		/// </summary>
		/// <param name="AgencyID">The agency identifier.</param>
		/// <param name="variable">The variable to report on.</param>
		/// <returns>CommAgencyReport containing the response data.</returns>
		public CommAgencyReport CommAgency(int AgencyID, Var variable)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.AgencyID = AgencyID;
			apiParameters.Var = VarStr[(int)variable];

			return this.sendRequest<CommAgencyReport>("CommAgency", apiParameters);
		}
		#endregion

		#region CommCustomers
		/// <summary>
		///   <para>Get a commission metric report for an Agency for all commission months.</para>
		/// Execute the "CommAgency" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommCustomers/
		/// </summary>
		/// <param name="variable">The variable to report on.</param>
		/// <param name="run">Which run report on.</param>
		/// <returns>List of CommCustomerResponse containing the results.</returns>
		public List<CommCustomerResponse> CommCustomers(Var variable, Run run)
		{
			return this.CommReport<CommCustomerResponse>("Customers", variable, run);
		}

		/// <summary>
		///   <para>Get a commission metric report for an Agency for all commission months.</para>
		/// Execute the "CommAgency" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommCustomers/
		/// </summary>
		/// <param name="variable">The variable to report on.</param>
		/// <param name="yyyy">Year in yyyy format.</param>
		/// <param name="mm">Month in mm format.</param>
		/// <returns>List of CommCustomerResponse containing the results.</returns>
		public List<CommCustomerResponse> CommCustomers(Var variable, string yyyy, string mm = "")
		{
			string Var = VarStr[(int)variable];
			string Run = yyyy + mm;
			return this.CommReport<CommCustomerResponse>("Customers", Var, Run);
		}
		#endregion

		#region CommItem
		public enum CommItemType { Item, Split };
		private string[] CommItemTypeStr = {"n", "s" }; 
		public CommItemResponse CommItem(CommItemType type, int ItemID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.itemID = String.Format("{0}{1}", this.CommItemTypeStr[(int)type], ItemID);
			return this.sendRequest<CommItemResponse>("CommItem", apiParameters);
		}
		#endregion

		#region CommPayments
		/// <summary>
		///   <para>Get a list of agency payout amounts for a commission run.</para>
		/// Execute the "CommPayments" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/CommPayments/
		/// </summary>
		/// <param name="yyyymm">The Run to Report on in yyyymm format.</param>
		/// <returns>List of CommAgencyPayment with the results</returns>
		public List<CommAgencyPayment> CommPayments(string yyyymm)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.CommRun = yyyymm;
			return this.CommPayments(apiParameters);
		}
		private List<CommAgencyPayment> CommPayments(dynamic apiParameters)
		{
			Dictionary<String, List<CommAgencyPayment>> response =
				this.sendRequest<Dictionary<String, List<CommAgencyPayment>>>("CommPayments", apiParameters);
			return response["Agencies"];
		}
		#endregion

		#region TODO:CommRep
		#endregion

		#region TODO:CommReps
		#endregion
		#endregion

		#region Customer
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
		#endregion

		#region CustomerAdd
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
		#endregion

		#region CustomerContactAdd
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
		#endregion

		#region CustomerContactEdit
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
		#endregion

		#region TODO:CustomerEdit
		#endregion

		#region CustomerLocationAdd
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
		#endregion

		#region CustomerLocationEdit
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
		#endregion

		#region Customers
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
		#endregion

		#region CustomerUpdate
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
		#endregion

		#region TODO:GetClassDefinition
		#endregion

		#region Info
		/// <summary>
		/// Execute the "Info" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/info/
		/// </summary>
		/// <returns>InfoResponse containing the response data.</returns>
		public InfoResponse Info()
		{
            InfoResponse result = this.sendRequest<InfoResponse>("Info");
            return result;
        }
		#endregion

		#region ProcActionsDue

		/// <summary>
		/// <para>Provides, for each Staff Users with actions due, a count of actions due per process.</para>
		/// Execute the "ProcActionsDue" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcActionsDue/
		/// </summary>
		/// <returns>List<ProcActionDueResponse> with the information.</returns>
		public List<ProcActionDueResponse> ProcActionsDue()
		{
			Dictionary<string, List<ProcActionDueResponse>> response =
				this.sendRequest<Dictionary<string, List<ProcActionDueResponse>>>("ProcActionsDue");
			return response["Procs"];
		}
		#endregion

		#region ProcForm
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
			return this.ProcForm(parameters);
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
		#endregion

		#region ProcFormAdd
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
		#endregion

		#region ProcFormEdit
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
		#endregion

		#region ProcFormNoteAdd
		/// <summary>
		/// <para>Add a notes to a Form (Process Name + Form Number)</para>
		/// Execute the "ProcFormNoteAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormNoteAdd/
		/// </summary>
		/// <param name="ProcessName">Name of the process.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <param name="Note">The note.</param>
		/// <param name="NoteForStaff">The note for staff.</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
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

		/// <summary>
		/// <para>Add a notes to a Form (Process ID + Form Number)</para>
		/// Execute the "ProcFormNoteAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormNoteAdd/
		/// </summary>
		/// <param name="ProcessID">The process identifier.</param>
		/// <param name="FormNumber">The form number.</param>
		/// <param name="Note">The note.</param>
		/// <param name="NoteForStaff">The note for staff.</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
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

		/// <summary>
		///   <para>Add a notes to a Form (Form ID)</para>
		/// Execute the "ProcFormNoteAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormNoteAdd/
		/// </summary>
		/// <param name="FormID">The form identifier.</param>
		/// <param name="Note">The note.</param>
		/// <param name="NoteForStaff">The note for staff.</param>
		/// <returns>ProcFormResponseWrapper witht the updated Form Information</returns>
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
		#endregion

		#region ProcFormParticipantAdd
		#region ProcFormParticipantAdd //FormID
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

		#region ProcFormParticipantAdd //ProcessID + FormNumber
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

		#region ProcFormParticipantAdd //ProcessName + FormNumber
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
		#endregion

		#region ProcForms
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
		/// Execute the "ProcForms" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcForms/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns></returns>
		private ProcFormsResponse ProcForms(dynamic apiParameters)
		{
			return this.sendRequest<ProcFormsResponse>("ProcForms", apiParameters);
		}
		#endregion

		#region ProcFormSetAdd
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
		#endregion

		#region TODO:ProcFormUpp
		private void ProcFormUpp(dynamic apiParameters)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region TODO:ProcFormUppUpdate
		private void ProcFormUppUpdate(dynamic apiParameters)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region TODO:ProcFormWellDataUpdate
		private void ProcFormWellDataUpdate(dynamic apiParameters)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region ProcFormWorksheet
		/// <summary>
		/// <para>Get a Worksheet's information</para>
		/// Execute the "ProcFormWorksheet" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormWorksheet/
		/// </summary>
		/// <param name="WorksheetID">The worksheet identifier.</param>
		/// <returns>WorksheetResponse with the Worksheet's information</returns>
		public WorksheetResponse ProcFormWorksheet(int WorksheetID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.WorksheetID = WorksheetID;
			return this.ProcFormWorksheet(apiParameters);
		}

		private WorksheetResponse ProcFormWorksheet(dynamic apiParameters)
		{
			Dictionary<string, WorksheetResponse> response =
				this.sendRequest<Dictionary<string, WorksheetResponse>>("ProcFormWorksheet", apiParameters);
			return response["Worksheet"];
		}
		#endregion

		#region ProcFormWorksheetAdd

		/// <summary>
		///   <para>Add a worksheet to a Form by copying an existing Worksheet.</para>
		/// Execute the "ProcFormWorksheet" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormWorksheetAdd/
		/// </summary>
		/// <param name="FormID">The form identifier.</param>
		/// <param name="WorksheetID">The worksheet identifier.</param>
		/// <returns>WorksheetResponse with the new Worksheet's information.</returns>
		public WorksheetResponse ProcFormWorksheetAdd(int FormID, int WorksheetID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.FormID = FormID;
			apiParameters.WorksheetID = WorksheetID;
			return this.ProcFormWorksheetAdd(apiParameters);
		}
		/// <summary>
		/// <para>Add a worksheet to a Form by copying an existing Worksheet.</para>
		/// Execute the "ProcFormWorksheet" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormWorksheetAdd/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>WorksheetResponse with the new Worksheet's information.</returns>
		private WorksheetResponse ProcFormWorksheetAdd(dynamic apiParameters)
		{
			Dictionary<String, WorksheetResponse> response = 
				this.sendRequest<Dictionary<String, WorksheetResponse>>("ProcFormWorksheetAdd", apiParameters);
			return response["Worksheet"];
		}
		#endregion

		#region ProcFormWorksheetTableAdd
		/// <summary>
		/// <para>Adds a Table to a Worksheet by copying an existent Table.</para>
		/// Execute the "ProcFormWorksheetTableAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormWorksheetTableAdd/
		/// </summary>
		/// <param name="WorksheetID">The worksheet identifier.</param>
		/// <param name="TableID">The table identifier.</param>
		/// <returns>WorksheetResponse with entire worksheet information including the new table</returns>
		public WorksheetResponse ProcFormWorksheetTableAdd(int WorksheetID, int TableID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.WorksheetID = WorksheetID;
			apiParameters.TableID = TableID;
			return this.ProcFormWorksheetTableAdd(apiParameters);
		}
		/// <summary>
		/// <para>Adds a Table to a Worksheet by copying an existent Table.</para>
		/// Execute the "ProcFormWorksheetTableAdd" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormWorksheetTableAdd/
		/// </summary>
		/// <param name="apiParameters">The API parameters.</param>
		/// <returns>WorksheetResponse with entire worksheet information including the new table</returns>
		private WorksheetResponse ProcFormWorksheetTableAdd(dynamic apiParameters)
		{
			Dictionary<String, WorksheetResponse> response =
				this.sendRequest<Dictionary<String, WorksheetResponse>>("ProcFormWorksheetTableAdd", apiParameters);
			return response["Worksheet"];
		}
		#endregion

		#region TODO:ProcRemindersEval
		public void ProcRemindersEval()
		{
			throw new NotImplementedException("ProcRemindersEval not implemented");
		}
		#endregion

		#region ProcFormWorksheetTableDataEdit
		/// <summary>
		/// <para>Add/edit the data inside a Table.</para>
		/// Execute the "ProcFormWorksheetTableDataEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/ProcFormWorksheetTableDataEdit/
		/// </summary>
		/// <param name="TableInfo">The table information.</param>
		/// <param name="RowOverwrite">if set to <c>false</c> the data will be inserted at the end of the table, true will replace the specific row’s data.</param>
		/// <returns>WorksheetResponse containing the entire Worksheet information including the newly added or edited data.</returns>
		public WorksheetResponse ProcFormWorksheetTableDataEdit(WorksheetTable TableInfo, bool RowOverwrite)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.TableID = TableInfo.ID;
			apiParameters.Data = TableInfo.Data;
			apiParameters.RowOverwrite = RowOverwrite;
			return this.ProcFormWorksheetTableDataEdit(apiParameters);
		}

		private WorksheetResponse ProcFormWorksheetTableDataEdit(dynamic apiParameters)
		{
			Dictionary<String, WorksheetResponse> response = this.sendRequest<Dictionary<String, WorksheetResponse>>("ProcFormWorksheetTableDataEdit", apiParameters);
			return response["Worksheet"];
		}
		#endregion

		#region Procs
		/// <summary>
		/// Execute the "Procs" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Procs/
		/// </summary>
		/// <returns>List of ProcResponse containing the response data.</returns>
		public List<ProcResponse> Procs()
		{
			Dictionary<String, List<ProcResponse>> response =
				this.sendRequest<Dictionary<String, List<ProcResponse>>>("Procs");

			return response["Procs"];
		}
		#endregion

		#region Rep
		/// <summary>
		/// <para>Get information for one Rep by ID.</para>
		/// Execute the "ProcFormWorksheetTableDataEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Rep/
		/// </summary>
		/// <param name="RepID">The rep identifier.</param>
		/// <returns>RepResponse with the Rep's information</returns>
		public RepResponse Rep(int RepID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.RepID = RepID;
			return this.Rep(apiParameters);
		}
		/// <summary>
		/// <para>Get information for one Rep Rep Name + Agency ID.</para>
		/// Execute the "ProcFormWorksheetTableDataEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Rep/
		/// </summary>
		/// <param name="Rep">The rep name.</param>
		/// <param name="AgencyID">The agency identifier.</param>
		/// <returns>RepResponse with the Rep's information</returns>
		public RepResponse RepByName(string Rep, int AgencyID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Rep = Rep;
			apiParameters.AgencyID = AgencyID;
			return this.Rep(apiParameters);
		}
		/// <summary>
		/// <para>Get information for one Rep Name + Agency Name</para>
		/// Execute the "ProcFormWorksheetTableDataEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Rep/
		/// </summary>
		/// <param name="Rep">The rep name.</param>
		/// <param name="Agency">The agency name.</param>
		/// <returns>RepResponse with the Rep's information</returns>
		public RepResponse RepByName(string Rep, string Agency)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Rep = Rep;
			apiParameters.Agency = Agency;
			return this.Rep(apiParameters);
		}
		/// <summary>
		/// <para>Get information for one Rep by Assignment Code + Supplier ID.</para>
		/// Execute the "ProcFormWorksheetTableDataEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Rep/
		/// </summary>
		/// <param name="AssignmentCode">The assignment code.</param>
		/// <param name="SupplierID">The supplier identifier.</param>
		/// <returns>RepResponse with the Rep's information</returns>
		public RepResponse RepByAssignmentCode(int AssignmentCode, int SupplierID)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.AssignmentCode = AssignmentCode;
			apiParameters.SupplierID = SupplierID;
			return this.Rep(apiParameters);
		}
		/// <summary>
		/// <para>Get information for one Rep by Assignment Code + Supplier Name.</para>
		/// Execute the "ProcFormWorksheetTableDataEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/Rep/
		/// </summary>
		/// <param name="AssignmentCode">The assignment code.</param>
		/// <param name="Supplier">The supplier name.</param>
		/// <returns>RepResponse with the Rep's information</returns>
		public RepResponse RepByAssignmentCode(int AssignmentCode, string Supplier)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.AssignmentCode = AssignmentCode;
			apiParameters.Supplier = Supplier;
			return this.Rep(apiParameters);
		}
		private RepResponse Rep(dynamic apiParameters)
		{
			return this.sendRequest<RepResponse>("Rep", apiParameters);
		}
		#endregion

		#region TODO:ServiceUsers
		public void ServiceUsers()
		{
			throw new NotImplementedException("ServiceUsers not Implemented");
		}
		#endregion

		#region TODO:SpectatorLogAdd
		#endregion
		#region TODO:SpectatorLogClose
		#endregion
		#region TODO:SpectatorLogOpen
		#endregion
		#region TODO:SpectatorLogUpdate
		#endregion

		#region Suppliers
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
		#endregion

		#region TODO:UPPUsers
		#endregion

		#region User
		#endregion

		#region UserPasswordEdit
		/// <summary>
		/// <para>Change user's password</para>
		/// Execute the "UserPasswordEdit" API endpoint.
		/// http://rpmsoftware.wordpress.com/api/UserPasswordEdit/
		/// </summary>
		/// <param name="Username">The username.</param>
		/// <param name="Password">The password.</param>
		/// <returns>Username provided</returns>
		public string UserPasswordEdit(string Username, string Password)
		{
			dynamic apiParameters = this.apiParameters();
			apiParameters.Username = Username;
			apiParameters.Password = Password;
			Dictionary<string, string> response = this.sendRequest<Dictionary<string, string>>("UserPasswordEdit", apiParameters);
			return response["Username"];
		}
		#endregion

		#region Helper Methods // Non API
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
		private T sendRequest<T>(string endpoint, dynamic apiParameters = null, Func<string, string> beforeDeserialize = null)
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
			if (beforeDeserialize != null)
			{
				response.Content = beforeDeserialize(response.Content);
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
		#endregion
	}
}

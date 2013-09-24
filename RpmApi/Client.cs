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

		public Client(string apiURL, string apiKey)
        {
            this.url = apiURL;
            if (!this.url.EndsWith("/"))
            {
                this.url += "/";
            }
            this.client = new RestClient(this.url + "Api2.svc");
            this.key = apiKey;
        }

		public AccountResponse Account(string Supplier = null, int SupplierID = 0, string Account = null, int AccountID = 0)
		{
			dynamic apiParameters = this.apiParameters();
			if (Supplier != null)
			{
				apiParameters.Supplier = Supplier;
			}
			if (SupplierID > 0)
			{
				apiParameters.SupplierID = SupplierID;
			}
			if (Account != null)
			{
				apiParameters.Account = Account;
			}
			if (AccountID > 0)
			{
				apiParameters.AccountID = AccountID;
			}
			return this.sendRequest<AccountResponse>("Account", apiParameters);
		}

		public List<AccountResponse> Accounts(string Supplier = null, int SupplierID = 0, string Customer = null, int CustomerID = 0)
		{
			dynamic apiParameters = this.apiParameters();
			if (Supplier != null)
			{
				apiParameters.Supplier = Supplier;
			}
			if (SupplierID > 0)
			{
				apiParameters.SupplierID = SupplierID;
			}
			if (Customer != null)
			{
				apiParameters.Account = Customer;
			}
			if (CustomerID > 0)
			{
				apiParameters.AccountID = CustomerID;
			}

			Dictionary<String, List<AccountResponse>> response = 
				this.sendRequest<Dictionary<String, List<AccountResponse>>>("Accounts", apiParameters);
			return response["Accounts"];
		}

		public AgenciesResponse Agencies()
		{
			dynamic apiParameters = this.apiParameters();
			return this.sendRequest<AgenciesResponse>("Agencies", apiParameters);
		}

		public AgencyResponse Agency(string Agency = null, int AgencyID = 0)
		{
			dynamic apiParameters = this.apiParameters();
			if (Agency != null)
			{
				apiParameters.Agency = Agency;	
			}
			if (AgencyID > 0)
			{
				apiParameters.AgencyID = AgencyID;
			}
			return this.sendRequest<AgencyResponse>("Agency", apiParameters);
		}

        public InfoResult info() {
            InfoResult result = this.sendRequest<InfoResult>("info");
            return result;
        }

        public ProcsResult Procs()
        {
            return this.sendRequest<ProcsResult>("Procs");
        }

        public ProcFormsResult ProcForms(int ProcessID = 0, string ProcessName = null, int ViewID = 0)
        {
            dynamic apiParameters = this.apiParameters();
            if (ProcessID > 0)
            {
                apiParameters.ProcessID = ProcessID;
            }
            if (ProcessName != null)
            {
                apiParameters.ProcessName = ProcessName;
            }
            if (ViewID > 0)
            {
                apiParameters.ViewID = ViewID;
            }
            return this.sendRequest<ProcFormsResult>("ProcForms", apiParameters);
        }

        internal ProcFormData ProcFormAdd(int processID, dynamic formData)
        {
            dynamic parameters = this.apiParameters();
            parameters.ProcessID = processID;
            parameters.Form = formData;
            return this.sendRequest<ProcFormData>("ProcFormAdd", parameters);
        }

        internal ProcFormData ProcForm(int processID = 0, string processName = "", int formNumber = 0, int formID = 0)
        {
            dynamic parameters = this.apiParameters();
            if (processID > 0)
            {
                parameters.ProcessID = processID;
            }
            if (processName != "")
            {
                parameters.ProcessName = processName;
            }
            if (formID > 0)
            {
                parameters.FormID = formID;
            }
            parameters.FormNumber = formNumber;
            return this.sendRequest<ProcFormData>("ProcForm", parameters);
        }

        internal ProcFormData ProcFormEdit(int processID = 0, string processName = "", dynamic formData = null)
        {
            dynamic parameters = this.apiParameters();
            if (processID > 0)
            {
                parameters.ProcessID = processID;
            }
            if (processName != "")
            {
                parameters.ProcessName = processName;
            }
            parameters.Form = formData;
            return this.sendRequest<ProcFormData>("ProcFormEdit", parameters);
        }

		public List<SupplierResponse> Suppliers()
		{
			Dictionary<String, List<SupplierResponse>> response =
				this.sendRequest<Dictionary<String, List<SupplierResponse>>>("Suppliers");
			return response["Suppliers"];
		}

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

        private dynamic apiParameters()
        {
            dynamic apiParameters = new ExpandoObject();
            apiParameters.Key = this.key;
            return apiParameters;
        }
    }
}

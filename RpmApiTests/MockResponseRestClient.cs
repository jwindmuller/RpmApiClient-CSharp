using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

using RestSharp;

namespace RpmApiTests
{
	class MockResponseRestClient : RestClient
	{

		Dictionary<string, string> fakeResponses;

		public MockResponseRestClient(string baseUrl) : base(baseUrl)
		{
			this.fakeResponses = new Dictionary<string, string>();
			this.fakeResponses.Add(
				"CustomerAdd@{\"Key\":\"\",\"Customer\":{\"Accounts\":[],\"Added\":\"0001-01-01T07:00:00Z\",\"Contacts\":[],\"CustomerID\":0,\"Fields\":[],\"Modified\":\"0001-01-01T07:00:00Z\",\"Name\":\"Tester\",\"Notes\":[],\"NotesForStaff\":[],\"Website\":\"\"}}",
				"{\"Result\":{\"CustomerID\":77777777,\"Name\":\"Tester\",\"Aliases\":[],\"Website\":\"\",\"Added\":\"2013-10-02\",\"Modified\":\"2013-10-02\",\"Locations\":[{\"IsPrimary\":true,\"LocationID\":7,\"Name\":\"Primary\",\"Address\":\"\",\"City\":\"\",\"StateProvince\":\"n/a\",\"Country\":\"Canada\",\"ZipPostalCode\":\"\"}],\"Contacts\":[],\"Fields\":[{\"Field\":\"quantity 1\",\"Value\":\"\"}],\"Accounts\":[],\"Notes\":[],\"NotesForStaff\":[]}}"
			);
			this.fakeResponses.Add(
				"CustomerContactAdd@{\"Key\":\"\",\"CustomerID\":77777777,\"Contact\":{\"ContactID\":0,\"Email\":\"\",\"FirstName\":\"Contact\",\"LastName\":\"Contactson\",\"PhoneNumbers\":[{\"Number\":\"555-0035\",\"PhoneNumberID\":0,\"Type\":1}],\"Salutation\":\"Mr.\",\"Title\":\"Title\"},\"IsPrimary\":true}",
				"{\"Result\":{\"Contact\":{\"ContactID\":130349,\"Salutation\":\"Mr.\",\"FirstName\":\"Contact\",\"LastName\":\"Contactson\",\"Title\":\"Title\",\"Email\":\"\",\"PhoneNumbers\":[{\"PhoneNumberID\":312754,\"Type\":1,\"Number\":\"555-0035\"},{\"PhoneNumberID\":0,\"Type\":2,\"Number\":\"\"},{\"PhoneNumberID\":0,\"Type\":3,\"Number\":\"\"},{\"PhoneNumberID\":0,\"Type\":6,\"Number\":\"\"}]}}}"
			);
			this.fakeResponses.Add(
				"CustomerContactAdd@{\"Key\":\"\",\"CustomerID\":77777777,\"Contact\":{\"ContactID\":0,\"Email\":\"\",\"FirstName\":\"Contact\",\"LastName\":\"Contactson\",\"PhoneNumbers\":[{\"Number\":\"none\",\"PhoneNumberID\":0,\"Type\":1}],\"Salutation\":\"Mr.\",\"Title\":\"Title\"},\"IsPrimary\":true}",
				"{\"Result\":{\"Contact\":{\"ContactID\":130380,\"Salutation\":\"Mr.\",\"FirstName\":\"Contact\",\"LastName\":\"Contactson\",\"Title\":\"Title\",\"Email\":\"\",\"PhoneNumbers\":[{\"PhoneNumberID\":312792,\"Type\":1,\"Number\":\"none\"},{\"PhoneNumberID\":0,\"Type\":2,\"Number\":\"\"},{\"PhoneNumberID\":0,\"Type\":3,\"Number\":\"\"},{\"PhoneNumberID\":0,\"Type\":6,\"Number\":\"\"}]}}}"
			);
		}

		public override IRestResponse Execute(IRestRequest request)
		{
			string resource = request.Resource;
			object data = SimpleJson.DeserializeObject<Object>((string)request.Parameters[0].Value);

			Regex removeKey = new Regex("\"Key\":\"[0-9a-z-]+\"");
			string parameters = (string)request.Parameters[0].Value;
			parameters = removeKey.Replace(parameters, "\"Key\":\"\"");
			var mockDataKey = String.Format("{0}@{1}", resource, parameters);
			if (this.fakeResponses.ContainsKey(mockDataKey))
			{
				string content = this.fakeResponses[mockDataKey];
				RestResponse response = new RestResponse();
				response.Content = content;
				response.StatusDescription = "OK";
				return response;
			}
			else
			{
				Console.Out.WriteLine(mockDataKey);
				IRestResponse response = base.Execute(request);
				System.Diagnostics.Debug.Write(response.Content);

				return response;
			}
		}
	}
}

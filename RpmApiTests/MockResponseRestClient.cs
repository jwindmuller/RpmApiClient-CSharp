using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				"CustomerAdd@{\"Key\":\"682f4d56-f155-4f28-a149-35493cb461a5\",\"Customer\":{\"Accounts\":[],\"Added\":\"0001-01-01T07:00:00Z\",\"Contacts\":[],\"CustomerID\":0,\"Fields\":[],\"Modified\":\"0001-01-01T07:00:00Z\",\"Name\":\"Tester\",\"Notes\":[],\"NotesForStaff\":[],\"Website\":\"\"}}",
				"{\"Result\":{\"CustomerID\":777777777,\"Name\":\"Tester\",\"Aliases\":[],\"Website\":\"\",\"Added\":\"2013-10-02\",\"Modified\":\"2013-10-02\",\"Locations\":[{\"IsPrimary\":true,\"LocationID\":7,\"Name\":\"Primary\",\"Address\":\"\",\"City\":\"\",\"StateProvince\":\"n/a\",\"Country\":\"Canada\",\"ZipPostalCode\":\"\"}],\"Contacts\":[],\"Fields\":[{\"Field\":\"quantity 1\",\"Value\":\"\"}],\"Accounts\":[],\"Notes\":[],\"NotesForStaff\":[]}}"
			);

		}

		public override IRestResponse Execute(IRestRequest request)
		{
			string resource = request.Resource;
			object data = SimpleJson.DeserializeObject<Object>((string)request.Parameters[0].Value);

			var mockDataKey = String.Format("{0}@{1}", resource, request.Parameters[0].Value);
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

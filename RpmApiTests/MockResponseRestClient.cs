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
				"CustomerAdd@{\"Key\":\"\",\"Customer\":{\"Accounts\":[],\"Added\":\"0001-01-01T07:00:00Z\",\"Contacts\":[],\"CustomerID\":0,\"Fields\":[],\"Locations\":[],\"Modified\":\"0001-01-01T07:00:00Z\",\"Name\":\"Tester\",\"Customer\":\"Tester\",\"Notes\":[],\"NotesForStaff\":[],\"Website\":\"\"}}",
				"{\"Result\":{\"CustomerID\":77777777,\"Name\":\"Tester\",\"Aliases\":[],\"Website\":\"\",\"Added\":\"2013-10-02\",\"Modified\":\"2013-10-02\",\"Locations\":[{\"IsPrimary\":true,\"LocationID\":7,\"Name\":\"Primary\",\"Address\":\"\",\"City\":\"\",\"StateProvince\":\"n/a\",\"Country\":\"Canada\",\"ZipPostalCode\":\"\"}],\"Contacts\":[],\"Fields\":[{\"Field\":\"quantity 1\",\"Value\":\"\"}],\"Accounts\":[],\"Notes\":[],\"NotesForStaff\":[]}}"
			);
			this.fakeResponses.Add(
				"CustomerContactAdd@{\"Key\":\"\",\"CustomerID\":77777777,\"Contact\":{\"ContactID\":0,\"Email\":\"\",\"FirstName\":\"Contact\",\"LastName\":\"Contactson\",\"PhoneNumbers\":[{\"Number\":\"555-0035\",\"PhoneNumberID\":0,\"Type\":1}],\"Salutation\":\"Mr.\",\"Title\":\"Title\"},\"IsPrimary\":true}",
				"{\"Result\":{\"Contact\":{\"ContactID\":130349,\"Salutation\":\"Mr.\",\"FirstName\":\"Contact\",\"LastName\":\"Contactson\",\"Title\":\"Title\",\"Email\":\"\",\"PhoneNumbers\":[{\"PhoneNumberID\":312754,\"Type\":1,\"Number\":\"555-0035\"},{\"PhoneNumberID\":0,\"Type\":2,\"Number\":\"\"},{\"PhoneNumberID\":0,\"Type\":3,\"Number\":\"\"},{\"PhoneNumberID\":0,\"Type\":6,\"Number\":\"\"}]}}}"
			);
			this.fakeResponses.Add(
				"CustomerContactAdd@{\"Key\":\"\",\"CustomerID\":77777777,\"Contact\":{\"ContactID\":0,\"Email\":\"\",\"FirstName\":\"Contact\",\"LastName\":\"Contactson\",\"PhoneNumbers\":[{\"Number\":\"\",\"PhoneNumberID\":0,\"Type\":1}],\"Salutation\":\"Mr.\",\"Title\":\"Title\"},\"IsPrimary\":true}",
				"{\"Result\":{\"Contact\":{\"ContactID\":130380,\"Salutation\":\"Mr.\",\"FirstName\":\"Contact\",\"LastName\":\"Contactson\",\"Title\":\"Title\",\"Email\":\"\",\"PhoneNumbers\":[{\"PhoneNumberID\":312792,\"Type\":1,\"Number\":\"none\"},{\"PhoneNumberID\":0,\"Type\":2,\"Number\":\"\"},{\"PhoneNumberID\":0,\"Type\":3,\"Number\":\"\"},{\"PhoneNumberID\":0,\"Type\":6,\"Number\":\"\"}]}}}"
			);
			this.fakeResponses.Add(
				"Customer@{\"Key\":\"\",\"CustomerID\":77777777}",
				"{\"Result\":{\"CustomerID\":77777777,\"Name\":\"Customer\",\"Aliases\":[],\"Website\":\"\",\"Added\":\"2013-09-19\",\"Modified\":\"2013-09-20\",\"Locations\":[{\"IsPrimary\":true,\"LocationID\":1,\"Name\":\"Primary\",\"Address\":\"\",\"City\":\"Calgary\",\"StateProvince\":\"n/a\",\"Country\":\"Canada\",\"ZipPostalCode\":\"\"}],\"Contacts\":[{\"IsPrimary\":true,\"Contact\":{\"ContactID\":130387,\"Salutation\":\"\",\"FirstName\":\"ert\",\"LastName\":\"wertert\",\"Title\":\"\",\"Email\":\"\",\"PhoneNumbers\":[{\"PhoneNumberID\":312802,\"Type\":1,\"Number\":\"1-800-0RPM\"},{\"PhoneNumberID\":0,\"Type\":2,\"Number\":\"\"},{\"PhoneNumberID\":312806,\"Type\":3,\"Number\":\"555-FAXS\"},{\"PhoneNumberID\":0,\"Type\":6,\"Number\":\"\"}]}}],\"Fields\":[{\"Field\":\"quantity 1\",\"Value\":\"\"}],\"Accounts\":[{\"Account\":\"Acc\",\"AccountID\":67619,\"Supplier\":\"Acceris\",\"SupplierID\":2}],\"Notes\":[],\"NotesForStaff\":[]}}"
			);
			this.fakeResponses.Add(
				"CustomerContactEdit@{\"Key\":\"\",\"CustomerID\":77777777,\"Contact\":{\"ContactID\":130387,\"Email\":\"\",\"FirstName\":\"Name\",\"LastName\":\"Last\",\"PhoneNumbers\":[{\"Number\":\"1-800-1RPM\",\"PhoneNumberID\":312802,\"Type\":1},{\"Number\":\"none\",\"PhoneNumberID\":312806,\"Type\":3}],\"Salutation\":\"\",\"Title\":\"\"},\"IsPrimary\":false}",
				"{\"Result\":{\"Contact\":{\"ContactID\":130387,\"Salutation\":\"\",\"FirstName\":\"Name\",\"LastName\":\"Last\",\"Title\":\"\",\"Email\":\"\",\"PhoneNumbers\":[{\"PhoneNumberID\":312802,\"Type\":1,\"Number\":\"1-800-1RPM\"},{\"PhoneNumberID\":0,\"Type\":2,\"Number\":\"\"},{\"PhoneNumberID\":312806,\"Type\":3,\"Number\":\"none\"},{\"PhoneNumberID\":0,\"Type\":6,\"Number\":\"\"}]}}}"
			);
			this.fakeResponses.Add(
				"CustomerLocationAdd@{\"Key\":\"\",\"CustomerID\":77777777,\"Location\":{\"CustomerLocationID\":0,\"IsPrimary\":false,\"LocationID\":0,\"Name\":\"Home Office\",\"Address\":\"205 - 5th Avenue SW\",\"City\":\"Calgary\",\"StateProvince\":\"Alberta\",\"Country\":\"Canada\",\"ZipPostalCode\":\"T2P 2V7\"}}",
				"{\"Result\":{\"CustomerLocationID\":13}}"
			);
			this.fakeResponses.Add(
				"CustomerLocationEdit@{\"Key\":\"\",\"CustomerID\":77777777,\"Location\":{\"CustomerLocationID\":0,\"IsPrimary\":true,\"LocationID\":1,\"Name\":\"Same Location, new name\",\"Address\":\"\",\"City\":\"Calgary\",\"StateProvince\":\"n/a\",\"Country\":\"Canada\",\"ZipPostalCode\":\"\"}}",
				"{\"Result\":{\"CustomerLocationID\":1}}"
			);
			this.fakeResponses.Add(
				"CustomerUpdate@{\"Key\":\"\",\"Customer\":{\"ID\":77777777,\"Name\":\"Customer\",\"Website\":\"joe\",\"Address\":\"\",\"City\":\"Calgary\",\"StateProvince\":\"n/a\",\"Country\":\"Canada\",\"ZipPostalCode\":\"\",\"Fields\":[{\"Field\":\"quantity 1\",\"Value\":\"\"}]}}",
				"{\"Result\":{\"CustomerID\":77777777,\"Name\":\"Joaquin Customer\",\"Aliases\":[],\"Website\":\"joe\",\"Added\":\"2013-09-19\",\"Modified\":\"2013-10-10\",\"Locations\":[{\"IsPrimary\":true,\"LocationID\":1,\"Name\":\"Same Location, new name\",\"Address\":\"\",\"City\":\"Calgary\",\"StateProvince\":\"n/a\",\"Country\":\"Canada\",\"ZipPostalCode\":\"\"},{\"IsPrimary\":false,\"LocationID\":14,\"Name\":\"Home Office\",\"Address\":\"205 - 5th Avenue SW\",\"City\":\"Calgary\",\"StateProvince\":\"Alberta\",\"Country\":\"Canada\",\"ZipPostalCode\":\"T2P 2V7\"}],\"Contacts\":[{\"IsPrimary\":true,\"Contact\":{\"ContactID\":130387,\"Salutation\":\"\",\"FirstName\":\"ert\",\"LastName\":\"wertert\",\"Title\":\"\",\"Email\":\"\",\"PhoneNumbers\":[{\"PhoneNumberID\":312802,\"Type\":1,\"Number\":\"1-800-1RPM\"},{\"PhoneNumberID\":0,\"Type\":2,\"Number\":\"\"},{\"PhoneNumberID\":312806,\"Type\":3,\"Number\":\"none\"},{\"PhoneNumberID\":0,\"Type\":6,\"Number\":\"\"}]}}],\"Fields\":[{\"Field\":\"quantity 1\",\"Value\":\"\"}],\"Accounts\":[{\"Account\":\"Acc\",\"AccountID\":67619,\"Supplier\":\"Acceris\",\"SupplierID\":2}],\"Notes\":[],\"NotesForStaff\":[]}}"
			);
			this.fakeResponses.Add(
				"ProcActionsDue@{\"Key\":\"\"}",
				"{\"Result\":{\"Procs\":[{\"Staff\":\"gfhgfh gfhgf\",\"StaffID\":5,\"Due\":[{\"Process\":\"WS T1\",\"ProcessID\":25,\"ActionsDueNormal\":1,\"ActionsDueHigh\":0,\"ActionsDue\":1}]},{\"Staff\":\"RPM Support\",\"StaffID\":1,\"Due\":[{\"Process\":\"cosa\",\"ProcessID\":9,\"ActionsDueNormal\":1,\"ActionsDueHigh\":0,\"ActionsDue\":1},{\"Process\":\"WS T1\",\"ProcessID\":25,\"ActionsDueNormal\":1,\"ActionsDueHigh\":0,\"ActionsDue\":1}]}]}}"
			);
			this.fakeResponses.Add(
				"ProcForm@{\"Key\":\"\",\"FormID\":77777777}",
				"{\"Result\":{\"Process\":\"Rep\",\"ProcessID\":77777777,\"Form\":{\"Number\":\"0008\",\"FormID\":77777777,\"Owner\":\"RPM Support\",\"Status\":\"New\",\"ApprovalResult\":\"\",\"Started\":\"2013-10-15\",\"Modified\":\"2013-10-15\",\"Fields\":[{\"Field\":\"Non Rep\",\"Value\":\"\"}],\"Worksheets\":[],\"Sets\":[{\"FormID\":77777777,\"Fields\":[{\"Field\":\"Field1\",\"Value\":\"\"}]}],\"Participants\":[{\"Name\":\"RPM Support\",\"Role\":\"System Manager\",\"Read\":\"2013-10-15\"}],\"Actions\":[],\"Notes\":[],\"NotesForStaff\":[]}}}"
			);
			this.fakeResponses.Add(
				"ProcFormSetAdd@{\"Key\":\"\",\"Form\":{\"FormID\":77777777,\"Fields\":[{\"Field\":\"Field1\",\"Value\":\"Value1\"}]}}",
				"{\"Result\":{\"Process\":\"Rep\",\"ProcessID\":77777777,\"Form\":{\"Number\":\"0008\",\"FormID\":77777777,\"Owner\":\"RPM Support\",\"Status\":\"New\",\"ApprovalResult\":\"\",\"Started\":\"2013-10-15\",\"Modified\":\"2013-10-15\",\"Fields\":[{\"Field\":\"Non Rep\",\"Value\":\"\"}],\"Worksheets\":[],\"Sets\":[{\"FormID\":77777777,\"Fields\":[{\"Field\":\"Field1\",\"Value\":\"\"}]},{\"FormID\":77777778,\"Fields\":[{\"Field\":\"Field1\",\"Value\":\"Value1\"}]}],\"Participants\":[{\"Name\":\"RPM Support\",\"Role\":\"System Manager\",\"Read\":\"2013-10-15\"}],\"Actions\":[],\"Notes\":[],\"NotesForStaff\":[]}}}"
			);
			this.fakeResponses.Add(
				"ProcFormNoteAdd@{\"Key\":\"\",\"Form\":{\"FormID\":77777777,\"Note\":\"Note\",\"NoteForStaff\":\"NoteForStaff\"}}",
				"{\"Result\": {\"Form\": {\"Actions\": [],\"ApprovalResult\": \"\",\"Fields\": [{\"Field\": \"Non Rep\",\"Value\": \"\"}],\"FormID\": 77777777,\"Modified\": \"2013-10-15\",\"Notes\": [{\"Added\": \"2013-10-15\",\"By\": \"Joaquin W\",\"Note\": \"Note\"}],\"NotesForStaff\": [{\"Added\": \"2013-10-15\",\"By\": \"Joaquin W\",\"Note\": \"NoteForStaff\"}],\"Number\": \"0008\",\"Owner\": \"RPM Support\",\"Participants\": [{\"Name\": \"RPM Support\",\"Read\": \"2013-10-15\",\"Role\": \"System Manager\"}],\"Sets\": [{\"Fields\": [{\"Field\": \"Field1\",\"Value\": \"\"}],\"FormID\": 77777777}],\"Started\": \"2013-10-15\",\"Status\": \"New\",\"Worksheets\": []},\"Process\": \"Rep\",\"ProcessID\": 77777777}}"
			);
			this.fakeResponses.Add(
				"ProcFormAdd@{\"Key\":\"\",\"ProcessID\":77777777,\"Form\":{\"ProcessID\":0,\"ProcessName\":\"\",\"Number\":\"\",\"Owner\":\"\",\"Participants\":[],\"Status\":\"\",\"ApprovalResult\":\"\",\"Started\":\"0001-01-01T07:00:00Z\",\"Modified\":\"0001-01-01T07:00:00Z\",\"Values\":[],\"Sets\":[],\"Notes\":[],\"NotesForStaff\":[],\"Worksheets\":[],\"FormID\":0,\"Fields\":[{\"Field\":\"Non Rep\",\"Value\":\"The Value\"}]}}",
				"{\"Result\":{\"Process\":\"Rep\",\"ProcessID\":77777777,\"Form\":{\"Number\":\"0006\",\"FormID\":200,\"Owner\":\"Joaquin W\",\"Status\":\"New\",\"ApprovalResult\":\"\",\"Started\":\"2013-10-15\",\"Modified\":\"2013-10-15\",\"Fields\":[{\"Field\":\"Non Rep\",\"Value\":\"The Value\"}],\"Worksheets\":[],\"Sets\":[{\"FormID\":200,\"Fields\":[{\"Field\":\"Field1\",\"Value\":\"\"}]}],\"Participants\":[{\"Name\":\"Joaquin W\",\"Role\":\"System Manager\"}],\"Actions\":[],\"Notes\":[],\"NotesForStaff\":[]}}}"
			);
			this.fakeResponses.Add(
				"ProcFormEdit@{\"Key\":\"\",\"Form\":{\"ProcessID\":0,\"ProcessName\":\"\",\"Number\":\"0008\",\"Owner\":\"RPM Support\",\"Participants\":[],\"Status\":\"New\",\"ApprovalResult\":\"\",\"Started\":\"2013-10-15T06:00:00Z\",\"Modified\":\"2013-10-15T06:00:00Z\",\"Values\":[],\"Sets\":[],\"Notes\":[],\"NotesForStaff\":[],\"Worksheets\":[],\"FormID\":77777777,\"Fields\":[]}}",
				"{\"Result\":{\"Process\":\"Rep\",\"ProcessID\":26,\"Form\":{\"Number\":\"0008\",\"FormID\":77777777,\"Owner\":\"RPM Support\",\"Status\":\"New\",\"ApprovalResult\":\"\",\"Started\":\"2013-10-15\",\"Modified\":\"2013-11-06\",\"Fields\":[{\"Field\":\"Non Rep\",\"Value\":\"Changed!\"}],\"Worksheets\":[],\"Sets\":[{\"FormID\":77777777,\"Fields\":[{\"Field\":\"Field1\",\"Value\":\"\"}]}],\"Participants\":[{\"Name\":\"RPM Support\",\"Role\":\"System Manager\",\"Read\":\"2013-10-15\"}],\"Actions\":[],\"Notes\":[],\"NotesForStaff\":[]}}}"
			);
			this.fakeResponses.Add(
				"ProcFormParticipantAdd@{\"Key\":\"\",\"Form\":{\"FormID\":1},\"AgencyID\":1}",
				"{\"Result\":{\"Process\":\"Joaquin\\u0027s Test\",\"ProcessID\":4,\"Form\":{\"Number\":\"0001\",\"FormID\":1,\"Owner\":\"RPM Support\",\"Status\":\"New\",\"ApprovalResult\":\"\",\"Started\":\"2013-05-24\",\"Modified\":\"2013-11-06\",\"Fields\":[{\"Field\":\"A nice text field \",\"Value\":\"\"},{\"Field\":\"How much money\",\"Value\":\"\"}],\"Worksheets\":[{\"WorksheetID\":2,\"Name\":\"test\"}],\"Sets\":[{\"FormID\":1,\"Fields\":[{\"Field\":\"A repeating field\",\"Value\":\"\"},{\"Field\":\"cash only\",\"Value\":\"\"},{\"Field\":\"Reason For Cash\",\"Value\":\"\"}]}],\"Participants\":[{\"Name\":\"Joaquin Agent\",\"Role\":\"MANAGER!!CAP\"},{\"Name\":\"Joaquin W\",\"Role\":\"System Manager\",\"Read\":\"2013-08-06\"},{\"Name\":\"RPM Support\",\"Role\":\"System Manager\",\"Read\":\"2013-11-06\"}],\"Actions\":[],\"Notes\":[],\"NotesForStaff\":[]}}}"
			);
			this.fakeResponses.Add(
				"ProcFormWorksheet@{\"Key\":\"\",\"WorksheetID\":77777777}",
				"{\"Result\":{\"Worksheet\":{\"ID\":77777777,\"Name\":\"ws1\",\"OwnerType\":520,\"OwnerID\":183,\"Header\":\"\",\"Footer\":\"\",\"WSTemplateID\":\"3\",\"IsEnabled\":true,\"DateAdded\":\"2013-09-30\",\"DateModified\":\"2013-12-11\",\"NumFields\":3,\"NumTables\":1,\"NumChildWorksheets\":0,\"Tables\":[{\"ID\":88888888,\"WSTemplateID\":77777777,\"Name\":\"Table 1\",\"RefName\":\"\",\"IsIncluded\":false,\"CommentName\":\"Comment\",\"Comment\":\"\",\"ShowComment\":true,\"ShowFooter\":true,\"IsEnabled\":true,\"Order\":0,\"RefType\":0,\"RefID\":0,\"RefFixed\":false,\"Columns\":[{\"ID\":37,\"Name\":\"Column 1\",\"RefTypeName\":\"\",\"ColType\":2,\"RefType\":0,\"FixedValue\":\"\",\"IsHidden\":false,\"IsFixed\":false,\"Order\":0},{\"ID\":38,\"Name\":\"Qty.\",\"RefTypeName\":\"\",\"ColType\":4,\"RefType\":0,\"FixedValue\":\"0\",\"IsHidden\":false,\"IsFixed\":false,\"Order\":1},{\"ID\":39,\"Name\":\"Unit Cost\",\"RefTypeName\":\"\",\"ColType\":3,\"RefType\":0,\"FixedValue\":\"0\",\"IsHidden\":false,\"IsFixed\":false,\"Order\":2},{\"ID\":40,\"Name\":\"Total\",\"RefTypeName\":\"\",\"ColType\":6,\"RefType\":0,\"FixedValue\":\"[1]*[2]\",\"IsHidden\":false,\"IsFixed\":true,\"Order\":3}],\"Data\":[{\"Value\":\"Anything\",\"RefName\":\"\",\"Row\":0,\"ColID\":37},{\"Value\":\"10\",\"RefName\":\"\",\"Row\":0,\"ColID\":38},{\"Value\":\"10\",\"RefName\":\"\",\"Row\":0,\"ColID\":39}],\"SuperHeaders\":[{\"Text\":\"Something!\",\"Span\":4,\"Row\":1,\"Start\":0},{\"Text\":\"This is Cool\",\"Span\":2,\"Row\":0,\"Start\":1}]}],\"Fields\":[{\"ID\":58,\"Name\":\"Added\",\"WorksheetID\":77777777,\"ObjectType\":10106,\"ObjectSpecificID\":8,\"Order\":0,\"Column\":0},{\"ID\":57,\"Name\":\"Some text\",\"WorksheetID\":8,\"ObjectType\":500,\"ObjectSpecificID\":885,\"Order\":0,\"Column\":1},{\"ID\":59,\"Name\":\"Modified\",\"WorksheetID\":8,\"ObjectType\":10076,\"ObjectSpecificID\":8,\"Order\":1,\"Column\":0}]}}}"
			);
			this.fakeResponses.Add(
				"ProcFormWorksheetTableDataEdit@{\"Key\":\"\",\"TableID\":88888888,\"Data\":[{\"ColID\":0,\"ID\":0,\"ColIndex\":0,\"RefName\":\"\",\"Row\":0,\"Value\":\"Something\"},{\"ColID\":0,\"ID\":0,\"ColIndex\":1,\"RefName\":\"\",\"Row\":0,\"Value\":\"10\"},{\"ColID\":0,\"ID\":0,\"ColIndex\":2,\"RefName\":\"\",\"Row\":0,\"Value\":\"10\"}],\"RowOverwrite\":true}",
				"{\"Result\":{\"Worksheet\":{\"ID\":77777777,\"Name\":\"ws1\",\"OwnerType\":520,\"OwnerID\":183,\"Header\":\"\",\"Footer\":\"\",\"WSTemplateID\":\"3\",\"IsEnabled\":true,\"DateAdded\":\"2013-09-30\",\"DateModified\":\"2013-12-11\",\"NumFields\":3,\"NumTables\":1,\"NumChildWorksheets\":0,\"Tables\":[{\"ID\":88888888,\"WSTemplateID\":77777777,\"Name\":\"Table 1\",\"RefName\":\"\",\"IsIncluded\":false,\"CommentName\":\"Comment\",\"Comment\":\"\",\"ShowComment\":true,\"ShowFooter\":true,\"IsEnabled\":true,\"Order\":0,\"RefType\":0,\"RefID\":0,\"RefFixed\":false,\"Columns\":[{\"ID\":37,\"Name\":\"Column 1\",\"RefTypeName\":\"\",\"ColType\":2,\"RefType\":0,\"FixedValue\":\"\",\"IsHidden\":false,\"IsFixed\":false,\"Order\":0},{\"ID\":38,\"Name\":\"Qty.\",\"RefTypeName\":\"\",\"ColType\":4,\"RefType\":0,\"FixedValue\":\"0\",\"IsHidden\":false,\"IsFixed\":false,\"Order\":1},{\"ID\":39,\"Name\":\"Unit Cost\",\"RefTypeName\":\"\",\"ColType\":3,\"RefType\":0,\"FixedValue\":\"0\",\"IsHidden\":false,\"IsFixed\":false,\"Order\":2},{\"ID\":40,\"Name\":\"Total\",\"RefTypeName\":\"\",\"ColType\":6,\"RefType\":0,\"FixedValue\":\"[1]*[2]\",\"IsHidden\":false,\"IsFixed\":true,\"Order\":3}],\"Data\":[{\"Value\":\"Something\",\"RefName\":\"\",\"Row\":0,\"ColID\":37},{\"Value\":\"10\",\"RefName\":\"\",\"Row\":0,\"ColID\":38},{\"Value\":\"10\",\"RefName\":\"\",\"Row\":0,\"ColID\":39}],\"SuperHeaders\":[{\"Text\":\"Something!\",\"Span\":4,\"Row\":1,\"Start\":0},{\"Text\":\"This is Cool\",\"Span\":2,\"Row\":0,\"Start\":1}]}],\"Fields\":[{\"ID\":58,\"Name\":\"Added\",\"WorksheetID\":8,\"ObjectType\":10106,\"ObjectSpecificID\":8,\"Order\":0,\"Column\":0},{\"ID\":57,\"Name\":\"Some text\",\"WorksheetID\":8,\"ObjectType\":500,\"ObjectSpecificID\":885,\"Order\":0,\"Column\":1},{\"ID\":59,\"Name\":\"Modified\",\"WorksheetID\":8,\"ObjectType\":10076,\"ObjectSpecificID\":8,\"Order\":1,\"Column\":0}]}}}"
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

			IRestResponse response;
			if (this.fakeResponses.ContainsKey(mockDataKey))
			{
				string content = this.fakeResponses[mockDataKey];
				response = new RestResponse();
				response.Content = content;
				response.StatusDescription = "OK";
			}
			else
			{
				response = base.Execute(request);
			}
			if (System.Diagnostics.Debugger.IsAttached)
			{
				var host = this.BaseUrl.Substring(this.BaseUrl.IndexOf("//") + 2);
				host = host.Substring(0, host.IndexOf("/"));
				string req = string.Format(
					"---\n\nPOST {0} HTTP/1.1\nHost: {1}\n\n{2}\n\n---",
					this.BaseUrl + "/" + resource, host, request.Parameters[0].Value
				);
				System.Diagnostics.Debug.Print(req);
			}
			return response;
		}
	}
}

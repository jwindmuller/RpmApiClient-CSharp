using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ProcFormsResponse
    {
        public string Process { get; set; }
        public int ProcessID { get; set; }
        public string View { get; set; }
        public List<string> Columns { get; set; }

        private List<ProcForm> _Forms { get; set; }
		public List<ProcForm> Forms
		{
			get
			{
				if (_Forms == null)
				{
					_Forms = new List<ProcForm>();
				}
				return _Forms;
			}
			set
			{
				_Forms = value;
				// Move the data from the Form.Fields into their actual properties
				if (value != null)
				{
					this.initializeForms();	
				}
				
			}
		}

		private void initializeForms()
		{
			foreach (ProcForm item in this.Forms)
			{
				this.initializeBasicFormFields(item);
			}
		}

		private void initializeBasicFormFields(ProcForm form)
		{
			form.Number = this.initializeFormField<string>(form, "Number");
			form.Owner = this.initializeFormField<string>(form, "Owner");
			form.Status = this.initializeFormField<string>(form, "Status");
			form.ApprovalResult = this.initializeFormField<string>(form, "ApprovalResult");
			form.Started = this.initializeFormField<DateTime>(form, "Started");
			form.Modified = this.initializeFormField<DateTime>(form, "Modified");
		}

		private T initializeFormField<T>(ProcForm form, string fieldName)
		{
			int index = this.Columns.IndexOf(fieldName);
			if (index == -1)
			{
				return default(T);
			}
			return (T) System.Convert.ChangeType(form.Values[index], Type.GetTypeCode(typeof(T)));
		}
    }
}

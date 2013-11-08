using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ProcFormsResponse : Abstract.Response
    {
		private string _Process { get; set; }
		public string Process
		{
			get
			{
				if (_Process == null)
				{
					_Process = "";
				}
				return _Process;
			}
			set { _Process = value; }
		}

        public int ProcessID { get; set; }

        private string _View { get; set; }
		public string View
		{
			get
			{
				if (_View == null)
				{
					_View = "";
				}
				return _View;
			}
			set { _View = value; }
		}

        private List<string> _Columns { get; set; }
		public List<string> Columns
		{
			get
			{
				if (_Columns == null)
				{
					_Columns = new List<string>();
				}
				return _Columns;
			}
			set { _Columns = value; }
		}

        private List<ProcFormResponse> _Forms { get; set; }
		public List<ProcFormResponse> Forms
		{
			get
			{
				if (_Forms == null)
				{
					_Forms = new List<ProcFormResponse>();
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
			foreach (ProcFormResponse item in this.Forms)
			{
				item.ProcessID = this.ProcessID;
				item.ProcessName = this.Process;
				this.initializeBasicFormFields(item);
				//this.copyCustomFieldNames(item);
			}
		}

		private void initializeBasicFormFields(ProcFormResponse form)
		{
			form.Number = this.initializeFormField<string>(form, "Number");
			form.Owner = this.initializeFormField<string>(form, "Owner");
			form.Status = this.initializeFormField<string>(form, "Status");
			form.ApprovalResult = this.initializeFormField<string>(form, "ApprovalResult");
			form.Started = this.initializeFormField<DateTime>(form, "Started");
			form.Modified = this.initializeFormField<DateTime>(form, "Modified");
		}

		private void copyCustomFieldNames(ProcFormResponse form)
		{
			for (int i = 0; i < this.Columns.Count; i++)
			{
				string fieldName = this.Columns[i];
				FieldResponse field = new FieldResponse();
				field.Field = fieldName;
				field.Value = form.Values[i];
				form.Fields.Add(field);
			}
		}

		private T initializeFormField<T>(ProcFormResponse form, string fieldName)
		{
			int index = this.Columns.IndexOf(fieldName);
			if (index == -1)
			{
				return default(T);
			}
			return (T) System.Convert.ChangeType(form.Values[index], Type.GetTypeCode(typeof(T)));
		}

		public override bool Equals(object obj)
		{
			ProcFormsResponse other = (ProcFormsResponse)obj;
			return
				this.Process == other.Process &&
				this.ProcessID == other.ProcessID &&
				this.View == other.View &&
				this.CollectionsAreEqual(this.Columns, other.Columns) &&
				this.CollectionsAreEqual(this.Forms, other.Forms);
		}

    }
}

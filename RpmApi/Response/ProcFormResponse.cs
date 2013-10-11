using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ProcFormResponse : Abstract.Response
    {
		private string _Process { get; set; }
		public string Process { 
			get {
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
			get {
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
			get {
				if (_Columns == null)
				{
					_Columns = new List<string>();
				}
				return _Columns;
			}
			set { _Columns = value; } 
		}

        private ProcForm _Form { get; set; }
		public ProcForm Form
		{ 
			get {
				if (_Form == null)
				{
					_Form = new ProcForm();
				}
				return _Form;
			}
			set { _Form = value; } 
		}

		public override bool Equals(object obj)
		{
			ProcFormResponse other = (ProcFormResponse)obj;
			return
				this.Process == other.Process &&
				this.ProcessID == other.ProcessID &&
				this.View == other.View &&
				this.CollectionsAreEqual(this.Columns, other.Columns) &&
				this.Form.Equals(other.Form);
		}
    }
}

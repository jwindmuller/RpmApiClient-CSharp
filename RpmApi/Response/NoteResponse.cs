using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class NoteResponse : Abstract.Response
	{

		private DateTime _Added { get; set; }
		public DateTime Added
		{
			get
			{
				if (_Added == null)
				{
					_Added = new DateTime();
				}
				return _Added;
			}
			set { _Added = value; }
		}

		private string _By { get; set; }
		public string By
		{
			get
			{
				if (_By == null)
				{
					_By = "";
				}
				return _By;
			}
			set { _By = value; }
		}

		private string _Note { get; set; }
		public string Note
		{
			get
			{
				if (_Note == null)
				{
					_Note = "";
				}
				return _Note;
			}
			set { _Note = value; }
		}

		public object Clone()
		{
			NoteResponse clone = new NoteResponse();
			clone.Added = this.Added;
			clone.By = this.By.Clone() as string;
			clone.Note = this.Note.Clone() as string;
			return clone;
		}
	}
}

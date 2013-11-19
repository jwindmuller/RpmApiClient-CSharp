using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class WorksheetTableSuperHeader : Abstract.Response
	{
		public int Row { get; set; }
		public int Span { get; set; }
		public int Start { get; set; }
		private string _Text { get; set; }
		public string Text
		{
			get
			{
				if (_Text == null)
				{
					_Text = "";
				}
				return _Text;
			}
			set { _Text = value; }
		}
	}
}

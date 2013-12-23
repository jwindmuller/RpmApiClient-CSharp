using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class SplitParticipantResponse: Abstract.Response
	{
		private string _Agency { get; set; }
		public string Agency
		{
			get
			{
				if (_Agency == null)
				{
					_Agency = "";
				}
				return _Agency;
			}
			set { _Agency = value; }
		}
		public int AgencyID { get; set; }

		private string _ItemID { get; set; }
		public string ItemID
		{
			get
			{
				if (_ItemID == null)
				{
					_ItemID = "";
				}
				return _ItemID;
			}
			set { _ItemID = value; }
		}

		private string _Percent { get; set; }
		public string Percent
		{
			get
			{
				if (_Percent == null)
				{
					_Percent = "";
				}
				return _Percent;
			}
			set { _Percent = value; }
		}

		private string _Rep { get; set; }
		public string Rep
		{
			get
			{
				if (_Rep == null)
				{
					_Rep = "";
				}
				return _Rep;
			}
			set { _Rep = value; }
		}
		public int RepID { get; set; }

		public override bool Equals(object obj)
		{
			SplitParticipantResponse other = obj as SplitParticipantResponse;
			return
				this.Agency.Equals(other.Agency) &&
				this.AgencyID == other.AgencyID &&
				this.ItemID.Equals(other.ItemID) &&
				this.Percent.Equals(other.Percent) &&
				this.Rep.Equals(other.Rep) &&
				this.RepID == other.RepID;
		}
	}
}

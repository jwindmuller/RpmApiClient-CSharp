using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class SplitsResponse: Abstract.Response
	{
		private string _OriginalNetBilled { get; set; }
		public string OriginalNetBilled
		{
			get
			{
				if (_OriginalNetBilled == null)
				{
					_OriginalNetBilled = "";
				}
				return _OriginalNetBilled;
			}
			set { _OriginalNetBilled = value; }
		}

		private List<SplitParticipantResponse> _Participants { get; set; }
		public List<SplitParticipantResponse> Participants
		{
			get
			{
				if (_Participants == null)
				{
					_Participants = new List<SplitParticipantResponse>();
				}
				return _Participants;
			}
			set { _Participants = value; }
		}

		public override bool Equals(object obj)
		{
			SplitsResponse other = obj as SplitsResponse;
			return
				this.OriginalNetBilled.Equals(other.OriginalNetBilled) &&
				this.CollectionsAreEqual(this.Participants, other.Participants);
		}
	}
}

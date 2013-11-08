using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class ParticipantResponse : Abstract.Response
	{
		public string Name { get; set; }
		public string Role { get; set; }


		public override bool Equals(object obj)
		{
			ParticipantResponse other = (ParticipantResponse)obj;
			return
				this.Name.Equals(other.Name) &&
				this.Role.Equals(other.Role);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPM.Api.Response
{
	public class CommItemResponse : Abstract.Response
	{
		private string _Account { get; set; }
		public string Account
		{
			get
			{
				if (_Account == null)
				{
					_Account = "";
				}
				return _Account;
			}
			set { _Account = value; }
		}
		public int AccountID { get; set; }

		private string _CommissionGroup { get; set; }
		public string CommissionGroup
		{
			get
			{
				if (_CommissionGroup == null)
				{
					_CommissionGroup = "";
				}
				return _CommissionGroup;
			}
			set { _CommissionGroup = value; }
		}
		public int CommissionGroupID { get; set; }

		private string _CommissionRun { get; set; }
		public string CommissionRun
		{
			get
			{
				if (_CommissionRun == null)
				{
					_CommissionRun = "";
				}
				return _CommissionRun;
			}
			set { _CommissionRun = value; }
		}

		private string _Customer { get; set; }
		public string Customer
		{
			get
			{
				if (_Customer == null)
				{
					_Customer = "";
				}
				return _Customer;
			}
			set { _Customer = value; }
		}
		public int CustomerID { get; set; }

		private string _GrossComm { get; set; }
		public string GrossComm
		{
			get
			{
				if (_GrossComm == null)
				{
					_GrossComm = "";
				}
				return _GrossComm;
			}
			set { _GrossComm = value; }
		}

		private string _GrossCommClaimedPercent { get; set; }
		public string GrossCommClaimedPercent
		{
			get
			{
				if (_GrossCommClaimedPercent == null)
				{
					_GrossCommClaimedPercent = "";
				}
				return _GrossCommClaimedPercent;
			}
			set { _GrossCommClaimedPercent = value; }
		}
		private string _GrossCommExpectedPercent { get; set; }
		public string GrossCommExpectedPercent
		{
			get
			{
				if (_GrossCommExpectedPercent == null)
				{
					_GrossCommExpectedPercent = "";
				}
				return _GrossCommExpectedPercent;
			}
			set { _GrossCommExpectedPercent = value; }
		}
		private string _GrossCommPercent { get; set; }
		public string GrossCommPercent
		{
			get
			{
				if (_GrossCommPercent == null)
				{
					_GrossCommPercent = "";
				}
				return _GrossCommPercent;
			}
			set { _GrossCommPercent = value; }
		}

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

		private string _Margin { get; set; }
		public string Margin
		{
			get
			{
				if (_Margin == null)
				{
					_Margin = "";
				}
				return _Margin;
			}
			set { _Margin = value; }
		}

		private string _NetBilled { get; set; }
		public string NetBilled
		{
			get
			{
				if (_NetBilled == null)
				{
					_NetBilled = "";
				}
				return _NetBilled;
			}
			set { _NetBilled = value; }
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
		private string _NoteForAgents { get; set; }
		public string NoteForAgents
		{
			get
			{
				if (_NoteForAgents == null)
				{
					_NoteForAgents = "";
				}
				return _NoteForAgents;
			}
			set { _NoteForAgents = value; }
		}

		private string _Origin { get; set; }
		public string Origin
		{
			get
			{
				if (_Origin == null)
				{
					_Origin = "";
				}
				return _Origin;
			}
			set { _Origin = value; }
		}

		public int Quantity { get; set; }

		//Referrals

		private SplitsResponse _Splits { get; set; }
		public SplitsResponse Splits
		{
			get
			{
				if (_Splits == null)
				{
					_Splits = new SplitsResponse();
				}
				return _Splits;
			}
			set { _Splits = value; }
		}

		private string _Supplier { get; set; }
		public string Supplier
		{
			get
			{
				if (_Supplier == null)
				{
					_Supplier = "";
				}
				return _Supplier;
			}
			set { _Supplier = value; }
		}
		public int SupplierID { get; set; }

		//Variables

		private string _Wholesale { get; set; }
		public string Wholesale
		{
			get
			{
				if (_Wholesale == null)
				{
					_Wholesale = "";
				}
				return _Wholesale;
			}
			set { _Wholesale = value; }
		}

		public override bool Equals(object obj)
		{
			return false;
		}
	}
}

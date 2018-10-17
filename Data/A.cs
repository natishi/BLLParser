using System;

namespace BLLParser.Data
{
	public class A : Base
	{
		private string mDepositBank;//bank lehafkada
		private string mDepositOffice;//senif lehafkada
		private string mDepositTypeAccount;//sug heshbon lehafkada
		private string mDepositNumberAccount;//mispar heshbon lehafkada
		private string mStrDate;
		private DateTime? mDate;
		private string mN = string.Empty;
		public static readonly int lineALength = 30;

		public string DepositBank
		{
			get
			{
				return mDepositBank;
			}
			set
			{
				mDepositBank = value; 
			}
		}
		public string DepositOffice
		{
			get
			{
				return mDepositOffice;
			}
			set
			{
				mDepositOffice = value; 
			}
		}
		public string DepositTypeAccount
			{
			get
			{
				return mDepositTypeAccount;
			}
			set
			{
				mDepositTypeAccount = value; 
			}
		}
		public string DepositNumberAccount
			{
			get
			{
				return mDepositNumberAccount;
			}
			set
			{
				mDepositNumberAccount = value; 
			}
		}
		public string StrDate
		{
			get 
			{
				return mStrDate;
			}
			set
			{
				mStrDate = value;
			}
		}
		public DateTime? Date
		{
			get
			{
				return mDate;
			}
			set
			{
				mDate = value; 
			}
		}
		public string N
		{
			get
			{
				return mN;
			}
			set
			{
				mN = value; 
			}
		}

		public override string ToString()
		{
			return this.Type +
				   this.RunningNumber +
				   mDepositBank +
				   mDepositOffice + 
				   mDepositTypeAccount + 
				   mDepositNumberAccount + 
				   mStrDate + 
				   mN;

		}
	}
}

using System;

namespace BLLParser.Data
{
	public class BD : Base
	{
		private string mDoseNumber;
		private string mGroupNumber;
		private string mConst;
		private string mCheckSumWithAgorot;
		private string mCheckNumber;
		private string mCredit = string.Empty;//only for D
		private string mFutureUse = string.Empty;//only for D
		private string mDepositType = string.Empty;//only for D
		private string mBank;
		private string mOffice;
		private string mActionCode;
		private string mAccountNumber;
		private string mStrDate;
		private DateTime? mMaturityDate;
		private string mInstitutionCode;
		public static readonly int lineBLength = 77;

		public string DoseNumber
			{
			get
				{
				return mDoseNumber;
				}
			set
				{
				mDoseNumber = value;
				}
			}
		public string GroupNumber
			{
			get
				{
				return mGroupNumber;
				}
			set
				{
				mGroupNumber = value;
				}
			}
		public string Const
			{
			get
				{
				return mConst;
				}
			set 
			{
				mConst = value;
			}
			}
		public string CheckSumWithAgorot
			{
			get
				{
				return mCheckSumWithAgorot;
				}
			set
				{
				mCheckSumWithAgorot = value;
				}
			}
		public string CheckNumber
			{
			get
				{
				return mCheckNumber;
				}
			set
				{
				mCheckNumber = value;
				}
			}
		public string Credit
			{
			get
				{
				return mCredit;
				}
			set
				{
				mCredit = value;
				}
			}
		public string FutureUse
			{
			get
				{
				return mFutureUse;
				}
			set
				{
				mFutureUse = value;
				}
			}
		public string DepositType
			{
			get
				{
				return mDepositType;
				}
			set
				{
				mDepositType = value;
				}
			}
		public string Bank
			{
			get
				{
				return mBank;
				}
			set
				{
				mBank = value;
				}
			}
		public string Office
			{
			get
				{
				return mOffice;
				}
			set
				{
				mOffice = value;
				}
			}
		public string ActionCode
			{
			get
				{
				return mActionCode;
				}
			set
				{
				mActionCode = value;
				}
			}
		public string AccountNumber
			{
			get
				{
				return mAccountNumber;
				}
			set
				{
				mAccountNumber = value;
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
		public DateTime? MaturityDate
			{
			get
				{
				return mMaturityDate;
				}
			set
				{
				mMaturityDate = value;
				}
			}
		public string InstitutionCode
			{
			get
				{
				return mInstitutionCode;
				}
			set
				{
				mInstitutionCode = value;
				}
			}

		public override string ToString()
			{
			return this.Type +
				    this.RunningNumber +
					mDoseNumber +
					mGroupNumber +
					mConst +
					mCheckSumWithAgorot +
					mCheckNumber +
					mCredit +
					mFutureUse +
					mDepositType +
					mBank +
					mOffice +
					mActionCode +
					mAccountNumber +
					mStrDate +
					mInstitutionCode;

			}

		}
}

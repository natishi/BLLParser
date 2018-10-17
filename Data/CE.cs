
namespace BLLParser.Data
{
	public class CE : Base
	{
		private string mTotalSumWithAgorot;
		private string mFalseAmount;
		private string mCheckAmount;
		public static readonly int lineCLength = 41;

		public string TotalSumWithAgorot
			{
			get
				{
				return mTotalSumWithAgorot;
				}
			set
				{
				mTotalSumWithAgorot = value;
				}
			}
		public string FalseAmount
			{
			get
				{
				return mFalseAmount;
				}
			set
				{
				mFalseAmount = value;
				}
			}
		public string CheckAmount
			{
			get
				{
				return mCheckAmount;
				}
			set
				{
				mCheckAmount = value;
				}
			}

		public override string ToString()
		{
			return this.Type +
					this.RunningNumber +
					mTotalSumWithAgorot +
					mFalseAmount +
					mCheckAmount;
		}

	}
}
	

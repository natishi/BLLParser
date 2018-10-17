using System;
using System.Text.RegularExpressions;

namespace BLLParser.Data
{
	public abstract class Base
	{
		private string mType; 
		private string mRunningNumber;
		public string Type
		{
			get
			{
				return mType;
			}
			set 
			{
				mType = value;
			}
		}
		public string RunningNumber
			{
			get
				{
				return mRunningNumber;
				}
			set
				{
				mRunningNumber = value;
				}
			}

		public string GetType(string line)
		{
			return line.Substring(0, 1);
		}
		
		public string GetRunnigNumber(string line)
		{
			return line.Substring(1, 7);
		}
		
		public static bool IsNumeric(string line)
		{
			Regex regex = new Regex("^[0-9]+$");
			if(regex.IsMatch(line))
				return true;
			return false;
		}
		
		public static bool LineValidation(string line, char type)
		{
			if (IsNumeric(line.Substring(1)))
			{
				switch(type)
					{
					case 'A':
						return line.StartsWith("A") && A.lineALength == line.Length;
					case 'B':
						return line.StartsWith("B") && BD.lineBLength == line.Length;
					case 'C':
						return line.StartsWith("C") && CE.lineCLength == line.Length;
					default:
						return false;
					}
			}
			return false;
		}
		
		/// <summary>
		/// Convert string to DateTime format
		/// </summary>
		/// <param name="date">ddMMyy format</param>
		/// <returns></returns>
		public DateTime? GetDate(string date)
		{
			try
			{
				return DateTime.ParseExact(date, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
			}
			catch(Exception e)
			{
				return null;
			}
		}

	}
}

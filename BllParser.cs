using BLLParser.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace BLLParser
{
	public class BllParser
	{
		private A mA;
		private List<BD> mBDList = new List<BD>();
		private CE mCE;

		public A A
		{
			get
			{
				return mA;
			}
			set
			{
				mA = value;
			}
		}
		public List<BD> BDList
		{
			get
			{
				return mBDList;
			}
			set
			{
				mBDList = value;
			}
		}
		public CE CE
		{
			get
			{
				return mCE;
			}
			set
			{
				mCE = value;
			}
		}

		List<string> errorMessage = new List<string>();
		bool Status = true;//A & C validation(B is optional)
		public BllParser() { }
		/// <summary>
		/// parse and populate data from file (A, List<BD>, CE)
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns>Item1(bool) - Success or Failed, Item2(List<string>) - Error messages list</returns>
		public Tuple<bool, List<string>> Parser(string filePath)
		{
			try
			{
				if(!File.Exists(filePath))
				{
					errorMessage.Add("File doesn't exists");
					return new Tuple<bool, List<string>>(false, errorMessage);
				}
				using(StreamReader Reader = new StreamReader(filePath))
				{
					string fileContent = Reader.ReadToEnd();
					if(string.IsNullOrEmpty(fileContent))
					{
						errorMessage.Add("File is empty");
						return new Tuple<bool, List<string>>(false, errorMessage);
					}
					string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
										.Where(x => !string.IsNullOrEmpty(x)).ToArray()
										.Select(line => line.Trim()).ToArray();
					PopulateDate(lines);
					return new Tuple<bool, List<string>>(Status, errorMessage);
				}
				
			}
			catch(Exception e)
			{
				Trace.Write(e.Message);
				errorMessage.Add("Issue was found while reading from file");
				return new Tuple<bool, List<string>>(false, errorMessage);
			}
    	}

		private void PopulateDate(string[] lines)
		{
			PopulateA(lines[0]);
			PopulateBD(lines);
			PopulateCE(lines[lines.Length - 1]);
		}

		private void PopulateA(string firstLine)
		{
			if(Base.LineValidation(firstLine, 'A') == false)
			{
				errorMessage.Add("First line is invalid");
				Status = false;
			}
			else
			{
				A = new A();
				A.Type = A.GetType(firstLine);
				A.RunningNumber = A.GetRunnigNumber(firstLine);
				A.DepositBank = firstLine.Substring(8, 2);
				A.DepositOffice = firstLine.Substring(10, 3);
				A.DepositTypeAccount = firstLine.Substring(13, 3);
				A.DepositNumberAccount = firstLine.Substring(16, 8);
				A.StrDate = firstLine.Substring(24, 6);
				A.Date = A.GetDate(A.StrDate);
				A.N = string.Empty;
			}
		}
		
		private void PopulateBD(string[] middleLines)
		{
			for(int i = 1; i < middleLines.Length - 1; i++)
			{
				if(Base.LineValidation(middleLines[i], 'B') == false)
				{
					errorMessage.Add($"Line B No.{i + 1} is invalid");
					Status = false;
					continue;
				}
				BD bd = new BD();
				bd.Type = bd.GetType(middleLines[i]);
				bd.RunningNumber = bd.GetRunnigNumber(middleLines[i]);
				bd.DoseNumber = middleLines[i].Substring(8, 2);
				bd.GroupNumber = middleLines[i].Substring(10, 2);
				bd.Const = middleLines[i].Substring(12, 1);
				bd.CheckSumWithAgorot = middleLines[i].Substring(13, 9);
				bd.CheckNumber = middleLines[i].Substring(22, 10);
				bd.Credit = string.Empty;//only for D
				bd.FutureUse = string.Empty;//only for D
				bd.DepositType = string.Empty;//only for D
				bd.Bank = middleLines[i].Substring(32, 10);
				bd.Office = middleLines[i].Substring(42, 8);
				bd.ActionCode = middleLines[i].Substring(50, 2);
				bd.AccountNumber = middleLines[i].Substring(52, 10);
				bd.StrDate = middleLines[i].Substring(62, 6);
				bd.MaturityDate = bd.GetDate(bd.StrDate);
				bd.InstitutionCode = middleLines[i].Substring(68, 9);
				BDList.Add(bd);
			}
		}
		
		private void PopulateCE(string lastLine)
		{
			if(Base.LineValidation(lastLine, 'C') == false)
			{
				errorMessage.Add("Last line is invalid");
				Status = false;
			}
			else
			{
				CE = new Data.CE();
				CE.Type = CE.GetType(lastLine);
				CE.RunningNumber = CE.GetRunnigNumber(lastLine);
				CE.TotalSumWithAgorot = lastLine.Substring(8, 14);
				CE.FalseAmount = lastLine.Substring(22, 14);
				CE.CheckAmount = lastLine.Substring(36, 5);
			}
		}

		/// <summary>
		/// Return update file content according to file path
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="bIsMehuyavAgaa"></param>
		/// <returns></returns>
		public string BuildNewBllForSlikaElectronit(string filePath, bool bIsMehuyavAgaa = false)
		{
			string result = string.Empty;
			BllParser bp = new BllParser();
			if(bp.Parser(filePath).Item1)
			{
				bp.A.N = "N";
				foreach(BD bd in bp.BDList)
				{
					bd.Type = "D";
					bd.Credit = "00";
					bd.FutureUse = "00000";
					bd.DepositType = bIsMehuyavAgaa ? "1" : "0";
				}
				bp.CE.Type = "E";
				result = bp.ToString();
			}
			return result;
		}

        /// <summary>
        /// Build string with new content.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileDestination"></param>
        /// <param name="result">extract 6 fields from b lines to string</param>
        /// <param name="isIncludeAgurot">sum include agurot or not(if yes it will insert '.')</param>
        /// <param name="Ignore2DigitOffice">ignore the last 2 digit in office field(in case it already contain the action code)</param>
        /// <returns>True - extract the fields to string and save new BLL file, False - otherwise</returns>
        public bool Build6FieldsBLL(string filePath, string fileDestination, ref string result, bool isIncludeAgurot, bool Ignore2DigitOffice = false)
        {
            bool success = true;
            try
            {
                StringBuilder sb = new StringBuilder();
                const string TAB = "\t";
                const int digitForDelete = 2;
                BllParser bp = new BllParser();
                Tuple<bool, List<string>> resTuple = bp.Parser(filePath);
                success = resTuple.Item1;
                if (success)
                {
                    foreach (BD bd in bp.BDList)
                    {
                        string date = bd.MaturityDate != null ? bd.MaturityDate?.ToString("dd/MM/yy") : "00/00/00";
                        string office = DeletePaddingZero(bd.Office);
                        string officeWithoutActionCode = Ignore2DigitOffice == true && office.Length >= digitForDelete ? office.Substring(0, office.Length - digitForDelete) : office;
                        string sum = DeletePaddingZero(bd.CheckSumWithAgorot);
                        sum = isIncludeAgurot == true ? sum.Insert(sum.Length - 2, ".") : sum;
                        string b = DeletePaddingZero(bd.CheckNumber) + TAB
                                + PaddingLeftZero(DeletePaddingZero(bd.Bank), 2) + TAB
                                + PaddingLeftZero(officeWithoutActionCode, 3) + bd.ActionCode + TAB
                                + DeletePaddingZero(bd.AccountNumber) + TAB
                                + sum + TAB
                                + date + Environment.NewLine;
                        sb.Append(b);
                    }
                    result = sb.ToString();
                    result = result.Remove(result.LastIndexOf(Environment.NewLine));
                }
                if (success && !string.IsNullOrEmpty(fileDestination))
                {
                    success = CreateFile(fileDestination, result);
                }
            }
            catch (Exception e)
            {
                Trace.Write(e.Message);
                success = false;
            }
            return success;
        }
        private bool CreateFile(string pathDestination, string content)
		{
			bool success = true;
			try
			{
				if(!File.Exists(pathDestination))
					File.Create(pathDestination).Dispose();
				else if(File.Exists(pathDestination))
					File.Delete(pathDestination);
				using(TextWriter tw = new StreamWriter(pathDestination))
				{
					tw.WriteLine(content);
				}
			}
			catch(Exception e)
			{
				Trace.Write(e.Message);
				success = false;
			}
			return success;
		}
		
		private string DeletePaddingZero(string str)
		{
			return str.TrimStart(new Char[] { '0' });
		}
		
		private string PaddingLeftZero(string str, int totalLength)
		{
			return str.PadLeft(totalLength,'0');
		}
		
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(A.ToString());
			sb.Append(Environment.NewLine);
			foreach(BD bd in BDList)
			{
				sb.Append(bd.ToString());
				sb.Append(Environment.NewLine);
			}
			sb.Append(CE.ToString());

			return sb.ToString();
		}


	}
}

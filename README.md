# BLLParser
Balamuth BLLParsr

BLLParser.dll
Purpose: parse .bll file.

There is 3 main functionality for this:
##	Create appropriate objects for this file.

	string path = ".bll file path";
	BllParserbllParser = newBllParser();
	Tuple<bool, List<string>>resTuple;
	resTuple = bllParser.Parser(path);
	bool success = resTuple.Item1;
	List<string> error = resTuple.Item2;
	if (success)
	{
		//now we have the object bllParser with all relevant information(A,B('BD'),C('CE'))
		string s = bllParser.ToString();
    }

##	Build new bll for slika electronit

  string path = ".bll file path";  
  	BllParserbllParser = newBllParser();  
  stringupdateFileContent = bllParser.BuildNewBllForSlikaElectronit(path, true);

##	Build string and create new file for 6 fields from B lines.

	string path = ".bll file path";
    string desPath = "new .bll file path";
    BllParserbllParser = newBllParser();
    string res = string.Empty;
    bool success = bllParser.Build6FieldsBLL(path, desPath, ref res, true);

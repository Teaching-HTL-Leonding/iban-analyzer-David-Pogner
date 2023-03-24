// Angabe: https://github.com/Teaching-HTL-Leonding/iban-analyzer-David-Pogner
// Level 1, 2 und 3 implementiert

if (args.Length > 0)
{
    string Choice = args[0];

    if (Choice != "build" && Choice != "analyze")
    {
        System.Console.WriteLine("Invalid command, must be build or analyze");
        Environment.Exit(0);
    }

    if (Choice == "build")
    {
        string BankCode = args[1];
        string AccountNumber = args[2];

        string TotalOutput = BuildIban(BankCode, AccountNumber, CalculateChecksum(BankCode, AccountNumber));
        System.Console.WriteLine(TotalOutput);

    #region Fehlerbehandlung Level 1
        for (int i = 0; i < BankCode.Length; i++)
        {
            if (char.IsLetter(BankCode[i]))
            {
                System.Console.WriteLine("Bank code must not contain letters");
                Environment.Exit(0);
            }
        }

        for (int a = 0; a < AccountNumber.Length; a++)
        {
            if (char.IsLetter(AccountNumber[a]))
            {
                System.Console.WriteLine("Account number must not contain letters");
                Environment.Exit(0);
            }
        }

        if (args.Length > 3)
        {
            Console.WriteLine("Too many arguments");
            Environment.Exit(0);
        }
        else if (args.Length < 2)
        {
            System.Console.WriteLine("Too few arguments");
            Environment.Exit(0);
        }
        if (BankCode.Length < 4 || BankCode.Length > 4)
        {
            Console.WriteLine("Bank code has wrong length, must contain 4 digits"!);
            Environment.Exit(0);
        }
        if (AccountNumber.Length < 6 || AccountNumber.Length > 6)
        {
            System.Console.WriteLine("Account number has wrong length, must contain 6 digits");
            Environment.Exit(0);
        }
        #endregion
    }
    else if (Choice == "analyze")
    {
        string InputStringToAnalyze = args[1];
        string Country = InputStringToAnalyze.Substring(0, 2);
        string NationalCheckDigit = InputStringToAnalyze.Substring(14, 1);

        #region Fehlerbehandlung Level 2


        if (Country != "NO")
        {
            System.Console.WriteLine("Wrong country code, we currently only support NO");
            Environment.Exit(0);
        }

        if (NationalCheckDigit != "7")
        {
            System.Console.WriteLine("Wrong national check digit, we currently only support 7");
            Environment.Exit(0);
        }

        if (InputStringToAnalyze.Length > 15 || InputStringToAnalyze.Length < 15)
        {
            System.Console.WriteLine("Wrong length of IBAN");
            Environment.Exit(0);
        }

        #endregion

        AnalyzeIban(InputStringToAnalyze);
    }
}

string BuildIban(string BankCode, string AccountNumber, string checksum)
{
    string Output = $"NO{checksum}{BankCode}{AccountNumber}7";
    return Output;
}


void AnalyzeIban(string InputStringToAnalyze)
{
    string BankCode = InputStringToAnalyze.Substring(4, 4);
    string AccountNumber = InputStringToAnalyze.Substring(8, 6);

    Console.WriteLine($"Bank code is: {BankCode}");
    Console.WriteLine($"Account number is: {AccountNumber}");
}


string CalculateChecksum(string BankCode, string AccountNumber)
{
    string StringToCalulate = $"{BankCode}{AccountNumber}7232400";
    long NumberForCecksum = long.Parse(StringToCalulate);

    long summe = 98-(NumberForCecksum % 97);

    string checksum = summe.ToString();
    return checksum;
}
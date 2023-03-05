using System.Globalization;
using System.Text.RegularExpressions;

namespace SimpleCalculator;

public class PercentOperations
{
    private decimal _number;
    private string _numberString;
    private string _operator;
    
    public string Transform(string entryEquation)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        MatchPercentNumber(entryEquation);

        char operatorChar = _numberString[0];
            
        switch (_operator)
        {
            case "+":
                _number = (_number / 100) + 1;
                operatorChar = '*';
                entryEquation = $"({MatchString(entryEquation)})";
                break;
            case "-":
                _number = 1 - (_number / 100);
                operatorChar = '*';
                entryEquation = $"({MatchString(entryEquation)})";
                break;
            case "*":
            case "/":
                _number = _number / 100;
                entryEquation = MatchString(entryEquation);
                break;
        }
            
        return entryEquation += $"{operatorChar}{_number}";
    }
    
    private void MatchPercentNumber(string entryEquation)
    {
        string pattern = @"([+-/*])\s*([\d.]+)$";
        Match matchPercent = Regex.Match(entryEquation, pattern);
        
        _numberString = matchPercent.Groups[0].Value;
        _number = decimal.Parse(matchPercent.Groups[2].Value);
        _operator = matchPercent.Groups[1].Value;
    }
    
    private string MatchString(string entryEquation)
    {
        string equationPriorPattern = @"(.*\d.*[\*\+\/\-])|([-+*/])(?!.*[-+*/])";
        Match matchPrior = Regex.Match(entryEquation, equationPriorPattern);

        return matchPrior.Value.Substring(0, matchPrior.Length - 1);
    }
}
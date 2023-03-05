using System.Text.RegularExpressions;

namespace SimpleCalculator;

public class SafetyCheck
{
    private static int _openParenthesis;
    private static int _closedParenthesis;
    
    // operator can be used only after number, closed parenthesis or percent sign
    public static bool Operator(string currentEquation)
    {
        string regexCheck = @"^.*[\d)%]$";

        if (!string.IsNullOrEmpty(currentEquation))
            return !Regex.IsMatch(currentEquation, regexCheck);

        return true;
    }

    // number button cannot be used after ) or % sign. Regex slightly modified, because for some reason it didnt include * char if previous to * was )
    public static bool Number(string currentEquation)
    {
        string regexCheck = @"[\)\%]$";

        if (string.IsNullOrEmpty(currentEquation))
            return false;
        
        return Regex.IsMatch(currentEquation, regexCheck);
    }

    // OP can only be placed after an operator
    public static bool OpenParenthesis(string currentEquation)
    {
        string regexCheck = @"[\+\-\*\/\(]$";

        if (string.IsNullOrEmpty(currentEquation))
        {
            _openParenthesis++;
            return false;
        }
            
        if (Regex.IsMatch(currentEquation, regexCheck))
        {
            _openParenthesis++;
            return false;
        }
        
        return true;
    }

    // CP can be placed after a number or % sign if there is enough OP
    public static bool ClosedParenthesis(string currentEquation)
    {
        string regexCheck = @"[\+\-\*\/\.\(]$";

        if (_closedParenthesis >= _openParenthesis)
            return true;
        if (Regex.IsMatch(currentEquation, regexCheck))
            return true;
        
        _closedParenthesis++;
        
        return false;
    }

    // comma can only be used if there is only 1 comma in 1 number
    public static bool Comma(string currentEquation)
    {
        string regexCheck = @"(\d+\.\d+|\.\d+|[\+\-\*\/\(\)\.\%])$";
        
        if (string.IsNullOrEmpty(currentEquation))
            return true;
        
        if (!Regex.IsMatch(currentEquation, regexCheck)) 
            return false;
        
        return true;
    }

    // percent sign can only be used after numbers and if there is at least 1 operator in play
    public static bool Percent(string currentEquation)
    {
        string regexCheck = @"^(?=.*[\+\-\*/])(?=.*[\d]$).*";

        if (string.IsNullOrEmpty(currentEquation))
            return true;

        if (Regex.IsMatch(currentEquation, regexCheck))
            return false;

        return true;
    }

    // equals cannot be used on a empty string, when parenthesis arent closed or when there is random operator in play.
    public static bool Equals(string currentEquation)
    {
        if (string.IsNullOrEmpty(currentEquation))
            return true;

        if (_closedParenthesis < _openParenthesis)
            return true;

        if (Regex.IsMatch(currentEquation, @"[\*\-\+\/\.]$"))
            return true;
        
        return false;
    }

    // safety check for equations that result in infinity
    public static bool Infinite(string currentEquation)
    {
        if (currentEquation is "∞" or "-∞")
            return true;

        return false;
    }
}
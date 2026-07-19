
using p42BaseLib;


namespace p42ConsoleLib;


public class P42ConsoleLogger : P42Logger
{
    ConsoleColor _infoColor = ConsoleColor.Green;
    ConsoleColor _errorColor = ConsoleColor.Red;
    ConsoleColor _debugColor = ConsoleColor.DarkGray;
    ConsoleColor _defaultColor = ConsoleColor.White;

    public P42ConsoleLogger()
    
    {
        
    }
    public P42ConsoleLogger(bool production, bool logToFile, bool logToConsole)
    : base(production, logToFile, logToConsole)
    {
        
    }


    static void WriteColoredLine(string text, ConsoleColor foreground, ConsoleColor? background = null)
    {
        var oldFg = Console.ForegroundColor;
        var oldBg = Console.BackgroundColor;

        Console.ForegroundColor = foreground;
        if (background.HasValue) Console.BackgroundColor = background.Value;

        Console.WriteLine(text);

        Console.ForegroundColor = oldFg;
        Console.BackgroundColor = oldBg;
    }
    

    protected override void WriteEntry(LogType logType, string message)
    {
        ConsoleColor clr;
        switch (logType)    
        {
            case LogType.Log:
                // getting logstring with timer
                message = $"{GetTimeStampLabel(GetTimeStamp())} {message}";
                clr = _defaultColor;
                break;
            case LogType.Error:
                clr = _errorColor;
                break;
            case LogType.Info:
                clr = _infoColor;
                break;
            case LogType.Debug:
                clr = _debugColor;
                break;
            default:
                clr = _defaultColor;
                break;
        }
 
        WriteColoredLine(message, clr);
    }

    
}
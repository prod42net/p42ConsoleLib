
using p42BaseLib;


namespace p42ConsoleLib;


public class P42ConsoleLogger() : P42Logger
{
    ConsoleColor _infoColor = ConsoleColor.Green;
    ConsoleColor _errorColor = ConsoleColor.Red;
    ConsoleColor _debugColor = ConsoleColor.DarkGray;
    ConsoleColor _defaultColor = ConsoleColor.White;

 

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
        ConsoleColor clr = logType switch
        {
            LogType.Log => _defaultColor,
            LogType.Error => _errorColor,
            LogType.Debug => _debugColor,
            LogType.Info => _infoColor,
            _ => _defaultColor
        };
        WriteColoredLine(message, clr);
    }

    
}
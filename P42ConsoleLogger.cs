
using p42BaseLib;
using p42BaseLib.Interfaces;


namespace p42ConsoleLib;


public class P42ConsoleLogger() : P42Logger
{
    ConsoleColor infoColor = ConsoleColor.Green;
    ConsoleColor errorColor = ConsoleColor.Red;
    ConsoleColor debugColor = ConsoleColor.DarkGray;
    ConsoleColor defaultColor = ConsoleColor.White;

 

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
    

    protected override void WriteEntry(p42BaseLib.LogType logType, string message)
    {
        ConsoleColor clr = defaultColor;
        string _path = "";

        clr = logType switch
        {
            LogType.Log => defaultColor,
            LogType.Error => errorColor,
            LogType.Debug => debugColor,
            LogType.Info => infoColor,
            _ => defaultColor,
        };
        


        WriteColoredLine(message, clr);

    }

    
}
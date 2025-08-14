
using p42ConsoleLib.Interfaces;


namespace p42ConsoleLib;

public enum LogType
{
    Log,
    Error,
    Debug,
    Info
}

public class P42Logger() : IP42Logger
{
    ConsoleColor infoColor = ConsoleColor.Green;
    ConsoleColor errorColor = ConsoleColor.Red;
    ConsoleColor debugColor = ConsoleColor.DarkGray;
    ConsoleColor defaultColor = ConsoleColor.White;

    bool _yearFolders = true;
    bool _monthFolders = true;
    bool _dayFolders = true;
    int _maxLogFiles = 999;
    int _maxErrorFiles = 999;
    bool _useUtc = false;

    int _logCounter = 1;
    int _errorCounter = 1;
    int _debugCounter = 1;
    int _infoCounter = 1;

    string _logPath = @".\Logs\Log.txt";
    string _errorPath = @".\Logs\Error.txt";
    string _debugPath = @".\Logs\Debug.txt";
    string _infoPath = @".\Logs\Info.txt";

    FixedSizeQueue<string> _logQueue = new(200);
    FixedSizeQueue<string> _errorQueue = new(500);

    bool Production { get; set; }
    void SetProduction(bool production) => Production = production;
    void SetLogQueueSize(int size) => _logQueue = new(size);
    void SetErrorQueueSize(int size) => _errorQueue = new(size);

    bool LogToFile { get; set; } = true;
    bool ErrorToFile { get; set; } = true;
    bool DebugToFile { get; set; } = false;
    bool InfoToFile { get; set; } = false;

    public P42Logger(bool production = false) : this()
    {
        SetProduction(production);
    }

    public P42Logger(string logPath, string errorPath, bool production = false) : this()
    {
        SetLogPath(logPath);
        SetErrorPath(errorPath);
    }

    public P42Logger(string logPath, string errorPath, int logQueueSize, int errorQueueSize,
        bool production = false) : this()
    {
        SetLogPath(logPath);
        SetErrorPath(errorPath);
        SetLogQueueSize(logQueueSize);
        SetErrorQueueSize(errorQueueSize);
    }

    string GetLogFileName(DateTime timeStamp,LogType logType, string path)
    {
        string? dir = Path.GetDirectoryName(path);
        string? fn = Path.GetFileName(path);
        string? ext = Path.GetExtension(path);
        
        string year = _yearFolders ? timeStamp.Year.ToString("0000")+"\\" : "";
        string month = _monthFolders ? timeStamp.Month.ToString("00")+"\\" : "";
        string day = _dayFolders ? timeStamp.Day.ToString("00")+"\\" : "";
        string counter = "";
        switch (logType)
        {
            case LogType.Log:
                counter = _logCounter.ToString(@"0000");
                break;
            case LogType.Error:
                counter = _errorCounter.ToString(@"0000");
                break;
            case LogType.Debug:
                counter = _debugCounter.ToString(@"0000");
                break;
            default:
                counter = _infoCounter.ToString(@"0000");
                break;
        }

        string fileName = $"{dir}\\{year}{month}{day}{counter}_{fn}{ext}";
        return fileName;
    }

    void SetLogPath(string path)
    {
        _logPath = path;
    }

    void SetErrorPath(string path)
    {
        _errorPath = path;
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


    public void Log(string message)
    {
        WriteEntry(LogType.Log, message);
        _logQueue.Enqueue(message);
    }

    public void Debug(string message)
    {
        if (Production) return;
        WriteEntry(LogType.Debug, message);
    }

    public void Error(string message)
    {
        WriteEntry(LogType.Error, message);
        _errorQueue.Enqueue(message);
        // should be stored somewhere at least or better someone gets notified ;)
    }

    public void Info(string message)
    {
        WriteEntry(LogType.Info, message);
    }

    void WriteEntry(LogType logType, string message)
    {
        DateTime ts = _useUtc ? DateTime.UtcNow : DateTime.Now; 
        ConsoleColor clr = defaultColor;
        string _path = "";
        switch (logType)
        {
            case LogType.Log:
                clr = defaultColor;
                _path = _logPath;
                break;
            case LogType.Error:
                clr = errorColor;
                _path = _errorPath;
                break;
            case LogType.Debug:
                clr = debugColor;
                _path = _debugPath;
                break;
            case LogType.Info:
                clr = infoColor;
                _path = _infoPath;
                break;
        }

        string timePrefix = (_yearFolders ? "" : DateTime.Now.Year.ToString(@"0000")) +
                           (_monthFolders ? "" : DateTime.Now.Month.ToString(@"00")) +
                           (_monthFolders ? "" : DateTime.Now.Month.ToString(@"00 "));
        message = $"[{timePrefix}{ts:HH:mm:ss.fff}] [{logType.ToString("")}] {message}"; 
        WriteColoredLine(message, clr);

            WriteToFile(ts,logType,message, _path);
    }

    void WriteToFile(DateTime timeStamp, LogType logType,string message, string path)
    {
        try
        {
            string fileName = GetLogFileName(timeStamp,logType, path);
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName) ?? String.Empty);

            using var sw = new StreamWriter(fileName, true);
            sw.WriteLine(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
namespace p42ConsoleLib;

public class CmdLine
{
    string _separator;
    string[] _args;
    Dictionary<string,string> _dict = new Dictionary<string,string>();
    
    public CmdLine(string[] args, string separator = "=")
    {
        _args = args;
        _separator = separator;
        Parse();
    }

    public string this[string key] => _dict[key];
    public bool ContainsKey(string key) => _dict.ContainsKey(key);
    public bool TryGetValue(string key, out string value) => _dict.TryGetValue(key, out value);
    public IEnumerable<string> Keys => _dict.Keys;
    public IEnumerable<string> Values => _dict.Values;
    public int Count => _dict.Count;
    public bool IsEmpty => _dict.Count == 0;
    
    public void SetSeparator(string separator) => _separator = separator;
    public void SetArgs(string[] args) => _args = args;
    public void Add(string key, string value) => _dict.Add(key, value);
    public void Remove(string key) => _dict.Remove(key);
    public string[] ToArray() => _dict.Select(x => $"{x.Key}{_separator}{x.Value}").ToArray();
    public string ToCommandLine() => string.Join(" ", ToArray());
    public override string ToString() => ToCommandLine();
    public void Print() => Console.WriteLine(ToCommandLine());
    public void Parse()
    {
        foreach (var arg in _args)
        {
            var parts = arg.Split(_separator);
            if (parts.Length == 2)
                _dict.Add(parts[0], parts[1]);
            else
                _dict.Add(parts[0], "");
        }
    }

    public void Clear()
    {
        _args = null;
        _dict.Clear();
    }


}
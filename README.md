# p42ConsoleLib

A lightweight C# library for console application utilities, focusing on simple and efficient command-line argument parsing.

## Features

### CmdLine

The `CmdLine` class provides an easy way to parse and manage command-line arguments. It supports:
- Key-Value pairs with a configurable separator (default is `=`).
- Flag arguments.
- Dictionary-like access to arguments.
- Conversion back to command-line string format.

### P42ConsoleLogger

A console-based logger that extends `P42Logger`. It provides:
- Colored output based on log levels.
- Support for `Log`, `Error`, `Info`, and `Debug` log types.
- Static helper for colored console output.

## Usage

### CmdLine Example

By default, `CmdLine` uses `=` as a separator between keys and values.

```csharp
using p42ConsoleLib;

// Example args: ["-i=input.json", "-o=output.env", "-w"]
CmdLine cmdLine = new CmdLine(args);

if (cmdLine.ContainsKey("-i"))
{
    string inputFile = cmdLine["-i"];
    Console.WriteLine($"Input: {inputFile}");
}
```

### P42ConsoleLogger Example

```csharp
using p42ConsoleLib;

var logger = new P42ConsoleLogger();
logger.Log("This is a standard log message.");
logger.Error("This is an error message.");
logger.Info("This is an info message.");
logger.Debug("This is a debug message.");
```

### Custom Separator (CmdLine)

You can specify a different separator if needed.

```csharp
// Example args: ["--user:admin", "--pass:1234"]
CmdLine cmdLine = new CmdLine(args, ":");
string user = cmdLine["--user"];
```

### Methods and Properties

#### CmdLine

- `this[string key]`: Access values by key.
- `ContainsKey(string key)`: Check if a key exists.
- `TryGetValue(string key, out string value)`: Safely attempt to get a value.
- `Count`: Number of arguments parsed.
- `IsEmpty`: Check if any arguments were provided.
- `ToArray()`: Convert the dictionary back to an array of strings.
- `ToString()`: Get the full command-line string representation.
- `Print()`: Print the command-line string to the console.

#### P42ConsoleLogger

- `Log(string message)`: Log a standard message.
- `Error(string message)`: Log an error message (Red).
- `Info(string message)`: Log an info message (Green).
- `Debug(string message)`: Log a debug message (Gray).

## Installation

This library is intended to be included as a project reference or compiled into a DLL for use in console applications.

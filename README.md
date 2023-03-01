# fwRelik.Terminal

Makes it easier to enter commands and process data from output.

> Under the hood, the process uses a PowerShell.

## TerminalClient Class

-   Namespace: `fwRelik.Terminal`
-   Assembly: `fwRelik.Terminal.dll`

### 📋 Methods

| Methods | Description                                  |
| ------- | -------------------------------------------- |
| [Command](#command) | Allows you to execute a command and process. |

### 📄 Examples

### Command
```cs
const string command = "dir | Select-Object -Property Name";
TerminalClient.Command(command, process =>
{
    if (process.Error is not null) throw process.Error;

    // Lists all filenames in the given directory.
    Console.WriteLine(process.StdOut);
});
```

## TerminalParser Class

-   Namespace: `fwRelik.Terminal.Extensions`
-   Assembly: `fwRelik.Terminal.dll`

### 📋 Methods

| Methods              | Description                                              |
| -------------------- | -------------------------------------------------------- |
| [CheckValue](#checkvalue)           | Checking value in output.                                |
| [ParseToNumarationRow](#parsetonumarationrow) | Parsing the output to create a line-by-line data format. |

### 📄 Examples

### CheckValue
```cs
const string command = "Get-ExecutionPolicy -List";
TerminalClient.Command(command, process =>
{
    if (process.Error is not null) throw process.Error;

    bool result = TerminalParser.CheckValue(
        stdOut: process.StdOut,
        keyword: "LocalMachine",
        stateValue: "Unrestricted"
    );

    Console.WriteLine($"Local Machine Execution policy is Unrestricted: {result}");
});
```
> If the result and the keyword are the same, then the output can be checked like this:

```cs
const string command = "Test-Path 'C:\Windows\System32'";
TerminalClient.Command(command, process =>
{
    if (process.Error is not null) throw process.Error;

    bool result = TerminalParser.CheckValue(
        stdOut: process.StdOut,
        keyword: "True",
        stateValue: "True"
    );

    Console.WriteLine($"Local Machine Execution policy is Unrestricted: {result}");
});
```
In this way, it is possible to check the boolean value and much more for the complete conformity of the output.

Searching on a different line from the keyword can be done with an additional parameter of `stateRow` by default it is `0`.

### ParseToNumarationRow

```cs
const string command = "dir | Select-Object -Property Mode, Name";
TerminalClient.Command(command, process =>
{
    if (process.Error is not null) throw process.Error;
    
    string[] rows = TerminalParser.ParseToNumarationRow(process.StdOut);

    for (int i = 0; i < rows.Length; i++)
        Console.WriteLine($"[{i}] : {rows[i]}");
});
```

There is also the meaning of `padding`. Indentation from above. Values within this range will be excluded from the list. Default value: `0`.
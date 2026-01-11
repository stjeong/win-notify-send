# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

win-notify-send is a Windows command-line tool for sending toast notifications, packaged as a .NET global tool. It uses a hybrid architecture with a C# CLI frontend and a native C++ DLL backend.

## Build Commands

```bash
# Build the entire solution
dotnet build win-notify-send.sln

# Build in Release mode
dotnet build -c Release win-notify-send.sln

# Run locally (development)
dotnet run --project win-notify-send -- "Test message"
dotnet run --project win-notify-send -- "Title" "Message body"

# Package as NuGet tool
dotnet pack win-notify-send/win-notify-send.csproj

# Install globally from local package
dotnet tool install -g --add-source ./nupkg win-notify-send
```

For the native C++ DLL (`win_toast`), use Visual Studio or MSBuild directly on `win_toast.vcxproj`.

## Architecture

```
User CLI args → Program.cs (C#) → P/Invoke → wintoast.dll (C++) → Windows.UI.Notifications
```

**Two projects in the solution:**

1. **win-notify-send/** (C# .NET 10.0 Console App)
   - `Program.cs` - Entry point, argument parsing, XML generation, P/Invoke calls
   - Targets `win-x64`, packaged as a dotnet global tool

2. **win_toast/** (Native C++ DLL)
   - `win_toast.c/h` - Exported `fnToastSimple(appId, appName, xmlText)` function
   - `WindowsToast.h` - Header-only WinRT wrapper library (from TwitchNotify)
   - Links: `shlwapi.lib`, `ole32.lib`, `runtimeobject.lib`

**Key integration point:** The C# code uses `[DllImport("wintoast.dll")]` to call the native toast function.

## Important Behaviors

- Windows-only: Runtime platform check in Program.cs
- First run: Creates a shortcut and waits 5 seconds for registry setup before showing notification
- Exit codes: 0 (success), 1 (usage/platform error), 2 (toast send failure)

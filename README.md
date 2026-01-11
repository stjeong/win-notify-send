# win-notify-send

A command-line tool for sending toast notifications on Windows. Provides functionality similar to Linux's `notify-send`.

## Installation

### Install from NuGet (Recommended)

```bash
# Install from NuGet
dotnet tool install -g win-notify-send
```

### Build from Source

```bash
# Build the solution
dotnet build -c Release win-notify-send.sln

# Create NuGet package
dotnet pack win-notify-send/win-notify-send.csproj -c Release

# Install from local package
dotnet tool install -g --add-source ./win-notify-send/nupkg win-notify-send
```

## Usage

```bash
# Send message only (title defaults to "win-notify-send")
win-notify-send "Hello, World!"

# Send with custom title and message
win-notify-send "Notification Title" "This is the notification body."

# dnx (.NET 10+ SDK)
dnx -y win-notify-send "Alarm" "Wake up!"
```

## Usage - settings.json sample for Claude Code
```json
{
    "hooks": {
        "Stop": [
            {
                "hooks": [
                    {
                        "type": "command",
                        "command": "dnx -y win-notify-send \"✅ Claude Code\" \"Task completed successfully\""
                    },
                    {
                        "type": "command",
                        "command": "powershell.exe -Command \"(New-Object Media.SoundPlayer 'C:\\Windows\\Media\\Windows Exclamation.wav').PlaySync()\""
                    }
                ]
            }
        ],
        "Notification": [
            {
                "hooks": [
                    {
                        "type": "command",
                        "command": "dnx -y win-notify-send \"🛑 Claude Code\" \"Action or permission required!\""
                    },
                    {
                        "type": "command",
                        "command": "powershell.exe -Command \"(New-Object Media.SoundPlayer 'C:\\Windows\\Media\\Windows Notify Messaging.wav').PlaySync()\""
                    }
                ]
            }
        ]
    }
}
```

## Exit Codes

| Code | Description |
|------|-------------|
| 0 | Success |
| 1 | Invalid usage or unsupported platform |
| 2 | Failed to send toast notification |

## Requirements

- Windows 8 or later
- .NET 10.0 Runtime

## Build Requirements

- Visual Studio 2022 or later (with C++ workload)
- .NET 10.0 SDK

## License

This project uses [WindowsToast.h](https://github.com/mmozeiko/TwitchNotify/) for Windows Runtime toast notification support.

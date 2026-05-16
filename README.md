# Criminal Drug Lord City

Native 3D game starter for a crime-sim sandbox with:

- C# runtime executable
- Unity-ready project scaffold
- C++ structs and offset schemas
- 4096x2056 terrain map metadata
- first-person and third-person camera support
- arsenal room weapon pickups
- NPC vehicle spawns
- seasonal DLC manifest support

## Run

```powershell
dotnet run --project .\CriminalDrugLordCity.Game
```

## Build Executable

```powershell
dotnet publish .\CriminalDrugLordCity.Game -c Release -r win-x64 --self-contained false
```

Published output lands under:

`C:\Users\ghost\Documents\Codex\2026-05-15\hey-yo-codex-let-s-make\CriminalDrugLordCity.Game\bin\Release\net10.0\win-x64\publish`

## Open In Unity

Open the workspace root in Unity Hub. Unity scaffolding lives in:

- `Packages/`
- `ProjectSettings/`
- `Assets/`

Unity-specific setup notes are in:

`C:\Users\ghost\Documents\Codex\2026-05-15\hey-yo-codex-let-s-make\Assets\README_UnitySetup.md`

Current Unity target:

`Unity 6.4 (6000.4.7f1)`

# Unity Setup

Open this folder in Unity Hub as a project.

## Recommended Editor

- Unity `6.4 (6000.4.7f1)`.

## After Opening

1. Let Unity import packages.
2. Create a new empty scene or use `Assets/Scenes/Main.unity` as the bootstrap placeholder.
3. Create an empty GameObject named `GameBootstrap`.
4. Add `CriminalDrugLordCity.Bootstrap.GameBootstrap`.
5. Assign:
   - `Assets/StreamingAssets/maps/metro_city_4096x2056.json`
   - `Assets/StreamingAssets/dlc/season_manifest.json`
6. Create a `Main Camera` and add `CriminalDrugLordCity.Gameplay.SimpleCameraController`.
7. Point the camera controller `target` to the generated `PlayerBlockout` after first play, or replace it with a proper player prefab.

## Notes

- The current Unity setup is a scaffold, not a finished scene file.
- JSON content mirrors the native prototype's map, DLC, vehicle, and pickup data.
- `native/` still contains the C++ offset and struct contract for future interop work.
- This project now targets Unity 6 and uses a minimal package manifest to reduce version migration issues.

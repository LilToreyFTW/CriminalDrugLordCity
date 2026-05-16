# Criminal Drug Lord City Architecture

## Stack

- Runtime: C# `net10.0` desktop executable using OpenTK.
- Native systems contract: C++ headers and offsets schema under `native/`.
- Content: JSON-driven maps, vehicle spawns, arsenal rooms, pickups, and DLC manifests.

## Module Boundaries

- `Runtime/CriminalDrugLordCityWindow.cs`: main loop, camera, input, player state.
- `Runtime/SimpleRenderer.cs`: terrain, vehicles, arsenal room, pickups, and humanoid rendering.
- `Runtime/GameContent.cs`: content loading for maps and seasonal DLC.
- `Content/maps`: each large map carries its own dimensions, spawn offsets, pickups, and vehicle spawns.
- `native/include`: C++ structs for map metadata and asset offset tables.
- `runtimeOffsets`: shared layout table for component, camera, player, and transform offsets.

## Gameplay Slice

- First-person and third-person camera toggle with `V`.
- Walk/fly controls with `WASD`, `Space`, `LeftShift`.
- Arsenal room pickups using `E`.
- NPC-style parked cars represented as stealable spawn archetypes for the prototype.

## Next Production Steps

- Replace placeholder cube characters with authored GLB humanoids and skeletal animation.
- Add road graphs, traffic AI, and actual vehicle possession logic.
- Stream terrain tiles instead of rendering a coarse macro-grid.
- Build the native C++ library once MSVC or CMake is installed in the environment.

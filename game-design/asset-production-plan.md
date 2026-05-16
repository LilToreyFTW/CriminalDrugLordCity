# Asset Production Plan

## Production Objective

Replace placeholder content with an original 4K high-poly environment and prop set that supports the current gameplay loop: traversal, deals, interiors, vehicles, police pressure, pickups, and progression.

## Priority 1: Hero Environment Kit

These assets will change the feel of the game fastest.

1. Modular street storefront kit
Includes facade variants, roll-up shutters, doors, windows, awnings, AC units, roof edges, signage mounts, and service alleys.

2. Modular warehouse kit
Includes loading docks, beams, doors, skylights, cages, office inserts, mezzanines, pipes, pallet bays, and breaker rooms.

3. Garage and workshop kit
Includes concrete bays, lifts, hose reels, tool walls, pegboards, parts shelves, drain channels, and fluorescent fixtures.

4. Apartment interior kit
Includes kitchen units, bathroom modules, hallway doors, cheap furniture, clutter surfaces, blinds, and utility panels.

## Priority 2: Hero Props

1. Money counting station
2. Vacuum-sealed package props
3. Digital scale set
4. Burner phone family
5. Security camera family
6. Steel shelving set
7. Worktables and chemistry-adjacent lab furniture
8. Warehouse pallets, bins, and wrapped cargo
9. Lockers, safes, and document boxes
10. Original district signage and billboard family

## Priority 3: Vehicles

1. Rusted compact sedan
2. Civilian hatchback
3. Cargo van
4. Contractor pickup
5. Premium SUV
6. Unmarked enforcement sedan

Each vehicle should include:

- Exterior high-poly
- Clean low-poly game mesh
- Separate interior shell
- Damage decal masks
- LOD0 to LOD2
- Wheel, glass, light, and door material separation

## Priority 4: Character Sets

1. Player base body
2. Dealer archetype set
3. Civilian archetype set
4. Security archetype set
5. Police archetype set

Each character needs:

- Neutral body mesh
- Clothing variants by district
- 4K skin and fabric materials for hero sets
- Hair cards or groom solution
- Rig-ready topology

## Priority 5: World Dressing

1. Sidewalk clutter pack
2. Utility infrastructure pack
3. Rooftop mechanical pack
4. Port logistics pack
5. Retail clutter pack
6. Luxury district landscape pack

## Technical Targets

- Hero props under 80k triangles before LOD generation
- Hero vehicles under 150k triangles at LOD0
- Modular architecture optimized for instancing
- PBR textures in linear workflow
- Naming: `nd_[category]_[asset]_[variant]`
- Export: `.fbx` or `.glb` with pivot at logical placement point

## First Milestone Deliverable

Ship the `Breakwater Yard` slice with:

- 1 street block
- 1 garage interior
- 1 warehouse exterior/interior shell
- 2 drivable vehicles
- 25 prop pieces
- 5 signage pieces
- 1 police vehicle

That gives the game an original vertical slice without needing the whole city at once.

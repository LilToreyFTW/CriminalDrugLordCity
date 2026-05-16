# Implementation Plan - CriminalDrugLordCity Visual Foundation

This plan outlines the steps to build the visual foundation for CriminalDrugLordCity, a gritty stylized low-poly crime-sim.

## 1. Core Art Bible
- [ ] Create `CriminalDrugLordCity_ArtBible.md` with color palette, lighting rules, material rules, and modularity guidelines.

## 2. Stylized Materials
- [ ] Create a base set of URP Stylized Materials.
- [ ] Generate tileable textures for asphalt, brick, concrete, and metal.
- [ ] Set up emission materials for neon and streetlights.

## 3. Modular Asset Kit
- [ ] Create basic low-poly meshes for road pieces, walls, and common props.
- [ ] Convert meshes into Prefabs with the `CDL_` prefix.
- [ ] Organize prefabs into the `Art/Prefabs` folder.

## 4. Lighting & Atmosphere
- [ ] Create 3 Lighting Preset ScriptableObjects (Sunset, Midnight, Morning).
- [ ] Set up Global Volume for URP Post-Processing (Bloom, Vignette, etc.).

## 5. Visual Prototype Scene
- [ ] Create `CriminalDrugLordCity_VisualPrototype` scene.
- [ ] Layout a city block with a street, alley, warehouse, apartment, garage, and shop.
- [ ] Decorate with props (dumpsters, crates, signs).
- [ ] Set up interior "zones" for hideout, warehouse, and garage.

## 6. UI Visual Direction
- [ ] Create UI style guidelines in the Art Bible.
- [ ] (Optional) Generate placeholder UI sprites.

## 7. Validation
- [ ] Verify scene layout and scale using screenshots.
- [ ] Ensure FP/TP camera readability.

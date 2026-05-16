# CriminalDrugLordCity Art Bible - 4K High Poly Edition

## Core Vision
A high-fidelity, gritty, photorealistic urban crime-sim. Transitioning from low-poly to high-poly with 4K textures, realistic PBR materials, and cinematic lighting.

## Color Palette
- **Asphalt/Concrete:** #1A1A1A, #333333 (Deep, weathered)
- **Bricks/Walls:** #4D2B2B, #2E3B44 (Aged, industrial)
- **Criminal/Neon:** #E63900 (High Heat), #00CC7A (Laundered Money), #6A1B9A (Syndicate)
- **UI:** Matte Black (#0D0D0D), Forest Green (#2E3B23), Gold (#C5B358), Crimson (#8B0000)

## Lighting Style
- **Cinematic & Moody:** High-resolution soft shadows, realistic light falloff.
- **SSAO & GI:** Heavy focus on Ambient Occlusion and Global Illumination for depth.
- **Sunset Hustle:** Deep amber and violet gradients, high-intensity golden hour highlights.
- **Midnight Deal:** Ultra-dark blues, sharp neon reflections, volumetric fog.

## Material Rules (4K PBR)
- **Textures:** All primary surfaces use 4K (4096x4096) textures.
- **PBR Maps:** Full support for Albedo, Normal, Metallic, and Roughness maps.
- **Grunge:** Procedural grime, oil stains, and micro-cracks to maximize fidelity.

## Building & Prop Rules
- **High Poly:** Silhouettes are complex and detailed. No sharp low-poly edges.
- **Interiors:** Highly detailed environments with micro-props (scattered shell casings, realistic trash, detailed furniture).

## Street Layout Rules
- **Realistic Urbanism:** Irregular cracks, puddles (using SSR/Reflections), and organic street clutter.

## UI Style
- **Minimalist Luxury:** Sleek, high-contrast, sharp typography with subtle metallic gradients.

## Naming Conventions
- **Prefabs:** `CDL_HD_Prop_Name`, `CDL_HD_Build_Wall_4x4`.
- **Materials:** `M_CDL_HD_Concrete`, `M_CDL_HD_Neon`.
- **Textures:** `T_CDL_HD_Asphalt_4K_A`, `T_CDL_HD_Asphalt_4K_N`.

## Lighting Style
- **Gritty & Contrast-Heavy:** Deep shadows, strong highlights from artificial sources.
- **Sunset Hustle:** Orange/Purple sky, warm shadows.
- **Midnight Deal:** Cool blues, high-contrast neon, foggy atmosphere.
- **Dirty Morning:** Desaturated, pale gray/blue, low contrast.

## Material Rules
- **Low Poly:** No high-frequency normal maps. Use simple gradients and flat colors with subtle grunge.
- **Stylized Shaders:** Use URP Lit or Simple Lit. Keep specular highlights sharp or disabled for a matte look.
- **Decals:** Use for graffiti and stains to break up repetition.

## Building & Prop Rules
- **Modular:** All buildings use a 4x4 or 8x8 meter grid.
- **Simplicity:** Focus on silhouettes. Avoid tiny details that clutter the view.
- **Interiors:** Readable layouts with clear entry/exit points. Use warm interior lights to contrast with cool exterior streetlights.

## Street Layout Rules
- **Grid-ish:** Mostly right angles but with "organic" alleys and industrial dead ends.
- **Scale:** Roads wide enough for NPC traffic; alleys narrow for hideouts.


## UI Visual Direction
- **Typography:** Bold Sans-serif (e.g., Inter, Roboto). White or Off-white text.
- **HUD Elements:**
  - **Money:** Top right, Neon Green (#00FF99).
  - **Reputation:** Star-based or bar, Dirty Yellow (#BDB76B).
  - **Heat:** Meter style, Neon Orange to Red gradient.
- **Inventory/Panels:**
  - Semi-transparent dark gray backgrounds (#1A1A1A, 80% alpha).
  - Subtle grunge borders or "scanline" overlays.
- **Interaction Prompts:**
  - Simple box with keybind (e.g., [E] Interact).
- **Icons:** Minimalist, flat, single-color.

## Interior Design Sets
- **Starter Hideout:** Small, cluttered, warm single-point lighting, plywood floor.
- **Production Warehouse:** Large open floor, concrete, industrial overhead lighting, storage racks.
- **Dealer Garage:** Metal focus, oil stains, tool benches, cool fluorescent lighting.

## Camera Guidelines
- **First-Person:** Ensure ceiling height is at least 3m to avoid claustrophobia. Doors at 2.2m.
- **Third-Person:** Buildings should have wide doorways and spacious alleys to prevent camera clipping. Use clear silhouettes for interaction points.


## Naming Conventions
- **Prefabs:** `CDL_Prop_Name`, `CDL_Build_Wall_4x4`, `CDL_Road_Straight`.
- **Materials:** `M_CDL_Concrete`, `M_CDL_Neon_Red`.
- **Textures:** `T_CDL_Asphalt_Base`, `T_CDL_Asphalt_Normal`.

3.0.1 (November 4th 2024)
This version also requires updating the Dynamic Effects extension to v3.0.1

Added:
- Intersection Foam, parameter to control the ripple speed separately

Changed:
- Render target inspector is now functional again

Fixed:
- Dynamic Effects extension not installed automatically
- Point/spot lights no longer affecting the water after a certain distance
- Incorrect configuration on Waterfall particle prefabs

3.0.0 (October 31st 2024)

What's new?
• Rewritten rendering code for Render Graph support
• Revamped wave animations, allowing for various types of waves
• Height Pre-pass, allows other shaders to read out the water surface height.
• GPU-based height query system, making rivers and Dynamic Effects readable
• Water decals, snaps textures onto the water (oil spills, weeds, targeting reticules)
• Improved wave crest foam shading (min/max range + bubbles)
• Ocean mesh component, 8x8km mesh with gradual vertex density
• Improved support for RigidBodies for the Align To Water component
• Waterfall prefabs (3 sizes)

Added:
- Option on shader to disable point/spot light: caustics & translucency
- Waterfall prefabs (mesh, material + particles)
- Support for the Waves feature on rivers
- Align Transform To Water component now better handles RigidBodies

Changed:
- Directional Caustics is now a per-material option
- Screen-space Reflections is now a per-material option
- Sharp and Smooth intersection foam styles are now merged into one feature
- "Align Transform To Waves" is now called "Align Transform To Water"

Removed:
- Integration for Dynamic Water Physics 2 (now deferred to author)
- Non-exponential Vertical Depth (deemed unused) option
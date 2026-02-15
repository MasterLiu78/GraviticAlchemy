# Acoustic Gravitic Universe Simulator – Core Spec

Goal: A Windows 11 64-bit desktop app that visualises the universe from the perspective of Acoustic Gravitic Theory (AGT), not General Relativity or ΛCDM.

Key physics framing:
- Universe is a bounded medium created by a cavitation event in a water-like field.
- This creates an ionised plasma cavity (firmament) bounded by “water walls”.
- Gravity = acoustic / pressure effect in a fluid or plasma medium (Bjerknes-style forces, magnetosonic waves, plasma filaments), not spacetime curvature.
- Causal chain: creation event → plasma instabilities → cosmic web filaments → galaxies and stars → waves → local gravity on Earth via vertical pressure gradients.

Visual / interaction requirements (high level):
- 3D camera with orbit + zoom, continuous from:
  - full bounded universe view (visible outer walls + cosmic web),
  - down through filaments / clusters / galaxies / solar system,
  - to Earth scale (Earth + atmosphere + magnetosphere).
- Left-hand info panel:
  - When mouse hovers over an object (filament, node, galaxy, star, planet, local Earth region) show:
    - object name + type + scale level
    - local medium properties: density [kg/m³], temperature [K], ionisation fraction
    - local wave properties: dominant frequencies [Hz], amplitudes, energy density
    - derived AGT values: effective g [m/s²], indicative Bjerknes-like force measure, resonance info.
- Wavelength mode:
  - Control in nanometres, with presets (radio/microwave/IR/visible/UV/X-ray/gamma).
  - Changing wavelength changes what is emphasised visually (e.g. large filaments in radio, jets in X-ray) but uses the same underlying fields.

Architecture (conceptual, not implementation language):
- Physics / numerical model:
  - Medium parameters per scale/region (density, temperature, ionisation).
  - WaveField with long-wave (large-scale structure) + short-wave (local gravity-like) components.
  - EarthGravityModel that computes g from vertical pressure/wave gradients.
- Rendering:
  - FilamentNetwork that draws a cosmic web (nodes + filaments + jets).
  - Multiscale LOD so zoom feels continuous.
- UI:
  - Main 3D viewport.
  - Left info panel (hover details).
  - Wavelength controls + scenario presets.
- Configuration:
  - At least two presets:
    - Baseline AGT universe.
    - ΛCDM-style comparison (visual only; still a medium, not spacetime).

Numerical requirements (approximate but consistent):
- Use physically named units:
  - density: kg/m³
  - temperature: K
  - frequency: Hz
  - wavelength: m or nm
  - pressure: Pa
  - acceleration: m/s²
- Equations can be simplified but must be dimensionally consistent and commented.

Target tech stack:
- Prefer Unity + C#:
  - Unity 3D project in this repo.
  - C# scripts for:
    - UniverseManager (scale & camera control),
    - MediumParameters / WaveField,
    - FilamentNetwork,
    - EarthGravityModel,
    - UI controllers for info panel and wavelength modes.
  - Clear instructions for building a Windows .exe in README.md for non-programmers.

Non-programmer requirement:
- README.md must explain, step by step, for Windows 11:
  - what to install (Unity Hub + specific Unity version),
  - how to open the project,
  - how to hit Play in the editor,
  - how to build a Windows .exe,
  - how to run, move the camera, hover, and change wavelength/presets.

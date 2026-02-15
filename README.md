# Gravitic Alchemy (AGT Universe Prototype)

This repository contains Unity scripts for an early **Acoustic Gravitic Theory** universe viewer prototype.

## Open the project in Unity

1. Install **Unity Hub**.
2. In Unity Hub, install a 3D-capable editor version (Unity 2022 LTS or newer).
3. Click **Add** in Unity Hub and select this folder: `GraviticAlchemy`.
4. Open the project.
5. Open the main scene from **Project** window:
   - `Assets/Scenes/Main.unity` (create this scene if it does not exist yet, then attach the scripts below).
6. Click **Play**.

## Scene wiring (Step 2 scripts)

Add the following scripts to your scene:
- `UniverseManager` on a manager object (assign `Universe Root`, `Earth Root`, and targets).
- `FilamentNetwork` on a universe root object to generate procedural nodes/filaments.
- `InfoPanelController` on a UI manager object and link a left-panel `Text` component.
- `HoverInfo` is attached automatically to generated filament objects and can be added manually to Earth objects.

## Camera + scale controls

- **Orbit camera:** hold **Right Mouse Button** and move mouse.
- **Zoom:** use **Mouse Wheel**.
- **Switch scales:**
  - Press **1** for Universe view.
  - Press **2** for Earth view.
- **Auto switch thresholds:** zooming in past the Earth threshold enters Earth view; zooming out past universe threshold returns to universe view.

## Hover info panel

Move the mouse over generated cosmic web nodes/filaments (or any object with `HoverInfo`) to display:
- object name,
- object type,
- scale level,
in the left-hand panel.

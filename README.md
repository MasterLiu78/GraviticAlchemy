# GraviticAlchemy (Unity Scaffold)

This repository now contains **Step 1 scaffolding** for a Unity-based project.
It includes a Unity project folder layout and placeholder C# scripts only (no full physics or rendering implementation yet).

## Unity version (Windows 11)

Install **Unity 2022.3 LTS** (recommended: **2022.3.42f1**) with Unity Hub on Windows 11.

> Why this version: it is an LTS release and matches the `ProjectSettings/ProjectVersion.txt` in this repository.

## Open this project in Unity Hub

1. Install Unity Hub.
2. In Unity Hub, go to **Projects**.
3. Click **Open** (or **Add project from disk** depending on Hub version).
4. Select this repository folder: `GraviticAlchemy`.
5. If prompted, install/use Unity Editor **2022.3.42f1**.
6. Open the project.

## Press Play in the Unity Editor

1. Wait for Unity to import assets and compile scripts.
2. Open or create a scene in the editor.
3. Click the **Play** button at the top center of the Unity Editor.
4. Click **Play** again to stop.

## Included Step 1 scripts (empty scaffolding)

- `UniverseManager`
- `MediumParameters`
- `WaveField`
- `FilamentNetwork`
- `EarthGravityModel`
- `InfoPanelController`
- `WavelengthController`

All scripts currently contain empty classes inheriting from `MonoBehaviour`.

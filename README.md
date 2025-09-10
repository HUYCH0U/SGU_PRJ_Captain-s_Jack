### Introduction
**Captain's Jack** is a 2D casual platformer game developed using Unity and the C# programming language. This is a project for the C# Programming course by a group of students from the Faculty of Information Technology, Saigon University.

The game draws inspiration from:
- **Hollow Knight**: Character design, enemies, and control mechanics.
- **Mario**: Level design, environments, and obstacles.

Players control the main character to overcome challenges, defeat enemies, collect items, and upgrade stats to defeat the final boss in Chapter 3. The game features simple gameplay and a user-friendly interface, suitable for casual players.

### Key Features
#### Gameplay
- Overcome obstacles, defeat enemies, and collect special items to progress through levels.
- Use collected coins to upgrade stats (via an in-game shop).
- Checkpoint system: Upon death, players respawn at the nearest checkpoint but lose all items and half their coins.
- Game completion: Defeat the final boss in Chapter 3.

#### Controls
- Move left/right: A/D keys.
- Jump: Spacebar.
- Melee attack (sword): Left mouse button.
- Ranged sword throw: Right mouse button.
- Interact with environment: E key.
- Use items: Keys 1-3.

#### Graphics and Sound
- Sprites for characters, enemies, environments, and UI sourced from open-source platforms.
- Optimized 2D textures (Sprite Mode: Single/Multiple, Pixels Per Unit: 16, Compression: None to preserve original quality).
- Parallax technique used for backgrounds to create depth.

#### Game Structure
- 3 Chapters with progressively challenging levels.
- Diverse enemies and engaging boss fights.
- UI system: Health, coins, items, and pause menu.

### Technologies Used
- **Engine**: Unity 2022.3.15f1 (Template: 2D Core).
- **Programming Language**: C#.
- **Unity Tools**: Scene View, Game View, Hierarchy, Inspector, Project, Console, Animator, Animation, Timeline, Tile Palette.
- **Libraries/Assets**: Open-source sprites; no third-party packages used (all code written from scratch).
- **Optimization**: Textures not compressed to avoid color distortion; Mesh Type: Tight, Filter Mode: Point for sharp visuals.

### Installation and Setup
#### System Requirements
- Unity Hub and Unity Editor version 2022.3.15f1 or higher.
- Operating System: Windows/Mac/Linux (tested on Windows).
- Git (for cloning the repository).

#### Steps to Run the Project
1. Clone the repository:
   ```
   git clone https://github.com/HUYCH0U/SGU_PRJ_Captain-s_Jack.git
   ```
2. Open Unity Hub, add the project from the cloned folder.
3. Open the project in Unity Editor.
4. Run the main scene (typically MainMenu or Level1 in the Scenes folder).
5. Build the game (if needed): File > Build Settings > Select platform (PC/Mac/WebGL) > Build.

**Note**: If you encounter asset or package errors, verify the Unity version. The project does not require additional packages beyond Unity's core.

### Team Members
- **Instructor**: Dr. Bùi Tiến Lên
- **Students**:
  - Châu Gia Huy (3122411061) - Leader, Gameplay Design & Main Coder.
  - Trần Minh Quân (3122411170) - Enemy AI & Level Coding.
  - Đặng Quốc Đông Quân (3122411165) - UI/UX & Sound.
  - Lê Đức Anh (3122411005) - Optimization & Testing.

### Evaluation and Limitations
- **Strengths**: Simple yet engaging gameplay, appealing pixel art graphics, clean C# code with features like movement, combat, and inventory systems.
- **Limitations**: No multiplayer support, limited sound variety, potential for further performance optimization on low-end devices.
- **Future Development**: Add new chapters, controller support, Steam achievements integration, and smarter enemy AI.

### References
- Detailed project documentation: Project PDF (includes design, code analysis, and evaluation).
- Unity Documentation: Unity Manual.
- Sprite sources: Free assets from itch.io, OpenGameArt (no copyright violations).

### License
This project is released under the MIT License. See the LICENSE file for details.

If you have feedback or issues, please open an Issue on GitHub! Thank you for your interest in **Captain's Jack**.

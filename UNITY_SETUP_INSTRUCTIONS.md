# Unity Project Setup Instructions - Task 01

## Current Status
✅ Folder structure created  
✅ FirstPersonController.cs script implemented  
✅ .gitignore configured  
⏳ Unity project needs to be created manually  

## Step 1: Create Unity Project

### 1.1 Open Unity Hub
1. Launch Unity Hub application
2. Click **"New Project"** button

### 1.2 Configure Project Settings
- **Template**: Select **"3D"** (NOT 3D URP or HDRP)
- **Project Name**: `LLM_StoryGame`
- **Location**: `/Users/manaskandimalla/Desktop/Projects/SeriousProjects/indie-game`
- Click **"Create Project"**

**IMPORTANT**: Unity will create the project in a subfolder. You need to:
1. Let Unity create the initial project
2. Then move the Unity-generated files (Assets/, ProjectSettings/, Packages/) to the existing indie-game folder
3. Or create the project directly in the indie-game folder if Unity Hub allows

## Step 2: Verify Folder Structure

After Unity creates the project, verify these folders exist in `Assets/`:

```
Assets/
├── Scripts/
│   ├── Core/
│   ├── LLM/
│   ├── Player/           ← FirstPersonController.cs is here
│   ├── NPC/
│   ├── World/
│   ├── Narrative/
│   ├── UI/
│   ├── Audio/
│   └── Utilities/
├── Data/
│   ├── Prompts/
│   └── Saves/
├── Scenes/
├── Prefabs/
└── Resources/
```

## Step 3: Configure Project Settings

### 3.1 Player Settings
1. Go to `Edit > Project Settings`
2. Select **Player** section
3. Set:
   - **Company Name**: Your name
   - **Product Name**: "LLM Story Game"

### 3.2 Quality Settings
1. In Project Settings, select **Quality**
2. Set default quality level to **"Medium"** or **"High"**

### 3.3 Input Manager (Verify)
1. In Project Settings, select **Input Manager**
2. Verify these axes exist (should be default):
   - Horizontal (A/D, Left/Right arrows)
   - Vertical (W/S, Up/Down arrows)
   - Mouse X
   - Mouse Y
   - Jump (Space)

## Step 4: Create Main Game Scene

### 4.1 Create Scene
1. In Unity, go to `File > New Scene`
2. Select **"Basic (Built-in)"** template
3. Save as `MainGame` in `Assets/Scenes/`

### 4.2 Set Up Player GameObject

#### Create Player Object
1. In Hierarchy, right-click → **Create Empty**
2. Name it **`Player`**
3. In Inspector, click the gear icon next to Transform → **Reset**
   - Position: (0, 0, 0)
   - Rotation: (0, 0, 0)
   - Scale: (1, 1, 1)

#### Add CharacterController Component
1. With Player selected, click **Add Component** in Inspector
2. Search for and add **Character Controller**
3. Configure CharacterController:
   - **Height**: 1.8
   - **Radius**: 0.4
   - **Center**: X=0, Y=0.9, Z=0

#### Add FirstPersonController Script
1. With Player still selected, click **Add Component**
2. Type **"FirstPersonController"**
3. Select the script (it should appear if the script is in Assets/Scripts/Player/)
4. Verify the script is attached and shows its public fields

### 4.3 Create Camera

#### Create Camera Object
1. Right-click on **Player** in Hierarchy → **Create Empty**
2. Name it **`Camera`**
3. Set Transform:
   - **Position**: X=0, Y=0.6, Z=0 (eye height)
   - **Rotation**: (0, 0, 0)
   - **Scale**: (1, 1, 1)

#### Add Camera Component
1. With Camera selected, click **Add Component**
2. Add **Camera** component
3. Configure Camera:
   - **Clear Flags**: Skybox
   - **Field of View**: 60
   - **Near Clipping Plane**: 0.1
   - **Far Clipping Plane**: 1000

**IMPORTANT**: The Camera must be a **child** of the Player GameObject for the controller to work!

### 4.4 Create Test Environment

#### Ground Plane
1. Right-click in Hierarchy → **3D Object → Plane**
2. Name: **`Ground`**
3. Transform:
   - **Position**: X=0, Y=-1, Z=0
   - **Rotation**: (0, 0, 0)
   - **Scale**: X=5, Y=1, Z=5 (creates 50x50 unit ground)

#### Test Obstacles
**Obstacle 1:**
1. Right-click in Hierarchy → **3D Object → Cube**
2. Name: **`Obstacle1`**
3. Transform:
   - **Position**: X=5, Y=0, Z=5
   - **Scale**: X=2, Y=3, Z=2

**Obstacle 2:**
1. Create another Cube: **`Obstacle2`**
2. Transform:
   - **Position**: X=-5, Y=0, Z=5
   - **Scale**: X=1, Y=4, Z=1

**Obstacle 3:**
1. Create another Cube: **`Obstacle3`**
2. Transform:
   - **Position**: X=0, Y=0, Z=10
   - **Scale**: X=8, Y=2, Z=1

#### Lighting
1. Verify **Directional Light** exists in scene (should be default)
2. If not: Right-click → **Light → Directional Light**
3. Set Rotation: X=50, Y=-30, Z=0 (for nice shadows)

#### Adjust Player Start Position
1. Select **Player** in Hierarchy
2. Set Position: X=0, Y=0, Z=0 (on the ground)

### 4.5 Configure Scene Settings
1. Go to `Window > Rendering > Lighting`
2. In **Environment** tab:
   - **Skybox Material**: Default-Skybox (should be default)
   - **Sun Source**: Directional Light
3. Close Lighting window

## Step 5: Test the Controller

### 5.1 Enter Play Mode
1. Click the **Play button (▶)** at top of Unity Editor
2. The scene should start and cursor should be locked

### 5.2 Test Movement
- **W/A/S/D** - Player should move forward/left/back/right
- **Hold Shift** while moving - Player should move faster (sprint)
- **Move mouse** - Camera should look around smoothly
- **Press Space** - Player should jump
- **Walk into obstacles** - Player should collide (not pass through)

### 5.3 Verify Behavior
- ✅ Camera should not flip upside down when looking up/down
- ✅ Cursor should be locked and hidden
- ✅ Movement should feel smooth and responsive
- ✅ Jumping should work only when on ground
- ✅ ESC key should unlock cursor

### 5.4 Exit Play Mode
Click the **Play button** again to stop

## Step 6: Troubleshooting

### Player falls through ground
**Solution**: Ensure Ground plane has a Collider component (should be automatic with Plane)

### Can't move
**Solution**: 
- Check Input Manager has Horizontal/Vertical axes
- Verify FirstPersonController script is attached to Player
- Check Console for errors

### Camera doesn't move
**Solution**: 
- Verify Camera is a **child** of Player GameObject
- Ensure Camera has Camera component
- Check FirstPersonController script is on Player (not Camera)

### Console errors
**Solution**: Read error messages carefully and fix missing components

## Step 7: Git Commit

Once everything is working:

```bash
cd /Users/manaskandimalla/Desktop/Projects/SeriousProjects/indie-game
git add .
git commit -m "Task 1: Project setup and first-person controller implementation"
git push origin main
```

## Acceptance Criteria Checklist

Before moving to Task 2, verify:

- [ ] Unity project created with proper folder structure
- [ ] FirstPersonController.cs in Assets/Scripts/Player/
- [ ] Player GameObject with CharacterController and FirstPersonController
- [ ] Camera as child of Player at eye height
- [ ] Test scene with ground and obstacles
- [ ] Player can move with WASD in all directions
- [ ] Player can sprint with Shift key
- [ ] Player can jump with Space key
- [ ] Mouse look works smoothly without gimbal lock
- [ ] Camera vertical rotation clamped to ±90 degrees
- [ ] Cursor locked and hidden during gameplay
- [ ] No console errors when playing
- [ ] Git commit made and pushed

## Next Steps

Once all acceptance criteria are met:
**Proceed to Task 2: LLM Integration Layer**

---

**Estimated Time for Task 1**: 2-4 hours  
**Status**: Ready for Unity project creation

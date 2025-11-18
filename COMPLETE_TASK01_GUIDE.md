# Complete Task 01 Setup Guide - Step by Step

## 🎯 Goal
Get a working first-person controller in Unity where you can move around with WASD, sprint, jump, and look with the mouse.

---

## 📋 Current Status Check

First, let's figure out where you are:

### Option A: You Ran the Auto Setup Script
If you ran `Tools > Task 01 > Auto Setup Scene`:
1. **Go to Unity**
2. **Run the diagnostic**: `Tools > Task 01 > Diagnose Setup`
3. **Read the Console** (bottom panel) - it will tell you exactly what's wrong
4. **Follow the fixes** it suggests
5. **Run diagnostic again** until it says "ALL CHECKS PASSED"

### Option B: You Haven't Run Anything Yet
Follow the **Quick Start** below.

### Option C: Something's Broken
Follow the **Manual Setup** below.

---

## 🚀 Quick Start (2 Minutes)

### Step 1: Open Unity Project
1. Open Unity Hub
2. Click on **"My project"**
3. Wait for Unity to load (30 seconds)

### Step 2: Run Auto Setup
1. In Unity menu bar: **Tools → Task 01 → Auto Setup Scene**
2. Wait for popup: "Setup Complete!"
3. Click **OK**

### Step 3: Run Diagnostic
1. In Unity menu bar: **Tools → Task 01 → Diagnose Setup**
2. Check Console (bottom panel)
3. If it says "ALL CHECKS PASSED ✅" → Go to Step 4
4. If it shows errors → Fix them (it tells you how), then run diagnostic again

### Step 4: Test It
1. Click **Play button (▶)** at top of Unity
2. Test controls:
   - **W/A/S/D** - Move
   - **Shift** - Sprint (hold while moving)
   - **Space** - Jump
   - **Mouse** - Look around
3. Click **Play** again to stop

**If it works**: You're done! Skip to "Finishing Up" section.

**If it doesn't work**: Continue to Manual Setup below.

---

## 🔧 Manual Setup (15 Minutes)

If the automated setup didn't work, follow these steps exactly:

### Step 1: Open or Create Scene

**Option A - If MainGame.unity exists**:
1. In Unity: **Tools → Task 01 → Open MainGame Scene**
2. OR: In Project panel (bottom) → Navigate to `Assets/Scenes/` → Double-click `MainGame.unity`

**Option B - Create new scene**:
1. **File → New Scene**
2. Select **"Basic (Built-in)"**
3. Click **Create**
4. **File → Save As...**
5. Navigate to `Assets/Scenes/`
6. Name: **`MainGame`**
7. Click **Save**

---

### Step 2: Create Player GameObject

**In Hierarchy panel (left side of Unity)**:

1. **Right-click** in empty space
2. Select **Create Empty**
3. Name it: **`Player`** (exactly, capital P)
4. **Select Player** (click on it)
5. **In Inspector panel (right side)**:
   - Find **Transform** component at top
   - Click the **gear icon** (⚙️) next to "Transform"
   - Click **Reset**
   - Verify Position is (0, 0, 0)

---

### Step 3: Add CharacterController Component

**With Player still selected**:

1. **In Inspector panel**, scroll to bottom
2. Click **Add Component** button
3. Type: **`character controller`** (search box appears)
4. Click **Character Controller** from the list
5. **Configure it** (in Inspector):
   - Find **Height** field → Set to: **1.8**
   - Find **Radius** field → Set to: **0.4**
   - Find **Center** → Set to: **X=0, Y=0.9, Z=0**

**Visual check**: You should see a green capsule outline around Player in Scene view.

---

### Step 4: Add FirstPersonController Script

**With Player still selected**:

1. Click **Add Component** again
2. Type: **`firstperson`**
3. You should see **"First Person Controller"** in the list
4. Click it

**If script doesn't appear**:
- Check Console (bottom) for red errors
- If you see errors, tell me what they say
- The script should be at: `Assets/Scripts/Player/FirstPersonController.cs`

**After adding, verify in Inspector**:
You should see "First Person Controller (Script)" with these fields:
- Walk Speed: 5
- Sprint Speed: 8
- Jump Force: 5
- Gravity: -9.81
- Mouse Sensitivity: 2
- Max Look Angle: 90

---

### Step 5: Create Camera (CRITICAL - Must Be Child of Player!)

**This is the most important step - Camera MUST be a child of Player**:

1. **In Hierarchy panel**, find **Player**
2. **Right-click directly ON "Player"** (not in empty space!)
3. Select **Create Empty**
4. Name it: **`Camera`** (exactly, capital C)
5. **Verify Camera is indented under Player**:
   ```
   Player
     └─ Camera    ← Should look like this (indented)
   ```

**If Camera is NOT indented**:
- Delete Camera
- Try again, making sure to right-click ON Player

**Configure Camera**:

1. **Select Camera** (click on it)
2. **In Inspector**, set Transform:
   - **Position**: X=0, Y=**0.6**, Z=0
   - **Rotation**: X=0, Y=0, Z=0
3. Click **Add Component**
4. Type: **`camera`**
5. Select **Camera** component
6. **Configure Camera component**:
   - **Clear Flags**: Skybox
   - **Field of View**: 60
   - **Near**: 0.1
   - **Far**: 1000

---

### Step 6: Create Ground

1. **In Hierarchy**, right-click in empty space
2. **3D Object → Plane**
3. Name it: **`Ground`**
4. **In Inspector**, set Transform:
   - **Position**: X=0, Y=**-1**, Z=0
   - **Rotation**: X=0, Y=0, Z=0
   - **Scale**: X=**5**, Y=1, Z=**5**

**Visual check**: You should see a large flat plane below Player in Scene view.

---

### Step 7: Create Test Obstacles

**Obstacle 1**:
1. Right-click → **3D Object → Cube**
2. Name: **`Obstacle1`**
3. Transform:
   - **Position**: X=**5**, Y=0, Z=**5**
   - **Scale**: X=**2**, Y=**3**, Z=**2**

**Obstacle 2**:
1. Right-click → **3D Object → Cube**
2. Name: **`Obstacle2`**
3. Transform:
   - **Position**: X=**-5**, Y=0, Z=**5**
   - **Scale**: X=**1**, Y=**4**, Z=**1**

**Obstacle 3**:
1. Right-click → **3D Object → Cube**
2. Name: **`Obstacle3`**
3. Transform:
   - **Position**: X=0, Y=0, Z=**10**
   - **Scale**: X=**8**, Y=**2**, Z=**1**

---

### Step 8: Check Lighting

1. **In Hierarchy**, look for **Directional Light**
2. If it exists:
   - Select it
   - Set **Rotation**: X=**50**, Y=**-30**, Z=0
3. If it doesn't exist:
   - Right-click → **Light → Directional Light**
   - Set rotation as above

---

### Step 9: Save Everything

1. **File → Save** (or Cmd+S / Ctrl+S)
2. Verify scene saved to: `Assets/Scenes/MainGame.unity`

---

### Step 10: Final Verification

**Run the diagnostic**:
1. **Tools → Task 01 → Diagnose Setup**
2. Check Console for results
3. Fix any issues it reports
4. Run diagnostic again until it passes

---

## 🎮 Testing

### Before Pressing Play - Visual Checklist

**Look at your Hierarchy** (should look like this):
```
Player
  └─ Camera
Ground
Obstacle1
Obstacle2
Obstacle3
Directional Light
```

**Select Player and check Inspector**:
- ✅ Transform component
- ✅ Character Controller component
- ✅ First Person Controller (Script) component

**Select Camera and check**:
- ✅ Camera is indented under Player
- ✅ Camera component exists
- ✅ Position is (0, 0.6, 0)

### Press Play and Test

1. Click **Play button (▶)** at top
2. **Cursor should lock** (disappear and center)
3. **Test each control**:

| Control | Expected Result |
|---------|----------------|
| **W** | Move forward |
| **S** | Move backward |
| **A** | Move left |
| **D** | Move right |
| **Shift + W** | Move faster (sprint) |
| **Space** | Jump (only works on ground) |
| **Move Mouse** | Camera looks around |
| **Look Up/Down** | Camera tilts (stops at 90°) |

4. **Walk into obstacles** - you should collide, not pass through
5. Click **Play** again to stop

---

## 🐛 Common Problems and Solutions

### Problem: "Nothing happens when I press Play"

**Check Console** (bottom panel) for errors:

**If you see**: "CharacterController component not found"
- **Fix**: Select Player → Add Component → Character Controller

**If you see**: "Camera component not found in children"
- **Fix**: Camera is not a child of Player
- Delete Camera, recreate it by right-clicking ON Player

**If you see**: No errors, but still can't move
- **Fix**: Check Input Manager
- Go to: Edit → Project Settings → Input Manager
- Verify "Horizontal" and "Vertical" axes exist

### Problem: "Player falls through ground"

**Fix**:
- Select Ground in Hierarchy
- In Inspector, verify it has a **Mesh Collider** component
- If not, Add Component → Mesh Collider

### Problem: "Camera doesn't move with mouse"

**Fix**:
- Camera MUST be a child of Player
- In Hierarchy, Camera should be indented under Player
- If not: Drag Camera onto Player to make it a child

### Problem: "Camera flips upside down"

**Fix**:
- This shouldn't happen with our script
- If it does, run diagnostic: Tools → Task 01 → Diagnose Setup
- Check if FirstPersonController script is properly attached

### Problem: "Cursor doesn't lock"

**Fix**:
- This is normal on first Play
- Press Play to stop, then Play again
- Cursor should lock on second attempt
- If not, check Console for errors

### Problem: "Movement is jerky/slow"

**Fix**:
- This is normal in Unity Editor (Editor has overhead)
- To improve: Edit → Project Settings → Quality → Set to "Low"
- Built game will be much smoother

---

## ✅ Success Checklist

Before moving to Task 02, verify:

- [ ] Unity project "My project" opens without errors
- [ ] MainGame scene exists in Assets/Scenes/
- [ ] Player GameObject exists with CharacterController
- [ ] FirstPersonController script attached to Player
- [ ] Camera is child of Player (indented in Hierarchy)
- [ ] Ground and 3 obstacles exist
- [ ] Diagnostic passes: "ALL CHECKS PASSED ✅"
- [ ] Can move with WASD
- [ ] Can sprint with Shift
- [ ] Can jump with Space
- [ ] Can look with mouse
- [ ] No red errors in Console

---

## 🎉 Finishing Up

Once everything works:

### 1. Save Your Work
- **File → Save** (Cmd+S / Ctrl+S)

### 2. Commit to Git

Open Terminal and run:
```bash
cd "/Users/manaskandimalla/Desktop/Projects/SeriousProjects/indie-game"
git add .
git commit -m "Task 1 Complete: Unity project setup with working first-person controller"
git push origin main
```

### 3. Celebrate! 🎉
You've completed Task 01!

---

## 📚 Next Steps

**Proceed to Task 02: LLM Integration Layer**
- Open: `tasks/TASK_02.md`
- This will add AI-powered dialogue to your game

---

## 💬 Still Having Issues?

If you're still stuck, tell me:

1. **What happens when you press Play?**
   - Describe exactly what you see
   - Does anything move?
   - What controls don't work?

2. **What's in the Console?**
   - Copy any red error messages
   - Include the full error text

3. **Run the diagnostic and share results**:
   - Tools → Task 01 → Diagnose Setup
   - Copy the Console output
   - Share it with me

4. **Hierarchy screenshot**:
   - What GameObjects do you see?
   - Is Camera indented under Player?

With this info, I can give you an exact, specific fix!

---

**You've got this! Follow the steps carefully and you'll have it working soon! 🚀**

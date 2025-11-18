# Task 01 Troubleshooting Guide

## What "Not Working" Could Mean

Let me help you diagnose and fix the issue. Please check which scenario matches your situation:

---

## Scenario 1: The Menu Item Doesn't Appear

**Problem**: You don't see "Tools > Task 01" in Unity menu

**Solution**:
1. Check Unity Console (bottom panel) for compilation errors
2. Look for red error messages
3. If you see errors about `UnityEditor`, the script is correct but Unity needs to recompile
4. Try: `Assets > Reimport All` (this takes a few minutes)

---

## Scenario 2: Script Ran But Nothing Happens in Play Mode

**Problem**: Scene was created but player doesn't move when you press Play

**Diagnosis Steps**:

### Step A: Check if Scene Was Created
1. In Unity Project panel (bottom), navigate to `Assets/Scenes/`
2. Do you see `MainGame.unity`?
3. **Double-click it** to open it

### Step B: Check Hierarchy
1. Look at Hierarchy panel (left side)
2. You should see:
   - Player
     - Camera (indented under Player)
   - Ground
   - Obstacle1, Obstacle2, Obstacle3
   - Directional Light

### Step C: Check Player Components
1. Click on **Player** in Hierarchy
2. In Inspector (right side), verify these components exist:
   - Transform
   - Character Controller
   - First Person Controller (Script)

**If any component is missing**, see Manual Setup below.

### Step D: Check Camera Setup
1. Click on **Camera** in Hierarchy
2. Verify:
   - It's **indented under Player** (it's a child)
   - Has Camera component
   - Position is (0, 0.6, 0)

### Step E: Test in Play Mode
1. Click Play button (▶)
2. **Check Console** for errors
3. Try moving with WASD
4. Try looking with mouse

**Common Issues**:
- **Cursor not locked**: Press Play again, cursor should lock
- **Can't move**: Check Console for "CharacterController not found" error
- **Camera doesn't move**: Check Camera is child of Player

---

## Scenario 3: Console Shows Errors

**If you see errors**, please tell me what they say. Common ones:

### Error: "FirstPersonController not found"
**Fix**: The script isn't compiled yet
1. Wait for Unity to finish importing
2. Check for red errors in Console
3. Fix any compilation errors first

### Error: "CharacterController not found"
**Fix**: Component wasn't added
- See Manual Setup below

### Error: "Camera not found in children"
**Fix**: Camera isn't a child of Player
- See Manual Setup below

---

## Manual Setup (If Automated Script Failed)

If the automated script didn't work, here's the **manual step-by-step**:

### 1. Open or Create Scene
- `File > Open Scene` → Select `Assets/Scenes/MainGame.unity`
- OR `File > New Scene` → Save as `MainGame` in `Assets/Scenes/`

### 2. Create Player GameObject

**In Hierarchy panel (left side)**:
1. Right-click → **Create Empty**
2. Name it: **`Player`**
3. In Inspector (right side):
   - Click gear icon next to Transform → **Reset**
   - Position: (0, 0, 0)

### 3. Add CharacterController to Player

**With Player selected**:
1. In Inspector, click **Add Component**
2. Type: **"Character Controller"**
3. Click to add it
4. Set these values:
   - **Height**: 1.8
   - **Radius**: 0.4
   - **Center**: X=0, Y=0.9, Z=0

### 4. Add FirstPersonController Script to Player

**With Player still selected**:
1. Click **Add Component**
2. Type: **"FirstPersonController"**
3. Select it from the list
4. You should see:
   - Walk Speed: 5
   - Sprint Speed: 8
   - Jump Force: 5
   - Gravity: -9.81
   - Mouse Sensitivity: 2
   - Max Look Angle: 90

**If script doesn't appear**:
- Check Console for compilation errors
- Verify file exists: `Assets/Scripts/Player/FirstPersonController.cs`
- Try `Assets > Refresh`

### 5. Create Camera as Child of Player

**IMPORTANT: Camera must be a CHILD of Player**:

1. **Right-click on Player** in Hierarchy (not in empty space!)
2. Select **Create Empty**
3. Name it: **`Camera`**
4. With Camera selected, set Transform:
   - **Position**: X=0, Y=0.6, Z=0
   - **Rotation**: (0, 0, 0)
5. Click **Add Component** → Add **Camera**
6. Set Camera properties:
   - **Clear Flags**: Skybox
   - **Field of View**: 60
   - **Near**: 0.1
   - **Far**: 1000

**Verify Camera is child**: In Hierarchy, Camera should be **indented under Player**:
```
Player
  └─ Camera    ← Should be indented like this
```

### 6. Create Ground

1. Right-click in Hierarchy → **3D Object → Plane**
2. Name: **`Ground`**
3. Transform:
   - **Position**: X=0, Y=-1, Z=0
   - **Rotation**: (0, 0, 0)
   - **Scale**: X=5, Y=1, Z=5

### 7. Create Test Obstacles

**Obstacle 1**:
1. Right-click → **3D Object → Cube**
2. Name: **`Obstacle1`**
3. Transform:
   - **Position**: X=5, Y=0, Z=5
   - **Scale**: X=2, Y=3, Z=2

**Obstacle 2**:
1. Create another Cube → Name: **`Obstacle2`**
2. Transform:
   - **Position**: X=-5, Y=0, Z=5
   - **Scale**: X=1, Y=4, Z=1

**Obstacle 3**:
1. Create another Cube → Name: **`Obstacle3`**
2. Transform:
   - **Position**: X=0, Y=0, Z=10
   - **Scale**: X=8, Y=2, Z=1

### 8. Verify Directional Light

1. Check Hierarchy for **Directional Light**
2. If it exists, select it and set:
   - **Rotation**: X=50, Y=-30, Z=0
3. If it doesn't exist:
   - Right-click → **Light → Directional Light**
   - Set rotation as above

### 9. Save Scene

- `File > Save` (or Ctrl+S / Cmd+S)
- Verify it saved to `Assets/Scenes/MainGame.unity`

---

## Testing Checklist

After setup (automated or manual), test these:

### Before Pressing Play:
- [ ] Player exists in Hierarchy
- [ ] Player has CharacterController component
- [ ] Player has FirstPersonController script
- [ ] Camera is **child** of Player (indented)
- [ ] Camera has Camera component
- [ ] Ground exists at Y=-1
- [ ] 3 Obstacles exist
- [ ] No red errors in Console

### After Pressing Play:
- [ ] Cursor locks and hides
- [ ] WASD moves player
- [ ] Shift makes player sprint (faster)
- [ ] Space makes player jump
- [ ] Mouse moves camera
- [ ] Camera doesn't flip upside down
- [ ] Player collides with obstacles
- [ ] No errors in Console

---

## Specific Issues and Fixes

### Issue: "Player falls through ground"
**Fix**: 
- Ground Plane automatically has a Mesh Collider
- If player still falls, select Ground and verify it has a collider component

### Issue: "Can't move at all"
**Fix**:
1. Check Console for errors
2. Verify FirstPersonController script is attached to Player
3. Check Input Manager: `Edit > Project Settings > Input Manager`
   - Verify "Horizontal" and "Vertical" axes exist

### Issue: "Camera doesn't follow mouse"
**Fix**:
1. Verify Camera is **child** of Player (indented in Hierarchy)
2. Check FirstPersonController script is on **Player**, not Camera
3. Check Console for "Camera not found in children" error

### Issue: "Cursor doesn't lock"
**Fix**:
- This is normal on first Play press
- Press Play button again to stop, then Play again
- Cursor should lock on second Play

### Issue: "Movement is jerky/laggy"
**Fix**:
- This is normal in Unity Editor
- Built game will be smoother
- Reduce quality: `Edit > Project Settings > Quality` → Set to "Low" for testing

---

## Debug Mode: Check What's Actually Happening

### Enable Debug Logs
1. Click on Player in Hierarchy
2. In Inspector, find FirstPersonController script
3. Press Play
4. Watch Console for any error messages

### Check Component References
1. Select Player
2. In Inspector, look at FirstPersonController script
3. All fields should have values (not "None" or "Missing")

### Verify Scene is Correct
1. `File > Open Scene`
2. Navigate to `Assets/Scenes/`
3. Open `MainGame.unity`
4. Verify all GameObjects are present

---

## Still Not Working?

Please provide these details:

1. **What happens when you press Play?**
   - Nothing?
   - Player falls through ground?
   - Can't move?
   - Camera doesn't work?

2. **What's in the Console?**
   - Any red error messages?
   - Any yellow warnings?
   - Copy the exact error text

3. **What's in your Hierarchy?**
   - List the GameObjects you see
   - Is Camera indented under Player?

4. **Player Inspector**:
   - Click Player, screenshot Inspector panel
   - Or list all components you see

With this info, I can give you an exact fix!

---

## Quick Verification Commands

Run these in Unity:

1. **Check if Player exists**:
   - Look in Hierarchy for "Player"

2. **Check if script is attached**:
   - Select Player
   - Look in Inspector for "First Person Controller (Script)"

3. **Check if Camera is child**:
   - In Hierarchy, Camera should be indented under Player

4. **Check Console**:
   - Look for red errors
   - Clear Console: Right-click → Clear
   - Press Play again
   - Check for new errors

---

**Let me know which scenario matches your issue, and I'll help you fix it!**

# Automated Task 01 Setup - 2 Minutes!

## ✅ What I've Done

I created an **automated setup script** that will do all the Unity work for you!

## 🚀 Quick Steps (2 minutes total)

### Step 1: Open Unity (30 seconds)
1. Open Unity Hub
2. Click on **"My project"** to open it in Unity Editor
3. Wait for Unity to finish importing (watch bottom-right corner)

### Step 2: Run Auto Setup (30 seconds)
1. In Unity Editor menu bar, click: **Tools → Task 01 → Auto Setup Scene**
2. A dialog will appear saying "Setup Complete!"
3. Click "OK"

**That's it!** The script automatically creates:
- ✅ Player GameObject with CharacterController
- ✅ Camera (as child of Player)
- ✅ Ground plane
- ✅ 3 test obstacles
- ✅ Directional Light
- ✅ MainGame scene (saved to Assets/Scenes/)

### Step 3: Test It! (1 minute)
1. Click the **Play button (▶)** at the top of Unity
2. Test controls:
   - **WASD** - Move
   - **Shift** - Sprint
   - **Space** - Jump
   - **Mouse** - Look around
3. Click **Play** again to stop

### Step 4: Optional - Configure Settings (30 seconds)
In Unity menu: **Tools → Task 01 → Configure Project Settings**
- Sets Product Name to "LLM Story Game"
- You can customize Company Name later

---

## 🎯 What the Script Does

The `AutoSetupTask01.cs` script automatically:

1. **Creates new scene** with proper setup
2. **Creates Player GameObject**:
   - Adds CharacterController (height: 1.8, radius: 0.4)
   - Adds FirstPersonController script
   - Position: (0, 0, 0)
3. **Creates Camera**:
   - As child of Player
   - Position: (0, 0.6, 0) - eye height
   - Field of view: 60°
4. **Creates test environment**:
   - Ground plane at Y=-1, scaled 5x5
   - 3 cube obstacles at different positions
5. **Configures lighting**:
   - Directional Light at (50, -30, 0) rotation
6. **Saves scene** to `Assets/Scenes/MainGame.unity`

---

## ✅ After Testing

Once you've tested and everything works:

```bash
cd "/Users/manaskandimalla/Desktop/Projects/SeriousProjects/indie-game"
git add .
git commit -m "Task 1: Complete Unity project setup with automated scene creation"
git push origin main
```

---

## 🎮 Controls Reference

| Key | Action |
|-----|--------|
| W/A/S/D | Move forward/left/back/right |
| Shift (hold) | Sprint |
| Space | Jump |
| Mouse | Look around |
| ESC | Unlock cursor (for testing) |

---

## 🐛 Troubleshooting

### "Tools menu doesn't show Task 01"
- Wait for Unity to finish importing scripts
- Check Console for errors
- The script is in: `Assets/Scripts/Utilities/AutoSetupTask01.cs`

### "Script compilation errors"
- Check Unity Console (bottom panel)
- Make sure FirstPersonController.cs is in `Assets/Scripts/Player/`

### "Player falls through ground"
- This shouldn't happen with the auto setup
- If it does, select Ground in Hierarchy and verify it has a Mesh Collider

---

## 📋 Task 01 Checklist

After running the automated setup:

- [✅] Unity project created
- [✅] Folder structure in place
- [✅] FirstPersonController.cs implemented
- [✅] Scene created with auto setup script
- [⏳] Tested in Play mode
- [⏳] Git commit and push

---

**Time saved: ~1.5 hours of manual Unity work!** 🎉

**Next**: After testing, proceed to **Task 02: LLM Integration Layer**

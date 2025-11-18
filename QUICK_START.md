# Quick Start Guide

## For LLMs Building This Project

This guide provides a streamlined workflow for an LLM to build the complete game by following the task list.

## Overview

You are building an LLM-powered first-person story game in Unity. The project is divided into **10 sequential tasks**. Each task builds on the previous one.

## How to Use This Repository

### 1. Start with the Master Task List
Open `TASK_LIST.md` - this is your roadmap. It contains:
- Overview of all 10 tasks
- Estimated time for each task
- Links to detailed task files

### 2. Follow Tasks Sequentially
**IMPORTANT:** Complete tasks in order (1 → 2 → 3 → ... → 10)

For each task:
1. Open the task file (e.g., `tasks/TASK_01.md`)
2. Read the complete instructions
3. Refer to `IMPLEMENTATION_GUIDE.md` for code samples
4. Implement all features
5. Test thoroughly
6. Git commit and push

### 3. Reference Documentation
- **README.md**: Project overview, architecture, troubleshooting
- **IMPLEMENTATION_GUIDE.md**: Complete code implementations
- **TASK_LIST.md**: Master index of all tasks
- **tasks/TASK_XX.md**: Detailed instructions for each task

## Task Checklist

Use this checklist to track progress:

- [ ] **Task 1**: Project Setup & First-Person Controller (2-4 hours)
  - Unity project created
  - FirstPersonController.cs implemented
  - Test scene working
  - Git commit pushed

- [ ] **Task 2**: LLM Integration Layer (4-6 hours)
  - .env file configured with API key
  - LLMClient.cs working
  - PromptBuilder.cs implemented
  - Test scene demonstrates API calls
  - Git commit pushed

- [ ] **Task 3**: State Management System (4-6 hours)
  - GameState.cs with all data structures
  - StateManager.cs singleton working
  - Save/load functionality tested
  - Git commit pushed

- [ ] **Task 4**: Interaction System & NPC Foundation (6-8 hours)
  - InteractionSystem.cs with raycasting
  - NPCController.cs implemented
  - DialogueUI working
  - DialogueManager integrates with LLM
  - Git commit pushed

- [ ] **Task 5**: Dynamic Narrator System (4-6 hours)
  - NarratorSystem.cs generates descriptions
  - LocationManager tracks locations
  - EventSystem handles events
  - NarrationDisplay UI working
  - Git commit pushed

- [ ] **Task 6**: Game Manager & Scene Flow (6-8 hours)
  - GameManager.cs orchestrates systems
  - Main menu scene created
  - Pause menu working
  - HUD displays information
  - Git commit pushed

- [ ] **Task 7**: Advanced NPC AI (6-8 hours)
  - RelationshipSystem.cs implemented
  - NPCMemory.cs stores facts
  - MoodSystem.cs affects dialogue
  - Enhanced NPC interactions
  - Git commit pushed

- [ ] **Task 8**: Inventory & Interactive Objects (6-8 hours)
  - InventorySystem.cs working
  - ItemSystem.cs implemented
  - DoorObject.cs and PuzzleObject.cs
  - InventoryUI functional
  - Git commit pushed

- [ ] **Task 9**: World State Evolution & Branching Narrative (8-10 hours)
  - WorldStateEvolution.cs implemented
  - BranchingNarrative.cs tracks choices
  - ConsequenceSystem.cs working
  - TimeSystem.cs and WeatherSystem.cs
  - Multiple endings possible
  - Git commit pushed

- [ ] **Task 10**: Polish, Optimization & Deployment (8-12 hours)
  - LLM caching and rate limiting
  - AudioManager.cs implemented
  - SettingsUI complete
  - Full testing completed
  - Documentation finalized
  - Release build created
  - Git commit and tag v1.0.0 pushed

## Key Files to Create

### Task 1
- `Assets/Scripts/Player/FirstPersonController.cs`
- `Assets/Scenes/MainGame.unity`

### Task 2
- `.env` (with OPENAI_API_KEY)
- `Assets/Scripts/Utilities/EnvLoader.cs`
- `Assets/Scripts/LLM/LLMClient.cs`
- `Assets/Scripts/LLM/PromptBuilder.cs`
- `Assets/Scripts/LLM/ResponseParser.cs`
- `Assets/Scenes/LLMTest.unity`

### Task 3
- `Assets/Scripts/Core/GameState.cs`
- `Assets/Scripts/Core/StateManager.cs`
- `Assets/Scripts/Core/SaveManager.cs`

### Task 4
- `Assets/Scripts/Utilities/IInteractable.cs`
- `Assets/Scripts/Player/InteractionSystem.cs`
- `Assets/Scripts/NPC/NPCController.cs`
- `Assets/Scripts/NPC/DialogueManager.cs`
- `Assets/Scripts/UI/DialogueUI.cs`

### Task 5
- `Assets/Scripts/NPC/NarratorSystem.cs`
- `Assets/Scripts/World/LocationManager.cs`
- `Assets/Scripts/World/EventSystem.cs`
- `Assets/Scripts/UI/NarrationDisplay.cs`

### Task 6
- `Assets/Scripts/Core/GameManager.cs`
- `Assets/Scripts/Core/SceneManager.cs`
- `Assets/Scripts/UI/MainMenuUI.cs`
- `Assets/Scripts/UI/PauseMenuUI.cs`
- `Assets/Scripts/UI/HUD.cs`
- `Assets/Scenes/MainMenu.unity`

### Task 7
- `Assets/Scripts/NPC/RelationshipSystem.cs`
- `Assets/Scripts/NPC/NPCMemory.cs`
- `Assets/Scripts/NPC/MoodSystem.cs`
- `Assets/Scripts/NPC/ReactionSystem.cs`

### Task 8
- `Assets/Scripts/Player/InventorySystem.cs`
- `Assets/Scripts/World/ItemSystem.cs`
- `Assets/Scripts/World/InteractiveObject.cs`
- `Assets/Scripts/World/DoorObject.cs`
- `Assets/Scripts/World/PuzzleObject.cs`
- `Assets/Scripts/UI/InventoryUI.cs`

### Task 9
- `Assets/Scripts/World/WorldStateEvolution.cs`
- `Assets/Scripts/Narrative/BranchingNarrative.cs`
- `Assets/Scripts/Narrative/ConsequenceSystem.cs`
- `Assets/Scripts/World/TimeSystem.cs`
- `Assets/Scripts/World/WeatherSystem.cs`
- `Assets/Scripts/Narrative/EndingSystem.cs`

### Task 10
- `Assets/Scripts/Audio/AudioManager.cs`
- `Assets/Scripts/UI/SettingsUI.cs`
- `CONTROLS.md`
- `TROUBLESHOOTING.md`

## Testing Strategy

### After Each Task
1. **Compile Check**: Ensure no errors in Unity console
2. **Functionality Test**: Test the specific features added
3. **Integration Test**: Verify new code works with existing systems
4. **Save Test**: Ensure state persists correctly

### Before Final Commit
1. **Full Playthrough**: Play from main menu to an ending
2. **Edge Cases**: Test unusual player actions
3. **Performance**: Monitor FPS and memory usage
4. **API Usage**: Check token consumption is reasonable

## Common Pitfalls to Avoid

### 1. API Key Security
- ✅ DO: Store in .env file
- ❌ DON'T: Hardcode in scripts
- ✅ DO: Add .env to .gitignore
- ❌ DON'T: Commit API keys to git

### 2. Unity Best Practices
- ✅ DO: Use CharacterController for player movement
- ❌ DON'T: Use Rigidbody for first-person controller
- ✅ DO: Lock cursor during gameplay
- ❌ DON'T: Forget to unlock cursor in menus

### 3. LLM Integration
- ✅ DO: Use coroutines for async API calls
- ❌ DON'T: Block the main thread
- ✅ DO: Implement error handling
- ❌ DON'T: Assume API calls always succeed
- ✅ DO: Trim conversation history
- ❌ DON'T: Send entire history (token overflow)

### 4. State Management
- ✅ DO: Save state regularly
- ❌ DON'T: Rely only on auto-save
- ✅ DO: Use DontDestroyOnLoad for StateManager
- ❌ DON'T: Create multiple StateManager instances

### 5. Git Workflow
- ✅ DO: Commit after each task
- ❌ DON'T: Wait until the end to commit
- ✅ DO: Write descriptive commit messages
- ❌ DON'T: Use vague messages like "updates"

## Debugging Tips

### LLM Not Responding
1. Check console for "API key loaded successfully"
2. Verify .env file exists and has correct key
3. Test with LLMTest scene first
4. Check internet connection
5. Verify OpenAI account has credits

### Player Can't Move
1. Ensure CharacterController component attached
2. Check Input Manager has Horizontal/Vertical axes
3. Verify FirstPersonController script is on Player GameObject
4. Check for console errors

### State Not Saving
1. Check Application.persistentDataPath in console
2. Verify file permissions
3. Test SaveState() method directly
4. Check for JSON serialization errors

### NPCs Not Interacting
1. Verify "Interactable" layer is set on NPC GameObject
2. Check InteractionSystem has correct layer mask
3. Ensure DialogueManager exists in scene
4. Test raycasting with Debug.DrawRay

## Success Criteria

The project is complete when:

1. ✅ All 10 tasks are checked off
2. ✅ Game runs from main menu to ending without errors
3. ✅ NPCs respond with LLM-generated dialogue
4. ✅ Locations are described atmospherically
5. ✅ Player choices affect the narrative
6. ✅ Game state saves and loads correctly
7. ✅ Inventory system works
8. ✅ Multiple endings are achievable
9. ✅ Performance is smooth (60+ FPS)
10. ✅ Documentation is complete
11. ✅ All code is committed and pushed to GitHub

## Final Deliverables

When Task 10 is complete, the repository should contain:

- ✅ Complete Unity project
- ✅ All scripts implemented
- ✅ Main menu and game scenes
- ✅ README.md with setup instructions
- ✅ CONTROLS.md with keybindings
- ✅ TROUBLESHOOTING.md with common issues
- ✅ .env.example (template without actual key)
- ✅ .gitignore (excluding .env)
- ✅ Git tag v1.0.0

## Getting Help

If you encounter issues:

1. **Check README.md**: Troubleshooting section
2. **Review IMPLEMENTATION_GUIDE.md**: Code examples
3. **Read task file**: Detailed instructions
4. **Check Unity Console**: Error messages
5. **Test in isolation**: Use test scenes
6. **Verify prerequisites**: API key, Unity version, etc.

## Time Management

**Total Estimated Time**: 54-76 hours

**Suggested Schedule** (20 hours/week):
- **Week 1**: Tasks 1-3 (Foundation)
- **Week 2**: Tasks 4-6 (Core Gameplay)
- **Week 3**: Tasks 7-9 (Advanced Features)
- **Week 4**: Task 10 (Polish & Deploy)

**Suggested Schedule** (30 hours/week):
- **Week 1**: Tasks 1-5
- **Week 2**: Tasks 6-9
- **Week 3**: Task 10 + Testing

## Next Steps

**Ready to start?**

1. Open `TASK_LIST.md`
2. Click on [Task 1: Project Setup and First-Person Controller](./tasks/TASK_01.md)
3. Follow the instructions step-by-step
4. Refer to `IMPLEMENTATION_GUIDE.md` for code
5. Test thoroughly
6. Commit and push
7. Move to Task 2

**Good luck building your LLM-powered story game!** 🎮🤖

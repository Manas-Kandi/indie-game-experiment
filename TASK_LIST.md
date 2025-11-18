# LLM-Powered First-Person Story Game - Master Task List

## Overview
This is the master task list for building a complete LLM-powered first-person story game in Unity. The project is divided into 10 comprehensive, sequential tasks. Each task has its own detailed instruction file.

**Total Tasks: 10**
**Estimated Timeline: 2-4 weeks for MVP**

## Project Goal
Upon completion of all 10 tasks, you will have a fully functional first-person story game where:
- An LLM (OpenAI GPT-4) generates dynamic narratives and NPC dialogue
- NPCs respond intelligently with personality and memory
- The world evolves based on player actions
- Choices create branching narratives
- Game state persists across sessions

**To Run the Final Game:**
1. Clone repository: `https://github.com/Manas-Kandi/indie-game-experiment.git`
2. Open project in Unity 2022.3 LTS or later
3. Create `.env` file in project root with: `OPENAI_API_KEY=your_key_here`
4. Open `Assets/Scenes/MainMenu.unity`
5. Press Play

---

## Task Index

### [Task 1: Project Setup and First-Person Controller](./tasks/TASK_01.md)
**Objective:** Set up Unity project structure, implement first-person movement and camera controls, establish file organization.

**Key Deliverables:**
- Unity project with proper folder structure
- Fully functional first-person controller (WASD movement, mouse look, jump, sprint)
- Test scene with basic environment
- Git repository initialized

**Estimated Time:** 2-4 hours

---

### [Task 2: LLM Integration Layer](./tasks/TASK_02.md)
**Objective:** Implement OpenAI API client, prompt building system, and response parsing.

**Key Deliverables:**
- LLMClient.cs with API communication
- PromptBuilder.cs for context-aware prompts
- ResponseParser.cs for handling LLM outputs
- Environment variable configuration (.env file)
- Test scene demonstrating API calls

**Estimated Time:** 4-6 hours

---

### [Task 3: State Management System](./tasks/TASK_03.md)
**Objective:** Implement game state tracking and persistence (player, world, NPCs, conversation history).

**Key Deliverables:**
- GameState data structures (PlayerState, WorldState, NPCState, ConversationEntry)
- StateManager singleton with save/load functionality
- Conversation history management with auto-trimming
- JSON serialization/deserialization
- Test scene for state management

**Estimated Time:** 4-6 hours

---

### [Task 4: Interaction System and NPC Foundation](./tasks/TASK_04.md)
**Objective:** Implement raycasting-based interaction, NPC controllers, and dialogue UI.

**Key Deliverables:**
- InteractionSystem with raycasting detection
- IInteractable interface
- NPCController with personality and state
- DialogueUI with input/output
- DialogueManager coordinating LLM-powered conversations
- Test scene with interactive NPCs

**Estimated Time:** 6-8 hours

---

### [Task 5: Dynamic Narrator System](./tasks/TASK_05.md)
**Objective:** Implement LLM-powered location descriptions and event narration.

**Key Deliverables:**
- NarratorSystem for generating atmospheric descriptions
- LocationManager tracking discovered locations
- EventSystem for dynamic event narration
- NarrationDisplay UI with animations
- Narrator prompt templates
- Test scene with multiple locations

**Estimated Time:** 4-6 hours

---

### [Task 6: Game Manager and Scene Flow](./tasks/TASK_06.md)
**Objective:** Implement central game manager, scene transitions, menus, and player progression.

**Key Deliverables:**
- GameManager singleton coordinating all systems
- SceneManager with loading screens
- Main menu with API key configuration
- Pause menu with save/load
- HUD displaying game information
- ProgressionSystem tracking player stats
- InputManager for centralized input

**Estimated Time:** 6-8 hours

---

### [Task 7: Advanced NPC AI](./tasks/TASK_07.md)
**Objective:** Implement sophisticated NPC behavior with relationships, memory, and mood systems.

**Key Deliverables:**
- RelationshipSystem tracking NPC-player relationships
- NPCMemory for fact storage and recall
- MoodSystem affecting dialogue
- ReactionSystem for NPC responses to player actions
- Enhanced dialogue prompts with context
- Test scene demonstrating advanced AI

**Estimated Time:** 6-8 hours

---

### [Task 8: Inventory and Interactive Objects](./tasks/TASK_08.md)
**Objective:** Implement player inventory, items, doors, puzzles, and object examination.

**Key Deliverables:**
- InventorySystem with weight/count limits
- ItemSystem with multiple item types
- InteractiveObject base class
- DoorObject with lock/unlock mechanics
- PuzzleObject with LLM-generated hints
- ExaminationSystem for detailed object descriptions
- InventoryUI for item management
- Test scene with various interactive objects

**Estimated Time:** 6-8 hours

---

### [Task 9: World State Evolution and Branching Narrative](./tasks/TASK_09.md)
**Objective:** Implement dynamic world changes, branching narratives, and consequence systems.

**Key Deliverables:**
- WorldStateEvolution applying player action consequences
- BranchingNarrative tracking choices
- ConsequenceSystem with delayed effects
- TimeSystem for day/night progression
- WeatherSystem affecting narration
- NarrativeTracker for story consistency
- EndingSystem with multiple endings
- Test scene demonstrating branching

**Estimated Time:** 8-10 hours

---

### [Task 10: Polish, Optimization, and Deployment](./tasks/TASK_10.md)
**Objective:** Final polish, performance optimization, UI refinement, and deployment preparation.

**Key Deliverables:**
- LLM response caching and rate limiting
- Token budgeting and cost tracking
- AudioManager with music and SFX
- Polished UI with animations
- SettingsUI with configuration options
- Debug console and performance monitor
- Release build configuration
- Complete documentation (README, CONTROLS, TROUBLESHOOTING)
- Final testing and QA
- Deployment instructions

**Estimated Time:** 8-12 hours

---

## Total Estimated Time
**54-76 hours** (approximately 2-4 weeks at 20-30 hours/week)

---

## Architecture Overview

### System Layers
```
┌─────────────────────────────────────────┐
│         Game Engine Layer               │
│  (Unity: Physics, Rendering, Input)     │
└─────────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────────┐
│         Game Manager Layer              │
│  (Orchestration, Scene Flow, Events)    │
└─────────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────────┐
│      State Management Layer             │
│  (GameState, Persistence, History)      │
└─────────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────────┐
│       LLM Integration Layer             │
│  (API Client, Prompts, Parsing)         │
└─────────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────────┐
│     Content Generation Layer            │
│  (Narrator, Dialogue, Events)           │
└─────────────────────────────────────────┘
```

### Core Systems
- **Player System**: Movement, interaction, inventory
- **NPC System**: Controllers, dialogue, AI, relationships
- **World System**: Locations, events, time, weather
- **Narrative System**: Branching stories, consequences, endings
- **UI System**: Menus, HUD, dialogue, narration
- **LLM System**: API communication, prompt building, response parsing
- **State System**: Game state, persistence, save/load

---

## File Structure
```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── StateManager.cs
│   │   ├── SceneManager.cs
│   │   ├── GameState.cs
│   │   └── ProgressionSystem.cs
│   ├── LLM/
│   │   ├── LLMClient.cs
│   │   ├── PromptBuilder.cs
│   │   ├── ResponseParser.cs
│   │   ├── LLMConfig.cs
│   │   └── NarratorPrompts.cs
│   ├── Player/
│   │   ├── FirstPersonController.cs
│   │   ├── InteractionSystem.cs
│   │   ├── InventorySystem.cs
│   │   └── ExaminationSystem.cs
│   ├── NPC/
│   │   ├── NPCController.cs
│   │   ├── DialogueManager.cs
│   │   ├── NarratorSystem.cs
│   │   ├── RelationshipSystem.cs
│   │   ├── NPCMemory.cs
│   │   ├── MoodSystem.cs
│   │   └── ReactionSystem.cs
│   ├── World/
│   │   ├── LocationManager.cs
│   │   ├── EventSystem.cs
│   │   ├── ItemSystem.cs
│   │   ├── InteractiveObject.cs
│   │   ├── DoorObject.cs
│   │   ├── PuzzleObject.cs
│   │   ├── TimeSystem.cs
│   │   └── WeatherSystem.cs
│   ├── Narrative/
│   │   ├── BranchingNarrative.cs
│   │   ├── ConsequenceSystem.cs
│   │   ├── NarrativeTracker.cs
│   │   └── EndingSystem.cs
│   ├── UI/
│   │   ├── MainMenuUI.cs
│   │   ├── PauseMenuUI.cs
│   │   ├── DialogueUI.cs
│   │   ├── NarrationDisplay.cs
│   │   ├── HUD.cs
│   │   ├── InventoryUI.cs
│   │   └── SettingsUI.cs
│   ├── Audio/
│   │   └── AudioManager.cs
│   └── Utilities/
│       ├── EnvLoader.cs
│       ├── IInteractable.cs
│       └── InputManager.cs
├── Data/
│   ├── Prompts/
│   ├── Saves/
│   └── gamestate.json
├── Scenes/
│   ├── MainMenu.unity
│   ├── MainGame.unity
│   └── [Test scenes]
├── Prefabs/
└── Resources/
```

---

## Cost Estimation

### Per Play Session (1 hour)
- **Requests**: ~50 LLM API calls
- **Tokens**: ~25,000 tokens (input + output combined)
- **Cost with GPT-4**: $0.50 - $1.50
- **Cost with GPT-3.5-turbo**: $0.05 - $0.15

### Optimization Strategies
- Use GPT-3.5-turbo for simple NPC dialogue
- Reserve GPT-4 for critical story moments
- Implement response caching for common queries
- Rate limit to 1 request per 2 seconds
- Set token budgets per session

---

## Development Best Practices

### For Each Task
1. **Read the full task file** before starting
2. **Follow instructions sequentially** - each step builds on previous ones
3. **Test thoroughly** before moving to next task
4. **Commit and push** at the end of each task
5. **Document any deviations** from the plan

### Code Quality
- Use XML documentation comments for all public methods
- Follow C# naming conventions (PascalCase for classes/methods, camelCase for variables)
- Keep methods focused and single-purpose
- Handle errors gracefully with try-catch and meaningful messages
- Log important events for debugging

### LLM Integration
- **Never hardcode API keys** - always use environment variables
- **Log all prompts and responses** during development
- **Test with different temperatures** to find optimal creativity
- **Implement fallbacks** for API failures
- **Monitor token usage** to control costs

### Git Workflow
- Commit at the end of each task with descriptive messages
- Use format: `"Task N: Brief description of what was implemented"`
- Push to remote repository after each commit
- Tag final release as `v1.0.0`

---

## Troubleshooting

### Common Issues

**API Key Not Found**
- Ensure `.env` file exists in project root
- Verify `OPENAI_API_KEY` is set correctly
- Check EnvLoader.cs is loading the key properly

**LLM Responses Are Slow**
- Normal for GPT-4 (3-5 seconds per response)
- Consider using GPT-3.5-turbo for faster responses
- Implement loading indicators so player knows something is happening

**Game State Not Saving**
- Check `Application.persistentDataPath` permissions
- Verify JSON serialization is working (test in StateTest scene)
- Ensure StateManager.SaveState() is being called

**NPCs Not Responding**
- Verify API key is valid and has credits
- Check LLM debug logs for error messages
- Ensure DialogueManager is properly initialized
- Test with LLMTest scene first

---

## Next Steps

1. **Start with Task 1**: [Project Setup and First-Person Controller](./tasks/TASK_01.md)
2. **Work sequentially** through all 10 tasks
3. **Test each task** before moving to the next
4. **Commit and push** after completing each task
5. **Celebrate** when you finish Task 10! 🎉

---

## Support and Resources

### Unity Documentation
- [CharacterController](https://docs.unity3d.com/ScriptReference/CharacterController.html)
- [Coroutines](https://docs.unity3d.com/Manual/Coroutines.html)
- [UnityWebRequest](https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.html)

### OpenAI Documentation
- [API Reference](https://platform.openai.com/docs/api-reference)
- [Chat Completions](https://platform.openai.com/docs/guides/chat)
- [Best Practices](https://platform.openai.com/docs/guides/prompt-engineering)

### Git Repository
- [indie-game-experiment](https://github.com/Manas-Kandi/indie-game-experiment.git)

---

**Good luck building your LLM-powered story game!** 🚀

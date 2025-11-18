# LLM-Powered First-Person Story Game

A Unity-based first-person adventure game where an LLM (OpenAI GPT-4) generates dynamic narratives, NPC dialogue, and atmospheric world descriptions in real-time.

## Features

- **Dynamic Storytelling**: LLM-generated narratives that adapt to player choices
- **Intelligent NPCs**: Characters with personality, memory, and mood-based dialogue
- **Atmospheric World**: Victorian mystery setting with procedurally described locations
- **Branching Narrative**: Player choices create unique story paths
- **Persistent State**: Full game state saves including conversation history
- **Interactive Objects**: Inventory system with items, doors, and puzzles

## Prerequisites

- Unity 2022.3 LTS or later
- OpenAI API account and API key
- Git for version control
- Basic knowledge of Unity and C#

## Quick Start

### 1. Clone Repository
```bash
git clone https://github.com/Manas-Kandi/indie-game-experiment.git
cd indie-game-experiment
```

### 2. Configure API Key
Create a `.env` file in the project root:
```
OPENAI_API_KEY=your_openai_api_key_here
OPENAI_MODEL=gpt-4
LLM_API_TIMEOUT=30
```

**IMPORTANT:** Never commit the `.env` file to version control!

### 3. Open in Unity
1. Open Unity Hub
2. Click "Add" and select the project folder
3. Open the project (Unity 2022.3 LTS recommended)

### 4. Run the Game
1. Open `Assets/Scenes/MainMenu.unity`
2. Click Play (▶)
3. Enter your API key in settings if not using .env
4. Start a new game

## Development Guide

### Building from Scratch

Follow the comprehensive task list in `TASK_LIST.md`. The project is divided into 10 sequential tasks:

1. **Project Setup & First-Person Controller** (2-4 hours)
2. **LLM Integration Layer** (4-6 hours)
3. **State Management System** (4-6 hours)
4. **Interaction System & NPC Foundation** (6-8 hours)
5. **Dynamic Narrator System** (4-6 hours)
6. **Game Manager & Scene Flow** (6-8 hours)
7. **Advanced NPC AI** (6-8 hours)
8. **Inventory & Interactive Objects** (6-8 hours)
9. **World State Evolution & Branching Narrative** (8-10 hours)
10. **Polish, Optimization & Deployment** (8-12 hours)

**Total Time:** 54-76 hours (2-4 weeks at 20-30 hours/week)

### Project Structure

```
Assets/
├── Scripts/
│   ├── Core/          # GameManager, StateManager, scene management
│   ├── LLM/           # API client, prompts, response parsing
│   ├── Player/        # Movement, interaction, inventory
│   ├── NPC/           # NPC controllers, dialogue, AI
│   ├── World/         # Locations, events, items, time/weather
│   ├── Narrative/     # Branching stories, consequences, endings
│   ├── UI/            # Menus, HUD, dialogue display
│   ├── Audio/         # Sound management
│   └── Utilities/     # Helper classes, interfaces
├── Data/
│   ├── Prompts/       # LLM prompt templates
│   └── Saves/         # Save game files
├── Scenes/            # Unity scenes
├── Prefabs/           # Reusable game objects
└── Resources/         # Runtime-loaded assets
```

## Controls

- **WASD** - Move
- **Shift** - Sprint
- **Space** - Jump
- **Mouse** - Look around
- **E** - Interact with objects/NPCs
- **I** - Open inventory
- **ESC** - Pause menu
- **Tab** - Toggle cursor (debug)

## Architecture

### System Layers

```
Game Engine (Unity)
        ↓
Game Manager (Orchestration)
        ↓
State Management (Persistence)
        ↓
LLM Integration (API Communication)
        ↓
Content Generation (Narrator, Dialogue, Events)
```

### Core Systems

- **Player System**: First-person movement, interaction, inventory management
- **NPC System**: AI-driven characters with personality, memory, and relationships
- **World System**: Dynamic locations, events, time progression, weather
- **Narrative System**: Branching stories with consequences and multiple endings
- **LLM System**: OpenAI API integration with prompt engineering
- **State System**: Complete game state tracking and persistence

## Cost Estimation

### Per Play Session (1 hour)
- **API Requests**: ~50 calls
- **Tokens Used**: ~25,000 tokens
- **Cost (GPT-4)**: $0.50 - $1.50
- **Cost (GPT-3.5-turbo)**: $0.05 - $0.15

### Optimization Strategies
- Response caching for common queries
- Rate limiting (1 request per 2 seconds)
- Token budgets per session
- Use GPT-3.5-turbo for simple dialogue, GPT-4 for critical moments

## Configuration

### LLM Settings

Edit `Assets/Data/LLMConfig` ScriptableObject:
- **Model**: gpt-4 or gpt-3.5-turbo
- **Temperature**: 0.7 (0=deterministic, 1=creative)
- **Max Tokens**: 500 (response length limit)
- **Token Budget**: 50,000 per session

### Game Settings

Adjust in Unity Inspector or Settings menu:
- Graphics quality
- Audio volume
- Mouse sensitivity
- API key configuration

## Troubleshooting

### API Key Not Found
- Ensure `.env` file exists in project root (same level as Assets/)
- Verify `OPENAI_API_KEY` is set correctly
- Check console for "API key loaded successfully" message

### Slow LLM Responses
- Normal for GPT-4 (3-5 seconds per response)
- Consider using GPT-3.5-turbo for faster responses
- Implement loading indicators (already included)

### Game State Not Saving
- Check `Application.persistentDataPath` permissions
- Verify `gamestate.json` is being created
- Check console for save/load messages

### NPCs Not Responding
- Verify API key is valid and has credits
- Check LLM debug logs in console
- Test with `LLMTest` scene first
- Ensure internet connection is active

## Development Best Practices

### Code Quality
- Use XML documentation comments for all public methods
- Follow C# naming conventions (PascalCase for classes, camelCase for variables)
- Keep methods focused and single-purpose
- Handle errors gracefully with try-catch
- Log important events for debugging

### LLM Integration
- **Never hardcode API keys** - always use environment variables
- Log all prompts and responses during development
- Test with different temperatures to find optimal creativity
- Implement fallbacks for API failures
- Monitor token usage to control costs

### Git Workflow
- Commit at the end of each task
- Use descriptive commit messages
- Push to remote after each commit
- Tag final release as `v1.0.0`

## Testing

### Unit Testing
- Test each system independently before integration
- Use test scenes for isolated feature testing
- Verify error handling with invalid inputs

### Integration Testing
- Test full gameplay loop from main menu to ending
- Verify save/load cycle works correctly
- Test edge cases (unusual player actions, API failures)
- Monitor performance (FPS, memory, token usage)

### Playtesting
- Play through complete game session
- Test different dialogue choices
- Verify narrative consistency
- Check for bugs and polish issues

## Deployment

### Building the Game

1. **Configure Build Settings:**
   - File > Build Settings
   - Select target platform (Windows/Mac/Linux)
   - Add all scenes (MainMenu, MainGame)
   - Click "Build"

2. **Include Documentation:**
   - README.md
   - CONTROLS.md
   - TROUBLESHOOTING.md
   - .env.example (template without actual key)

3. **Test Build:**
   - Run executable
   - Verify API key configuration works
   - Test full gameplay

### Distribution

- Package game with clear setup instructions
- Include .env.example file
- Provide API key setup guide
- Document system requirements

## Contributing

This is a learning project demonstrating LLM integration in games. Feel free to:
- Fork and experiment
- Improve prompt engineering
- Add new features
- Optimize performance
- Share your findings

## License

[Specify your license here]

## Acknowledgments

- Unity Technologies for the game engine
- OpenAI for GPT-4 API
- Victorian literature for atmospheric inspiration

## Support

For issues, questions, or suggestions:
- Open an issue on GitHub
- Check TROUBLESHOOTING.md
- Review task documentation in `tasks/` directory

---

**Built with Unity and powered by OpenAI GPT-4** 🎮🤖

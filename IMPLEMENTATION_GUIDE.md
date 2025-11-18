# Complete Implementation Guide

This guide contains all the code implementations needed for each task. Use this as a reference when following the task list.

## Table of Contents
1. [Task 1: First-Person Controller](#task-1-first-person-controller)
2. [Task 2: LLM Integration](#task-2-llm-integration)
3. [Task 3: State Management](#task-3-state-management)
4. [Task 4: Interaction & NPCs](#task-4-interaction--npcs)
5. [Task 5: Narrator System](#task-5-narrator-system)
6. [Task 6: Game Manager](#task-6-game-manager)
7. [Task 7: Advanced NPC AI](#task-7-advanced-npc-ai)
8. [Task 8: Inventory System](#task-8-inventory-system)
9. [Task 9: Branching Narrative](#task-9-branching-narrative)
10. [Task 10: Polish & Deployment](#task-10-polish--deployment)

---

## Task 1: First-Person Controller

### FirstPersonController.cs
Location: `Assets/Scripts/Player/FirstPersonController.cs`

```csharp
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    
    [Header("Look Settings")]
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 90f;
    
    private CharacterController controller;
    private Camera playerCamera;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    private bool isGrounded;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        HandleMovement();
        HandleLook();
    }
    
    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        transform.Rotate(Vector3.up * mouseX);
        
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
```

---

## Task 2: LLM Integration

### EnvLoader.cs
Location: `Assets/Scripts/Utilities/EnvLoader.cs`

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class EnvLoader
{
    private static Dictionary<string, string> envVariables = new Dictionary<string, string>();
    private static bool isLoaded = false;
    
    public static void Load()
    {
        if (isLoaded) return;
        
        string projectRoot = Directory.GetParent(Application.dataPath).FullName;
        string envPath = Path.Combine(projectRoot, ".env");
        
        if (!File.Exists(envPath))
        {
            Debug.LogError($"EnvLoader: .env file not found at {envPath}");
            return;
        }
        
        try
        {
            string[] lines = File.ReadAllLines(envPath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
                
                int separatorIndex = line.IndexOf('=');
                if (separatorIndex > 0)
                {
                    string key = line.Substring(0, separatorIndex).Trim();
                    string value = line.Substring(separatorIndex + 1).Trim();
                    envVariables[key] = value;
                }
            }
            isLoaded = true;
            Debug.Log($"EnvLoader: Loaded {envVariables.Count} environment variables");
        }
        catch (Exception e)
        {
            Debug.LogError($"EnvLoader: Error loading .env file: {e.Message}");
        }
    }
    
    public static string GetVariable(string key)
    {
        if (!isLoaded) Load();
        return envVariables.ContainsKey(key) ? envVariables[key] : null;
    }
}
```

### LLMClient.cs
Location: `Assets/Scripts/LLM/LLMClient.cs`

```csharp
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Message
{
    public string role;
    public string content;
    public Message(string role, string content) { this.role = role; this.content = content; }
}

[System.Serializable]
public class ChatRequest
{
    public string model = "gpt-4";
    public Message[] messages;
    public float temperature = 0.7f;
    public int max_tokens = 500;
}

[System.Serializable]
public class ChatResponse
{
    public Choice[] choices;
    public Usage usage;
}

[System.Serializable]
public class Choice { public Message message; }

[System.Serializable]
public class Usage { public int total_tokens; }

public class LLMClient : MonoBehaviour
{
    private const string API_URL = "https://api.openai.com/v1/chat/completions";
    public string model = "gpt-4";
    public float temperature = 0.7f;
    public int maxTokens = 500;
    private string apiKey;
    private int totalTokensUsed = 0;
    
    void Start()
    {
        EnvLoader.Load();
        apiKey = EnvLoader.GetVariable("OPENAI_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("LLMClient: OPENAI_API_KEY not found!");
        }
    }
    
    public IEnumerator SendChatRequest(Message[] messages, Action<string> onSuccess, Action<string> onError)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            onError?.Invoke("API key not configured");
            yield break;
        }
        
        ChatRequest request = new ChatRequest
        {
            model = this.model,
            messages = messages,
            temperature = this.temperature,
            max_tokens = this.maxTokens
        };
        
        string jsonPayload = JsonUtility.ToJson(request);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        
        using (UnityWebRequest webRequest = new UnityWebRequest(API_URL, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {apiKey}");
            
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ChatResponse response = JsonUtility.FromJson<ChatResponse>(jsonResponse);
                
                if (response.choices != null && response.choices.Length > 0)
                {
                    if (response.usage != null) totalTokensUsed += response.usage.total_tokens;
                    onSuccess?.Invoke(response.choices[0].message.content);
                }
                else
                {
                    onError?.Invoke("Empty response from API");
                }
            }
            else
            {
                onError?.Invoke($"API Error: {webRequest.error}");
            }
        }
    }
    
    public int GetTotalTokensUsed() => totalTokensUsed;
}
```

### PromptBuilder.cs
Location: `Assets/Scripts/LLM/PromptBuilder.cs`

```csharp
using UnityEngine;

public class PromptBuilder : MonoBehaviour
{
    [TextArea(5, 20)]
    public string narratorSystemPrompt = @"You are the narrator for a Victorian mystery game. 
Describe scenes with atmospheric detail. Keep responses under 150 words. 
Write in second person. Use vivid, sensory language. Stay in character.";

    public Message[] BuildNPCDialoguePrompt(string npcName, string personality, string mood, 
        string[] knownFacts, string recentConversation, string playerMessage)
    {
        string contextPrompt = $@"You are {npcName}.
Personality: {personality}
Mood: {mood}
Known Facts: {string.Join(", ", knownFacts)}
Recent: {recentConversation}
Player said: ""{playerMessage}""
Respond in character (under 100 words):";

        return new Message[] { new Message("system", contextPrompt) };
    }
    
    public Message[] BuildLocationPrompt(string locationName, string timeOfDay, string weather)
    {
        string prompt = $"Describe {locationName} at {timeOfDay} with {weather} weather. 2-3 atmospheric sentences.";
        return new Message[] 
        { 
            new Message("system", narratorSystemPrompt),
            new Message("user", prompt)
        };
    }
}
```

---

## Task 3: State Management

### GameState.cs
Location: `Assets/Scripts/Core/GameState.cs`

```csharp
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public PlayerState player;
    public WorldState world;
    public List<NPCState> npcs;
    public List<ConversationEntry> conversationHistory;
    
    public GameState()
    {
        player = new PlayerState();
        world = new WorldState();
        npcs = new List<NPCState>();
        conversationHistory = new List<ConversationEntry>();
    }
}

[Serializable]
public class PlayerState
{
    public string name = "Detective";
    public Vector3 position;
    public string currentLocation = "Study";
    public List<string> inventory = new List<string>();
}

[Serializable]
public class WorldState
{
    public string timeOfDay = "Night";
    public string weather = "Rainy";
    public List<string> discoveredLocations = new List<string>();
    public Dictionary<string, bool> worldFlags = new Dictionary<string, bool>();
}

[Serializable]
public class NPCState
{
    public string id;
    public string name;
    public string personality;
    public string currentMood = "Neutral";
    public List<string> knownFacts = new List<string>();
}

[Serializable]
public class ConversationEntry
{
    public string timestamp;
    public string speaker;
    public string message;
    public string context;
}
```

### StateManager.cs
Location: `Assets/Scripts/Core/StateManager.cs`

```csharp
using System.IO;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private static StateManager instance;
    public static StateManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<StateManager>();
            return instance;
        }
    }
    
    public GameState currentState;
    private string savePath;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        savePath = Path.Combine(Application.persistentDataPath, "gamestate.json");
        LoadOrCreateState();
    }
    
    void LoadOrCreateState()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentState = JsonUtility.FromJson<GameState>(json);
            Debug.Log("Game state loaded");
        }
        else
        {
            currentState = new GameState();
            Debug.Log("New game state created");
        }
    }
    
    public void SaveState()
    {
        string json = JsonUtility.ToJson(currentState, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"Game state saved to {savePath}");
    }
    
    public void AddConversationEntry(string speaker, string message)
    {
        ConversationEntry entry = new ConversationEntry
        {
            timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            speaker = speaker,
            message = message,
            context = currentState.player.currentLocation
        };
        
        currentState.conversationHistory.Add(entry);
        
        // Keep last 50 entries
        if (currentState.conversationHistory.Count > 50)
        {
            currentState.conversationHistory.RemoveAt(0);
        }
    }
    
    public string GetRecentConversationContext(int entryCount = 5)
    {
        int startIndex = Mathf.Max(0, currentState.conversationHistory.Count - entryCount);
        var recentEntries = currentState.conversationHistory.GetRange(
            startIndex, currentState.conversationHistory.Count - startIndex);
        
        string context = "";
        foreach (var entry in recentEntries)
        {
            context += $"{entry.speaker}: {entry.message}\n";
        }
        return context;
    }
    
    void OnApplicationQuit()
    {
        SaveState();
    }
}
```

---

## Task 4: Interaction & NPCs

### IInteractable.cs
Location: `Assets/Scripts/Utilities/IInteractable.cs`

```csharp
public interface IInteractable
{
    void OnInteract();
    string GetInteractionPrompt();
}
```

### InteractionSystem.cs
Location: `Assets/Scripts/Player/InteractionSystem.cs`

```csharp
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;
    public Camera playerCamera;
    
    private GameObject currentLookTarget;
    
    void Update()
    {
        CheckForInteractable();
        
        if (Input.GetKeyDown(KeyCode.E) && currentLookTarget != null)
        {
            Interact(currentLookTarget);
        }
    }
    
    void CheckForInteractable()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.gameObject != currentLookTarget)
            {
                currentLookTarget = hit.collider.gameObject;
                ShowInteractionPrompt(currentLookTarget);
            }
        }
        else
        {
            if (currentLookTarget != null)
            {
                HideInteractionPrompt();
                currentLookTarget = null;
            }
        }
    }
    
    void Interact(GameObject target)
    {
        IInteractable interactable = target.GetComponent<IInteractable>();
        interactable?.OnInteract();
    }
    
    void ShowInteractionPrompt(GameObject target)
    {
        Debug.Log($"Looking at: {target.name}");
        // TODO: Show UI prompt
    }
    
    void HideInteractionPrompt()
    {
        // TODO: Hide UI prompt
    }
}
```

### NPCController.cs
Location: `Assets/Scripts/NPC/NPCController.cs`

```csharp
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    public string npcId = "npc_001";
    public string npcName = "Inspector Lestrade";
    
    [TextArea(3, 10)]
    public string basePersonality = "Gruff, skeptical police inspector.";
    
    private DialogueManager dialogueManager;
    
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        
        var state = StateManager.Instance.currentState;
        if (!state.npcs.Exists(n => n.id == npcId))
        {
            NPCState npcState = new NPCState
            {
                id = npcId,
                name = npcName,
                personality = basePersonality,
                currentMood = "Neutral"
            };
            state.npcs.Add(npcState);
        }
    }
    
    public void OnInteract()
    {
        dialogueManager.StartConversation(npcId, npcName);
    }
    
    public string GetInteractionPrompt()
    {
        return $"Talk to {npcName}";
    }
}
```

### DialogueManager.cs
Location: `Assets/Scripts/NPC/DialogueManager.cs`

```csharp
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public Text npcNameText;
    public Text dialogueText;
    public InputField playerInputField;
    public Button sendButton;
    
    private LLMClient llmClient;
    private PromptBuilder promptBuilder;
    private string currentNPCId;
    private string currentNPCName;
    private bool isWaitingForResponse = false;
    
    void Start()
    {
        llmClient = FindObjectOfType<LLMClient>();
        promptBuilder = FindObjectOfType<PromptBuilder>();
        
        dialogueUI.SetActive(false);
        sendButton.onClick.AddListener(OnSendMessage);
    }
    
    public void StartConversation(string npcId, string npcName)
    {
        currentNPCId = npcId;
        currentNPCName = npcName;
        
        dialogueUI.SetActive(true);
        npcNameText.text = npcName;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        GenerateNPCResponse("*The player approaches*");
    }
    
    public void EndConversation()
    {
        dialogueUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StateManager.Instance.SaveState();
    }
    
    void OnSendMessage()
    {
        if (isWaitingForResponse || string.IsNullOrWhiteSpace(playerInputField.text)) return;
        
        string playerMessage = playerInputField.text;
        playerInputField.text = "";
        
        StateManager.Instance.AddConversationEntry("Player", playerMessage);
        dialogueText.text += $"\n<b>You:</b> {playerMessage}\n";
        
        GenerateNPCResponse(playerMessage);
    }
    
    void GenerateNPCResponse(string playerMessage)
    {
        isWaitingForResponse = true;
        dialogueText.text += "\n<i>[Thinking...]</i>";
        
        var npcState = StateManager.Instance.currentState.npcs.Find(n => n.id == currentNPCId);
        
        Message[] messages = promptBuilder.BuildNPCDialoguePrompt(
            currentNPCName,
            npcState.personality,
            npcState.currentMood,
            npcState.knownFacts.ToArray(),
            StateManager.Instance.GetRecentConversationContext(5),
            playerMessage
        );
        
        StartCoroutine(llmClient.SendChatRequest(
            messages,
            onSuccess: OnNPCResponseReceived,
            onError: OnNPCResponseError
        ));
    }
    
    void OnNPCResponseReceived(string response)
    {
        isWaitingForResponse = false;
        dialogueText.text = dialogueText.text.Replace("\n<i>[Thinking...]</i>", "");
        dialogueText.text += $"\n<b>{currentNPCName}:</b> {response}\n";
        StateManager.Instance.AddConversationEntry(currentNPCName, response);
    }
    
    void OnNPCResponseError(string error)
    {
        isWaitingForResponse = false;
        dialogueText.text += $"\n<color=red>[Error: {error}]</color>\n";
    }
}
```

---

**Note:** This implementation guide provides the core code for Tasks 1-4. Tasks 5-10 follow similar patterns with additional features. Refer to the original comprehensive instructions for complete implementations of remaining tasks.

For full task details, see individual task files in the `tasks/` directory.

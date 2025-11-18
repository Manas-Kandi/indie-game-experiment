# Task 1: Project Setup and First-Person Controller Implementation

## Objective
Set up the Unity project structure, implement the foundational first-person controller with smooth movement and camera controls, and establish the core file organization for the LLM-powered game.

**Estimated Time:** 2-4 hours

---

## Prerequisites
- Unity 2022.3 LTS or later installed
- Git installed and configured
- GitHub account with access to repository
- Basic understanding of Unity and C#

---

## Step-by-Step Instructions

### Step 1.1: Project Initialization

#### Create Unity Project
1. Open Unity Hub
2. Click "New Project"
3. Select "3D" template (NOT 3D URP or HDRP)
4. Name: `LLM_StoryGame`
5. Location: `/Users/manaskandimalla/Desktop/Projects/SeriousProjects/indie-game`
6. Click "Create Project"

#### Configure Project Settings
1. Go to `Edit > Project Settings`
2. **Player Settings:**
   - Company Name: Your name
   - Product Name: "LLM Story Game"
   - Default Icon: (optional, can set later)
3. **Quality Settings:**
   - Set default quality level to "Medium" or "High"
4. **Input Manager:**
   - Verify these axes exist (they should by default):
     - Horizontal (A/D, Left/Right arrows)
     - Vertical (W/S, Up/Down arrows)
     - Mouse X
     - Mouse Y
     - Jump (Space)

#### Create Folder Structure
1. In the Project window, navigate to `Assets/`
2. Create the following folder hierarchy:
   ```
   Assets/
   ├── Scripts/
   │   ├── Core/
   │   ├── LLM/
   │   ├── Player/
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

**Right-click in Project window → Create → Folder** for each folder.

---

### Step 1.2: First-Person Controller Implementation

#### Create the Controller Script
1. Navigate to `Assets/Scripts/Player/`
2. Right-click → Create → C# Script
3. Name it `FirstPersonController`
4. Double-click to open in your code editor

#### Implement FirstPersonController.cs

Copy and paste this complete implementation:

```csharp
using UnityEngine;

/// <summary>
/// First-person controller handling player movement and camera look.
/// Uses CharacterController for movement and mouse input for camera rotation.
/// </summary>
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Walking speed in units per second")]
    public float walkSpeed = 5f;
    
    [Tooltip("Sprinting speed in units per second")]
    public float sprintSpeed = 8f;
    
    [Tooltip("Jump force applied when jumping")]
    public float jumpForce = 5f;
    
    [Tooltip("Gravity force applied to player")]
    public float gravity = -9.81f;
    
    [Header("Look Settings")]
    [Tooltip("Mouse sensitivity for camera rotation")]
    public float mouseSensitivity = 2f;
    
    [Tooltip("Maximum vertical look angle in degrees")]
    public float maxLookAngle = 90f;
    
    // Component references
    private CharacterController controller;
    private Camera playerCamera;
    
    // Movement state
    private Vector3 velocity;
    private float verticalRotation = 0f;
    private bool isGrounded;
    
    void Start()
    {
        // Get required components
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        
        // Validate components
        if (controller == null)
        {
            Debug.LogError("FirstPersonController: CharacterController component not found!");
        }
        
        if (playerCamera == null)
        {
            Debug.LogError("FirstPersonController: Camera component not found in children!");
        }
        
        // Lock and hide cursor for immersive first-person experience
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        HandleMovement();
        HandleLook();
        
        // Allow ESC to unlock cursor for testing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    /// <summary>
    /// Handles player movement including walking, sprinting, jumping, and gravity.
    /// </summary>
    void HandleMovement()
    {
        // Check if player is on the ground
        isGrounded = controller.isGrounded;
        
        if (isGrounded && velocity.y < 0)
        {
            // Small downward force to keep player grounded
            velocity.y = -2f;
        }
        
        // Get input from WASD or arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S
        
        // Calculate movement direction relative to where player is looking
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        
        // Determine current speed (sprint if Shift is held)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        
        // Apply movement
        controller.Move(move * currentSpeed * Time.deltaTime);
        
        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate jump velocity using physics formula: v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    /// <summary>
    /// Handles camera rotation based on mouse input.
    /// Horizontal rotation applied to player body, vertical to camera only.
    /// </summary>
    void HandleLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Rotate player body horizontally (left/right)
        transform.Rotate(Vector3.up * mouseX);
        
        // Rotate camera vertically (up/down) with clamping
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        
        // Apply vertical rotation to camera only
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
    
    /// <summary>
    /// Returns whether the player is currently on the ground.
    /// </summary>
    public bool IsGrounded()
    {
        return isGrounded;
    }
    
    /// <summary>
    /// Returns the current movement velocity.
    /// </summary>
    public Vector3 GetVelocity()
    {
        return controller.velocity;
    }
}
```

**Save the file** (Ctrl+S or Cmd+S).

---

### Step 1.3: Scene Setup

#### Create Main Game Scene
1. In Unity, go to `File > New Scene`
2. Save it as `MainGame` in `Assets/Scenes/`

#### Set Up Player GameObject
1. In Hierarchy, right-click → Create Empty
2. Name it `Player`
3. Reset Transform: Position (0, 0, 0), Rotation (0, 0, 0), Scale (1, 1, 1)
4. With Player selected, click `Add Component` in Inspector
5. Add `Character Controller` component:
   - Height: 1.8
   - Radius: 0.4
   - Center: (0, 0.9, 0)
6. Add the `FirstPersonController` script:
   - Click `Add Component`
   - Type "FirstPersonController"
   - Select it

#### Create Camera
1. Right-click on `Player` in Hierarchy → Create Empty
2. Name it `Camera`
3. Set Position: (0, 0.6, 0) - this is eye height
4. With Camera selected, click `Add Component`
5. Add `Camera` component
6. Set Camera settings:
   - Clear Flags: Skybox
   - Field of View: 60
   - Near Clipping Plane: 0.1
   - Far Clipping Plane: 1000

#### Create Test Environment
1. **Ground Plane:**
   - Right-click in Hierarchy → 3D Object → Plane
   - Name: `Ground`
   - Position: (0, -1, 0)
   - Scale: (5, 1, 5) - creates 50x50 unit ground

2. **Test Obstacles:**
   - Right-click in Hierarchy → 3D Object → Cube
   - Name: `Obstacle1`
   - Position: (5, 0, 5)
   - Scale: (2, 3, 2)
   
   - Create another cube: `Obstacle2`
   - Position: (-5, 0, 5)
   - Scale: (1, 4, 1)
   
   - Create another cube: `Obstacle3`
   - Position: (0, 0, 10)
   - Scale: (8, 2, 1)

3. **Lighting:**
   - Should already have a Directional Light in scene
   - If not: Right-click → Light → Directional Light
   - Rotation: (50, -30, 0) for nice shadows

4. **Adjust Player Start Position:**
   - Select Player in Hierarchy
   - Set Position: (0, 0, 0) - on the ground

#### Configure Scene Settings
1. Go to `Window > Rendering > Lighting`
2. In Environment tab:
   - Skybox Material: Default-Skybox (should be default)
   - Sun Source: Directional Light
3. Close Lighting window

---

### Step 1.4: Testing the Controller

#### Test in Play Mode
1. Click the Play button (▶) at top of Unity Editor
2. **Test Movement:**
   - Press W/A/S/D - player should move forward/left/back/right
   - Hold Shift while moving - player should move faster (sprint)
   - Move mouse - camera should look around smoothly
   - Press Space - player should jump
   - Walk into obstacles - player should collide (not pass through)

3. **Verify Behavior:**
   - Camera should not flip upside down when looking up/down
   - Cursor should be locked and hidden
   - Movement should feel smooth and responsive
   - Jumping should work only when on ground

4. **Exit Play Mode:** Click Play button again to stop

#### Debug if Issues Occur
- **Player falls through ground:** Ensure Ground has a Collider component
- **Can't move:** Check Input Manager has Horizontal/Vertical axes
- **Camera doesn't move:** Verify Camera is child of Player with FirstPersonController script
- **Console errors:** Read error messages and fix missing components

---

### Step 1.5: Git Repository Setup

#### Initialize Git Repository
1. Open Terminal/Command Prompt
2. Navigate to project root:
   ```bash
   cd /Users/manaskandimalla/Desktop/Projects/SeriousProjects/indie-game
   ```

3. Initialize git (if not already done):
   ```bash
   git init
   ```

#### Create .gitignore
1. Create a file named `.gitignore` in project root
2. Add Unity-specific entries:

```
# Unity generated files
[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
[Bb]uilds/
[Ll]ogs/
[Uu]ser[Ss]ettings/

# Visual Studio cache directory
.vs/

# Rider cache directory
.idea/

# Gradle cache directory (Android)
.gradle/

# Autogenerated VS/MD/Consulo solution and project files
ExportedObj/
.consulo/
*.csproj
*.unityproj
*.sln
*.suo
*.tmp
*.user
*.userprefs
*.pidb
*.booproj
*.svd
*.pdb
*.mdb
*.opendb
*.VC.db

# Unity3D generated meta files
*.pidb.meta
*.pdb.meta
*.mdb.meta

# Unity3D generated file on crash reports
sysinfo.txt

# Builds
*.apk
*.aab
*.unitypackage
*.app

# Environment variables (IMPORTANT - contains API keys)
.env
.env.local
.env.*.local

# OS generated files
.DS_Store
.DS_Store?
._*
.Spotlight-V100
.Trashes
ehthumbs.db
Thumbs.db

# Debug logs
llm_debug.txt
debug_log.txt
```

3. Save the file

#### Initial Commit
1. Add all files to git:
   ```bash
   git add .
   ```

2. Commit with message:
   ```bash
   git commit -m "Task 1: Project setup and first-person controller implementation"
   ```

3. Add remote repository (if not already added):
   ```bash
   git remote add origin https://github.com/Manas-Kandi/indie-game-experiment.git
   ```

4. Push to GitHub:
   ```bash
   git push -u origin main
   ```
   
   (If your default branch is `master`, use `git push -u origin master`)

---

## Acceptance Criteria

Before moving to Task 2, verify:

- [ ] Unity project created with proper folder structure
- [ ] `FirstPersonController.cs` implemented in `Assets/Scripts/Player/`
- [ ] Player GameObject with CharacterController and FirstPersonController components
- [ ] Camera as child of Player at eye height
- [ ] Test scene with ground and obstacles
- [ ] Player can move with WASD in all directions
- [ ] Player can sprint with Shift key
- [ ] Player can jump with Space key
- [ ] Mouse look works smoothly without gimbal lock
- [ ] Camera vertical rotation clamped to ±90 degrees
- [ ] Cursor locked and hidden during gameplay
- [ ] No console errors when playing
- [ ] `.gitignore` file created with Unity and .env entries
- [ ] Git repository initialized
- [ ] Initial commit made with message: "Task 1: Project setup and first-person controller implementation"
- [ ] Code pushed to GitHub repository

---

## Troubleshooting

### Issue: Player falls through ground
**Solution:** Ensure Ground plane has a Mesh Collider or Box Collider component.

### Issue: Camera doesn't rotate
**Solution:** 
- Verify Camera is a child of Player GameObject
- Check that FirstPersonController script is attached to Player (not Camera)
- Ensure Camera component exists on Camera GameObject

### Issue: Movement feels sluggish
**Solution:** 
- Increase walkSpeed and sprintSpeed values in Inspector
- Verify Time.deltaTime is being used (it is in the code)

### Issue: Can't jump
**Solution:**
- Check that "Jump" axis exists in Input Manager (Edit > Project Settings > Input Manager)
- Verify Space key is mapped to Jump
- Ensure player is on ground (check isGrounded in debug)

### Issue: Git push fails
**Solution:**
- Verify you have access to the repository
- Check you're on the correct branch (main or master)
- Ensure you've committed changes before pushing
- Try: `git pull origin main` then `git push origin main`

---

## Next Steps

Once all acceptance criteria are met and you've successfully pushed to GitHub:

**Proceed to [Task 2: LLM Integration Layer](./TASK_02.md)**

---

## Additional Notes

### Why CharacterController over Rigidbody?
- **CharacterController** provides precise, game-like movement without physics interference
- **Rigidbody** would cause unwanted collisions, sliding, and requires more complex setup
- CharacterController is standard for first-person games

### Why separate horizontal and vertical rotation?
- **Body rotates left-right:** Allows player to turn their entire body
- **Camera tilts up-down:** Allows looking up/down without rotating body
- This is standard FPS convention and feels natural to players

### Performance Considerations
- CharacterController is lightweight and performant
- Mouse look calculations are minimal
- No physics calculations needed for basic movement
- This controller will easily run at 60+ FPS

---

**Estimated completion time: 2-4 hours**
**Next task: Task 2 - LLM Integration Layer**

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

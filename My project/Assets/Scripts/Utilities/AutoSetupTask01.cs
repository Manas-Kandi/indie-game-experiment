using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Automated setup script for Task 01.
/// Run this from Unity Editor: Tools > Task 01 > Auto Setup Scene
/// </summary>
public class AutoSetupTask01
{
    [MenuItem("Tools/Task 01/Auto Setup Scene")]
    public static void SetupScene()
    {
        Debug.Log("=== Starting Task 01 Auto Setup ===");
        
        // Create new scene
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        Debug.Log("✓ Created new scene");
        
        // Remove default camera (we'll create our own)
        Camera defaultCamera = GameObject.FindObjectOfType<Camera>();
        if (defaultCamera != null)
        {
            GameObject.DestroyImmediate(defaultCamera.gameObject);
            Debug.Log("✓ Removed default camera");
        }
        
        // Create Player GameObject
        GameObject player = new GameObject("Player");
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.identity;
        
        // Add CharacterController
        CharacterController controller = player.AddComponent<CharacterController>();
        controller.height = 1.8f;
        controller.radius = 0.4f;
        controller.center = new Vector3(0, 0.9f, 0);
        Debug.Log("✓ Created Player with CharacterController");
        
        // Add FirstPersonController script
        player.AddComponent<FirstPersonController>();
        Debug.Log("✓ Added FirstPersonController script");
        
        // Create Camera as child of Player
        GameObject cameraObj = new GameObject("Camera");
        cameraObj.transform.SetParent(player.transform);
        cameraObj.transform.localPosition = new Vector3(0, 0.6f, 0);
        cameraObj.transform.localRotation = Quaternion.identity;
        
        Camera cam = cameraObj.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.Skybox;
        cam.fieldOfView = 60f;
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane = 1000f;
        
        // Tag as MainCamera
        cameraObj.tag = "MainCamera";
        Debug.Log("✓ Created Camera as child of Player");
        
        // Create Ground
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.position = new Vector3(0, -1, 0);
        ground.transform.localScale = new Vector3(5, 1, 5);
        Debug.Log("✓ Created Ground plane");
        
        // Create Obstacle 1
        GameObject obstacle1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstacle1.name = "Obstacle1";
        obstacle1.transform.position = new Vector3(5, 0, 5);
        obstacle1.transform.localScale = new Vector3(2, 3, 2);
        Debug.Log("✓ Created Obstacle1");
        
        // Create Obstacle 2
        GameObject obstacle2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstacle2.name = "Obstacle2";
        obstacle2.transform.position = new Vector3(-5, 0, 5);
        obstacle2.transform.localScale = new Vector3(1, 4, 1);
        Debug.Log("✓ Created Obstacle2");
        
        // Create Obstacle 3
        GameObject obstacle3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obstacle3.name = "Obstacle3";
        obstacle3.transform.position = new Vector3(0, 0, 10);
        obstacle3.transform.localScale = new Vector3(8, 2, 1);
        Debug.Log("✓ Created Obstacle3");
        
        // Ensure Directional Light exists
        Light[] lights = GameObject.FindObjectsOfType<Light>();
        bool hasDirectionalLight = false;
        foreach (Light light in lights)
        {
            if (light.type == LightType.Directional)
            {
                hasDirectionalLight = true;
                light.transform.rotation = Quaternion.Euler(50, -30, 0);
                break;
            }
        }
        
        if (!hasDirectionalLight)
        {
            GameObject lightObj = new GameObject("Directional Light");
            Light light = lightObj.AddComponent<Light>();
            light.type = LightType.Directional;
            lightObj.transform.rotation = Quaternion.Euler(50, -30, 0);
            Debug.Log("✓ Created Directional Light");
        }
        else
        {
            Debug.Log("✓ Configured existing Directional Light");
        }
        
        // Save scene
        if (!System.IO.Directory.Exists("Assets/Scenes"))
        {
            System.IO.Directory.CreateDirectory("Assets/Scenes");
        }
        
        string scenePath = "Assets/Scenes/MainGame.unity";
        EditorSceneManager.SaveScene(scene, scenePath);
        Debug.Log($"✓ Saved scene to {scenePath}");
        
        // Select Player in hierarchy
        Selection.activeGameObject = player;
        
        Debug.Log("=== Task 01 Auto Setup Complete! ===");
        Debug.Log("Press PLAY to test the controller!");
        
        EditorUtility.DisplayDialog(
            "Setup Complete!", 
            "Task 01 scene setup is complete!\n\n" +
            "✓ Player with CharacterController\n" +
            "✓ Camera (child of Player)\n" +
            "✓ Ground and obstacles\n" +
            "✓ Scene saved to Assets/Scenes/MainGame.unity\n\n" +
            "Press PLAY to test!\n" +
            "Controls: WASD, Shift (sprint), Space (jump), Mouse (look)",
            "OK"
        );
    }
    
    [MenuItem("Tools/Task 01/Configure Project Settings")]
    public static void ConfigureProjectSettings()
    {
        Debug.Log("=== Configuring Project Settings ===");
        
        // Set product name
        PlayerSettings.productName = "LLM Story Game";
        PlayerSettings.companyName = "Your Name"; // User can change this
        
        Debug.Log("✓ Set Product Name: LLM Story Game");
        Debug.Log("✓ Project settings configured");
        Debug.Log("You can change Company Name in Edit > Project Settings > Player");
        
        EditorUtility.DisplayDialog(
            "Settings Configured",
            "Project settings have been configured!\n\n" +
            "Product Name: LLM Story Game\n\n" +
            "You can customize more settings in:\n" +
            "Edit > Project Settings",
            "OK"
        );
    }
}

using UnityEngine;
using UnityEditor;

/// <summary>
/// Diagnostic tool to check Task 01 setup and identify issues
/// Run from: Tools > Task 01 > Diagnose Setup
/// </summary>
public class DiagnoseTask01
{
    [MenuItem("Tools/Task 01/Diagnose Setup")]
    public static void DiagnoseSetup()
    {
        Debug.Log("=== TASK 01 DIAGNOSTIC REPORT ===\n");
        
        bool allGood = true;
        
        // Check 1: Player GameObject
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("❌ PROBLEM: Player GameObject not found in scene!");
            Debug.Log("   FIX: Create an empty GameObject named 'Player'");
            allGood = false;
        }
        else
        {
            Debug.Log("✓ Player GameObject exists");
            
            // Check CharacterController
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc == null)
            {
                Debug.LogError("❌ PROBLEM: Player missing CharacterController component!");
                Debug.Log("   FIX: Select Player, Add Component > Character Controller");
                allGood = false;
            }
            else
            {
                Debug.Log($"✓ CharacterController found (Height: {cc.height}, Radius: {cc.radius})");
            }
            
            // Check FirstPersonController script
            FirstPersonController fpc = player.GetComponent<FirstPersonController>();
            if (fpc == null)
            {
                Debug.LogError("❌ PROBLEM: Player missing FirstPersonController script!");
                Debug.Log("   FIX: Select Player, Add Component > FirstPersonController");
                allGood = false;
            }
            else
            {
                Debug.Log($"✓ FirstPersonController script attached");
                Debug.Log($"   - Walk Speed: {fpc.walkSpeed}");
                Debug.Log($"   - Sprint Speed: {fpc.sprintSpeed}");
                Debug.Log($"   - Jump Force: {fpc.jumpForce}");
                Debug.Log($"   - Mouse Sensitivity: {fpc.mouseSensitivity}");
            }
            
            // Check Camera as child
            Camera cam = player.GetComponentInChildren<Camera>();
            if (cam == null)
            {
                Debug.LogError("❌ PROBLEM: No Camera found as child of Player!");
                Debug.Log("   FIX: Right-click Player > Create Empty > Name it 'Camera' > Add Camera component");
                allGood = false;
            }
            else
            {
                Debug.Log($"✓ Camera found as child of Player");
                Debug.Log($"   - Position: {cam.transform.localPosition}");
                Debug.Log($"   - Field of View: {cam.fieldOfView}");
                
                // Check if Camera is actually a child
                if (cam.transform.parent != player.transform)
                {
                    Debug.LogWarning("⚠️  WARNING: Camera exists but is NOT a child of Player!");
                    Debug.Log("   FIX: Drag Camera onto Player in Hierarchy to make it a child");
                    allGood = false;
                }
            }
        }
        
        // Check 2: Ground
        GameObject ground = GameObject.Find("Ground");
        if (ground == null)
        {
            Debug.LogWarning("⚠️  WARNING: Ground GameObject not found!");
            Debug.Log("   FIX: Create 3D Object > Plane, name it 'Ground', position at (0, -1, 0)");
        }
        else
        {
            Debug.Log($"✓ Ground exists at position {ground.transform.position}");
        }
        
        // Check 3: Scene saved
        string scenePath = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
        if (string.IsNullOrEmpty(scenePath))
        {
            Debug.LogWarning("⚠️  WARNING: Scene is not saved!");
            Debug.Log("   FIX: File > Save As > Save to Assets/Scenes/MainGame.unity");
        }
        else
        {
            Debug.Log($"✓ Scene saved: {scenePath}");
        }
        
        // Check 4: Console for compilation errors
        Debug.Log("\n--- Checking for script compilation ---");
        Debug.Log("If you see any RED errors above this line, fix those first!");
        
        // Final verdict
        Debug.Log("\n=== DIAGNOSTIC SUMMARY ===");
        if (allGood)
        {
            Debug.Log("✅ ALL CHECKS PASSED!");
            Debug.Log("Your setup looks good. Press PLAY to test!");
            Debug.Log("\nControls:");
            Debug.Log("  WASD - Move");
            Debug.Log("  Shift - Sprint");
            Debug.Log("  Space - Jump");
            Debug.Log("  Mouse - Look around");
            
            EditorUtility.DisplayDialog(
                "Diagnostic Complete ✅",
                "All checks passed!\n\n" +
                "Your Task 01 setup is correct.\n\n" +
                "Press PLAY (▶) to test the controller!\n\n" +
                "Controls:\n" +
                "• WASD - Move\n" +
                "• Shift - Sprint\n" +
                "• Space - Jump\n" +
                "• Mouse - Look around",
                "OK"
            );
        }
        else
        {
            Debug.LogError("❌ ISSUES FOUND - See errors above for fixes");
            Debug.Log("\nRead the error messages above carefully.");
            Debug.Log("Each error has a 'FIX:' line telling you exactly what to do.");
            Debug.Log("\nAfter fixing, run this diagnostic again!");
            
            EditorUtility.DisplayDialog(
                "Issues Found ❌",
                "Problems detected with your setup!\n\n" +
                "Check the Console (bottom panel) for detailed error messages.\n\n" +
                "Each error has a 'FIX:' line with the solution.\n\n" +
                "Fix the issues, then run this diagnostic again:\n" +
                "Tools > Task 01 > Diagnose Setup",
                "OK"
            );
        }
        
        Debug.Log("=== END DIAGNOSTIC REPORT ===\n");
    }
    
    [MenuItem("Tools/Task 01/Open MainGame Scene")]
    public static void OpenMainGameScene()
    {
        string scenePath = "Assets/Scenes/MainGame.unity";
        
        if (System.IO.File.Exists(scenePath))
        {
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath);
            Debug.Log($"✓ Opened scene: {scenePath}");
        }
        else
        {
            Debug.LogError($"Scene not found: {scenePath}");
            Debug.Log("Run 'Tools > Task 01 > Auto Setup Scene' first!");
            
            EditorUtility.DisplayDialog(
                "Scene Not Found",
                "MainGame.unity scene doesn't exist yet.\n\n" +
                "Run: Tools > Task 01 > Auto Setup Scene\n\n" +
                "This will create the scene automatically.",
                "OK"
            );
        }
    }
}

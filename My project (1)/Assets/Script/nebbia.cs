using UnityEngine;

public class DisableFogForCamera : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            // Create a new render texture for the camera
            RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 16);
            cam.targetTexture = renderTexture;
            cam.clearFlags = CameraClearFlags.SolidColor; // Clear the background
        }
    }

    void OnPreRender()
    {
        // Disable fog before rendering
        RenderSettings.fog = false;
    }

    void OnPostRender()
    {
        // Re-enable fog after rendering
        RenderSettings.fog = true;
    }
}
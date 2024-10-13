using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    public RectTransform minimapUI;                // The RectTransform for the minimap
    public KeyCode toggleKey = KeyCode.M;          // The key to toggle fullscreen minimap
    public Vector2 cornerMinimapSize = new Vector2(150, 150); // Size of the corner minimap
    public Vector2 fullscreenMinimapSize = new Vector2(1920, 1080); // Size of the fullscreen minimap
    public Vector2 cornerMinimapPosition = new Vector2(100, 100); // Position of the corner minimap
    public Vector2 fullscreenMinimapPosition = new Vector2(0, 0); // Position of the fullscreen minimap
    public CanvasGroup minimapCanvasGroup;         // CanvasGroup to control opacity

    private bool isFullscreen = false;   
    private Camera Mappa;      
    private float scala; // Variable to hold camera scale
    
    void Start()
    {
        Mappa = GameObject.Find("mappa").GetComponent<Camera>();
        
        
        SetMinimapToCorner();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            // Toggle between fullscreen and corner minimap
            if (isFullscreen)
            {
                SetMinimapToCorner();
            }
            else
            {
                SetMinimapToFullscreen();
            }
        }
    }

    // Set the minimap to fullscreen mode
    void SetMinimapToFullscreen()
    {
        minimapUI.sizeDelta = fullscreenMinimapSize;  // Set to fullscreen size
        minimapUI.anchoredPosition = fullscreenMinimapPosition; // Position at center of the screen
        minimapCanvasGroup.alpha = 0.0f; // Make it fully visible (no transparency)
        Mappa.orthographicSize = 10; // Change the camera scale or variable as needed
        isFullscreen = true;
    }

    // Set the minimap to corner mode
    void SetMinimapToCorner()
    {
        minimapUI.sizeDelta = cornerMinimapSize;  // Set to corner size
        minimapUI.anchoredPosition = cornerMinimapPosition; // Position it in the corner
        minimapCanvasGroup.alpha = 1f; // Make it slightly transparent
        Mappa.orthographicSize = 3; // Change the camera scale or variable as needed
        isFullscreen = false;
    }
}

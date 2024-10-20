using UnityEngine;
using UnityEngine.UI;

public class ToggleRawImage : MonoBehaviour
{
    public RawImage rawImage; // Reference to the RawImage component
    private bool isVisible = false; // Track the visibility state

    void Start()
    {
        if (rawImage != null)
        {
            rawImage.enabled = false; // Start with the RawImage disabled
        }
        else
        {
            Debug.LogError("RawImage reference is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // Check if the M key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleImage();
        }
    }

    // Method to toggle the visibility of the RawImage
    private void ToggleImage()
    {
        if (rawImage != null)
        {
            isVisible = !isVisible; // Toggle the visibility state
            rawImage.enabled = isVisible; // Set the RawImage enabled state
        }
    }
}
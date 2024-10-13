using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Include this for scene management

public class SliderController : MonoBehaviour
{
    public Slider slider; // Reference to the UI Slider
    public string sceneToLoad; // The name of the scene to load

    void Start()
    {
        // Initialize the slider value from LunghezzaManager
        slider.value = LunghezzaManager.Instance.lunghezza;

        // Set the slider's minimum and maximum values
        slider.minValue = 10; // Minimum value (adjust as needed)
        slider.maxValue = 100; // Maximum value (adjust as needed)

        // Add a listener to update the variable when the slider value changes
        slider.onValueChanged.AddListener(UpdateLunghezza);
    }

    void UpdateLunghezza(float value)
    {
        LunghezzaManager.Instance.lunghezza = Mathf.RoundToInt(value); // Update the lunghezza variable as an integer
    }
}

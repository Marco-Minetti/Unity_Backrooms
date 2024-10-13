using UnityEngine;

public class PulsingLight : MonoBehaviour
{
    public Light pulsingLight;        // Reference to the light component
    public float minIntensity = 0.5f; // Minimum intensity of the light
    public float maxIntensity = 2.0f; // Maximum intensity of the light
    public float pulseSpeed = 2.0f;   // Speed of the pulsing effect

    private float pulseTimer = 0f;

    void Start()
    {
        // Ensure the light component is assigned
        if (pulsingLight == null)
        {
            pulsingLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        PulseLight();
    }

    void PulseLight()
    {
        // Increment the pulse timer based on pulse speed
        pulseTimer += Time.deltaTime * pulseSpeed;

        // Calculate the light's intensity using a sine wave
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(pulseTimer) * 0.5f + 0.5f);

        // Apply the calculated intensity to the light
        pulsingLight.intensity = intensity;
    }
}

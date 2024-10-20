using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PulseButton : MonoBehaviour
{
    private float minScale = 0.9f;  // Scala minima del pulsante
    private float maxScale = 1.1f;  // Scala massima del pulsante
    private float speed = 1.0f;     // Velocità dell’effetto pulsazione

    private RectTransform rectTransform;
    private bool isPulsing = true;
    private Vector3 originalScale;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;  // Ottieni la scala originale DOPO aver ottenuto rectTransform
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        while (isPulsing)
        {
            // Scala verso l'alto
            for (float t = 0; t < 1; t += Time.deltaTime * speed)
            {
                float scale = Mathf.Lerp(minScale, maxScale, t);
                rectTransform.localScale = originalScale * scale;  // Usa la scala originale come base
                yield return null;
            }

            // Scala verso il basso
            for (float t = 0; t < 1; t += Time.deltaTime * speed)
            {
                float scale = Mathf.Lerp(maxScale, minScale, t);
                rectTransform.localScale = originalScale * scale;  // Usa la scala originale come base
                yield return null;
            }
        }
    }
}

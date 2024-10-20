using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f; // Velocità di movimento

    void Update()
    {
        // Muovi il personaggio a sinistra rispetto all'asse del piano
        Vector3 leftMovement = -transform.right * moveSpeed * Time.deltaTime; // Usa -transform.right per muovere a sinistra
        transform.Translate(leftMovement);
    }
}
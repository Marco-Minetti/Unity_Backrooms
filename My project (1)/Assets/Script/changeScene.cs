using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameScene()
    {
        SceneManager.LoadScene("gioco"); // Sostituisci "GameScene" con il nome della scena del gioco
    }
}


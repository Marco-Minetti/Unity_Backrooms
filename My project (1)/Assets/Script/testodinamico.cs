using UnityEngine;

using TMPro;

public class DynamicText : MonoBehaviour
{
    public TMP_Text uiText; // Reference to the UI Text component
    
    
    void Start()
    {
        int variableToDisplay= LunghezzaManager.Instance.lunghezza;
        UpdateText(variableToDisplay); // Update the text at the start
    }

    void Update()
    {
            int variableToDisplay= LunghezzaManager.Instance.lunghezza;
            UpdateText(variableToDisplay); // Update the text
       
        
    }

    void UpdateText(int variableToDisplay)
    {

{
    if (variableToDisplay <= 20)
    {
        uiText.text = $"{variableToDisplay}x{variableToDisplay}\n<color=green>Nella media</color>";
    }
    else if (variableToDisplay <= 30 && variableToDisplay > 20)
    {
        uiText.text = $"{variableToDisplay}x{variableToDisplay}\n<color=blue>Bel PC!</color>";
    }
    else if (variableToDisplay <= 50 && variableToDisplay > 30)
    {
        uiText.text = $"{variableToDisplay}x{variableToDisplay}\n<color=orange>Amante dei 5FPS</color>";
    }
    else if (variableToDisplay <= 70 && variableToDisplay > 50)
    {
        uiText.text = $"{variableToDisplay}x{variableToDisplay}\n<color=red>Torna indietro</color>";
    }
    else if (variableToDisplay <= 100 && variableToDisplay > 70)
    {
        uiText.text = $"{variableToDisplay}x{variableToDisplay}\n<color=purple>Contento?</color>";
    }
}


    }
}

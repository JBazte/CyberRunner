using UnityEngine;
using UnityEngine.UIElements;

public class TapButtonEffect : MonoBehaviour
{
    private UIDocument tapDoc;
    private Button tapButton;
    private float animationTime = 0f;

    void Start()
    {
        // Obt�n el componente UIDocument del GameObject actual
        tapDoc = GetComponent<UIDocument>();

        if (tapDoc != null)
        {
            // Obt�n el bot�n "TapButton" del UIDocument
            tapButton = tapDoc.rootVisualElement.Q<Button>("TapButton");

            if (tapButton != null)
            {
                // Inicializa el tama�o de la fuente
                tapButton.style.fontSize = 175;
            }
            else
            {
                Debug.LogError("No se encontr� el bot�n 'TapButton' en el UIDocument.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� el componente UIDocument en el GameObject actual.");
        }
    }

    void Update()
    {
        if (tapButton != null)
        {
            // Aplica un efecto de agrandamiento m�s gradual al texto del bot�n en el m�todo Update
            float scaleFactor = Mathf.Lerp(0.9f, 1.1f, Mathf.PingPong(animationTime / 1.5f, 1f)); // Ajusta la velocidad y amplitud
            tapButton.text = "Tap To Play!"; // Ajusta el texto seg�n tu necesidad
            tapButton.style.fontSize = scaleFactor * 175f;

            // Actualiza el tiempo de animaci�n
            animationTime += Time.deltaTime;
        }
    }
}

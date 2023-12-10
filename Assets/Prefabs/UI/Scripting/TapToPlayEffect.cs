using UnityEngine;
using UnityEngine.UIElements;

public class TapButtonEffect : MonoBehaviour
{
    private UIDocument tapDoc;
    private Button tapButton;
    private float animationTime = 0f;

    void Start()
    {
        // Obtén el componente UIDocument del GameObject actual
        tapDoc = GetComponent<UIDocument>();

        if (tapDoc != null)
        {
            // Obtén el botón "TapButton" del UIDocument
            tapButton = tapDoc.rootVisualElement.Q<Button>("TapButton");

            if (tapButton != null)
            {
                // Inicializa el tamaño de la fuente
                tapButton.style.fontSize = 175;
            }
            else
            {
                Debug.LogError("No se encontró el botón 'TapButton' en el UIDocument.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el componente UIDocument en el GameObject actual.");
        }
    }

    void Update()
    {
        if (tapButton != null)
        {
            // Aplica un efecto de agrandamiento más gradual al texto del botón en el método Update
            float scaleFactor = Mathf.Lerp(0.9f, 1.1f, Mathf.PingPong(animationTime / 1.5f, 1f)); // Ajusta la velocidad y amplitud
            tapButton.text = "Tap To Play!"; // Ajusta el texto según tu necesidad
            tapButton.style.fontSize = scaleFactor * 175f;

            // Actualiza el tiempo de animación
            animationTime += Time.deltaTime;
        }
    }
}

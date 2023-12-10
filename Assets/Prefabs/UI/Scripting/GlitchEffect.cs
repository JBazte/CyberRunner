using UnityEngine;
using UnityEngine.UIElements;

public class GlitchEffect : MonoBehaviour
{
    private UIDocument GameOverUI;
    private Label label;
    private float originalPositionX;
    private float originalPositionY;

    public float glitchIntensity = 2f; // Ajusta la intensidad del glitch para que sea más suave

    void Start()
    {
        // Obtén el componente UIDocument del GameObject actual
        GameOverUI = GetComponent<UIDocument>();

        if (GameOverUI != null)
        {
            label = GameOverUI.rootVisualElement.Q("GameOverLab") as Label;

            if (label != null)
            {
                originalPositionX = label.resolvedStyle.left;
                originalPositionY = label.resolvedStyle.top;
            }
            else
            {
                Debug.LogError("No se encontró la Label 'GameOverLab' en el UIDocument.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el componente UIDocument en el GameObject actual.");
        }
    }

    void Update()
    {
        if (label != null && GameOverUI.enabled == true)
        {
            label = GameOverUI.rootVisualElement.Q("GameOverLab") as Label;
            ApplyGlitch();
        }
    }

    void ApplyGlitch()
    {
        // Aplica un desplazamiento aleatorio en X y Y para simular el efecto glitch
        float xOffset = Random.Range(-glitchIntensity, glitchIntensity);
        float yOffset = Random.Range(-glitchIntensity, glitchIntensity);

        // Aplica el desplazamiento al estilo del texto
        label.style.left = originalPositionX + xOffset;
        label.style.top = originalPositionY + yOffset;
    }
}

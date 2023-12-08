using UnityEngine;
using UnityEngine.UIElements;

public class GlitchEffect : MonoBehaviour
{
    [SerializeField]
    private UIDocument GameOverUI;
    private Label label;
    private float originalPositionX;
    private float originalPositionY;

    public float glitchIntensity = 5f;

    void Start()
    {
        GameOverUI.GetComponent<UIDocument>();
        label = GameOverUI.rootVisualElement.Q("GameOverLab") as Label;
        originalPositionX = label.resolvedStyle.left;
        originalPositionY = label.resolvedStyle.top;

        // Inicia la animación de glitch
        InvokeRepeating("ApplyGlitch", 0f, 0.1f);
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


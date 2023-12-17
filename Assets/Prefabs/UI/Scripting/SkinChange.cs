using UnityEngine;
using UnityEngine.UIElements;

public class SkinChange : MonoBehaviour
{
    // Prefab del nuevo modelo que se asigna por serialización
    [SerializeField]
    private GameObject nuevoModeloPrefab;
    [SerializeField]
    private GameObject jugador;

    UIDocument cosmeticsDoc;
    Button new_skin_btn;

    private void Start()
    {
        cosmeticsDoc = GetComponent<UIDocument>();   
        new_skin_btn = cosmeticsDoc.rootVisualElement.Q("Skin_button") as Button;
        new_skin_btn.RegisterCallback<ClickEvent>(CambiarModelo);
        // Asegúrate de que se haya asignado el prefab desde el Inspector
        if (nuevoModeloPrefab == null)
        {
            Debug.LogError("No se ha asignado el prefab del nuevo modelo.");
        }

        if (jugador == null)
        {
            Debug.LogError("No se ha asignado el GameObject del jugador.");
        }

    }

    // Método para cambiar el modelo del jugador
    public void CambiarModelo(ClickEvent evt)
    {
        if (nuevoModeloPrefab != null && jugador != null)
        {
            // Crea una nueva instancia del nuevo modelo
            GameObject nuevoModelo = Instantiate(nuevoModeloPrefab, jugador.transform.position, jugador.transform.rotation);

            // Transfiere la posición y la rotación del jugador al nuevo modelo
            nuevoModelo.transform.position = jugador.transform.position;
            nuevoModelo.transform.rotation = jugador.transform.rotation;

            // Destruye el modelo antiguo
            Destroy(jugador);

            // Actualiza la referencia al jugador con el nuevo modelo
            jugador = nuevoModelo;
        }
        else
        {
            Debug.LogError("Asegúrate de asignar el prefab del nuevo modelo y el GameObject del jugador.");
        }
    }
    
}
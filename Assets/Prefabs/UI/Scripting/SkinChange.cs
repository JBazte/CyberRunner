using UnityEngine;
using UnityEngine.UIElements;

public class SkinChange : MonoBehaviour
{
    // Prefab del nuevo modelo que se asigna por serialización
    [SerializeField]
    private GameObject[] characterModels;
    [SerializeField]
    private Avatar[] characterAvatar;
    [SerializeField]
    private Animator currentAvatar;

    UIDocument cosmeticsDoc;
    Button new_skin_btn;

    private void OnEnable()
    {
        cosmeticsDoc = GetComponent<UIDocument>();   
        new_skin_btn = cosmeticsDoc.rootVisualElement.Q("Skin_button") as Button;
        new_skin_btn.RegisterCallback<ClickEvent>(CambiarModelo);
    }

    // Método para cambiar el modelo del jugador
    public void CambiarModelo(ClickEvent evt)
    {
        Debug.Log(currentAvatar.avatar);
        if (characterModels[0].activeSelf)
        {
            currentAvatar.avatar = characterAvatar[0];
        } else {
            currentAvatar.avatar = characterAvatar[1];
        }
        characterModels[0].SetActive(!characterModels[0].activeSelf);
        characterModels[1].SetActive(!characterModels[1].activeSelf);
    }
    
}
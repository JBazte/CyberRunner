using UnityEngine;
using UnityEngine.UIElements;

public class SkinChange : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultSkin;
    [SerializeField]
    private Avatar     defaultAvatar;

    [SerializeField]
    private GameObject destroyedSkin;
    [SerializeField]
    private Avatar     destroyedAvatar;


    [SerializeField]
    private Animator   currentAvatar;
    private GameObject currentSkin;

    UIDocument cosmeticsDoc;
    Button     default_skin_btn;
    Button     destroyed_skin_btn;

    private void OnEnable()
    {
        cosmeticsDoc = GetComponent<UIDocument>();
        RootElements();
    }

    public void RootElements()
    {
        default_skin_btn = cosmeticsDoc.rootVisualElement.Q("Default_skin_button") as Button;
        default_skin_btn.RegisterCallback<ClickEvent>(evt => ChangeSkin(evt, defaultSkin, defaultAvatar));
        destroyed_skin_btn = cosmeticsDoc.rootVisualElement.Q("Destroyed_skin_button") as Button;
        destroyed_skin_btn.RegisterCallback<ClickEvent>(evt => ChangeSkin(evt, destroyedSkin, destroyedAvatar));
    }

    private void Start()
    {
        if(!defaultSkin.activeSelf) defaultSkin.SetActive(true);
        destroyedSkin.SetActive(false);
        // It should get the skin that the player has last used
        currentSkin = defaultSkin;
    }

    public void ChangeSkin(ClickEvent evt, GameObject modelToChange, Avatar avatarToChange)
    {
        currentAvatar.avatar = avatarToChange;

        if(!modelToChange.activeSelf)
        {
            modelToChange.SetActive(true);
            currentSkin.SetActive(false);
            currentSkin = modelToChange;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloseButton : MonoBehaviour
{
    UIDocument thisDoc;

    Button closeButton;

    private void OnEnable()
    {
        thisDoc = GetComponent<UIDocument>();

        closeButton = thisDoc.rootVisualElement.Q("CloseButton") as Button;

        closeButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        //thisDoc.enabled = false;
        GameManager.Instance.OutShop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        closeButton = thisDoc.rootVisualElement.Q("CloseButton") as Button;
        closeButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}

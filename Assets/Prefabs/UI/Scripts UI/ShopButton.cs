using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    UIDocument shopDoc;

    Button UIbutton;

    private void OnEnable()
    {
        shopDoc = GetComponent<UIDocument>();

        UIbutton = shopDoc.rootVisualElement.Q("ResumeButton") as Button;

        UIbutton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        shopDoc.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}



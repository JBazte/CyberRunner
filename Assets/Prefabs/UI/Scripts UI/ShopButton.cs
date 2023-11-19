using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    UIDocument shopDoc;

    UIDocument m_thisDoc;

    Button UIbutton;

    private void OnEnable()
    {
        m_thisDoc = GetComponent<UIDocument>();

        UIbutton = m_thisDoc.rootVisualElement.Q("ShopButton") as Button;
       

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
        UIbutton = shopDoc.rootVisualElement.Q("ShopButton") as Button;

        UIbutton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}



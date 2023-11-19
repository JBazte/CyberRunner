using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    UIDocument shopDoc;

    UIDocument m_thisDoc;

    Button shopButton;

    private void OnEnable()
    {
        m_thisDoc = GetComponent<UIDocument>();

        shopButton = m_thisDoc.rootVisualElement.Q("ShopButton") as Button;
       

        shopButton.RegisterCallback<ClickEvent>(OnButtonclick);
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
        shopButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HomeButton : MonoBehaviour
{
    UIDocument thisDoc;

    Button m_homeButton;

    private void OnEnable()
    {
        thisDoc = GetComponent<UIDocument>();

        m_homeButton = thisDoc.rootVisualElement.Q("HomeButton") as Button;

        m_homeButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        //Here Alex's code
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_homeButton = thisDoc.rootVisualElement.Q("HomeButton") as Button;

        m_homeButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}

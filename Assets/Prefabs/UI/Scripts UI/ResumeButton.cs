using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResumeButton : MonoBehaviour
{
    [SerializeField]
    UIDocument inGameDoc;
    
    UIDocument m_thisDoc;

    Button resumeButton;

    private void OnEnable()
    {
        m_thisDoc = GetComponent<UIDocument>();

        resumeButton = m_thisDoc.rootVisualElement.Q("ResumeButton") as Button;

        resumeButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        m_thisDoc.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        resumeButton = m_thisDoc.rootVisualElement.Q("ResumeButton") as Button;

        resumeButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResumeButton : MonoBehaviour
{
    //[SerializeField]
    //UIDocument inGameDoc;

    private UIDocument m_thisDoc;

    private Button     m_resumeButton;

    private void OnEnable()
    {
        m_thisDoc = GetComponent<UIDocument>();

        m_resumeButton = m_thisDoc.rootVisualElement.Q("ResumeButton") as Button;

        m_resumeButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        m_thisDoc.enabled = false;
        GameManager.Instance.Resume();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_resumeButton = m_thisDoc.rootVisualElement.Q("ResumeButton") as Button;
        m_resumeButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}


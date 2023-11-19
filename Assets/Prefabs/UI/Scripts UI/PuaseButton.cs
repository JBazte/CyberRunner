using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    UIDocument puaseDoc;

    UIDocument m_thisDoc;

    Button pauseButton;

    private void OnEnable()
    {
        m_thisDoc = GetComponent<UIDocument>();

        pauseButton = m_thisDoc.rootVisualElement.Q("PuaseButton") as Button;

        pauseButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        puaseDoc.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pauseButton = m_thisDoc.rootVisualElement.Q("PuaseButton") as Button;
        pauseButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}

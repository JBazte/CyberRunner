using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResumeButton : MonoBehaviour
{
    [SerializeField]
    UIDocument inGameDoc, puaseDoc;

    Button UIbutton;

    private void OnEnable()
    {
        puaseDoc = GetComponent<UIDocument>();

        UIbutton = puaseDoc.rootVisualElement.Q("ResumeButton") as Button;

        UIbutton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        inGameDoc.enabled = true;
        puaseDoc.enabled = false;

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


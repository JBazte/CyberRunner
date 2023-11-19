using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    UIDocument inGameDoc, puaseDoc;

    Button UIbutton;

    private void OnEnable()
    {
        inGameDoc = GetComponent<UIDocument>();

        if(inGameDoc == null)
        {
            Debug.Log("Doc not fund");
        }
        else
        {
            Debug.Log("found");
        }

        UIbutton = inGameDoc.rootVisualElement.Q("PuaseButton") as Button;

        if (UIbutton != null)
        {
            Debug.Log("Button found");
        }

        UIbutton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        inGameDoc.enabled = false;
        puaseDoc.enabled = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIbutton = inGameDoc.rootVisualElement.Q("PuaseButton") as Button;
        UIbutton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}

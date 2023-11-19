using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TapToPlay : MonoBehaviour
{
    UIDocument thisDoc;
    Button tapButton;

    private void OnEnable()
    {
        thisDoc = GetComponent<UIDocument>();
        tapButton = thisDoc.rootVisualElement.Q("TapButton") as Button;

        tapButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        thisDoc.enabled = false;
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

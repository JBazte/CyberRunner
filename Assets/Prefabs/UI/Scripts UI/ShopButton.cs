using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    UIDocument shopDoc, gameOverDoc;

    Button UIbutton, closeButton;

    private void OnEnable()
    {
        gameOverDoc = GetComponent<UIDocument>();

        UIbutton = gameOverDoc.rootVisualElement.Q("ShopButton") as Button;
       
        /*if (UIbutton != null)
        {
            Debug.Log("Button shop found");
        }*/

        UIbutton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        gameOverDoc.enabled = false;
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



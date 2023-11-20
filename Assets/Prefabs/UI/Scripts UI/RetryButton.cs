using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class RetryButton : MonoBehaviour
{
    //[SerializeField]
    //UIDocument shopDoc;

    UIDocument m_thisDoc;

    Button shopButton;

    private void OnEnable()
    {
        m_thisDoc = GetComponent<UIDocument>();

        shopButton = m_thisDoc.rootVisualElement.Q("RetryButton") as Button;
       

        shopButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt)
    {
        //shopDoc.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DisplayData : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    UIDocument thisDoc;
    Label scoreLabel, coinsLabel;

    private void OnEnable()
    {
        thisDoc = GetComponent<UIDocument>();

        scoreLabel = thisDoc.rootVisualElement.Q("ScoreLab") as Label;

        coinsLabel = thisDoc.rootVisualElement.Q("CoinsLab") as Label;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = ((int)gameManager.Score).ToString();    
        if(coinsLabel != null)
        {
            coinsLabel.text = gameManager.CoinsObtained.ToString();
        }
    }
}

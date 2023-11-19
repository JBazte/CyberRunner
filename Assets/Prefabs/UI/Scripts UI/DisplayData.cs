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
        gameManager = GetComponent<GameManager>();
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
        scoreLabel.text = ((int)gameManager.m_score).ToString();
        
        if(coinsLabel != null )
        {
            coinsLabel.text = gameManager.CoinsObtained.ToString();
        }

        Debug.Log("Score="+scoreLabel.text);
        Debug.Log("Coins="+coinsLabel.text);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : TemporalSingleton<UIManager>
{
    [SerializeField]
    UIDocument tapDoc, inGameDoc, pauseDoc, gameOverDoc, shopDoc;

    Button btn_home, btn_shop, btn_resume, btn_pause, btn_close, btn_tap;

    Label scoreLabel, finalScoreLabel, coinsLabel;

    private void OnEnable()
    {
        btn_tap = tapDoc.rootVisualElement.Q("TapButton") as Button;
        btn_tap.RegisterCallback<ClickEvent>(ToGame);

        btn_home = gameOverDoc.rootVisualElement.Q("HomeButton") as Button;
        btn_home.RegisterCallback<ClickEvent>(ToTap);

        btn_shop = gameOverDoc.rootVisualElement.Q("ShopButton") as Button;
        btn_shop.RegisterCallback<ClickEvent>(ToShop);

        btn_resume = pauseDoc.rootVisualElement.Q("ResumeButton") as Button;
        btn_resume.RegisterCallback<ClickEvent>(ToResume);

        btn_pause = inGameDoc.rootVisualElement.Q("PauseButton") as Button;
        btn_pause.RegisterCallback<ClickEvent>(OnPause);

        btn_close = shopDoc.rootVisualElement.Q("CloseButton") as Button;
        btn_close.RegisterCallback<ClickEvent>(OnClose);

        scoreLabel = inGameDoc.rootVisualElement.Q("ScoreLab") as Label;

        coinsLabel = inGameDoc.rootVisualElement.Q("CoinsLab") as Label;

        finalScoreLabel = gameOverDoc.rootVisualElement.Q("ScoreLab") as Label;
    }

    public void ToGame(ClickEvent evt)
    {
        tapDoc.enabled = false;
        GameManager.Instance.StartRun();
        inGameDoc.enabled = true;

        /*btn_pause = inGameDoc.rootVisualElement.Q("PauseButton") as Button;
        btn_pause.RegisterCallback<ClickEvent>(OnPause);
        scoreLabel = inGameDoc.rootVisualElement.Q("ScoreLab") as Label;
        coinsLabel = inGameDoc.rootVisualElement.Q("CoinsLab") as Label;*/
        RestartUI(1);
    }

    public void ToTap(ClickEvent evt) {
        //AQUI TIENE QUE IR EL RESTART
        gameOverDoc.enabled = false;
        tapDoc.enabled = true;


        /*btn_tap = tapDoc.rootVisualElement.Q("TapButton") as Button;
        btn_tap.RegisterCallback<ClickEvent>(ToGame);*/
        RestartUI(2);
    }

    public void ToShop(ClickEvent evt)
    {
        shopDoc.enabled = true;

        /*btn_close = shopDoc.rootVisualElement.Q("CloseButton") as Button;
        btn_close.RegisterCallback<ClickEvent>(OnClose);*/

        RestartUI(3);
    }

    public void ToResume(ClickEvent evt)
    {
        GameManager.Instance.Resume();
        pauseDoc.enabled = false;
        inGameDoc.enabled = true;

        /*btn_pause = inGameDoc.rootVisualElement.Q("PauseButton") as Button;
        btn_pause.RegisterCallback<ClickEvent>(OnPause);
        scoreLabel = inGameDoc.rootVisualElement.Q("ScoreLab") as Label;
        coinsLabel = inGameDoc.rootVisualElement.Q("CoinsLab") as Label;*/
        RestartUI(1);
    }

    public void OnPause(ClickEvent evt)
    {
        GameManager.Instance.PauseRun();
        inGameDoc.enabled = false;
        pauseDoc.enabled = true;

        /*btn_resume = pauseDoc.rootVisualElement.Q("ResumeButton") as Button;
        btn_resume.RegisterCallback<ClickEvent>(ToResume);*/
        RestartUI(4);
    }
    
    public void OnClose(ClickEvent evt)
    {
        shopDoc.enabled = false;
    }

    public void ToGameOver()
    {
        inGameDoc.enabled = false;
        gameOverDoc.enabled = true;

        /*btn_home = gameOverDoc.rootVisualElement.Q("HomeButton") as Button;
        btn_home.RegisterCallback<ClickEvent>(ToTap);

        btn_shop = gameOverDoc.rootVisualElement.Q("ShopButton") as Button;
        btn_shop.RegisterCallback<ClickEvent>(ToShop);

        finalScoreLabel = gameOverDoc.rootVisualElement.Q("ScoreLab") as Label;

        finalScoreLabel.text = scoreLabel.text;*/
        RestartUI(5);
    }

    void RestartUI(int caso)
    {
        switch (caso)
        {
            case 1:
                btn_pause = inGameDoc.rootVisualElement.Q("PauseButton") as Button;
                btn_pause.RegisterCallback<ClickEvent>(OnPause);
                scoreLabel = inGameDoc.rootVisualElement.Q("ScoreLab") as Label;
                coinsLabel = inGameDoc.rootVisualElement.Q("CoinsLab") as Label;
                break;

            case 2:
                btn_tap = tapDoc.rootVisualElement.Q("TapButton") as Button;
                btn_tap.RegisterCallback<ClickEvent>(ToGame);
                break;

            case 3:
                btn_close = shopDoc.rootVisualElement.Q("CloseButton") as Button;
                btn_close.RegisterCallback<ClickEvent>(OnClose);
                break;

            case 4:
                btn_resume = pauseDoc.rootVisualElement.Q("ResumeButton") as Button;
                btn_resume.RegisterCallback<ClickEvent>(ToResume);
                break;

            case 5:
                btn_home = gameOverDoc.rootVisualElement.Q("HomeButton") as Button;
                btn_home.RegisterCallback<ClickEvent>(ToTap);

                btn_shop = gameOverDoc.rootVisualElement.Q("ShopButton") as Button;
                btn_shop.RegisterCallback<ClickEvent>(ToShop);

                finalScoreLabel = gameOverDoc.rootVisualElement.Q("ScoreLab") as Label;

                finalScoreLabel.text = scoreLabel.text;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = ((int)GameManager.Instance.Score).ToString();
        coinsLabel.text = GameManager.Instance.CoinsObtained.ToString();
    }
}

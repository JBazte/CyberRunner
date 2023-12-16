using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : TemporalSingleton<UIManager>
{
    [SerializeField]
    private PlayFabManager playFabManager;

    [SerializeField]
    UIDocument tapDoc, inGameDoc, pauseDoc, gameOverDoc, shopDoc, cosmeticsDoc, tutorialDoc;

    Button btn_play, btnShop, btnResume, btnPause, btnCloseShop, btnCloseCosmetics, btnTap, btnLeaderBoard, btnItems, btnCosmetics;
    Button m_motoShopBtn, m_bootsShopBtn, m_hyperspeedShopBtn, m_wallsShopBtn, m_droneShopBtn;

    Label scoreLabel, finalScoreLabel, coinsLabel, comboLabel, tutorialLabel;

    VisualElement tutorialImage;

    private bool m_corroutineRuning;

    private void OnEnable()
    {
        btnTap = tapDoc.rootVisualElement.Q("TapButton") as Button;
        btnTap.RegisterCallback<ClickEvent>(ToGame);

        btn_play = gameOverDoc.rootVisualElement.Q("RetryButton") as Button;
        btn_play.RegisterCallback<ClickEvent>(ToTap);

        btnShop = gameOverDoc.rootVisualElement.Q("ShopButton") as Button;
        btnShop.RegisterCallback<ClickEvent>(ToShop);

        btnResume = pauseDoc.rootVisualElement.Q("ResumeButton") as Button;
        btnResume.RegisterCallback<ClickEvent>(ToResume);

        btnPause = inGameDoc.rootVisualElement.Q("PauseButton") as Button;
        btnPause.RegisterCallback<ClickEvent>(OnPause);

        btnCloseShop = shopDoc.rootVisualElement.Q("CloseButton") as Button;
        btnCloseShop.RegisterCallback<ClickEvent>(OnClose);
        
        btnCloseCosmetics = cosmeticsDoc.rootVisualElement.Q("CloseButton") as Button;
        btnCloseCosmetics.RegisterCallback<ClickEvent>(OnClose);

        btnCosmetics = shopDoc.rootVisualElement.Q("CostemticsButton") as Button;
        btnCosmetics.RegisterCallback<ClickEvent>(ToCosmetics);

        btnLeaderBoard = gameOverDoc.rootVisualElement.Q("LeaderboardButton") as Button;
        btnLeaderBoard.RegisterCallback<ClickEvent>(ToLeaderboard);

        btnItems = cosmeticsDoc.rootVisualElement.Q("ItemsButton") as Button;
        btnItems.RegisterCallback<ClickEvent>(ToShop);


        m_motoShopBtn       = shopDoc.rootVisualElement.Q("MotoShopButton") as Button;
        m_motoShopBtn.RegisterCallback<ClickEvent>(evt => UpgradePowerUp(evt, PowerUpsEnum.MOTORBIKE));
        m_bootsShopBtn      = shopDoc.rootVisualElement.Q("BootsShopButton") as Button;
        m_bootsShopBtn.RegisterCallback<ClickEvent>(evt => UpgradePowerUp(evt, PowerUpsEnum.BOOTS));
        m_hyperspeedShopBtn = shopDoc.rootVisualElement.Q("HyperspeedShopButton") as Button;
        m_hyperspeedShopBtn.RegisterCallback<ClickEvent>(evt => UpgradePowerUp(evt, PowerUpsEnum.HYPERSPEED));
        m_wallsShopBtn      = shopDoc.rootVisualElement.Q("WallsShopButton") as Button;
        m_wallsShopBtn.RegisterCallback<ClickEvent>(evt => UpgradePowerUp(evt, PowerUpsEnum.WALLS));
        m_droneShopBtn      = shopDoc.rootVisualElement.Q("DroneShopButton") as Button;
        m_droneShopBtn.RegisterCallback<ClickEvent>(evt => UpgradePowerUp(evt, PowerUpsEnum.DRON));


        scoreLabel = inGameDoc.rootVisualElement.Q("ScoreLab") as Label;
        coinsLabel = inGameDoc.rootVisualElement.Q("CoinsLab") as Label;
        finalScoreLabel = gameOverDoc.rootVisualElement.Q("ScoreLab") as Label;
        comboLabel = inGameDoc.rootVisualElement.Q("ComboLab") as Label;
        tutorialLabel = tutorialDoc.rootVisualElement.Q("Tutorial_label") as Label;

        tutorialImage = tutorialDoc.rootVisualElement.Q("Tut_img_label") as Image;
    }

    private void UpgradePowerUp(ClickEvent evt, PowerUpsEnum powerUp)
    {
        switch(powerUp)
        {
            case PowerUpsEnum.BOOTS:
                switch(PlayerPrefs.GetInt(AppPlayerPrefs.BOOTS_TIER))
                {
                    case 1:
                        if(GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_2)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.BOOTS_TIER, 2);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins()-(int)PowerUpsTierUpCosts.TO_LVL_2);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 2:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_3)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.BOOTS_TIER, 3);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_3);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 3:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_4)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.BOOTS_TIER, 4);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_4);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 4:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_5)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.BOOTS_TIER, 5);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_5);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 5:
                        Debug.Log("Boots have max tier");
                        break;
                    default:
                        Debug.Log("Corrupted Tier for Boots PowerUp");
                        break;
                }
                break;
            case PowerUpsEnum.DRON:
                switch (PlayerPrefs.GetInt(AppPlayerPrefs.DRON_TIER))
                {
                    case 1:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_2)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.DRON_TIER, 2);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_2);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 2:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_3)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.DRON_TIER, 3);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_3);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 3:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_4)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.DRON_TIER, 4);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_4);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 4:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_5)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.DRON_TIER, 5);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_5);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 5:
                        Debug.Log("Dron has max tier");
                        break;
                    default:
                        Debug.Log("Corrupted Tier for Dron PowerUp");
                        break;
                }
                break;
            case PowerUpsEnum.WALLS:
                switch (PlayerPrefs.GetInt(AppPlayerPrefs.WALLS_TIER))
                {
                    case 1:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_2)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.WALLS_TIER, 2);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_2);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 2:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_3)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.WALLS_TIER, 3);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_3);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 3:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_4)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.WALLS_TIER, 4);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_4);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 4:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_5)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.WALLS_TIER, 5);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_5);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 5:
                        Debug.Log("Walls have max tier");
                        break;
                    default:
                        Debug.Log("Corrupted Tier for Walls PowerUp");
                        break;
                }
                break;
            case PowerUpsEnum.HYPERSPEED:
                switch (PlayerPrefs.GetInt(AppPlayerPrefs.HYPERSPEED_TIER))
                {
                    case 1:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_2)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.HYPERSPEED_TIER, 2);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_2);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 2:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_3)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.HYPERSPEED_TIER, 3);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_3);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 3:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_4)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.HYPERSPEED_TIER, 4);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_4);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 4:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_5)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.DRON_TIER, 5);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_5);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 5:
                        Debug.Log("Hyperspeed has max tier");
                        break;
                    default:
                        Debug.Log("Corrupted Tier for Hyperspeed PowerUp");
                        break;
                }
                break;
            case PowerUpsEnum.MOTORBIKE:
                switch (PlayerPrefs.GetInt(AppPlayerPrefs.MOTORBIKE_TIER))
                {
                    case 1:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_2)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.MOTORBIKE_TIER, 2);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_2);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 2:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_3)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.MOTORBIKE_TIER, 3);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_3);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 3:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_4)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.MOTORBIKE_TIER, 4);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_4);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 4:
                        if (GameManager.Instance.GetPlayerAccountCoins() >= (int)PowerUpsTierUpCosts.TO_LVL_5)
                        {
                            PlayerPrefs.SetInt(AppPlayerPrefs.MOTORBIKE_TIER, 5);
                            PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, GameManager.Instance.GetPlayerAccountCoins() - (int)PowerUpsTierUpCosts.TO_LVL_5);
                        }
                        else Debug.Log("Not enough coins to level up");
                        break;
                    case 5:
                        Debug.Log("Motorbike has max tier");
                        break;
                    default:
                        Debug.Log("Corrupted Tier for Motorbike PowerUp");
                        break;
                }
                break;
            default:
                Debug.Log("INVALID POWER-UP ERROR");
                break;
        }
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
        ModuleManager.Instance.SetInitialScenario();
        GameManager.Instance.GetPlayer().PlayAnimation("idle");
        GameManager.Instance.GetPlayer().setSide(SIDE.Middle);
        Debug.Log(GameManager.Instance.GetPlayer().m_anim);
        /*btn_tap = tapDoc.rootVisualElement.Q("TapButton") as Button;
        btn_tap.RegisterCallback<ClickEvent>(ToGame);*/
        RestartUI(2);
    }

    public void ToShop(ClickEvent evt)
    {
        cosmeticsDoc.enabled = false;
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
        if(shopDoc.enabled == true)
        {
            shopDoc.enabled = false;
        }else if(cosmeticsDoc.enabled == true){
            cosmeticsDoc.enabled = false;
        }
    }

    public void ToGameOver()
    {
        //GameManager.Instance.GameOver();
        inGameDoc.enabled = false;
        gameOverDoc.enabled = true;

        PlayerPrefs.SetInt(AppPlayerPrefs.PLAYER_COINS, (int)GameManager.Instance.CoinsObtained);

        /*btn_home = gameOverDoc.rootVisualElement.Q("HomeButton") as Button;
        btn_home.RegisterCallback<ClickEvent>(ToTap);

        btn_shop = gameOverDoc.rootVisualElement.Q("ShopButton") as Button;
        btn_shop.RegisterCallback<ClickEvent>(ToShop);

        finalScoreLabel = gameOverDoc.rootVisualElement.Q("ScoreLab") as Label;

        finalScoreLabel.text = scoreLabel.text;*/
        RestartUI(5);
    }

    public void ToLeaderboard(ClickEvent evt)
    {
        gameOverDoc.enabled = false;
        //GameManager.Instance.OpenLeaderboard();
        playFabManager.GetLeaderboardEntriesAroundPlayer();
    }

    public void OutLeaderboard()
    {
        playFabManager.CloseLeaderboardPanel();
        ToGameOver();
    }

    public void ToCosmetics(ClickEvent evt)
    {
        shopDoc.enabled = false;
        cosmeticsDoc.enabled = true;
        RestartUI(6);
    }

    public void SlashTutorial()
    {
        tutorialDoc.enabled = true;
        tutorialLabel.text = "Slide down to dodge enemy attack...\nand finish him!!!";
        Time.timeScale = Mathf.Lerp(1, 0, 0.8f);
        GameManager.Instance.GetPlayer().SetTutorialActive(TutorialBlock.Slash);
        StartCoroutine(TimeScaleDecrease());
    }

    public void ShieldTutorial()
    {
        tutorialDoc.enabled = true;
        tutorialLabel.text = "Jump to avoid it's shield...";
        Time.timeScale = Mathf.Lerp(1, 0, 0.8f);
        GameManager.Instance.GetPlayer().SetTutorialActive(TutorialBlock.Shield);
        StartCoroutine(TimeScaleDecrease());
    }

    public void GroundWaveTutorial()
    {
        tutorialDoc.enabled = true;
        tutorialLabel.text = "Jump to avoid it's attack...\nit's everything he's got!!!";
        Time.timeScale = Mathf.Lerp(1, 0, 0.8f);
        GameManager.Instance.GetPlayer().SetTutorialActive(TutorialBlock.GroundWave);
        StartCoroutine(TimeScaleDecrease());
    }

    public void OutOfTutorial()
    {
        tutorialDoc.enabled = false;
        m_corroutineRuning = false;
        StopCoroutine(TimeScaleDecrease());
        Time.timeScale = 1;
    }

    private IEnumerator TimeScaleDecrease()
    {
        m_corroutineRuning = true;
        yield return new WaitForSeconds(0.5f);
        if(m_corroutineRuning) Time.timeScale = 0.0f;
        m_corroutineRuning = false;
    }

    void RestartUI(int caso)
    {
        switch (caso)
        {
            case 1:
                btnPause = inGameDoc.rootVisualElement.Q("PauseButton") as Button;
                btnPause.RegisterCallback<ClickEvent>(OnPause);
                scoreLabel = inGameDoc.rootVisualElement.Q("ScoreLab") as Label;
                coinsLabel = inGameDoc.rootVisualElement.Q("CoinsLab") as Label;
                coinsLabel = inGameDoc.rootVisualElement.Q("CoinsLab") as Label;
                break;

            case 2:
                btnTap = tapDoc.rootVisualElement.Q("TapButton") as Button;
                btnTap.RegisterCallback<ClickEvent>(ToGame);
                break;

            case 3:
                btnCloseShop = shopDoc.rootVisualElement.Q("CloseButton") as Button;
                btnCloseShop.RegisterCallback<ClickEvent>(OnClose);

                btnCosmetics = shopDoc.rootVisualElement.Q("CostemticsButton") as Button;
                btnCosmetics.RegisterCallback<ClickEvent>(ToCosmetics);
                break;

            case 4:
                btnResume = pauseDoc.rootVisualElement.Q("ResumeButton") as Button;
                btnResume.RegisterCallback<ClickEvent>(ToResume);
                break;

            case 5:
                btn_play = gameOverDoc.rootVisualElement.Q("RetryButton") as Button;
                btn_play.RegisterCallback<ClickEvent>(ToTap);

                btnShop = gameOverDoc.rootVisualElement.Q("ShopButton") as Button;
                btnShop.RegisterCallback<ClickEvent>(ToShop);

                btnLeaderBoard = gameOverDoc.rootVisualElement.Q("LeaderboardButton") as Button;
                btnLeaderBoard.RegisterCallback<ClickEvent>(ToLeaderboard);

                finalScoreLabel = gameOverDoc.rootVisualElement.Q("ScoreLab") as Label;

                finalScoreLabel.text = scoreLabel.text;
                break;

            case 6:
                btnItems = cosmeticsDoc.rootVisualElement.Q("ItemsButton") as Button;
                btnItems.RegisterCallback<ClickEvent>(ToShop);

                btnCloseCosmetics = cosmeticsDoc.rootVisualElement.Q("CloseButton") as Button;
                btnCloseCosmetics.RegisterCallback<ClickEvent>(OnClose);
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
        comboLabel.text = "\nx"+GameManager.Instance.AccumulatedCombo.ToString();
    }
}

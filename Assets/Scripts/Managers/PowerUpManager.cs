using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpsEnum { BOOTS = 0, DRON = 1, WALLS = 2, HYPERSPEED = 3, MOTORBIKE = 4 }
//                          35%         20%       20%         15%              10%

public enum PowerUpsTierUpCosts { TO_LVL_2 = 100, TO_LVL_3 = 200, TO_LVL_4 = 300, TO_LVL_5 = 400 }

public class PowerUpManager : TemporalSingleton<PowerUpManager>
{
    private static uint      m_totalNumofPowerUps = 5;
    public bool              m_powerUpAppears;
    [SerializeField]
    private PowerUpEffect[]  m_allPowerUps = new PowerUpEffect[m_totalNumofPowerUps];
    private float            m_powerUpTimer;
    [SerializeField]
    private float            m_powerUpSpawnTime;
    private PlayerController m_player;

    private int m_bootsTierPlayerPrefs;
    private int m_dronTierPlayerPrefs;
    private int m_wallsTierPlayerPrefs;
    private int m_motorbikeTierPlayerPrefs;
    private int m_hyperspeedTierPlayerPrefs;

    private void Start()
    {
        m_bootsTierPlayerPrefs      = PlayerPrefs.GetInt(AppPlayerPrefs.BOOTS_TIER);
        if(m_bootsTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayerPrefs.BOOTS_TIER, 1);

        m_dronTierPlayerPrefs       = PlayerPrefs.GetInt(AppPlayerPrefs.DRON_TIER);
        if (m_dronTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayerPrefs.DRON_TIER, 1);

        m_wallsTierPlayerPrefs      = PlayerPrefs.GetInt(AppPlayerPrefs.WALLS_TIER);
        if (m_wallsTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayerPrefs.WALLS_TIER, 1);

        m_motorbikeTierPlayerPrefs  = PlayerPrefs.GetInt(AppPlayerPrefs.MOTORBIKE_TIER);
        if (m_motorbikeTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayerPrefs.MOTORBIKE_TIER, 1);

        m_hyperspeedTierPlayerPrefs = PlayerPrefs.GetInt(AppPlayerPrefs.HYPERSPEED_TIER);
        if (m_hyperspeedTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayerPrefs.HYPERSPEED_TIER, 1);

        m_player = FindObjectOfType<PlayerController>();
        m_powerUpAppears = false;
        m_powerUpTimer = 0.0f;
        m_powerUpSpawnTime = 5.0f;

        CreateAllPowerUps();
        m_player.InitializePowerUpObjects();
    }

    private void Update()
    {
        m_powerUpTimer += Time.deltaTime;
        if (m_powerUpTimer >= m_powerUpSpawnTime)
        {
            m_powerUpTimer = 0.0f;
            m_powerUpAppears = true;
        }
    }

    private void CreateAllPowerUps()
    {
        m_allPowerUps[(int)PowerUpsEnum.BOOTS]      = BootsPowerUp.CreateInstance(m_bootsTierPlayerPrefs);
        m_allPowerUps[(int)PowerUpsEnum.DRON]       = DronPowerUp.CreateInstance(m_dronTierPlayerPrefs);
        m_allPowerUps[(int)PowerUpsEnum.WALLS]      = WallPowerUp.CreateInstance(m_wallsTierPlayerPrefs);
        m_allPowerUps[(int)PowerUpsEnum.HYPERSPEED] = HyperspeedPowerUp.CreateInstance(m_hyperspeedTierPlayerPrefs);
        m_allPowerUps[(int)PowerUpsEnum.MOTORBIKE]  = MotorbikePowerup.CreateInstance(m_motorbikeTierPlayerPrefs);
    }

    public void SetPowerUpAppears(bool powerUpAppears) { m_powerUpAppears = powerUpAppears; }
    public bool GetPowerUpAppears() { return m_powerUpAppears; }
    public uint GetTotalNumOfPowerUps() { return m_totalNumofPowerUps; }
    public PowerUpEffect GetPowerUpEffect(int pu)
    {
        if (0 <= pu && pu < 35) return m_allPowerUps[0];
        else if (35 <= pu && pu < 55) return m_allPowerUps[1];
        else if (55 <= pu && pu < 75) return m_allPowerUps[2];
        else if (75 <= pu && pu < 90) return m_allPowerUps[3];
        else if (90 <= pu && pu < 100) return m_allPowerUps[4];
        else return m_allPowerUps[0];
    }

    public HyperspeedPowerUp GetHyperspeedPowerUp()
    {
        return (HyperspeedPowerUp)m_allPowerUps[(int)PowerUpsEnum.HYPERSPEED];
    }

    public MotorbikePowerup GetMotorbikePowerUp()
    {
        return (MotorbikePowerup)m_allPowerUps[(int)PowerUpsEnum.MOTORBIKE];
    }

    public PlayerController GetPlayer() { return m_player; }
}
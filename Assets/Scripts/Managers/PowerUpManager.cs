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
    private int              m_chosenPowerUp;
    [SerializeField]
    private float            m_powerUpSpawnTime;
    private PlayerController m_player;

    private int m_bootsTierPlayerPrefs;
    private int m_dronTierPlayerPrefs;
    private int m_wallsTierPlayerPrefs;
    private int m_motorbikeTierPlayerPrefs;
    private int m_hyperspeedTierPlayerPrefs;

    [SerializeField]
    private Mesh m_bootsMesh;
    [SerializeField]
    private Mesh m_dronMesh;
    [SerializeField]
    private Mesh m_motorbikeMesh;
    [SerializeField]
    private Mesh m_wallsMesh;
    [SerializeField]
    private Mesh m_hyperspeedMesh;

    private void Start()
    {
        m_bootsTierPlayerPrefs      = PlayerPrefs.GetInt(AppPlayePrefs.BOOTS_TIER);
        if(m_bootsTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayePrefs.BOOTS_TIER, 1);

        m_dronTierPlayerPrefs       = PlayerPrefs.GetInt(AppPlayePrefs.DRON_TIER);
        if (m_dronTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayePrefs.DRON_TIER, 1);

        m_wallsTierPlayerPrefs      = PlayerPrefs.GetInt(AppPlayePrefs.WALLS_TIER);
        if (m_wallsTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayePrefs.WALLS_TIER, 1);

        m_motorbikeTierPlayerPrefs  = PlayerPrefs.GetInt(AppPlayePrefs.MOTORBIKE_TIER);
        if (m_motorbikeTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayePrefs.MOTORBIKE_TIER, 1);

        m_hyperspeedTierPlayerPrefs = PlayerPrefs.GetInt(AppPlayePrefs.HYPERSPEED_TIER);
        if (m_hyperspeedTierPlayerPrefs == 0) PlayerPrefs.SetInt(AppPlayePrefs.HYPERSPEED_TIER, 1);

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
        if (0 <= pu && pu < 35)
        {
            m_chosenPowerUp = (int)PowerUpsEnum.BOOTS;
            return m_allPowerUps[m_chosenPowerUp];
        }
        else if (35 <= pu && pu < 55)
        {
            m_chosenPowerUp = (int)PowerUpsEnum.DRON;
            return m_allPowerUps[m_chosenPowerUp];
        }
        else if (55 <= pu && pu < 75)
        {
            m_chosenPowerUp = (int)PowerUpsEnum.WALLS;
            return m_allPowerUps[m_chosenPowerUp];
        }
        else if (75 <= pu && pu < 90)
        {
            m_chosenPowerUp = (int)PowerUpsEnum.HYPERSPEED;
            return m_allPowerUps[m_chosenPowerUp];
        }
        else if (90 <= pu && pu < 100)
        {
            m_chosenPowerUp = (int)PowerUpsEnum.MOTORBIKE;
            return m_allPowerUps[m_chosenPowerUp];
        }
        else
        {
            m_chosenPowerUp = (int)PowerUpsEnum.BOOTS;
            return m_allPowerUps[(int)PowerUpsEnum.BOOTS];
        }
    }

    public Mesh GetPowerUpMesh()
    {
        switch(m_chosenPowerUp)
        {
            case (int)PowerUpsEnum.BOOTS:
                return m_bootsMesh;
            case (int)PowerUpsEnum.DRON:
                return m_dronMesh;
            case (int)PowerUpsEnum.WALLS:
                return m_wallsMesh;
            case (int)PowerUpsEnum.HYPERSPEED:
                return m_hyperspeedMesh;
            case (int)PowerUpsEnum.MOTORBIKE:
                return m_motorbikeMesh;
            default:
                return m_bootsMesh;
        }
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum PowerUpsEnum { BOOTS = 0, DRON = 1, WALLS = 2, HYPERSPEED = 3, MOTORBIKE = 4 }

public class GameManager : TemporalSingleton<GameManager>
{
    [SerializeField]
    private bool m_runActive = true;
    private float m_timer;
    private float m_auxRunSpeed;
    [SerializeField]
    private float m_metersTraveled;
    private float m_score;
    private float m_accumulatedCombo;
    private uint  m_coinsObtained;

    private static uint  m_totalNumofPowerUps = 5;
    public bool          m_powerUpAppears;
    [SerializeField]
    private PowerUpEffect[] m_allPowerUps = new PowerUpEffect[m_totalNumofPowerUps];
    private float        m_powerUpTimer;
    [SerializeField]
    private float        m_powerUpSpawnTime;

    private void OnEnable()
    {
        m_allPowerUps[(int)PowerUpsEnum.BOOTS]      = ScriptableObject.CreateInstance<BootsPowerUp>();
        m_allPowerUps[(int)PowerUpsEnum.DRON]       = ScriptableObject.CreateInstance<DronPowerUp>();
        m_allPowerUps[(int)PowerUpsEnum.WALLS]      = ScriptableObject.CreateInstance<WallPowerUp>();
        m_allPowerUps[(int)PowerUpsEnum.HYPERSPEED] = ScriptableObject.CreateInstance<HyperspeedPowerUp>();
        m_allPowerUps[(int)PowerUpsEnum.MOTORBIKE]  = ScriptableObject.CreateInstance<MotorbikePowerup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_runActive        = true;
        m_metersTraveled   = 0.0f;
        m_timer            = 0.0f;
        m_auxRunSpeed      = 0.0f;
        m_accumulatedCombo = 0.0f;
        m_score            = 0.0f;
        m_coinsObtained    = 0;
        m_powerUpAppears   = false;
        m_powerUpTimer     = 0.0f;
        m_powerUpSpawnTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //e = v * t
        m_metersTraveled += (m_timer + Time.deltaTime) * SpeedManager.Instance.GetRunSpeed();

        // (s * m/s = m in one frame) * combo player has in that frame = score acumulated in the frame
        m_score += Time.deltaTime * SpeedManager.Instance.GetRunSpeed() * TranslateCombo();

        m_powerUpTimer += Time.deltaTime;
        if(m_powerUpTimer >= m_powerUpSpawnTime)
        {
            m_powerUpTimer = 0.0f;
            m_powerUpAppears = true;
        }
    }

    public void AddComboPoint() { m_accumulatedCombo++; }
    public void ResetCombo() { m_accumulatedCombo = 0; }

    public void AddCoin() { m_coinsObtained++; }

    public float TranslateCombo() //This function will control the combo traduction to score multiplicator
    {
        return 1 + m_accumulatedCombo / 10;
    }

    public void Resume()
    {
        SpeedManager.Instance.SetRunSpeed(m_auxRunSpeed);
        m_runActive = true;
    }
    public void GameOver()
    {
        m_auxRunSpeed = SpeedManager.Instance.GetRunSpeed();
        SpeedManager.Instance.SetRunSpeed(0.0f);
        m_runActive = false;
    }

    public bool GetRunActive()              { return m_runActive; }
    public void SetRunActive(bool runSpeed) { m_runActive = runSpeed; }
    public float GetTraveledMeters() { return m_metersTraveled; }
    public void SetPowerUpAppears(bool powerUpAppears) { m_powerUpAppears = powerUpAppears; }
    public bool GetPowerUpAppears() { return m_powerUpAppears; }
    public uint  GetTotalNumOfPowerUps() { return m_totalNumofPowerUps; }
    public PowerUpEffect GetPowerUpEffect(int pu) { return m_allPowerUps[pu]; }
}

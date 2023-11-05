using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : TemporalSingleton<GameManager>
{
    [SerializeField]
    private bool      m_runActive = true;
    private float m_timer;
    private float m_auxRunSpeed;
    [SerializeField]
    private float     m_metersTravelled;
    private float     m_score;
    private float     m_accumulatedCombo;
    private uint      m_coinsObtained;
   

    // Start is called before the first frame update
    void Start()
    {
        m_runActive        = true;
        m_metersTravelled  = 0.0f;
        m_timer            = 0.0f;
        m_auxRunSpeed      = 0.0f;
        m_accumulatedCombo = 0.0f;
        m_score            = 0.0f;
        m_coinsObtained = 0;
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //e = v * t
        m_metersTravelled += (m_timer + Time.deltaTime) * SpeedManager.Instance.GetRunSpeed();

        m_score = m_metersTravelled * TraduceCombo();
    }

    public void AddComboPoint() { m_accumulatedCombo++; }
    public void ResetCombo() { m_accumulatedCombo = 0; }

    public void AddCoin() { m_coinsObtained++; }
    
    public float TraduceCombo() //This function will control the combo traduction to score multiplicator
    {
        return 1 + m_accumulatedCombo/10;
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

    public bool GetRunActive() { return m_runActive; }
    public void SetRunActive(bool runSpeed) { m_runActive = runSpeed; }

    public float GetTraveledMeters() { return m_metersTravelled; }

}

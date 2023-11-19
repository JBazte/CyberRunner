using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : TemporalSingleton<GameManager>
{
    [SerializeField]
    private bool  m_runActive;
    private float m_timer;
    private float m_auxRunSpeed;
    [SerializeField]
    private float m_metersTraveled;
    private float m_score;
    private int   m_accumulatedCombo;
    private uint  m_coinsObtained;
    private float m_initialRunSpeed = 12.0f;
    private float m_initialAcceleration = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        m_runActive        = false;
        m_metersTraveled   = 0.0f;
        m_timer            = 0.0f;
        m_auxRunSpeed      = 0.0f;
        m_accumulatedCombo = 0;
        m_score            = 0.0f;
        m_coinsObtained    = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //e = v * t
        m_metersTraveled += (m_timer + Time.deltaTime) * SpeedManager.Instance.GetRunSpeed();

        // (s * m/s = m in one frame) * combo player has in that frame = score acumulated in the frame
        if(m_runActive) m_score += Time.deltaTime * SpeedManager.Instance.GetRunSpeed() * TranslateCombo();
    }

    public void AddComboPoint() { AccumulatedCombo++; }
    public void ResetCombo() { AccumulatedCombo = 0; }

    public void AddCoin() { CoinsObtained++; }

    public float TranslateCombo() //This function will control the combo traduction to score multiplicator
    {
        return 1 + AccumulatedCombo / 10;
    }

    public void StartRun() { 
        m_runActive = true; 
        SpeedManager.Instance.SetRunSpeed(m_initialRunSpeed);
        SpeedManager.Instance.SetAcceleration(m_initialAcceleration);
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

    public float Score { get => m_score; set => m_score = value; }
    public uint CoinsObtained { get => m_coinsObtained; set => m_coinsObtained = value; }
    public float AccumulatedCombo { get => m_accumulatedCombo; set => m_accumulatedCombo = value; }
}

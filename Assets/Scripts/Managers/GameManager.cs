using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TemporalSingleton<GameManager>
{
    [SerializeField]
    private bool m_runActive = true;
    [SerializeField]
    private float     m_metersTravelled;
    private float     m_timer;
    private float     m_auxRunSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_runActive = true;
        m_metersTravelled = 0.0f;
        m_timer           = 0.0f;
        m_auxRunSpeed     = 0.0f;
    }

    private void FixedUpdate()
    {
        //e = v * t
        m_metersTravelled += (m_timer + Time.fixedDeltaTime) * SpeedManager.Instance.getRunSpeed();
        Debug.Log(m_metersTravelled);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void resume()
    {
        SpeedManager.Instance.setRunSpeed(m_auxRunSpeed);
        m_runActive = true;
    }
    public void gameOver()
    {
        m_auxRunSpeed = SpeedManager.Instance.getRunSpeed();
        SpeedManager.Instance.setRunSpeed(0.0f);
        m_runActive = false;
    }

    public bool getRunActive() { return m_runActive; }
}

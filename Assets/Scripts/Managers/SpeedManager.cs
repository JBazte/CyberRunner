using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : TemporalSingleton<SpeedManager>
{
    private float m_runSpeed;
    [SerializeField]
    private float m_acceleration;
    private float m_maxSpeed;
    private float m_hyperspeedVelocity;
    private float m_auxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_runSpeed = 0.0f; //12.0f
        m_maxSpeed = 30.0f;
        m_acceleration = 0.01f; //0.1f;
        m_hyperspeedVelocity = 50.0f;
        m_auxSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_runSpeed <= m_maxSpeed && GameManager.Instance.GetRunActive())
        {
            m_runSpeed += m_acceleration * Time.deltaTime;
        }
    }

    public float GetRunSpeed() { return m_runSpeed; }
    public void SetRunSpeed(float runSpeed) { m_runSpeed = runSpeed; }
    public void SetAcceleration(float acceleration) { m_acceleration = acceleration; }
    public void Hyperspeed()
    {
        m_auxSpeed = m_runSpeed;
        m_runSpeed = m_hyperspeedVelocity;
    }

    public void ExitHyperspeed()
    {
        m_runSpeed = m_auxSpeed;
    }
}

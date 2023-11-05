using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : TemporalSingleton<SpeedManager>
{
    private float m_runSpeed;
    [SerializeField]
    private float m_acceleration;
    private float m_maxSpeed;
    private float m_currentspeed;

    // Start is called before the first frame update
    void Start()
    {
        m_runSpeed     = 12.0f;
        m_maxSpeed     = 30.0f;
        m_acceleration = 0.1f;
    }

    private void FixedUpdate()
    {
       
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
    public float GetCurrentSpeed() { return m_currentspeed; }
    public void SetRunSpeed(float runSpeed) { m_runSpeed = runSpeed; }
}

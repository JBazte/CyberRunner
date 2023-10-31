using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : TemporalSingleton<SpeedManager>
{
    private float m_runSpeed;
    [SerializeField]
    private float m_acceleration;
    private float m_maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_runSpeed     = 12.0f;
        m_maxSpeed     = 30.0f;
        m_acceleration = 0.1f;
    }

    private void FixedUpdate()
    {
        if (m_runSpeed <= m_maxSpeed && GameManager.Instance.getRunActive())
        {
            m_runSpeed += m_acceleration * Time.fixedDeltaTime;
            Debug.Log("SPEED ------->    " + m_runSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float getRunSpeed() { return m_runSpeed; }
    public void setRunSpeed(float runSpeed) { m_runSpeed = runSpeed; }
}

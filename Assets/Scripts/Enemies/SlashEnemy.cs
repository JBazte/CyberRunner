using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEnemy : EnemyAbstract
{
    private GameObject m_waeapon;
    private float      m_targetTime = 4.0f;
    private bool       m_isAttaking = false;

    void Start()
    {
        m_waeapon = gameObject.transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        m_targetTime -= Time.deltaTime;
        if (m_targetTime <= 0)
        {
            Attack();
        }

        if(!m_hasAttacked )
        {
            // Que compruebe distancia con el jugador para ver cuando realiza el ataque
        }
    }

    public override void Attack()
    {
        if(!m_hasAttacked)
        { 
            if (!m_isAttaking)
            {
                m_waeapon.SetActive(true);
                m_isAttaking = true;
                m_targetTime = 1.0f;
            }
            else if (m_isAttaking)
            {
                m_waeapon.SetActive(false);
                m_isAttaking = false;
                m_targetTime = 3.0f;
            }
        }

        m_hasAttacked = true;
    }
}

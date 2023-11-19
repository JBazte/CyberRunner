using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : EnemyAbstract
{ 
    public GameObject m_player;
    private GameObject m_waeapon;
    private bool       m_isAttaking = false;
    
    
    void Start()
    {
        m_waeapon = gameObject.transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        if(gameObject.transform.position.z <= 15)
        {
            Attack();
        }
        if (m_waeapon.transform.position.z <= -5.0f)
        {
            m_waeapon.SetActive(false);
        }
    }

    public override void Attack()
    {
        if(!m_hasAttacked)
        { 
            m_waeapon.SetActive(true);
            m_isAttaking = true;
        }

        m_hasAttacked = true;
    }
}
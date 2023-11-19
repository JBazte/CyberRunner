using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEnemy : EnemyAbstract
{
    private bool       m_isAttaking = false;


    void Start()
    {
        //m_weapon = gameObject.transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        if(gameObject.transform.position.z <= 3.5)
        {
            Attack();
        }
        if (m_weapon.transform.position.z <= -4.0f)
        {
            m_weapon.SetActive(false);
        }
    }

    public override void Attack()
    {
        if(!m_hasAttacked)
        { 
            m_weapon.SetActive(true);
            m_isAttaking = true;
        }

        m_hasAttacked = true;
    }
}

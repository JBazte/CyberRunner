using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEnemy : EnemyAbstract
{
    private bool       m_isAttaking = false;
    private Animator Slash;
    
    void Start()
    {
        Slash = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 11);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player")&& !m_hasAttacked)
            {
                anim();
                Invoke("Attack", 0.4f);
            }
        }
        
        if (m_weapon.transform.position.z <= -2.0f)
        {
            m_weapon.SetActive(false);
            Slash.Play("Idle");    

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
    public void anim()
    {
        Slash.Play("Attack");    }
}
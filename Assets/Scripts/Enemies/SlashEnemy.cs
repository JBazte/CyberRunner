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
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 17);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player")&& !m_hasAttacked)
            {
                anim();
                Invoke("Attack", 1.0f);
            }
        }
        
        if (m_weapon.transform.position.z <= -3.0f)
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
        Slash.SetTrigger("DontAttack");
        Slash.ResetTrigger("Attack");

    }
    public void anim()
    {
        Slash.SetTrigger("Attack");
    }
}
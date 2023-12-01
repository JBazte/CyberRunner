using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : EnemyAbstract
{ 
    private bool       m_isAttaking = false;
    private Animator Shield;
    
    void Start()
    {
        Shield = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 30);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                anim();
                Invoke("Attack", 1.3f);
            }
        }

        if (m_weapon.transform.position.z <= -3.0f)
        {
            m_weapon.SetActive(false);
            Shield.SetTrigger("DontAttack");
            

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
        Shield.SetTrigger("Attack");

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GroundWaveEnemy : EnemyAbstract
{
    private float CollPosz;
    private float CollPosx;
    private float CollPosy;
    private Animator Wave;

    void Start()
    {
        Wave = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        CollPosz = gameObject.transform.position.z;
        CollPosx = gameObject.transform.position.x;
        CollPosy = gameObject.transform.position.y;
        m_weapon.transform.position += new Vector3(0,0 , (-SpeedManager.Instance.GetRunSpeed()-1) * Time.deltaTime);
        
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 40);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player")&& !m_hasAttacked)
            {
                anim();
                Invoke("Attack", 1.6f);
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
            m_weapon.transform.position = new Vector3(CollPosx,CollPosy ,CollPosz-2);
        }

        m_hasAttacked = true;
        Wave.SetTrigger("DontAttack");
        Wave.ResetTrigger("Attack");

    }
    public void anim()
    {
        Wave.SetTrigger("Attack");
    }
}
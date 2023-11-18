using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GroundWaveEnemy : EnemyAbstract

{
    private GameObject m_waeapon;
    private float CollPosz;
    private float CollPosx;
    private float CollPosy;
    private bool isAttaking = false;
    void Start()
    {
        m_waeapon = gameObject.transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        CollPosz = gameObject.transform.position.z;
        CollPosx = gameObject.transform.position.x;
        CollPosy = gameObject.transform.position.y;
        m_waeapon.transform.position += new Vector3(0,0 , (-SpeedManager.Instance.GetRunSpeed()-1) * Time.deltaTime);
        
        if(gameObject.transform.position.z <= 13.0f)
        {
            Attack();
        }
        if (m_waeapon.transform.position.z <= -3.0f)
        {
            m_waeapon.SetActive(false);
        }
    }

    public override void Attack()
    {
        if(!m_hasAttacked)
        {
            
            m_waeapon.SetActive(true);
            m_waeapon.transform.position = new Vector3(CollPosx,CollPosy ,CollPosz);
            isAttaking = true;
           
        }

        m_hasAttacked = true;
    }
}
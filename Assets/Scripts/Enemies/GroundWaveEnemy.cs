using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GroundWaveEnemy : EnemyAbstract

{
    private GameObject Weapon;
    private float TargetTime = 4.0f;
    private float CollPosz;
    private float CollPosx;
    private float CollPosy;

    
    private bool isAttaking = false;
    public bool EnemyActive = true;

    void Start()
    {
        Weapon = gameObject.transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        TargetTime -= Time.deltaTime;
        CollPosz = gameObject.transform.position.z;
        CollPosx = gameObject.transform.position.x;
        CollPosy = gameObject.transform.position.y;
        Weapon.transform.position += new Vector3(0,0 , (-SpeedManager.Instance.GetRunSpeed()-1) * Time.deltaTime);
        
        if (TargetTime <= 0)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        if(!m_hasAttacked)
        {
            if (!isAttaking)
            {
                Weapon.SetActive(true);
                Weapon.transform.position = new Vector3(CollPosx,CollPosy ,CollPosz);
                isAttaking = true;
                TargetTime = 3.0f;
            }
            else if (isAttaking)
            {
                Weapon.SetActive(false);
                isAttaking = false;
                TargetTime = 3.0f;
            }
        }

        m_hasAttacked = true;
    }
}
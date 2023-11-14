using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class EnemyType2 : Enemy
{
    private GameObject Weapon;
    private float TargetTime = 4.0f;
    
    private bool isAttaking = false;
    public bool EnemyActive = true;
    

    void Start()
    {
        Weapon = gameObject.transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        TargetTime -= Time.deltaTime;
        if (TargetTime <= 0)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        if (!isAttaking)
        {
            Weapon.SetActive(true);
            isAttaking = true;
            TargetTime = 1.0f;
        }
        else if (isAttaking)
        {
            Weapon.SetActive(false);
            isAttaking = false;
            TargetTime = 3.0f;
        }
    }

    public override void ActivateEnemy()
    {
        gameObject.SetActive(true);
        EnemyActive = true;
    }

    public override void DeactivateEnemy()
    {
        gameObject.SetActive(false);
        EnemyActive = false;

    }
}
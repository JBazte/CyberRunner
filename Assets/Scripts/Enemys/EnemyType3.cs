using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : Enemy
{
    
    public bool EnemyActive = true;


    public override void Attack()
    {
        throw new System.NotImplementedException();
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
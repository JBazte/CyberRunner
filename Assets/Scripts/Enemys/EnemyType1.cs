using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    private BoxCollider collider;
    private float TargetTime = 4.0f;
    private float zSize;
    private float AuxZ;
    
    private bool isAttaking = false;
    public bool EnemyActive = true;
    

    void Start()
    {
         collider = gameObject.GetComponent<BoxCollider>();
         AuxZ = gameObject.GetComponent<BoxCollider>().size.z;
    }
    
    void Update()
    {
        TargetTime -= Time.deltaTime;
        if (!isAttaking && TargetTime <= 0)
        {
            collider.size = new Vector3(collider.size.x, collider.size.y, zSize);
            isAttaking = true;
            TargetTime = 4.0f;
        }

        if (isAttaking && TargetTime <= 3)
        {
            collider.size = new Vector3(collider.size.x, collider.size.y, AuxZ);
            isAttaking = false;
            TargetTime = 4.0f;
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

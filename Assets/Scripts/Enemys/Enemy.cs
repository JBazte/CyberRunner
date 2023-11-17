using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void ActivateEnemy();
    public abstract void DeactivateEnemy();
    public abstract void Attack();
}

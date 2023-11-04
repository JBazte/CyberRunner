using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public abstract class PowerUpEffect : ScriptableObject
{
    [SerializeField]
    protected float      m_duration;
    protected GameObject m_player;

    public abstract void ExecuteAction(GameObject player);
    public abstract void FinishAction();

    public void isPickedUp()
    {
        
    }

    public abstract IEnumerator StartCountDown();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public abstract class PowerUpEffect : ScriptableObject
{
    [SerializeField]
    protected float      m_duration;
    protected GameObject m_player;
    protected bool       m_isAlreadyActive = false;

    public abstract void SetTier(int tier);

    public abstract void ExecuteAction(GameObject player);
    public abstract void FinishAction();
    //public abstract void ResetCountDown();

    public IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }

    public bool GetIsAlreadyActive() {  return m_isAlreadyActive; }
}
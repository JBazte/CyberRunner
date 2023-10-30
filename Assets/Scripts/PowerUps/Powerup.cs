using System;
using System.Timers;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    protected float countdownTimer;
    protected bool isCountingDown = false;
    protected MonoBehaviour powerUpScript;

    // M�todo para iniciar una cuenta atr�s con un temporizador
    public void StartCountdown(float seconds)
    {
        countdownTimer = seconds;
        isCountingDown = true;
    }

    // M�todo para detectar una colisi�n OnTrigger
    protected abstract void OnTriggerEnter(Collider other);

    private void Update()
    {
        if (isCountingDown)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                CountdownFinished();
                isCountingDown = false;
            }
        }
    }

    // M�todo llamado cuando la cuenta atr�s termina
    protected virtual void CountdownFinished()
    {
        this.enabled = false;
    }
}
using System;
using System.Timers;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    protected float countdownTimer;
    protected bool isCountingDown = false;
    protected MonoBehaviour powerUpScript;

    // Método para iniciar una cuenta atrás con un temporizador
    public void StartCountdown(float seconds)
    {
        countdownTimer = seconds;
        isCountingDown = true;
    }

    // Método para detectar una colisión OnTrigger
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

    // Método llamado cuando la cuenta atrás termina
    protected virtual void CountdownFinished()
    {
        this.enabled = false;
    }
}
using System;
using System.Timers;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    protected float timerDuration;
    protected float timerElapsed;
    protected bool isCountingDown = false;

    public abstract void ActivatePowerUp();
    public abstract void DeactivatePowerUp();

    // M�todo para iniciar una cuenta atr�s con un temporizador
    public void StartCountdown(float seconds)
    {
        if (!isCountingDown)
        {
            timerDuration = seconds;
            isCountingDown = true;
            timerElapsed = 0;
            ActivatePowerUp();
        }
    }

    // M�todo para detectar una colisi�n OnTrigger
    protected abstract void OnTriggerEnter(Collider other);

    private void Update()
    {
        Debug.Log("Tiempo: "+timerDuration.ToString()); //Debug

        if (isCountingDown)
        {
            timerElapsed += Time.deltaTime;
            if (timerElapsed >= timerDuration)
            {
                CountdownFinished();
            }
        }
    }

    // M�todo llamado cuando la cuenta atr�s termina
    protected virtual void CountdownFinished()
    {
        if (isCountingDown)
        {
            DeactivatePowerUp();
            isCountingDown = false;
        }
    }
}
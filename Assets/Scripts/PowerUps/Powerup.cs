using System;
using System.Timers;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    private Timer timer;

    protected Powerup(double interval)
    {
        timer = new Timer(interval);
    }

    public void Iniciar()
    {
        timer.Start();
    }

    public void Stop()
    {
        timer.Stop();
    }
}

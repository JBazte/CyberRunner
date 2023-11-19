using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect m_powerUpEffect;

    public void SetPowerUpEffect(PowerUpEffect powerUpEffect)
    {
        m_powerUpEffect = powerUpEffect;
    }

    public PowerUpEffect GetPowerUpEffect() { return m_powerUpEffect; }
}
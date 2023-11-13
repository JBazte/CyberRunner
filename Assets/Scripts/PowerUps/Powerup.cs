using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect m_powerUpEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            m_powerUpEffect.ExecuteAction(other.gameObject);
            StartCoroutine(m_powerUpEffect.StartCountDown());
            gameObject.SetActive(false);
            Debug.Log("COGIDO : " + m_powerUpEffect);
        }
    }

    public void SetPowerUpEffect(PowerUpEffect powerUpEffect)
    {
        m_powerUpEffect = powerUpEffect;
    }
}
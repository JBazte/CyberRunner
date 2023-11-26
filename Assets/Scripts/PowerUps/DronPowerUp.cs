using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DronPowerUp")]
public class DronPowerUp : PowerUpEffect
{
    private DronObject m_dron;

    public static PowerUpEffect CreateInstance(int tier)
    {
        DronPowerUp instance = new DronPowerUp();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(int tier)
    {
        switch (tier)
        {
            case 1:
                m_duration = 5;
                break;
            case 2:
                m_duration = 5.5f;
                break;
            case 3:
                m_duration = 6f;
                break;
            case 4:
                m_duration = 7f;
                break;
            case 5:
                m_duration = 8.5f;
                break;
            default:
                Debug.Log("PowerUp TIER for " + this + " is not valid!!");
                break;
        }
    }

    public override void ExecuteAction(GameObject player)
    {
        m_dron = FindObjectOfType<DronObject>();
        m_isAlreadyActive = true;
        m_dron.ActivateDron();
    }

    public override void FinishAction()
    {
        m_dron.DeactivateDron();
        m_isAlreadyActive = false;
    }

}



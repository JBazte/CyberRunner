using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DronPowerUp")]
public class DronPowerUp : PowerUpEffect
{
    private DronObject m_dron;

    public static PowerUpEffect CreateInstance(PowerUpTiers tier)
    {
        DronPowerUp instance = new DronPowerUp();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(PowerUpTiers tier)
    {
        if (tier == PowerUpTiers.TIER1)
        {
            m_duration = 5;
        }
        else if (tier == PowerUpTiers.TIER2)
        {
            m_duration = 5.5f;
        }
        else if (tier == PowerUpTiers.TIER3)
        {
            m_duration = 6f;
        }
        else if (tier == PowerUpTiers.TIER4)
        {
            m_duration = 7f;
        }
        else if (tier == PowerUpTiers.TIER5)
        {
            m_duration = 8.5f;
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



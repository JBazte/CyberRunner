using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/MotorbikePowerUp")]
public class MotorbikePowerup : PowerUpEffect
{
    private float m_motorbikeDuration;

    public static PowerUpEffect CreateInstance(PowerUpTiers tier)
    {
        MotorbikePowerup instance = new MotorbikePowerup();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(PowerUpTiers tier)
    {
        m_duration = 1;

        if (tier == PowerUpTiers.TIER1)
        {
            m_motorbikeDuration = 5;
        }
        else if (tier == PowerUpTiers.TIER2)
        {
            m_motorbikeDuration = 6;
        }
        else if (tier == PowerUpTiers.TIER3)
        {
            m_motorbikeDuration = 7;
        }
        else if (tier == PowerUpTiers.TIER4)
        {
            m_motorbikeDuration = 8;
        }
        else if (tier == PowerUpTiers.TIER5)
        {
            m_motorbikeDuration = 10;
        }
    }

    public override void ExecuteAction(GameObject player)
    {
        PlayerPrefs.SetInt("MotorbikeCharges", PlayerPrefs.GetInt("MotorbikeCharges") + 1);
    }

    public override void FinishAction()
    {
        
    }

    public float GetMotorbikeDuration() { return m_motorbikeDuration; }

}

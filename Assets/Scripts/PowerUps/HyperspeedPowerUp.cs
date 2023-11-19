using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HyperspeedPowerUp")]
public class HyperspeedPowerUp : PowerUpEffect
{
    private float m_hyperspeedMetersDuration;

    public static PowerUpEffect CreateInstance(PowerUpTiers tier)
    {
        HyperspeedPowerUp instance = new HyperspeedPowerUp();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(PowerUpTiers tier)
    {
        m_duration = 1;

        if (tier == PowerUpTiers.TIER1)
        {
            m_hyperspeedMetersDuration = 90;
        }
        else if (tier == PowerUpTiers.TIER2)
        {
            m_hyperspeedMetersDuration = 100;
        }
        else if (tier == PowerUpTiers.TIER3)
        {
            m_hyperspeedMetersDuration = 110;
        }
        else if (tier == PowerUpTiers.TIER4)
        {
            m_hyperspeedMetersDuration = 130;
        }
        else if (tier == PowerUpTiers.TIER5)
        {
            m_hyperspeedMetersDuration = 150;
        }
    }

    public float GetHyperspeedMetersDuration() { return m_hyperspeedMetersDuration; }

    public override void ExecuteAction(GameObject player)
    {
        Debug.Log("INITIAL CHARGES: " + PlayerPrefs.GetInt("HyperspeedCharges"));
        PlayerPrefs.SetInt("HyperspeedCharges", PlayerPrefs.GetInt("HyperspeedCharges") + 1);
    }

    public override void FinishAction()
    {
        Debug.Log("FINAL CHARGES: " + PlayerPrefs.GetInt("HyperspeedCharges"));
    }
}

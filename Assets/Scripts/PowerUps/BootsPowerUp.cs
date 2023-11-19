using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BootsPowerUp")]
public class BootsPowerUp : PowerUpEffect {
    [SerializeField]
    private float m_jumpIncrease;

    public static PowerUpEffect CreateInstance(PowerUpTiers tier)
    {
        BootsPowerUp instance = new BootsPowerUp();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(PowerUpTiers tier)
    {
        if(tier == PowerUpTiers.TIER1)
        {
            m_jumpIncrease = 13;
            m_duration     = 5;
        }
        else if(tier == PowerUpTiers.TIER2)
        {
            m_jumpIncrease = 14;
            m_duration = 6;
        }
        else if (tier == PowerUpTiers.TIER3)
        {
            m_jumpIncrease = 15;
            m_duration = 7;
        }
        else if (tier == PowerUpTiers.TIER4)
        {
            m_jumpIncrease = 16;
            m_duration = 8;
        }
        else if (tier == PowerUpTiers.TIER5)
        {
            m_jumpIncrease = 19;
            m_duration = 10;
        }
    }

    public override void ExecuteAction(GameObject player) {
        m_player = player;
        m_isAlreadyActive = true;
        m_player.GetComponent<PlayerController>().JumpForce += m_jumpIncrease;
    }

    public override void FinishAction() {
        m_isAlreadyActive = false;
        m_player.GetComponent<PlayerController>().JumpForce -= m_jumpIncrease;
    }
}

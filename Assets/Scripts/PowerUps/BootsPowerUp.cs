using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BootsPowerUp")]
public class BootsPowerUp : PowerUpEffect {
    [SerializeField]
    private float m_jumpIncrease;

    public static PowerUpEffect CreateInstance(int tier)
    {
        BootsPowerUp instance = new BootsPowerUp();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(int tier)
    {
        switch(tier)
        {
            case 1:
                m_jumpIncrease = 13;
                m_duration = 5;
                break;
            case 2:
                m_jumpIncrease = 14;
                m_duration = 6;
                break;
            case 3:
                m_jumpIncrease = 15;
                m_duration = 7;
                break;
            case 4:
                m_jumpIncrease = 16;
                m_duration = 8;
                break;
            case 5:
                m_jumpIncrease = 19;
                m_duration = 10;
                break;
            default:
                Debug.Log("PowerUp TIER for " + this + " is not valid!!");
                break;
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

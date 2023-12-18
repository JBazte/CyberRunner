using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/MotorbikePowerUp")]
public class MotorbikePowerup : PowerUpEffect
{
    private float m_motorbikeDuration;

    public static PowerUpEffect CreateInstance(int tier)
    {
        MotorbikePowerup instance = new MotorbikePowerup();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(int tier)
    {
        m_duration = 1;
        switch (tier)
        {
            case 1:
                m_motorbikeDuration = 5;
                break;
            case 2:
                m_motorbikeDuration = 6;
                break;
            case 3:
                m_motorbikeDuration = 7;
                break;
            case 4:
                m_motorbikeDuration = 8;
                break;
            case 5:
                m_motorbikeDuration = 10;
                break;
            default:
                Debug.Log("PowerUp TIER for " + this + " is not valid!!");
                break;
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

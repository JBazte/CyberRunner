using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HyperspeedPowerUp")]
public class HyperspeedPowerUp : PowerUpEffect
{
    private float m_hyperspeedMetersDuration;

    public static PowerUpEffect CreateInstance(int tier)
    {
        HyperspeedPowerUp instance = new HyperspeedPowerUp();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(int tier)
    {
        m_duration = 1;
        switch (tier)
        {
            case 1:
                m_hyperspeedMetersDuration = 90;
                break;
            case 2:
                m_hyperspeedMetersDuration = 100;
                break;
            case 3:
                m_hyperspeedMetersDuration = 110;
                break;
            case 4:
                m_hyperspeedMetersDuration = 130;
                break;
            case 5:
                m_hyperspeedMetersDuration = 150;
                break;
            default:
                Debug.Log("PowerUp TIER for " + this + " is not valid!!");
                break;
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

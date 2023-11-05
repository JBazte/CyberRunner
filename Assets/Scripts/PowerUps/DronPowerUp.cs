using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DronPowerUp")]
public class DronPowerUp : PowerUpEffect
{
    [SerializeField]
    private DronObject m_dron;

    public override void ExecuteAction(GameObject player)
    {
        m_dron = FindObjectOfType<DronObject>();
        m_dron.ActivateDron();
    }

    public override void FinishAction()
    {
        m_dron.DeactivateDron();
    }

    public override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }
}



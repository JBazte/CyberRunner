using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BootsPowerUp")]
public class BootsPowerUp : PowerUpEffect
{
    [SerializeField]
    private float m_jumpIncrease;

    public override void ExecuteAction(GameObject player)
    {
        m_player = player;
        m_player.GetComponent<PlayerController>().JumpForce += m_jumpIncrease;
    }

    public override void FinishAction()
    {
        m_player.GetComponent<PlayerController>().JumpForce -= m_jumpIncrease;
    }

    public override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }
}

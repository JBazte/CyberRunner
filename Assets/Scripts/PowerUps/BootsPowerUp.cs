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
        Debug.Log("LLEGA");
        m_player.GetComponent<PlayerController>().JumpForce -= m_jumpIncrease;
    }

    public override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }

    private void Awake()
    {
        m_jumpIncrease = 13.0f;
        m_duration = 10.0f;
    }

    /*PlayerController playerController = new PlayerController();
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) //Hay que añadir este tag
        {
            enabled = true;
        };
    }

    public override void ActivatePowerUp()
    {
        playerController.JumpForce = 24.0f;
    }


    public override void DeactivatePowerUp()
    {
        playerController.JumpForce = 12.0f;
    }
    */

}

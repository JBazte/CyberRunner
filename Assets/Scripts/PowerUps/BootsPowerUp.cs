using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsPowerUp : Powerup
{
    PlayerController playerController = new PlayerController();
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
}

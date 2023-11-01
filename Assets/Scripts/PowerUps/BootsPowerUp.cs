using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsPowerUp : Powerup
{
    PlayerController playerController;
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
        throw new System.NotImplementedException();
    }

    public override void DectivatePowerDown()
    {
        throw new System.NotImplementedException();
    }
}

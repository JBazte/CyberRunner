using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class MotorbikePowerup : Powerup
{
    private int Counter;
    private bool taping;

    [SerializeField] private MeshFilter ActualPlayerModel;
    [SerializeField] private Mesh MotoModel;
    [SerializeField] private Mesh PlayerModel;




    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        taping = Input.GetKeyDown(KeyCode.Space) || InputManager.Instance.Tap;
        if (Counter >= 1 && taping)
        {
            ActivatePowerUp();
            Counter++;
        }
    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) //Hay que añadir este tag
        {
            
        }
    }

    public override void ActivatePowerUp()
    {
        ActualPlayerModel.mesh = MotoModel;
        StartCountdown(5f);
    }

    public override void DeactivatePowerUp()
    {
        ActualPlayerModel.mesh = PlayerModel;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/MotorbikePowerUp")]
public class MotorbikePowerup : PowerUpEffect
{
    /*
    private int Counter;
    private bool taping;

    
    [SerializeField] private MeshFilter ActualPlayerModel;
    [SerializeField] private Mesh MotorbikeModel;
    [SerializeField] private Mesh PlayerModel;
    */

    /*void Update()
    {
        taping = Input.GetKeyDown(KeyCode.Space) || InputManager.Instance.Tap;
        if (Counter >= 1 && taping)
        {
            ActivatePowerUp();
            Counter++;
        }
    }*/

    public override void ExecuteAction(GameObject player)
    {
        Debug.Log("INITIAL CHARGES: " + PlayerPrefs.GetInt("MotorbikeCharges"));
        PlayerPrefs.SetInt("MotorbikeCharges", PlayerPrefs.GetInt("MotorbikeCharges") + 1);
        //ActualPlayerModel.mesh = MotorbikeModel;
    }

    public override void FinishAction()
    {
        Debug.Log("FINAL CHARGES: " + PlayerPrefs.GetInt("MotorbikeCharges"));
        //ActualPlayerModel.mesh = PlayerModel;
    }

    public override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }
}

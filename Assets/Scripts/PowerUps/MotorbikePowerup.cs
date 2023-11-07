using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/MotorbikePowerUp")]
public class MotorbikePowerup : PowerUpEffect
{
    public override void ExecuteAction(GameObject player)
    {
        PlayerPrefs.SetInt("MotorbikeCharges", PlayerPrefs.GetInt("MotorbikeCharges") + 1);
    }

    public override void FinishAction()
    {
        
    }

    public override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }
}
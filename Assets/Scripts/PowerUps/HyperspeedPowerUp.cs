using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HyperspeedPowerUp")]
public class HyperspeedPowerUp : PowerUpEffect
{
    public override void ExecuteAction(GameObject player)
    {
        Debug.Log("INITIAL CHARGES: " + PlayerPrefs.GetInt("HyperspeedCharges"));
        PlayerPrefs.SetInt("HyperspeedCharges", PlayerPrefs.GetInt("HyperspeedCharges") + 1);
    }

    public override void FinishAction()
    {
        Debug.Log("FINAL CHARGES: " + PlayerPrefs.GetInt("HyperspeedCharges"));
    }

    public override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }
}

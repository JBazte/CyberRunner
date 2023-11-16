using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HyperspeedAbility : MonoBehaviour
{
    private GameObject m_player;
    private float      m_metersDuration = 100.0f;

    public HyperspeedAbility(GameObject player)
    {
        m_player = player;
    }

    public void setMetersDuration(float metersDuration)
    {
        m_metersDuration = metersDuration;
    }
    
    public void ActivateHyperspeed()
    {
        PlayerPrefs.SetInt("HyperspeedCharges", PlayerPrefs.GetInt("HyperspeedCharges") - 1);
        m_player.GetComponent<PlayerController>().SetInvulneravility(true);
        m_player.GetComponent<PlayerController>().SetIsOnHyperspeed(true);
        SpeedManager.Instance.Hyperspeed();
    }

    public void ExitHyperspeed()
    {
        DestroyObstacles(m_player.transform.position, 20.0f);
        m_player.GetComponent<PlayerController>().SetInvulneravility(false);
        m_player.GetComponent<PlayerController>().SetIsOnHyperspeed(false);
        SpeedManager.Instance.ExitHyperspeed();
    }

    public void DestroyObstacles(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.name != ("ModuleFloor") && hitCollider.gameObject.name != ("Player") && hitCollider.gameObject.name != ("LeftLane") && hitCollider.gameObject.name != ("MiddleLane") && hitCollider.gameObject.name != ("RightLane"))
            {
                hitCollider.gameObject.SetActive(false);
            }
        }
    }

    public float GetMetersDuration() {  return m_metersDuration; }
}
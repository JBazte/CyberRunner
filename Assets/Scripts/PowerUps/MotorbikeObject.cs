using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MotorbikeObject : MonoBehaviour
{
    private GameObject       m_player;
    private PlayerController m_playerScript;
    private Mesh             m_motoModel;

    public MotorbikeObject(GameObject player, Mesh motoModel)
    {
        m_player = player;
        m_playerScript = m_player.GetComponent<PlayerController>();
        m_motoModel = motoModel;
    }

    public void ActivateMotorbike()
    {
        m_playerScript.SetMotoActive(true);
        m_playerScript.SetMesh(m_motoModel);
        PlayerPrefs.SetInt("MotorbikeCharges", PlayerPrefs.GetInt("MotorbikeCharges") - 1);
    }

    public void DeactivateMotorbike()
    {
        DestroyObstacles(m_player.transform.position, 30.0f);
        m_playerScript.SetMotoActive(false);
        m_playerScript.SetMesh(m_playerScript.GetPlayerMesh());

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
}
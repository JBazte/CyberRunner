using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect m_powerUpEffect;
    [SerializeField]
    private MeshFilter    m_meshFilter;

    private void Start()
    {
        //m_meshFilter = GetComponent<MeshFilter>();
    }

    public void SetPowerUpEffect(PowerUpEffect powerUpEffect, Mesh powerUpMesh)
    {
        m_powerUpEffect   = powerUpEffect;
        m_meshFilter.mesh = powerUpMesh;
        Debug.Log("MEEEEEEESHHH" + powerUpMesh);
    }

    public PowerUpEffect GetPowerUpEffect() { return m_powerUpEffect; }
}
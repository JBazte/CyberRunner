using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBehaviour : MonoBehaviour
{
    private float        TimeLeft = 6.0f;
    private GameObject[] tentaculos;
    private bool         BossIsActive;
    private bool         FirstTent = false;
    private Animator     Boss;
    protected bool       m_hasAttacked = false;


    

    // Start is called before the first frame update
    void Start()
    {
        Boss = gameObject.GetComponent<Animator>();
        Boss.Play("Idle");
        
    }

    void OnEnable()
    {
        Debug.Log("BOOOOOSSS" + Boss);
        m_hasAttacked = false;    
    }

    // Update is called once per frame
    void Update()
    {
        //BossIsActive = ModuleManager.Instance.GetTentaclesUp();
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 24);
        foreach (var hitCollider in hitColliders)
        {
            int randTent = Random.Range(0, 6);
            if (hitCollider.gameObject.CompareTag("Player") && !m_hasAttacked)
            {
                Attack(randTent);
            }
        }
    }
    
    public void SpawnFirstTentacle()
    {
        int randTent = Random.Range(0, tentaculos.Length);
        tentaculos[randTent].SetActive(true);
        FirstTent = true;
    }

    public void Attack(int randTent)
    {
        
        if (randTent == 0 || randTent == 1 || randTent == 2 || randTent == 3)
        {
            Boss.Play("Attack1");
        }
        else if (randTent == 4 || randTent == 5 || randTent == 3)
        {
            Boss.Play("Attack2");
        }
        m_hasAttacked = true;
    }
}
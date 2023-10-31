using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TemporalSingleton<GameManager>
{
    private Transform m_player;
    private float     m_meters;

    // Start is called before the first frame update
    void Start()
    {
        m_player = FindObjectOfType<CharacterController>().transform;
        m_meters = 0;
    }

    private void FixedUpdate()
    {
        m_meters = Time.deltaTime;    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

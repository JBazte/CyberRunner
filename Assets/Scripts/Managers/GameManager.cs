using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_player = FindObjectOfType<CharacterController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

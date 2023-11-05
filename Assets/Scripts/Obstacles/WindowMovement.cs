using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMovement : MonoBehaviour
{
    private Transform    m_player;
    private MeshRenderer m_renderer;
    [SerializeField]
    private float        m_distanceToAppear; //Cuando el jugador se encuentre a esta distancia, ocupara el carril
    private  float       m_distanceFromPlayer;
    [SerializeField]
    private Animation    m_spawnAnimation;
    void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.enabled = false;
        m_player = FindObjectOfType<CharacterController>().transform;
        m_distanceFromPlayer = this.transform.position.z - m_player.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        m_distanceFromPlayer = this.transform.position.z - m_player.position.z;
        if(m_distanceFromPlayer <= m_distanceToAppear)
        {
            //Aparecería la animación de ocupar el carril
            m_renderer.enabled = true;
        }
    }
}

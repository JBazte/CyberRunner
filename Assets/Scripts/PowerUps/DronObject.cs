using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DronObject : TemporalSingleton<GameManager>
{
    [SerializeField]
    private float            m_killingDistance;
    private MeshRenderer     m_renderer;
    private Collider         m_collider;
    private bool             m_isActive;
    private Queue<Transform> m_enemyList;
    private LineRenderer     m_laser;

    private void Start()
    {
        m_killingDistance  = 15.0f;
        m_isActive         = false;
        m_renderer         = GetComponent<MeshRenderer>();
        m_renderer.enabled = false;
        m_collider         = GetComponent<Collider>();
        m_collider.enabled = false;
        m_enemyList        = new Queue<Transform>();
        m_laser            = GetComponent<LineRenderer>();
        m_laser.positionCount = 1;
        m_laser.enabled    = false;
    }

    void Update()
    {
        m_laser.SetPosition(0, transform.position);
        m_laser.positionCount = m_enemyList.Count + 1;
        if (m_isActive)
        {
            KillEnemies();
        }
    }

    public void KillEnemies()
    {
        if (m_enemyList.Count > 0)
        {
            foreach (Transform enemy in m_enemyList)
            {
                float distance = enemy.position.z - transform.position.z;
                m_laser.enabled = true;
                AimEnemy(enemy);

                if (distance < m_killingDistance && enemy.gameObject.activeSelf)
                {
                    Debug.Log("Enemigo eliminado");
                    enemy.gameObject.SetActive(false);
                    m_laser.SetPosition(m_enemyList.ToList().IndexOf(enemy), Vector3.zero);
                    m_enemyList.Dequeue();
                }
            }
        }
    }

    private void AimEnemy(Transform enemyToAim)
    {
        m_laser.SetPosition(m_enemyList.ToList().IndexOf(enemyToAim), enemyToAim.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            m_enemyList.Enqueue(other.gameObject.transform);
        }
    }

    public void ActivateDron()
    {
        m_isActive = true;
        m_renderer.enabled = true;
        m_collider.enabled = true;
    }

    public void DeactivateDron()
    {
        m_isActive = false;
        m_renderer.enabled = false;
        m_collider.enabled = false;
    }
}

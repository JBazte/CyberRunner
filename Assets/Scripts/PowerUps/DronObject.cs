using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DronObject : MonoBehaviour
{
    private float            m_killingDistance;
    private MeshRenderer     m_renderer;
    private Collider         m_collider;
    private bool             m_isActive;
    private Queue<Transform> m_enemyList;
    private LineRenderer     m_laser;
    [SerializeField]
    private Transform        m_aimPoint;

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
        //m_laser.SetPosition(0, m_aimPoint.position);
        if (m_isActive)
        {
            //m_laser.positionCount = m_enemyList.Count + 1;
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
                //m_laser.enabled = true;
                //AimEnemy(enemy);
                //m_laser.SetPosition(m_enemyList.ToList().IndexOf(enemy), enemy.position);
                if (distance < m_killingDistance && enemy.gameObject.activeSelf)
                {
                    Debug.Log("Enemigo eliminado  ----  " + m_enemyList.ToList().IndexOf(enemy));
                    enemy.gameObject.SetActive(false);
                    
                    m_enemyList.Dequeue();
                    //m_laser.SetPosition(m_enemyList.ToList().IndexOf(enemy), Vector3.);
                    //m_laser.positionCount--;
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
            Debug.Log("Enemigo detectado");
            m_enemyList.Enqueue(other.gameObject.transform);
            //m_laser.positionCount++;
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

    public void SetKillingDistance(float killingDistance) { m_killingDistance = killingDistance; }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DronObject : MonoBehaviour
{
    private float            m_killingDistance;
    private bool             m_isActive;
    private Quaternion       m_rotation;
    private Queue<Transform> m_enemyList;
    private LineRenderer     m_laser;
    [SerializeField]
    private Transform        m_aimPoint;
    [SerializeField]
    private LineRenderer lr; 
    

    private void Start()
    {
        gameObject.SetActive(false);
        m_killingDistance  = 15.0f;
        m_isActive         = false;
        m_enemyList        = new Queue<Transform>();
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        m_rotation = gameObject.transform.rotation;
        //m_laser            = GetComponent<LineRenderer>();
        //m_laser.positionCount = 1;
        //m_laser.enabled    = false;

    }

    void Update()
    {
        //m_laser.SetPosition(0, m_aimPoint.position);
        if (m_isActive)
        {
            Debug.Log("matar");
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
                lr.enabled = true;
                Vector3 playerPosition = enemy.position;
                Vector3 direction = playerPosition - m_aimPoint.position;
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, angle+180, 0f);
                gameObject.transform.rotation = rotation;

                lr.SetPosition(0, m_aimPoint.position);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    if (hit.collider)
                    {
                        lr.SetPosition(1, hit.point);
                    }
                    else
                    {
                        lr.SetPosition(1, -transform.right * 5000);
                    }
                }
                
                
                if (distance < m_killingDistance && enemy.gameObject.activeSelf)
                {
                    Debug.Log("Enemigo eliminado  ----  " + m_enemyList.ToList().IndexOf(enemy));
                    enemy.gameObject.SetActive(false);
                    
                    m_enemyList.Dequeue();
                    lr.enabled = false;
                    gameObject.transform.rotation = m_rotation;
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
        gameObject.SetActive(true);
    }

    public void DeactivateDron()
    {
        m_isActive = false;
        gameObject.SetActive(true);
    }

    public void SetKillingDistance(float killingDistance) { m_killingDistance = killingDistance; }
}

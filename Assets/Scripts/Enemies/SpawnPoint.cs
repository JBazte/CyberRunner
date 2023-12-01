using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ENEMY_TYPES { SHIELD_ENEMY = 0, SLASH_ENEMY = 1, GROUNDWAVE_ENEMY = 2 }

public class SpawnPoint : MonoBehaviour
{
    public GameObject m_enemy;
    private int       m_enemyTypesNum = 3;
    private Animator anim;

    public void SpawnRandomEnemy()
    {
        int randomEnemyType = Random.Range(0, m_enemyTypesNum);
        //Quaternion newAngle = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 180.0f, transform.rotation.z);

        if(m_enemy != null) Destroy(m_enemy);

        if (randomEnemyType == (int)ENEMY_TYPES.SHIELD_ENEMY)
        {
            m_enemy = Instantiate(ModuleManager.Instance.GetShieldEnemy(), transform.position, transform.rotation, transform);
            m_enemy.gameObject.GetComponent<EnemyAbstract>().SetIsSpawn(true);
        }
        else if (randomEnemyType == (int)ENEMY_TYPES.SLASH_ENEMY)
        {
            m_enemy = Instantiate(ModuleManager.Instance.GetSlashEnemy(), transform.position, transform.rotation, transform);
            m_enemy.gameObject.GetComponent<EnemyAbstract>().SetIsSpawn(true);
        }
        else if (randomEnemyType == (int)ENEMY_TYPES.GROUNDWAVE_ENEMY)
        {
            m_enemy = Instantiate(ModuleManager.Instance.GetGroundWaveEnemy(), transform.position, transform.rotation, transform);
            m_enemy.gameObject.GetComponent<EnemyAbstract>().SetIsSpawn(true);
        }
        else
        {
            Debug.Log("Spawn trying to instance a non existing EnemyType");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ENEMY_TYPES { SHIELD_ENEMY = 0, SLASH_ENEMY = 1, GROUNDWAVE_ENEMY = 2 }

public class SpawnPoint : MonoBehaviour
{
    public EnemyAbstract m_enemy;
    private int           m_enemyTypesNum = 3;

    public void SpawnRandomEnemy()
    {
        int randomEnemyType = Random.Range(0, m_enemyTypesNum);
        Quaternion newRotation = transform.rotation * Quaternion.Euler(0, 180, 0);

        if (randomEnemyType == (int)ENEMY_TYPES.SHIELD_ENEMY)
        {
            m_enemy = Instantiate(ModuleManager.Instance.GetShieldEnemy(), transform.position, newRotation, transform);
            m_enemy.SetIsSpawn(true);
        }
        else if (randomEnemyType == (int)ENEMY_TYPES.SLASH_ENEMY)
        {
            m_enemy = Instantiate(ModuleManager.Instance.GetSlashEnemy(), transform.position, newRotation, transform);
            m_enemy.SetIsSpawn(true);
        }
        else if (randomEnemyType == (int)ENEMY_TYPES.GROUNDWAVE_ENEMY)
        {
            m_enemy = Instantiate(ModuleManager.Instance.GetGroundWaveEnemy(), transform.position, newRotation, transform);
            m_enemy.SetIsSpawn(true);
        }
        else
        {
            Debug.Log("Spawn trying to instance a non existing EnemyType");
        }
    }
}

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
            Instantiate(ModuleManager.Instance.GetShieldEnemy(), transform.position, newRotation);
        }
        else if (randomEnemyType == (int)ENEMY_TYPES.SLASH_ENEMY)
        {
            Instantiate(ModuleManager.Instance.GetSlashEnemy(), transform.position, newRotation);
        }
        else if (randomEnemyType == (int)ENEMY_TYPES.GROUNDWAVE_ENEMY)
        {
            Instantiate(ModuleManager.Instance.GetGroundWaveEnemy(), transform.position, newRotation);
        }
        else
        {
            Debug.Log("Spawn trying to instance a non existing EnemyType");
        }
    }
}

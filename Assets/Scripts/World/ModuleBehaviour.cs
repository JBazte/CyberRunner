using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


public class ModuleBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject   m_obstaclesContainer;
    private GameObject[] m_obstacles;
    private int          m_obstacleCount;

    [SerializeField]
    private GameObject   m_enemiesContainer;
    private GameObject[] m_enemies;
    private int          m_enemiesCount;

    [SerializeField]
    private GameObject   m_coinsContainer;
    private GameObject[] m_coins;
    private int          m_coinsCount;

    [SerializeField]
    private GameObject   m_powerUpsContainer;
    private GameObject[] m_powerUps;
    private int          m_powerUpsCount;
    private int          m_lastPowerUp = -1;

    public void InitializeModule()
    {
        //We save the lenght to use it lately to access count os arrays when any GameObject of them is not active
        m_obstacleCount = m_obstaclesContainer.transform.childCount;
        m_enemiesCount = m_enemiesContainer.transform.childCount;
        m_coinsCount = m_coinsContainer.transform.childCount;
        m_powerUpsCount = m_powerUpsContainer.transform.childCount;

        m_obstacles = new GameObject[m_obstacleCount];
        m_enemies = new GameObject[m_enemiesCount];
        m_coins = new GameObject[m_coinsCount];
        m_powerUps = new GameObject[m_powerUpsCount];

        m_obstacles = m_obstaclesContainer.GetComponentsInChildren<Transform>(true)
                                          .Where(child => child != m_obstaclesContainer.transform)
                                          .Select(child => child.gameObject)
                                          .ToArray();
        m_enemies = m_enemiesContainer.GetComponentsInChildren<Transform>(true)
                                          .Where(child => child != m_enemiesContainer.transform)
                                          .Select(child => child.gameObject)
                                          .ToArray();
        m_coins = m_coinsContainer.GetComponentsInChildren<Transform>(true)
                                          .Where(child => child != m_coinsContainer.transform)
                                          .Select(child => child.gameObject)
                                          .ToArray();
        m_powerUps = m_powerUpsContainer.GetComponentsInChildren<Transform>(true)
                                          .Where(child => child != m_powerUpsContainer.transform)
                                          .Select(child => child.gameObject)
                                          .ToArray();
        DeactivateAllPowerUps();
    }

    public void RandomizeModule()
    {
        ResetObstacles();
        RandomizeEnemies();
        ResetCoins();
        RandomizePowerUps();
    }

    private void ResetObstacles()
    {
        foreach (GameObject obstacle in m_obstacles)
        {
            if (!obstacle.activeSelf) obstacle.SetActive(true);
        }
    }

    private void RandomizeEnemies()
    {
        foreach (GameObject enemy in m_enemies)
        {
            //if (!enemy.activeSelf) enemy.SetActive(true);
            //PENDIENTE DEL CODIGO DE GUILLE PARA HACER QUE SE ALEATORICEZ SI SON RANDOM
        }
    }

    private void ResetCoins()
    {
        foreach (GameObject coin in m_coins)
        {
            if(!coin.activeSelf) coin.SetActive(true);
        }
    }

    private void RandomizePowerUps()
    {
        if(GameManager.Instance.GetPowerUpAppears())
        {
            int randomPowerUpPos = Random.Range(0, m_powerUpsCount - 1);
            int randomPowerUp    = Random.Range(0, (int)GameManager.Instance.GetTotalNumOfPowerUps() - 1);

            // If the latest powerUp activated was not picked up and it was active it is deactivated
            if (m_lastPowerUp >= 0 && m_powerUps[m_lastPowerUp].activeSelf) m_powerUps[m_lastPowerUp].SetActive(false); 

            m_powerUps[randomPowerUpPos].SetActive(true); //If the powerUp is inactive it activates it
                                                           //It asks the GameManager for the instance of the random powerUp effect
            m_powerUps[randomPowerUpPos].GetComponent<PowerUp>().SetPowerUpEffect(GameManager.Instance.GetPowerUpEffect(randomPowerUp));
            m_lastPowerUp = randomPowerUpPos;

            GameManager.Instance.SetPowerUpAppears(false);
        }
    }

    private void DeactivateAllPowerUps() 
    {
        foreach(GameObject powerUp in m_powerUps)
        {
            powerUp.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/WallsPowerUp")]
public class WallPowerUp : PowerUpEffect {
    [SerializeField]
    private GameObject m_wallLeft;
    [SerializeField]
    private GameObject m_wallRight;
    [SerializeField]
    private GameObject[] m_walls;
    private PlayerController m_playerController;

    public static PowerUpEffect CreateInstance(int tier)
    {
        WallPowerUp instance = new WallPowerUp();
        instance.SetTier(tier);
        return instance;
    }

    public override void SetTier(int tier)
    {
        switch (tier)
        {
            case 1:
                m_duration = 7;
                break;
            case 2:
                m_duration = 7.5f;
                break;
            case 3:
                m_duration = 8f;
                break;
            case 4:
                m_duration = 8.5f;
                break;
            case 5:
                m_duration = 9.5f;
                break;
            default:
                Debug.Log("PowerUp TIER for " + this + " is not valid!!");
                break;
        }
    }

    public override void ExecuteAction(GameObject player) {
        m_player = player;
        m_isAlreadyActive = true;
        m_walls = GameObject.FindGameObjectsWithTag("Wall");
        m_wallLeft = m_walls[0];
        m_wallRight = m_walls[1];
        m_wallRight.GetComponent<MeshRenderer>().enabled = true;
        m_wallLeft.GetComponent<MeshRenderer>().enabled = true;
        m_wallRight.GetComponent<Collider>().enabled = true;
        m_wallLeft.GetComponent<Collider>().enabled = true;
        //m_wallRight.SetActive(true);
        //m_wallLeft .SetActive(true);
    }

    public override void FinishAction() {
        m_wallRight.GetComponent<MeshRenderer>().enabled = false;
        m_isAlreadyActive = false;
        m_wallLeft.GetComponent<MeshRenderer>().enabled = false;
        m_wallRight.GetComponent<Collider>().enabled = false;
        m_wallLeft.GetComponent<Collider>().enabled = false;
        m_playerController = m_player.GetComponent<PlayerController>();
        if (m_playerController.getIsOnWall())
        {
            if      (m_playerController.getSide() == SIDE.LeftWall)  m_playerController.setSide(SIDE.Left);
            else if (m_playerController.getSide() == SIDE.RightWall) m_playerController.setSide(SIDE.Right);
        }
        //m_wallRight.SetActive(false);
        //m_wallLeft .SetActive(false);
    }
}

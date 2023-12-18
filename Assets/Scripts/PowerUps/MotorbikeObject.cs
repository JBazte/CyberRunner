using System.Collections;
using UnityEngine;

public class MotorbikeObject : MonoBehaviour {
    private GameObject       m_player;
    private PlayerController m_playerScript;
    private Mesh             m_motoModel;
    private float            m_duration;

    public MotorbikeObject(GameObject player, Mesh motoModel) {
        m_player = player;
        m_playerScript = m_player.GetComponent<PlayerController>();
        m_motoModel = motoModel;
        m_duration = 0.0f;
    }

    public void SetDuration(float duration)
    {
        m_duration = duration;
    }

    public void ActivateMotorbike() {
        m_playerScript.SetMotoActive(true);
        m_playerScript.SetMesh(m_motoModel);
        PlayerPrefs.SetInt("MotorbikeCharges", PlayerPrefs.GetInt("MotorbikeCharges") - 1);
        Debug.Log(PlayerPrefs.GetInt("MotorbikeCharges"));
    }

    public void DeactivateMotorbike() {
        DestroyObstacles(m_player.transform.position, 30.0f);
        m_playerScript.SetMotoActive(false);
        m_playerScript.SetMesh(m_playerScript.GetPlayerMesh());
    }

    public void DestroyObstacles(Vector3 center, float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.CompareTag("Obstacle")) {
                hitCollider.gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        if(m_playerScript.GetMotoActive())
            DeactivateMotorbike();
    }
}
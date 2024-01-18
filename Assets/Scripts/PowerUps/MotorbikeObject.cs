using System.Collections;
using UnityEngine;

public class MotorbikeObject : MonoBehaviour {
    private GameObject       m_player;
    private PlayerController m_playerScript;
    private Mesh             m_motoModel;
    private float            m_duration;
    public Animation        m_idleAnim;

    public MotorbikeObject(GameObject player) {
        m_player = player;
        m_playerScript = m_player.GetComponent<PlayerController>();
        m_duration = 0.0f;
    }

    private void Awake()
    {
        m_idleAnim = GetComponent<Animation>();
    }

    private void Update()
    {
        if(gameObject.activeSelf && !m_idleAnim.isPlaying)
            m_idleAnim.Play();
    }

    public void SetDuration(float duration)
    {
        m_duration = duration;
    }

    public void ActivateMotorbike() {
        m_playerScript.SetMotoActive(true);
        PlayerPrefs.SetInt("MotorbikeCharges", PlayerPrefs.GetInt("MotorbikeCharges") - 1);
        Debug.Log(PlayerPrefs.GetInt("MotorbikeCharges"));
    }

    public void DeactivateMotorbike() {
        DestroyObstacles(m_player.transform.position, 30.0f);
        m_playerScript.SetMotoActive(false);
    }

    public void DestroyObstacles(Vector3 center, float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.CompareTag("Obstacle") || hitCollider.gameObject.CompareTag("Ramp")) {
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
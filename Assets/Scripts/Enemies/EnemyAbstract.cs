using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    public GameObject m_weapon;
    public bool       m_isSpawn = false;
    protected bool    m_hasAttacked = false;
    private Animator  m_anim;
    private float     m_timeTillDissapear;
    
    private void OnEnable()
    {
        m_weapon = gameObject.transform.GetChild(0).gameObject;
        m_anim = gameObject.GetComponentInChildren<Animator>();
        gameObject.GetComponent<Collider>().enabled = true;
        m_anim.Play("Idle");
    }

    private void Start()
    {
        m_timeTillDissapear = 1.5f;
    }

    public abstract void Attack();

    public void Die()
    {
        Debug.Log("ENEMIGO MUERTO");
        m_anim.Play("Die");
        gameObject.GetComponent<Collider>().enabled = false;
        GameManager.Instance.AddComboPoint();
        StartCoroutine(DieTimer());
    }

    private IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(1.5f);
        if (!m_isSpawn) gameObject.SetActive(false);
        else if (m_isSpawn) Destroy(gameObject);
    }

    public bool GetIsSpawn() { return m_isSpawn; }
    public void SetIsSpawn(bool isSpawn) { m_isSpawn = isSpawn; }
    public bool GetHasAttacked() { return m_hasAttacked; }
    public void SetHasAttacked(bool hasAttacked) { m_hasAttacked = hasAttacked; }
    public void DeactivateWeapon()
    {
        if (m_weapon.activeSelf)
        {
            m_weapon.SetActive(false);
        }
    }
}

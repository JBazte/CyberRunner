using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    public GameObject m_weapon;
    public bool          m_isSpawn = false;
    protected bool       m_hasAttacked = false;

    private void OnEnable()
    {
        m_weapon = gameObject.transform.GetChild(0).gameObject;
    }

    public abstract void Attack();
    public void Die()
    {
        if (!m_isSpawn) gameObject.SetActive(false);
        else if (m_isSpawn) Destroy(gameObject);

        GameManager.Instance.AddComboPoint();
    }
    public bool GetIsSpawn() { return m_isSpawn; }
    public void SetIsSpawn(bool isSpawn) { m_isSpawn = isSpawn; }
    public bool GetHasAttacked() { return m_hasAttacked; }
    public void SetHasAttacked(bool hasAttacked) { m_hasAttacked = hasAttacked; }
    public void DeactivateWeapon()
    {
        Debug.Log(m_weapon);
        if (m_weapon.activeSelf)
        {
            m_weapon.SetActive(false);
        }
    }
}
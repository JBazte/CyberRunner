using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : TemporalSingleton<GameManager>
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) 
        {
            GameManager.Instance.AddCoin();
            gameObject.SetActive(false);
        }
    }
}

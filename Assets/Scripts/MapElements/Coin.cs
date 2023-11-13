using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) 
        {
            GameManager.Instance.AddCoin();
            gameObject.GetComponent<Collider>()    .enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

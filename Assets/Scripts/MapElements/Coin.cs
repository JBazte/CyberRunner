using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : TemporalSingleton<GameManager>
{

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) 
        {
            GameManager.Instance.AddCoin();
            gameObject.SetActive(false);
        }
    }
}

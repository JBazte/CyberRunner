using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotoPowerUpObj : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.position += new Vector3(0, 0, -SpeedManager.Instance.GetRunSpeed() * Time.deltaTime);

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Player")
        {
            SetInt("charges",PlayerPrefs.GetInt("charges")+1);
            Destroy(gameObject);
        }
    }
    private void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }
}

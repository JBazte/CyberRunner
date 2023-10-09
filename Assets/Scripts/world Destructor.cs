using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldDestructor : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("holaaaaaa");
       Destroy(collision.gameObject);
    }
    
}

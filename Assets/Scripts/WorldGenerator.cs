using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject Tile1;
   
    public GameObject StartTile;

    private float Index = 0;

    private void Start()
    {
        GameObject StartPlane1 = Instantiate(StartTile, transform);
        StartPlane1.transform.position = new Vector3(0, 0, 15);
        GameObject StartPlane2 = Instantiate(StartTile, transform);
        StartPlane2.transform.position = new Vector3(0, 0, 30);
        GameObject StartPlane3 = Instantiate(StartTile, transform);
        StartPlane3.transform.position = new Vector3(0, 0, 45);
        GameObject StartPlane4 = Instantiate(StartTile, transform);
        StartPlane4.transform.position = new Vector3(0, 0, 60);
        GameObject StartPlane5 = Instantiate(StartTile, transform);
        StartPlane5.transform.position = new Vector3(0, 0, 75);
    }

    private void Update()
    {
        gameObject.transform.position += new Vector3(0, 0, -12 * Time.deltaTime);

        if (transform.position.z <= Index)
        {
            int RandomInt1 = Random.Range(0, 2);

            if (RandomInt1 == 1)
            {
                GameObject TempTile1 = Instantiate(Tile1, transform);
                TempTile1.transform.position = new Vector3(0, 0, 90);
            }
            else if (RandomInt1 == 0)
            {
                GameObject TempTile1 = Instantiate(Tile1, transform);
                TempTile1.transform.position = new Vector3(0, 0, 90);
            }

            Index = Index - 15.0f;
        }


    }
}
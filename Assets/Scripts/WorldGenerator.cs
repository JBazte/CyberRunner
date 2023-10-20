using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject Tile1;
    public GameObject Tile2;


    public GameObject StartTile;

    private float Index = 0;

    private void Start()
    {
        GameObject StartPlane1 = Instantiate(StartTile, transform);
        StartPlane1.transform.position = new Vector3(0, 0, 15);
        
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
                TempTile1.transform.position = new Vector3(0, 0, 105);
            }
            else if (RandomInt1 == 0)
            {
                GameObject TempTile2 = Instantiate(Tile2, transform);
                TempTile2.transform.position = new Vector3(0, 0, 105);
            }

            Index = Index - 100.0f;
        }


    }
}
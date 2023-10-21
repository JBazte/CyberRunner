using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject Tile1;
    public GameObject Tile2;
    public GameObject Tile3;



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
        Tile1.transform.position += new Vector3(0, 0, -12 * Time.deltaTime);
        Tile2.transform.position += new Vector3(0, 0, -12 * Time.deltaTime);
        Tile3.transform.position += new Vector3(0, 0, -12 * Time.deltaTime);



        if (transform.position.z <= Index)
        {
            int RandomInt1 = Random.Range(0, 3);
            


            if (RandomInt1 == 0)
            {
                if (Tile1.active && Tile1.transform.position.z <= -30)
                {
                    Tile1.transform.position = new Vector3(0, 0, 105);
                }
                else if (!Tile1.active)
                {
                    Tile1.SetActive(true);
                    Tile1.transform.position = new Vector3(0, 0, 105);
                }
                else
                    RandomInt1 = 1;

            }


             if (RandomInt1 == 1)
            {
                if (Tile2.active && Tile2.transform.position.z <= -30)
                {
                    Tile2.transform.position = new Vector3(0, 0, 105);
                }
                else if (!Tile2.active)
                {
                    Tile2.SetActive(true);
                    Tile2.transform.position = new Vector3(0, 0, 105);
                }
                else
                    RandomInt1 = 2;
            }


            if (RandomInt1 == 2)
            {
                if (Tile3.active && Tile3.transform.position.z <= -30)
                {
                    Tile3.transform.position = new Vector3(0, 0, 105);
                }
                else if (!Tile3.active)
                {
                    Tile3.SetActive(true);
                    Tile3.transform.position = new Vector3(0, 0, 105);
                }
                else 
                {
                    if (Tile1.active && Tile1.transform.position.z <= -30)
                    {
                        Tile1.transform.position = new Vector3(0, 0, 105);
                    }
                    else if (!Tile1.active)
                    {
                        Tile1.SetActive(true);
                        Tile1.transform.position = new Vector3(0, 0, 105);
                    }
                    
                }
            
            }

            Index = Index - 100.0f;
        }


    }
}
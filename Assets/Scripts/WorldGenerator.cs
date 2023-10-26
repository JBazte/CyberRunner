using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour 
{
    public GameObject StartTile;
    public GameObject[] Tiles;
    public float worldVelocity;
    private float Index = 0;


    private void Start()
    {
        GameObject StartPlane1 = Instantiate(StartTile, transform);
        StartPlane1.transform.position = new Vector3(0, 0, 41);

        
    }

    private void Update() 
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i].transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);
        }
        gameObject.transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);

        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i].active && Tiles[i].transform.position.z <= -80)
            {
                Tiles[i].SetActive(false);
            }
        }

        if (transform.position.z <= Index) 
        {
            int RandomInt1 = Random.Range(0, Tiles.Length);

            if (Tiles[RandomInt1].active && Tiles[RandomInt1].transform.position.z <= -30)
            {
                Tiles[RandomInt1].transform.position = new Vector3(0, 0, 139);
            }
            else if (!Tiles[RandomInt1].active)
            {
                Tiles[RandomInt1].SetActive(true);
                Tiles[RandomInt1].transform.position = new Vector3(0, 0, 139);
            }
            else
            {
                RandomInt1 += 1;

                if (Tiles[RandomInt1].active && Tiles[RandomInt1].transform.position.z <= -30)
                {
                    Tiles[RandomInt1].transform.position = new Vector3(0, 0, 139);
                }
                else if (!Tiles[RandomInt1].active)
                {
                    Tiles[RandomInt1].SetActive(true);
                    Tiles[RandomInt1].transform.position = new Vector3(0, 0, 139);
                }

            }      
            
            Index = Index - 100.0f;
        }

    }
}
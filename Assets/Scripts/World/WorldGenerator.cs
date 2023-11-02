using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : TemporalSingleton<WorldGenerator> 
{
    public GameObject StartTile;
    private GameObject[] Tiles;
    private float index = 0;

    private void Start()
    {
        GameObject StartPlane1 = Instantiate(StartTile, transform);
        StartPlane1.transform.position = new Vector3(0, 0, 41);
        Tiles = GameObject.FindGameObjectsWithTag("Module");
        foreach (GameObject tile in Tiles){
            tile.SetActive(false);
        }
        Debug.Log(Tiles.Length);
    }

    private void Update() 
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i].transform.position += new Vector3(0, 0, -SpeedManager.Instance.GetRunSpeed() * Time.deltaTime);
        }
        gameObject.transform.position += new Vector3(0, 0, -SpeedManager.Instance.GetRunSpeed() * Time.deltaTime);

        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i].activeSelf && Tiles[i].transform.position.z <= -80)
            {
                Tiles[i].SetActive(false);
            }
        }

        if (transform.position.z <= index) 
        {
            int RandomInt1 = Random.Range(0, Tiles.Length);

            if (Tiles[RandomInt1].activeSelf && Tiles[RandomInt1].transform.position.z <= -60)
            {
                Tiles[RandomInt1].transform.position = new Vector3(0, 0, 139);
            }
            else if (!Tiles[RandomInt1].activeSelf)
            {
                Tiles[RandomInt1].SetActive(true);
                Tiles[RandomInt1].transform.position = new Vector3(0, 0, 139);
            }
            else
            {
                RandomInt1 += 1;

                if (Tiles[RandomInt1].activeSelf && Tiles[RandomInt1].transform.position.z <= -60)
                {
                    Tiles[RandomInt1].transform.position = new Vector3(0, 0, 139);
                }
                else if (!Tiles[RandomInt1].activeSelf)
                {
                    Tiles[RandomInt1].SetActive(true);
                    Tiles[RandomInt1].transform.position = new Vector3(0, 0, 139);
                }

            }      
            
            index = index - 100.0f;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
    public GameObject Tile1;
    public GameObject Tile2;
    public GameObject Tile3;
    public GameObject Tile4;
    public GameObject Tile5;
    public GameObject Tile6;
    public GameObject StartTile;

    public float worldVelocity;

    private float Index = 0;

    private void Start() {
        GameObject StartPlane1 = Instantiate(StartTile, transform);
        StartPlane1.transform.position = new Vector3(0, 0, 15);

    }

    private void Update() {
        gameObject.transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);
        Tile1.transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);
        Tile2.transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);
        Tile3.transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);
        Tile4.transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);
        Tile5.transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);
        Tile6.transform.position += new Vector3(0, 0, -worldVelocity * Time.deltaTime);



        if (transform.position.z <= Index) {
            int RandomInt1 = Random.Range(0, 6);



            if (RandomInt1 == 0) {
                if (Tile1.active && Tile1.transform.position.z <= -30) {
                    Tile1.transform.position = new Vector3(0, 0, 105);
                } else if (!Tile1.active) {
                    Tile1.SetActive(true);
                    Tile1.transform.position = new Vector3(0, 0, 105);
                } else
                    RandomInt1 = 1;

            }


            if (RandomInt1 == 1) {
                if (Tile2.active && Tile2.transform.position.z <= -30) {
                    Tile2.transform.position = new Vector3(0, 0, 105);
                } else if (!Tile2.active) {
                    Tile2.SetActive(true);
                    Tile2.transform.position = new Vector3(0, 0, 105);
                } else
                    RandomInt1 = 2;
            }

            if (RandomInt1 == 2) {
                if (Tile4.active && Tile4.transform.position.z <= -30) {
                    Tile4.transform.position = new Vector3(0, 0, 105);
                } else if (!Tile4.active) {
                    Tile4.SetActive(true);
                    Tile4.transform.position = new Vector3(0, 0, 105);
                } else
                    RandomInt1 = 3;
            }

            if (RandomInt1 == 3) {
                if (Tile5.active && Tile5.transform.position.z <= -30) {
                    Tile5.transform.position = new Vector3(0, 0, 105);
                } else if (!Tile5.active) {
                    Tile5.SetActive(true);
                    Tile5.transform.position = new Vector3(0, 0, 105);
                } else
                    RandomInt1 = 4;
            }

            if (RandomInt1 == 4) {
                if (Tile6.active && Tile6.transform.position.z <= -30) {
                    Tile6.transform.position = new Vector3(0, 0, 105);
                } else if (!Tile6.active) {
                    Tile6.SetActive(true);
                    Tile6.transform.position = new Vector3(0, 0, 105);
                } else
                    RandomInt1 = 5;
            }


            if (RandomInt1 == 5) {
                if (Tile3.active && Tile3.transform.position.z <= -30) {
                    Tile3.transform.position = new Vector3(0, 0, 105);
                } else if (!Tile3.active) {
                    Tile3.SetActive(true);
                    Tile3.transform.position = new Vector3(0, 0, 105);
                } else {
                    if (Tile1.active && Tile1.transform.position.z <= -30) {
                        Tile1.transform.position = new Vector3(0, 0, 105);
                    } else if (!Tile1.active) {
                        Tile1.SetActive(true);
                        Tile1.transform.position = new Vector3(0, 0, 105);
                    } else
                        RandomInt1 = 1;

                }

            }

            Index = Index - 100.0f;
        }


    }
}
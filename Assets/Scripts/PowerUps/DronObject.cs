using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronObject : TemporalSingleton<GameManager>
{
    public GameObject player;
    public float distancia = 2.0f;
    public float distanciaEliminacion = 5.0f; // Distancia para eliminar enemigos
    public string enemyTag = "Enemy"; // Tag de los enemigos

    [SerializeField] public GameObject dronObject;
    private CharacterController playerController;

    void Update()
    {
        if (dronObject != null && playerController != null)
        {
            Vector3 newPosition = playerController.transform.position + new Vector3(0, distancia, 0);
            dronObject.transform.position = newPosition;

            // Verificar y eliminar enemigos que están cerca del jugador
            DetectarYEliminarEnemigosCercanos();
        }
    }

    void DetectarYEliminarEnemigosCercanos()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(playerController.transform.position, enemy.transform.position);

            //Debug.Log("Distancia a enemigo: " + distance);

            if (distance < distanciaEliminacion)
            {
                Debug.Log("Eliminando enemigo");
                Destroy(enemy);
            }
        }
    }
}

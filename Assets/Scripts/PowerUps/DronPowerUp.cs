using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronPowerUp : Powerup
{
    public GameObject player;
    public float distancia = 2.0f;
    public float distanciaEliminacion = 5.0f; // Distancia para eliminar enemigos
    public string enemyTag = "Enemy"; // Tag de los enemigos

    [SerializeField] public GameObject dronObject;
    private CharacterController playerController;

    void Start()
    {
       
    }

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

    void CrearDron()
    {
        Vector3 playerPosition = playerController.transform.position;
        Vector3 startPosition = playerPosition + new Vector3(0, distancia, 0);
        dronObject = Instantiate(dronObject, startPosition, Quaternion.identity);
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

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) //Hay que añadir este tag
        {
            enabled = true;
        }
    }

    protected override void CountdownFinished()
    {
        base.CountdownFinished(); // Llama al método de la clase base para desactivar el script
        if (dronObject != null)
        {
            Destroy(dronObject); // Destruye el objeto "dron"
        }
        Debug.Log("Cuenta atrás finalizada y dron destruido.");
    }

    public override void ActivatePowerUp()
    {
        playerController = player.GetComponent<CharacterController>();
        CrearDron();
        StartCountdown(5f);
    }

    public override void DeactivatePowerUp()
    {
        throw new System.NotImplementedException();
    }
}



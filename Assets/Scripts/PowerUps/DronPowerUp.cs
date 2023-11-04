using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DronPowerUp")]
public class DronPowerUp : PowerUpEffect
{
    [SerializeField]
    private GameObject m_dron;

    public override void ExecuteAction(GameObject player)
    {
        m_dron = FindObjectOfType<DronObject>().gameObject;
        m_dron.GetComponent<MeshRenderer>().enabled = true;
    }

    public override void FinishAction()
    {
        m_dron.GetComponent<MeshRenderer>().enabled = false;
    }

    public override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }

    /*public GameObject player;
    public float distancia = 2.0f;
    public float distanciaEliminacion = 5.0f; // Distancia para eliminar enemigos
    public string enemyTag = "Enemy"; // Tag de los enemigos

    [SerializeField] public GameObject dronObject;
    private CharacterController playerController;

    void Start()
    {
       //ActivatePowerUp();
    }

    void Update()
    {
       if (dronObject != null && playerController != null)
       {
           Vector3 newPosition = playerController.transform.position + new Vector3(0, distancia, 0);
           dronObject.transform.position = newPosition;

           // Verificar y eliminar enemigos que est�n cerca del jugador
           DetectarYEliminarEnemigosCercanos();
       }
    }

    void CrearDron()
    {
       dronObject.SetActive(true);
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
       if (other.CompareTag("PowerUp")) //Hay que a�adir este tag
       {
           enabled = true;
       }
    }

    public override void ActivatePowerUp()
    {
       playerController = player.GetComponent<CharacterController>();
       CrearDron();
       StartCountdown(5f);
    }

    public override void DeactivatePowerUp()
    {
       Destroy(dronObject);
    }
    */

}



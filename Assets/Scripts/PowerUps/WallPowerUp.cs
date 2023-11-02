using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPowerUp : Powerup
{
    public GameObject player;

    [SerializeField] public GameObject wallLeft;
    [SerializeField] public GameObject wallRight;
    private CharacterController playerController;

    private Coroutine followPlayerCoroutineWallRight;
    private Coroutine followPlayerCoroutineWallLeft;

    public override void ActivatePowerUp()
    {
        
    }

    public override void DeactivatePowerUp()
    {
        // Detenemos espec�ficamente las coroutines de wallRight y wallLeft
        if (followPlayerCoroutineWallRight != null)
        {
            StopCoroutine(followPlayerCoroutineWallRight);
        }

        if (followPlayerCoroutineWallLeft != null)
        {
            StopCoroutine(followPlayerCoroutineWallLeft);
        }

        // Borramos las paredes despu�s de 5 segundos
        StartCoroutine(DestroyWallsAfterDelay(5f));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent <CharacterController>();

        // Iniciamos las coroutines para seguir al jugador
        followPlayerCoroutineWallRight = StartCoroutine(FollowPlayerPosition(wallRight));
        followPlayerCoroutineWallLeft = StartCoroutine(FollowPlayerPosition(wallLeft));
    }

    // Update is called once per frame
    void Update()
    {
        if (wallRight != null && wallLeft != null && playerController != null)
        {
            // Obtener la posici�n del jugador
            Vector3 playerPosition = playerController.transform.position;

            // Establece la posici�n de las paredes en funci�n de la posici�n del jugador en el eje Z
            wallRight.transform.position = new Vector3(wallRight.transform.position.x, wallRight.transform.position.y, playerPosition.z);
            wallLeft.transform.position = new Vector3(wallLeft.transform.position.x, wallLeft.transform.position.y, playerPosition.z);
        }
    }

    private IEnumerator DestroyWallsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destruimos las paredes
        Destroy(wallRight);
        Destroy(wallLeft);
    }

    private IEnumerator FollowPlayerPosition(GameObject wall)
    {
        while (true)
        {
            if (wall != null && playerController != null)
            {
                // Obt�n la posici�n del jugador
                Vector3 playerPosition = playerController.transform.position;

                // Establece la posici�n de la pared en funci�n de la posici�n del jugador en el eje Z
                wall.transform.position = new Vector3(wall.transform.position.x, wall.transform.position.y, playerPosition.z);
            }

            yield return null;
        }
    }
}

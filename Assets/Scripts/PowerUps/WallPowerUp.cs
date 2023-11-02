using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallPowerUp : Powerup
{
    public GameObject player;

    [SerializeField] public GameObject wallLeft;
    [SerializeField] public GameObject wallRight;
    private CharacterController playerController;

    public override void ActivatePowerUp()
    {
        throw new System.NotImplementedException();
    }

    public override void DeactivatePowerUp()
    {

        // Detenemos la actualización de la posición de las paredes
        StopAllCoroutines();

        // Borramos las paredes después de 5 segundos
        StartCoroutine(DestroyWallsAfterDelay(5f));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wallRight != null && wallLeft != null && playerController != null)
        {
            // Obtener la posición del jugador
            Vector3 playerPosition = playerController.transform.position;

            // Establece la posición de las paredes en función de la posición del jugador en el eje Z
            wallRight.transform.position = new Vector3(wallRight.transform.position.x, wallRight.transform.position.y, playerPosition.z);
            wallLeft.transform.position = new Vector3(wallLeft.transform.position.x, wallLeft.transform.position.y, playerPosition.z);
        }

        DeactivatePowerUp();
    }

    private IEnumerator DestroyWallsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destruimos las paredes
        Destroy(wallRight);
        Destroy(wallLeft);
    }
}

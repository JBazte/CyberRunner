using System.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/WallsPowerUp")]
public class WallPowerUp : PowerUpEffect
{
    [SerializeField]
    private GameObject m_wallLeft;
    [SerializeField] 
    private GameObject m_wallRight;
    public override void ExecuteAction(GameObject player)
    {
        m_player = player;
        m_wallLeft = GameObject.FindGameObjectWithTag("WallLeft");
        m_wallRight = GameObject.FindGameObjectWithTag("WallRight");
        m_wallRight.GetComponent<MeshRenderer>().enabled = true;
        m_wallLeft.GetComponent<MeshRenderer>() .enabled = true;
        m_wallRight.GetComponent<Collider>()    .enabled = true;
        m_wallLeft.GetComponent<Collider>()     .enabled = true;
        //m_wallRight.SetActive(true);
        //m_wallLeft .SetActive(true);
    }

    public override void FinishAction()
    {
        m_wallRight.GetComponent<MeshRenderer>().enabled = false;
        m_wallLeft.GetComponent<MeshRenderer>() .enabled = false;
        m_wallRight.GetComponent<Collider>()    .enabled = false;
        m_wallLeft.GetComponent<Collider>()     .enabled = false;
        //m_wallRight.SetActive(false);
        //m_wallLeft .SetActive(false);
    }

    public override IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(m_duration);
        FinishAction();
    }

    /*public GameObject player;

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
        // Detenemos específicamente las coroutines de wallRight y wallLeft
        if (followPlayerCoroutineWallRight != null)
        {
            StopCoroutine(followPlayerCoroutineWallRight);
        }

        if (followPlayerCoroutineWallLeft != null)
        {
            StopCoroutine(followPlayerCoroutineWallLeft);
        }

        // Borramos las paredes después de 5 segundos
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
            // Obtener la posición del jugador
            Vector3 playerPosition = playerController.transform.position;

            // Establece la posición de las paredes en función de la posición del jugador en el eje Z
            wallRight.transform.position = new Vector3(wallRight.transform.position.x, wallRight.transform.position.y, playerPosition.z);
            wallLeft.transform.position = new Vector3(wallLeft.transform.position.x, wallLeft.transform.position.y, playerPosition.z);
        }
    }

    private IEnumerator DestroyWallsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destruimos las paredes
        wallRight.SetActive(false);
        wallLeft.SetActive(false);
    }

    private IEnumerator FollowPlayerPosition(GameObject wall)
    {
        while (true)
        {
            if (wall != null && playerController != null)
            {
                // Obtén la posición del jugador
                Vector3 playerPosition = playerController.transform.position;

                // Establece la posición de la pared en función de la posición del jugador en el eje Z
                wall.transform.position = new Vector3(wall.transform.position.x, wall.transform.position.y, playerPosition.z);
            }

            yield return null;
        }
    }
    */
}

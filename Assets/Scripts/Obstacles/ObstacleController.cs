using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    /*
    private CameraShake cameraShake;

    private void Start() {
        cameraShake = FindAnyObjectByType<CameraShake>();
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Player")) {
            if (!col.gameObject.GetComponent<PlayerController>().GetInvulneravility())
            {
                // Shake camera & inflict damage to player
                StartCoroutine(cameraShake.Shake(.3f, .2f));
                GameManager.Instance.GameOver();
                if (col.gameObject.GetComponent<PlayerController>().GetMotoActive())
                {
                    col.gameObject.GetComponent<PlayerController>().MotorbikeCrashed();
                    GameManager.Instance.Resume();
                }
            }
        }
    }
    */
}

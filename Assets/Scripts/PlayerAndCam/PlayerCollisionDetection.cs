using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {
    private PlayerController playerController;

    void Start() {
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Player")) {
            return;
        }
        if (!playerController.GetInvulneravility()) {
            Debug.Log(col.gameObject.name);
            if (playerController.GetMotoActive()) {
                playerController.MotorbikeCrashed();
            } else {
                playerController.OnPlayerColliderHit(col.collider);
            }
        }
    }
}
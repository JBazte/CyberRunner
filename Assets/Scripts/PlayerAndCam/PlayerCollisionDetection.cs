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
        playerController.OnPlayerColliderHit(col.collider);
        Debug.Log(col.gameObject.name);
    }
}

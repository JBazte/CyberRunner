using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {
    private CameraShake cameraShake;
    private WorldGenerator worldGenerator;

    private void Start() {
        cameraShake = FindAnyObjectByType<CameraShake>();
        worldGenerator = FindAnyObjectByType<WorldGenerator>();
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Player")) {
            Debug.Log(col.gameObject.name);
            // Shake camera & inflict damage to player
            StartCoroutine(cameraShake.Shake(.3f, .2f));
            worldGenerator.worldVelocity = 0;
        }
    }
}

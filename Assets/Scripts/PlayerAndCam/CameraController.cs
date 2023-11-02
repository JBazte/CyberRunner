using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform player;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    void Start() {
        player = FindObjectOfType<PlayerController>().transform;
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z + zOffset);
    }

    void FixedUpdate() {
        float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, SmoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, player.position.y + yOffset, ref velocity.y, SmoothTime);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
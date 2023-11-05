using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform player;
    private Vector3 m_offset;
    private float yPos;
    private float smoothTime;
    private float shakeDuration;
    private float shakeMagnitude;
    private float shakeSpeed;
    private Vector3 originalPosition;

    private void Start() {
        smoothTime = 5f;
        shakeSpeed = 1f;
        m_offset = transform.position;
        originalPosition = transform.position;
    }

    private void LateUpdate() {
        Vector3 followPos = player.position + m_offset;
        RaycastHit hit;
        if (Physics.Raycast(player.position, Vector3.down, out hit, 4.5f)) {
            yPos = Mathf.Lerp(yPos, hit.point.y, Time.deltaTime * smoothTime);
        } else {
            yPos = Mathf.Lerp(yPos, player.position.y, Time.deltaTime * smoothTime);
        }
        followPos.y = m_offset.y + yPos;
        if (shakeDuration > 0) {
            Vector3 shake = Random.insideUnitSphere * shakeMagnitude;
            transform.position = followPos + shake;
            shakeDuration -= Time.deltaTime * shakeSpeed;
        } else {
            transform.position = followPos;
        }

    }

    public void ShakeCamera(float duration, float magnitude) {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}

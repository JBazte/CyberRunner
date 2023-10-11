using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class InputManager : MonoBehaviour {
    private const float SWIPE_DEADZONE = 100f;
    public static InputManager Instance { get; private set; }
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    private Vector2 SwipeDelta { get { return swipeDelta; } }

    private void Awake() {
        Instance = this;
    }

    void Update() {
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0)) {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0)) {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0) {
            if (Input.touches[0].phase == TouchPhase.Began) {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            } else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (isDraging) {
            if (Input.touches.Length < 0) {
                swipeDelta = Input.touches[0].position - startTouch;
            } else if (Input.GetMouseButton(0)) {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if (swipeDelta.magnitude > SWIPE_DEADZONE) {
            float xDistance = swipeDelta.x;
            float yDistance = swipeDelta.y;
            if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance)) {
                if (xDistance < 0) {
                    swipeLeft = true;
                } else {
                    swipeRight = true;
                }
            } else {
                if (yDistance < 0) {
                    swipeDown = true;
                } else {
                    swipeUp = true;
                }
            }
            Reset();
        }
    }

    private void Reset() {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}

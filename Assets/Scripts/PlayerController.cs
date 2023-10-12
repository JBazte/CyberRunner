using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum SIDE { Left, Middle, Right }

public class PlayerController : MonoBehaviour {
    private CharacterController m_player;
    [SerializeField]
    private SIDE m_side = SIDE.Middle;
    private bool moveLeft, moveRight, moveUp, moveDown, isJumping, isSliding;
    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float dodgeSpeed;
    private float transitionXPos, transitionYPos;
    private float xPos, xValue;
    private Animator m_anim;
    [SerializeField]
    private float jumpForce = 7f;
    private float m_initHeight, m_colCenterY;

    void Start() {
        m_player = GetComponent<CharacterController>();
        m_anim = GetComponent<Animator>();
        m_side = SIDE.Middle;
        xPos = 0;
        xValue = 3.0f;
        m_initHeight = m_player.height;
        m_colCenterY = m_player.center.y;
        isJumping = false;
        transform.position = Vector3.zero;
    }

    void Update() {
        moveLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || InputManager.Instance.SwipeLeft;
        moveRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || InputManager.Instance.SwipeRight;
        moveUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || InputManager.Instance.SwipeUp;
        moveDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || InputManager.Instance.SwipeDown;

        if (moveLeft && !isSliding) {
            if (m_side == SIDE.Middle) {
                xPos = -xValue;
                m_side = SIDE.Left;
                //m_anim.Play("moveLeft");
            } else if (m_side == SIDE.Right) {
                xPos = 0;
                m_side = SIDE.Middle;
                //m_anim.Play("moveLeft");
            } else {
                // TODO: Enable player instant death on next bump 
                //m_anim.Play("bumpLeft");
            }
        } else if (moveRight && !isSliding) {
            if (m_side == SIDE.Middle) {
                xPos = xValue;
                m_side = SIDE.Right;
                //m_anim.Play("moveRight");
            } else if (m_side == SIDE.Left) {
                xPos = 0;
                m_side = SIDE.Middle;
                //m_anim.Play("moveRight");
            } else {
                // TODO: Enable player instant death on next bump 
                //m_anim.Play("bumpRight");
            }
        }

        Jump();
        Slide();
    }

    private void FixedUpdate() {
        transitionXPos = Mathf.Lerp(transitionXPos, xPos, dodgeSpeed * Time.fixedDeltaTime);
        Vector3 moveVector = new Vector3(transitionXPos - transform.position.x, transitionYPos * Time.fixedDeltaTime, forwardSpeed * Time.fixedDeltaTime);
        m_player.Move(moveVector);
    }

    private void Jump() {
        if (m_player.isGrounded) {
            // if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("isFalling"))
            // {
            // m_anim.Play("isLanding");
            // isJumping = false;
            // }
            if (moveUp) {
                transitionYPos = jumpForce;
                //m_anim.CrossFadeInFixedTime("isJumping", 0.1f);
                isJumping = true;
            }
        } else {
            transitionYPos -= jumpForce * 2 * Time.deltaTime;
            if (m_player.velocity.y < -0.1f) {
                //m_anim.Play("isFalling");
            }
        }
    }

    internal float slideTimer;
    private void Slide() {
        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0f) {
            slideTimer = 0f;
            m_player.center = new Vector3(0, m_colCenterY, 0);
            m_player.height = m_initHeight;
            isSliding = false;
        }
        if (moveDown) {
            // TODO: check animation duration
            slideTimer = 0.8f;
            transitionYPos -= 10f;
            m_player.center = new Vector3(0, m_colCenterY / 2, 0);
            m_player.height = m_initHeight / 2f;
            //m_anim.CrossFadeInFixedTime("isSliding", 0.1f);
            isSliding = true;
            isJumping = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SIDE { Left, Middle, Right, LeftWall, RightWall }

public class PlayerController : MonoBehaviour {
    private CharacterController m_player;
    [SerializeField]
    private SIDE m_side = SIDE.Middle;
    private bool moveLeft, moveRight, moveUp, moveDown, isJumping, isSliding, Tap, DoubleTap;
    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float dodgeSpeed;
    private float transitionXPos, transitionYPos;
    private float xPos, xValue;
    private float yPos, yValue;
    private Animator m_anim;
    [SerializeField]
    private float jumpForce;
    private float m_initHeight, m_colCenterY;
    private bool  m_invulnerability;

    private bool isOnWall = false;  // Para rastrear si el jugador est� en una pared
    private Vector3 originalPosition;  // Para rastrear la posici�n original del jugador
    private int Motocharge = 0;
    private bool MotActive = false;
    private float timer;
    private Mesh PlayerModel;
    
    [SerializeField] private MeshFilter ActualPlayerModel;
    [SerializeField] private Mesh MotoModel;

    public float JumpForce
    {
        get
        {
            return this.jumpForce;
        }
        set
        {
            jumpForce = value;
        }
    }

    void Start()
    {
        Motocharge = 0;
        PlayerModel = ActualPlayerModel.mesh;
        m_player = GetComponent<CharacterController>();
        m_anim = GetComponent<Animator>();
        m_side = SIDE.Middle;
        xPos = 0;
        xValue = 3.0f;
        yPos = 0;
        yValue = 5.5f;
        m_initHeight = m_player.height;
        m_colCenterY = m_player.center.y;
        isJumping = false;
        transform.position = Vector3.zero;
        m_invulnerability = false;
        PlayerPrefs.SetInt("charges",0);

        //TEST
        originalPosition = transform.position;
    }

    void Update() {
        moveLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || InputManager.Instance.SwipeLeft;
        moveRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || InputManager.Instance.SwipeRight;
        moveUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || InputManager.Instance.SwipeUp;
        moveDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || InputManager.Instance.SwipeDown;
        Tap = Input.GetKeyDown(KeyCode.Space) || InputManager.Instance.Tap;
        


        if (moveLeft)
        {
            if (m_side == SIDE.Middle)
            {
                xPos = -xValue;
                m_side = SIDE.Left;
                //m_anim.Play("moveLeft");
            }
            else if (m_side == SIDE.Right)
            {
                xPos = 0;
                m_side = SIDE.Middle;
                //m_anim.Play("moveLeft");
            }
            else if (m_side == SIDE.Left)
            {
                yPos = yValue;
                m_side = SIDE.LeftWall;
                // Aqu� actualizamos la posici�n en el eje Y
                transitionYPos = yPos;
            }
            else if (m_side == SIDE.RightWall)
            {
                xPos = xValue;
                yPos = 0;
                m_side = SIDE.Right;
                // Aqu� tambi�n actualizamos la posici�n en el eje Y
                transitionYPos = yPos;
            }
            else
            {
                // TODO: Enable player instant death on next bump 
                //m_anim.Play("bumpLeft");
            }
        }
        else if (moveRight)
        {
            if (m_side == SIDE.Middle)
            {
                xPos = xValue;
                m_side = SIDE.Right;
                //m_anim.Play("moveRight");
            }
            else if (m_side == SIDE.Left)
            {
                xPos = 0;
                m_side = SIDE.Middle;
                //m_anim.Play("moveRight");
            }
            else if (m_side == SIDE.Right)
            {
                yPos = yValue;
                m_side = SIDE.RightWall;
                // Aqu� actualizamos la posici�n en el eje Y
                transitionYPos = yPos;
            }
            else if (m_side == SIDE.LeftWall)
            {
                xPos = -xValue;
                yPos = 0;
                m_side = SIDE.Left;
                // Aqu� tambi�n actualizamos la posici�n en el eje Y
                transitionYPos = yPos;
            }
            else
            {
                // TODO: Enable player instant death on next bump 
                //m_anim.Play("bumpRight");
            }
        }
        Motocharge = PlayerPrefs.GetInt("charges");
        if (Motocharge >= 1 && Tap && !MotActive)
        {
            Byke();
            PlayerPrefs.SetInt("charges",PlayerPrefs.GetInt("charges")-1);
        }
        
        Jump();
        Slide();
    }

    private void FixedUpdate() {
        Debug.Log("Estado: " + isOnWall + " Side: " + m_side);
        if (m_side == SIDE.LeftWall || m_side == SIDE.RightWall)
        {
            if (!isOnWall)
            {
                // El jugador acaba de entrar en la pared, ajusta su posici�n en Y a 5.5
                transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);
                isOnWall = true;
            }
        }
        else
        {
            if (isOnWall)
            {
                // El jugador ha dejado la pared, restaura su posici�n original en Y
                transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
                isOnWall = false;
            }
<<<<<<< Updated upstream
=======

            // Aqu� puedes controlar manualmente la ca�da del jugador si no est� en la pared
            // Por ejemplo, puedes disminuir gradualmente la posici�n en Y para simular la ca�da.
            // Aseg�rate de que la l�gica sea lo que necesitas en este caso.
>>>>>>> Stashed changes
        }


        transitionXPos = Mathf.Lerp(transitionXPos, xPos, dodgeSpeed * Time.fixedDeltaTime);
        Vector3 moveVector = new Vector3(transitionXPos - transform.position.x, transitionYPos * Time.fixedDeltaTime, forwardSpeed * Time.fixedDeltaTime);
        m_player.Move(moveVector);
    }

    private void Byke()
    {
        MotActive = true;
        ActualPlayerModel.mesh = MotoModel;
    }
    
    void DestroyObstacles(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.name != ("ModuleFloor") && hitCollider.gameObject.name != ("Player") && hitCollider.gameObject.name != ("LeftLane") && hitCollider.gameObject.name != ("MiddleLane") && hitCollider.gameObject.name != ("RightLane"))
            {
                hitCollider.gameObject.SetActive(false);
            }
        }
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
            transitionYPos -= jumpForce * 2f * Time.deltaTime;
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
    public bool GetMotActive() { return MotActive; }
    public void SetMotActive(bool Activate) { MotActive = Activate; }
}

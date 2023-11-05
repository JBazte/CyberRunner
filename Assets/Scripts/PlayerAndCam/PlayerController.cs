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

<<<<<<< HEAD
    private bool isOnWall = false;  // Para rastrear si el jugador estï¿½ en una pared
    private Vector3 originalPosition;  // Para rastrear la posiciï¿½n original del jugador
    private int Motocharge = 0;
    private bool MotActive = false;
    private float timer;
    private Mesh PlayerModel;
    
    [SerializeField] private MeshFilter ActualPlayerModel;
    [SerializeField] private Mesh MotoModel;
=======
    private bool isOnWall = false;  // Para rastrear si el jugador está en una pared
    //private Vector3 originalPosition;  // Para rastrear la posición original del jugador
>>>>>>> f9660a83aa1f55015ab067af62002e40f404955f

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
<<<<<<< HEAD
        PlayerPrefs.SetInt("charges",0);

        //TEST
        originalPosition = transform.position;
=======
>>>>>>> f9660a83aa1f55015ab067af62002e40f404955f
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
                // Aquï¿½ actualizamos la posiciï¿½n en el eje Y
                transitionYPos = yPos;
            }
            else if (m_side == SIDE.RightWall)
            {
                xPos = xValue;
                yPos = 0;
                m_side = SIDE.Right;
                // Aquï¿½ tambiï¿½n actualizamos la posiciï¿½n en el eje Y
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
                // Aquï¿½ actualizamos la posiciï¿½n en el eje Y
                transitionYPos = yPos;
            }
            else if (m_side == SIDE.LeftWall)
            {
                xPos = -xValue;
                yPos = 0;
                m_side = SIDE.Left;
                // Aquï¿½ tambiï¿½n actualizamos la posiciï¿½n en el eje Y
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
                // El jugador acaba de entrar en la pared, ajusta su posiciï¿½n en Y a 5.5
                transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);
                isOnWall = true;
            }
        }
        else
        {
            if (isOnWall)
            {
<<<<<<< HEAD
                // El jugador ha dejado la pared, restaura su posiciï¿½n original en Y
                transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
=======
                // El jugador ha dejado la pared, restaura su posición original en Y
                transform.position = new Vector3(transform.position.x, 1.25f, transform.position.z);
>>>>>>> f9660a83aa1f55015ab067af62002e40f404955f
                isOnWall = false;
            }
<<<<<<< Updated upstream
=======

            // Aquï¿½ puedes controlar manualmente la caï¿½da del jugador si no estï¿½ en la pared
            // Por ejemplo, puedes disminuir gradualmente la posiciï¿½n en Y para simular la caï¿½da.
            // Asegï¿½rate de que la lï¿½gica sea lo que necesitas en este caso.
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

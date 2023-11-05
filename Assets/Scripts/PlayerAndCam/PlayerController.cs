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
    private SIDE  m_side = SIDE.Middle;
    private bool  moveLeft, moveRight, moveUp, moveDown, isJumping, isSliding, Tap, DoubleTap;
    private float m_doubleTapTime = 0.25f;
    private float m_lastClickTime;

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

    private bool    isOnWall = false;  // Para rastrear si el jugador esta en una pared
    private Vector3 originalPosition;  // Para rastrear la posicion original del jugador
    private bool    m_motorbikeActive = false;
    private float   timer;

    [SerializeField]
    private MeshFilter m_actualPlayerModel;
    [SerializeField]
    private Mesh       m_playerModel;
    [SerializeField]
    private Mesh       m_motorbikeModel;

    private MotorbikeObject   m_motorbike;
    private HyperspeedAbility m_hyperspeedAbility;
    private bool              m_isOnHyperspeed;
    private float             m_hyperspeedPointEnd;

    public float JumpForce
    {
        get
        {
            return jumpForce;
        }
        set
        {
            jumpForce = value;
        }
    }



    void Start()
    {
        m_playerModel       = gameObject.GetComponent<MeshFilter>().mesh;
        m_actualPlayerModel = gameObject.GetComponent<MeshFilter>();
        m_player            = GetComponent<CharacterController>();
        m_anim              = GetComponent<Animator>();
        m_side              = SIDE.Middle;
        xPos                = 0;
        xValue              = 3.0f;
        yPos                = 0;
        yValue              = 5.5f;
        m_initHeight        = m_player.height;
        m_colCenterY        = m_player.center.y;
        isJumping           = false;
        transform.position  = Vector3.zero;
        m_invulnerability   = false;

        m_motorbike         = new MotorbikeObject(gameObject, m_motorbikeModel);
        m_hyperspeedAbility = new HyperspeedAbility(gameObject);
        m_isOnHyperspeed    = false;

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
                // Aquí actualizamos la posiciï¿½n en el eje Y
                transitionYPos = yPos;
            }
            else if (m_side == SIDE.RightWall)
            {
                xPos = xValue;
                yPos = 0;
                m_side = SIDE.Right;
                // Aquí también actualizamos la posición en el eje Y
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

        if (Tap)                                              // HERE WE DETECT THE FIRST TAP AND SAVE THE TIME WHEN IT HAS BEEN DONE
        {
            if (Time.time - m_lastClickTime < m_doubleTapTime)// HERE WE CHECK IF ITS A DOUBLE TAP
            {
                if (PlayerPrefs.GetInt("MotorbikeCharges") < 0)//HERE WE CHECK IF MOTORBIKE IS AVAILABLE TO USE IT
                {
                    Debug.Log("NO MOTORBIKE CHARGES");
                }
                else if (m_motorbikeActive)
                {
                    Debug.Log("MOTORBIKE ALREADY ACTIVE");
                }
                else
                {
                    ActivateMotorbike();
                }
            }
            m_lastClickTime = Time.time;
        }

        if(!m_isOnHyperspeed && Input.GetKeyDown(KeyCode.H) && GameManager.Instance.GetTraveledMeters() < 100.0f /*&& PlayerPrefs.GetInt("HyperspeedCharges") > 0*/) //HERE WE ACTIVATE HYPERSPEED
        {
            m_hyperspeedPointEnd  = GameManager.Instance.GetTraveledMeters() + m_hyperspeedAbility.GetMetersDuration();
            ActivateHyperspeed();
        }

        if(m_isOnHyperspeed)
        {
            if(GameManager.Instance.GetTraveledMeters() >= m_hyperspeedPointEnd) 
                ExitHyperspeed();
            else transitionYPos = 10.0f;
        }
        else { 
            Jump();
            Slide();
        }
        
        
    }

    private void FixedUpdate() {
        Debug.Log("Estado: " + isOnWall + " Side: " + m_side);
        if (m_side == SIDE.LeftWall || m_side == SIDE.RightWall)
        {
            if (!isOnWall)
            {
                // El jugador acaba de entrar en la pared, ajusta su posición en Y a 5.5
                transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);
                isOnWall = true;
            }
        }
        else
        {
            if (isOnWall)
            {
                // El jugador ha dejado la pared, restaura su posición original en Y
                transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);

                // El jugador ha dejado la pared, restaura su posición original en Y
                //transform.position = new Vector3(transform.position.x, 1.25f, transform.position.z);
                isOnWall = false;
            }
        }


        transitionXPos = Mathf.Lerp(transitionXPos, xPos, dodgeSpeed * Time.fixedDeltaTime);
        Vector3 moveVector = new Vector3(transitionXPos - transform.position.x, transitionYPos * Time.fixedDeltaTime, forwardSpeed * Time.fixedDeltaTime);
        m_player.Move(moveVector);
    }

    private void ActivateMotorbike()
    {
        m_motorbike.ActivateMotorbike();
    }

    public void MotorbikeCrashed()
    {
        m_motorbike.DeactivateMotorbike();
    }

    private void ActivateHyperspeed()
    {
        m_hyperspeedAbility.ActivateHyperspeed();
    }

    private void ExitHyperspeed()
    {
        m_hyperspeedAbility.ExitHyperspeed();
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
    public bool GetMotoActive() { return m_motorbikeActive; }
    public void SetMotoActive(bool Activate) { m_motorbikeActive = Activate; }
    public void SetMesh(Mesh mesh) { m_actualPlayerModel.mesh = mesh; }

    public Mesh GetPlayerMesh() { return m_playerModel; }

    public void SetInvulneravility(bool invulnerability) { m_invulnerability = invulnerability; }
    public bool GetInvulneravility() {  return m_invulnerability; }

    public void SetIsOnHyperspeed(bool isOnHS) { m_isOnHyperspeed = isOnHS; }
}

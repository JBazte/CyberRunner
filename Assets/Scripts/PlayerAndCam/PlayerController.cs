using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SIDE { Left = -3, Middle = 0, Right = 3, LeftWall = 4, RightWall = 5 }
public enum HitBoxX { Left, Middle, Right, None }
public enum HitBoxY { Up, Middle, LowMiddle, Down, None }
public enum HitBoxZ { Forward, Middle, Backward, None }

public class PlayerController : MonoBehaviour
{
    private CharacterController m_player;
    [SerializeField]
    private SIDE m_side = SIDE.Middle;
    public HitBoxX m_hitBoxX = HitBoxX.None;
    public HitBoxY m_hitBoxY = HitBoxY.None;
    public HitBoxZ m_hitBoxZ = HitBoxZ.None;
    private bool moveLeft, moveRight, moveUp, moveDown, isJumping, isSliding, Tap, DoubleTap;
    private float m_doubleTapTime = 0.25f;
    private float m_lastClickTime;

    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float dodgeSpeed;
    private float transitionXPos, transitionYPos;
    private SIDE m_lastSide;
    private Animator m_anim;
    [SerializeField]
    private float jumpForce;
    private float m_initHeight, m_colCenterY;
    public CameraController cameraController;
    private WorldGenerator worldGenerator;
    private bool stopAllAnim = false;
    private float stumbleTolerance = 10f;
    private float StumbleTime;
    private bool isInputEnabled = true;
    [SerializeField]
    private Collider CollisionCol;
    private bool m_invulnerability;

    private bool isOnWall = false;  // Para rastrear si el jugador esta en una pared
    private Vector3 originalPosition;  // Para rastrear la posicion original del jugador
    private bool m_motorbikeActive = false;
    private float timer;

    [SerializeField]
    private MeshFilter m_actualPlayerModel;
    [SerializeField]
    private Mesh m_playerModel;
    [SerializeField]
    private Mesh m_motorbikeModel;

    private MotorbikeObject m_motorbike;
    private HyperspeedAbility m_hyperspeedAbility;
    private bool m_isOnHyperspeed;
    private float m_hyperspeedPointEnd;

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
        m_playerModel = gameObject.GetComponent<MeshFilter>().mesh;
        m_actualPlayerModel = gameObject.GetComponent<MeshFilter>();
        m_player = GetComponent<CharacterController>();
        m_anim = GetComponent<Animator>();
        cameraController = FindObjectOfType<CameraController>();
        worldGenerator = FindObjectOfType<WorldGenerator>();

        StumbleTime = stumbleTolerance;
        m_side = SIDE.Middle;
        m_initHeight = m_player.height;
        m_colCenterY = m_player.center.y;
        isJumping = false;
        transform.position = Vector3.up;
        m_invulnerability = false;

        m_motorbike = new MotorbikeObject(gameObject, m_motorbikeModel);
        m_hyperspeedAbility = new HyperspeedAbility(gameObject);
        m_isOnHyperspeed = false;

        //TEST
        originalPosition = transform.position;
    }
    void Update()
    {
        CollisionCol.isTrigger = !isInputEnabled;
        if (!isInputEnabled)
        {
            m_player.Move(Vector3.down * 10f * Time.deltaTime);
            return;
        }
        moveLeft = (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || InputManager.Instance.SwipeLeft) && isInputEnabled;
        moveRight = (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || InputManager.Instance.SwipeRight) && isInputEnabled;
        moveUp = (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || InputManager.Instance.SwipeUp) && isInputEnabled;
        moveDown = (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || InputManager.Instance.SwipeDown) && isInputEnabled;
        Tap = (Input.GetKeyDown(KeyCode.Space) || InputManager.Instance.Tap) && isInputEnabled;

        if (moveLeft)
        {
            if (m_side == SIDE.Middle)
            {
                m_lastSide = m_side;
                m_side = SIDE.Left;
                //PlayAnimation("moveLeft");
            }
            else if (m_side == SIDE.Right)
            {
                m_lastSide = m_side;
                m_side = SIDE.Middle;
                //PlayAnimation("moveLeft");
            }
            else if (m_side == SIDE.Left)
            {
                m_lastSide = m_side;
                m_side = SIDE.LeftWall;
                //PlayAnimation("jumpUpWallLeft");
            }
            else if (m_side == SIDE.RightWall)
            {
                m_lastSide = m_side;
                m_side = SIDE.Right;
                //PlayAnimation("jumpOffWallRight");
            }
            else if (m_side != m_lastSide)
            {
                m_lastSide = m_side;
                //PlayAnimation("stumbleOffLeft");
            }
        }
        else if (moveRight)
        {
            if (m_side == SIDE.Middle)
            {
                m_lastSide = m_side;
                m_side = SIDE.Right;
                //PlayAnimation("moveRight");
            }
            else if (m_side == SIDE.Left)
            {
                m_lastSide = m_side;
                m_side = SIDE.Middle;
                //PlayAnimation("moveRight");
            }
            else if (m_side == SIDE.Right)
            {
                m_lastSide = m_side;
                m_side = SIDE.RightWall;
                //PlayAnimation("jumpUpWallRight");
            }
            else if (m_side == SIDE.LeftWall)
            {
                m_lastSide = m_side;
                m_side = SIDE.Left;
                //PlayAnimation("jumpOffWallLeft");
            }
            else if (m_side != m_lastSide)
            {
                m_lastSide = m_side;
                //PlayAnimation("stumbleOffRight");
            }
        }

        /*if (m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
            m_anim.SetLayerWeight(1, 0);
            stopAllAnim = false;
        }*/

        StumbleTime = Mathf.MoveTowards(StumbleTime, stumbleTolerance, Time.deltaTime);

        transitionXPos = Mathf.Lerp(transitionXPos, (int)m_side, dodgeSpeed * Time.deltaTime);
        Vector3 moveVector = new Vector3(transitionXPos - transform.position.x, transitionYPos * Time.deltaTime, forwardSpeed * Time.deltaTime);
        m_player.Move(moveVector);

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

        if (!m_isOnHyperspeed && Input.GetKeyDown(KeyCode.H) && GameManager.Instance.GetTraveledMeters() < 100.0f /*&& PlayerPrefs.GetInt("HyperspeedCharges") > 0*/) //HERE WE ACTIVATE HYPERSPEED
        {
            m_hyperspeedPointEnd = GameManager.Instance.GetTraveledMeters() + m_hyperspeedAbility.GetMetersDuration();
            ActivateHyperspeed();
        }

        if (m_isOnHyperspeed)
        {
            if (GameManager.Instance.GetTraveledMeters() >= m_hyperspeedPointEnd)
                ExitHyperspeed();
            else transitionYPos = 10.0f;
        }
        else
        {
            Jump();
            Slide();
        }

        /*if (m_side == SIDE.LeftWall || m_side == SIDE.RightWall) {
            if (!isOnWall) {
                // El jugador acaba de entrar en la pared, ajusta su posici�n en Y a 5.5
                transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);
                isOnWall = true;
            }
        } else {
            if (isOnWall) {
                // El jugador ha dejado la pared, restaura su posici�n original en Y
                transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);

                // El jugador ha dejado la pared, restaura su posici�n original en Y
                //transform.position = new Vector3(transform.position.x, 1.25f, transform.position.z);
                isOnWall = false;
            }
        }*/
    }

    public IEnumerator DeathPlayer(string anim)
    {
        stopAllAnim = true;
        cameraController.ShakeCamera(0.5f, 0.2f);
        SpeedManager.Instance.SetRunSpeed(0);
        //m_anim.SetLayerWeight(1, 0);
        //m_anim.Play(anim);
        yield return new WaitForSeconds(0.2f);
        isInputEnabled = false;
    }

    public void PlayAnimation(string anim)
    {
        if (stopAllAnim) return;
        //m_anim.Play(anim);
    }

    public void Stumble(string anim)
    {
        //m_anim.ForceStateNormalizedTime(0.0f);
        stopAllAnim = true;
        cameraController.ShakeCamera(0.5f, 0.2f);
        //m_anim.Play(anim);
        if (StumbleTime < stumbleTolerance / 2f)
        {
            StartCoroutine(DeathPlayer("stumbleLow"));
            return;
        }

        StumbleTime -= 6f;
        ResetCollision();
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

    private void Jump()
    {
        if (m_player.isGrounded)
        {
            // if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("isFalling"))
            // {
            // PlayAnimation("isLanding");
            // isJumping = false;
            // }
            if (moveUp)
            {
                transitionYPos = jumpForce;
                //m_anim.CrossFadeInFixedTime("isJumping", 0.1f);
                isJumping = true;
            }
        }
        else
        {
            transitionYPos -= jumpForce * 2f * Time.deltaTime;
            if (m_player.velocity.y < -0.1f)
            {
                //PlayAnimation("isFalling");
            }
        }
    }

    internal float slideTimer;
    private void Slide()
    {
        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0f)
        {
            slideTimer = 0f;
            m_player.center = new Vector3(0, m_colCenterY, 0);
            m_player.height = m_initHeight;
            isSliding = false;
        }
        if (moveDown)
        {
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

    public void OnPlayerColliderHit(Collider col)
    {
        m_hitBoxX = GetHitBoxX(col);
        m_hitBoxY = GetHitBoxY(col);
        m_hitBoxZ = GetHitBoxZ(col);

        if (m_hitBoxZ == HitBoxZ.Forward && m_hitBoxX == HitBoxX.Middle)
        {
            // Shake camera & death of player

            if (m_hitBoxY == HitBoxY.LowMiddle)
            {
                Stumble("stumbleLow");
            }
            else if (m_hitBoxY == HitBoxY.Down)
            {
                StartCoroutine(DeathPlayer("deathLow"));
                ResetCollision();
            }
            else if (m_hitBoxY == HitBoxY.Middle)
            {
                if (col.CompareTag("DynamicObstacle"))
                {
                    StartCoroutine(DeathPlayer("deathDynamicObstacle"));
                    ResetCollision();
                }
                else if (!col.CompareTag("Ramp"))
                {
                    StartCoroutine(DeathPlayer("deathBounce"));
                    ResetCollision();
                }
            }
            else if (m_hitBoxY == HitBoxY.Up && !isSliding)
            {
                StartCoroutine(DeathPlayer("deathUpper"));
                ResetCollision();
            }
        }
        else if (m_hitBoxZ == HitBoxZ.Middle)
        {
            if (m_hitBoxX == HitBoxX.Right)
            {
                m_side = m_lastSide;
                Stumble("stumbleSideRight");
            }
            else if (m_hitBoxX == HitBoxX.Left)
            {
                m_side = m_lastSide;
                Stumble("stumbleSideLeft");
            }
        }
        else
        {
            if (m_hitBoxX == HitBoxX.Right)
            {
                //m_anim.SetLayerWeight(1, 1);
                Stumble("stumbleRightCorner");
            }
            else if (m_hitBoxX == HitBoxX.Left)
            {
                //m_anim.SetLayerWeight(1, 1);
                Stumble("stumbleLeftCorner");
            }
        }
    }

    private void ResetCollision()
    {
        m_hitBoxX = HitBoxX.None;
        m_hitBoxY = HitBoxY.None;
        m_hitBoxZ = HitBoxZ.None;
    }

    public HitBoxX GetHitBoxX(Collider col)
    {
        Bounds player_bounds = m_player.bounds;
        Bounds col_bounds = col.bounds;

        float min_x = Mathf.Max(col_bounds.min.x, player_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, player_bounds.max.x);
        float average_x = (min_x + max_x) / 2f - col_bounds.min.x;

        HitBoxX hitX;
        if (average_x > col_bounds.size.x - 0.33f)
        {
            hitX = HitBoxX.Right;
        }
        else if (average_x < 0.33f)
        {
            hitX = HitBoxX.Left;
        }
        else
        {
            hitX = HitBoxX.Middle;
        }
        return hitX;
    }

    public HitBoxY GetHitBoxY(Collider col)
    {
        Bounds player_bounds = m_player.bounds;
        Bounds col_bounds = col.bounds;

        float min_y = Mathf.Max(col_bounds.min.y, player_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, player_bounds.max.y);
        float average_y = ((min_y + max_y) / 2f - player_bounds.min.y) / player_bounds.size.y;

        HitBoxY hitY;
        if (average_y < 0.17f)
        {
            hitY = HitBoxY.LowMiddle;
        }
        else if (average_y < 0.33f)
        {
            hitY = HitBoxY.Down;
        }
        else if (average_y < 0.66f)
        {
            hitY = HitBoxY.Middle;
        }
        else
        {
            hitY = HitBoxY.Up;
        }
        return hitY;
    }

    public HitBoxZ GetHitBoxZ(Collider col)
    {
        Bounds player_bounds = m_player.bounds;
        Bounds col_bounds = col.bounds;

        float min_z = Mathf.Max(col_bounds.min.z, player_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, player_bounds.max.z);
        float average_z = ((min_z + max_z) / 2f - player_bounds.min.z) / player_bounds.size.z;

        HitBoxZ hitZ;
        if (average_z < 0.33f)
        {
            hitZ = HitBoxZ.Backward;
        }
        else if (average_z < 0.66f)
        {
            hitZ = HitBoxZ.Middle;
        }
        else
        {
            hitZ = HitBoxZ.Forward;
        }
        return hitZ;
    }
    public bool GetMotoActive() { return m_motorbikeActive; }
    public void SetMotoActive(bool Activate) { m_motorbikeActive = Activate; }
    public void SetMesh(Mesh mesh) { m_actualPlayerModel.mesh = mesh; }
    public Mesh GetPlayerMesh() { return m_playerModel; }
    public void SetInvulneravility(bool invulnerability) { m_invulnerability = invulnerability; }
    public bool GetInvulneravility() { return m_invulnerability; }
    public void SetIsOnHyperspeed(bool isOnHS) { m_isOnHyperspeed = isOnHS; }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SIDE { LeftWall = -5, Left = -3, Middle = 0, Right = 3, RightWall = 5 }
public enum HitBoxX { Left, Middle, Right, None }
public enum HitBoxY { Up, Middle, LowMiddle, Down, None }
public enum HitBoxZ { Forward, Middle, Backward, None }
public enum TutorialBlock { Slash, Shield, GroundWave }

public class PlayerController : MonoBehaviour {
    private CharacterController m_player;
    [SerializeField]
    private SIDE m_side = SIDE.Middle;
    public HitBoxX m_hitBoxX = HitBoxX.None;
    public HitBoxY m_hitBoxY = HitBoxY.None;
    public HitBoxZ m_hitBoxZ = HitBoxZ.None;
    private bool moveLeft, moveRight, moveUp, moveDown, isJumping, isSliding, Tap, DoubleTap;
    private float m_doubleTapTime = 0.2f;
    private float m_lastClickTime;

    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float dodgeSpeed;
    private float transitionXPos, transitionYPos;
    private SIDE m_lastSide;
    public Animator m_anim;
    [SerializeField]
    private float jumpForce;
    private float m_initHeight, m_colCenterY;
    public CameraController cameraController;
    private bool stopAllAnim = false;
    private float stumbleTolerance = 10f;
    private float stumbleTime;
    private bool m_isInputEnabled = false;
    [SerializeField]
    private Collider CollisionCol;
    private bool m_invulnerability;
    public bool leftWallDetected, rightWallDetected, m_isOnWall = false;
    [SerializeField]
    private float m_maxTimeOnWall;
    [SerializeField]
    private float m_wallWalkCooldown;
    private bool m_wallOnCooldown = false;
    private RaycastHit m_hitLeft, m_hitRight;
    private bool m_motorbikeActive = false;
    [SerializeField]
    private MeshFilter m_currentPlayerModel;
    [SerializeField]
    private Mesh m_playerModel;
    [SerializeField]
    private Mesh m_motorbikeModel;
    private MotorbikeObject m_motorbike;
    private HyperspeedAbility m_hyperspeedAbility;
    private bool m_isOnHyperspeed;
    private float m_hyperspeedPointEnd;

    private bool m_isOnTutorial;
    private TutorialBlock m_tutorialActive;

    public float JumpForce {
        get {
            return jumpForce;
        }
        set {
            jumpForce = value;
        }
    }

    private void Awake()
    {
        m_motorbikeModel = Resources.Load<Mesh>(AppPaths.MOTORBIKE_MODEL_1);
        m_motorbike = new MotorbikeObject(gameObject, m_motorbikeModel);
        m_hyperspeedAbility = new HyperspeedAbility(gameObject);
    }

    void Start() {
        m_playerModel = gameObject.GetComponent<MeshFilter>().mesh;
        m_currentPlayerModel = gameObject.GetComponent<MeshFilter>();
        m_player = GetComponent<CharacterController>();
        m_anim = GetComponentInChildren<Animator>();
        cameraController = FindObjectOfType<CameraController>();

        stumbleTime = stumbleTolerance;
        m_side = SIDE.Middle;
        m_initHeight = m_player.height;
        m_colCenterY = m_player.center.y;
        isJumping = false;
        transform.position = Vector3.up;
        m_invulnerability = false;
        m_maxTimeOnWall = 2.0f;
        m_wallWalkCooldown = 1.0f;
        m_isOnTutorial = false;

        m_motorbike = new MotorbikeObject(gameObject, m_motorbikeModel);
        m_hyperspeedAbility = new HyperspeedAbility(gameObject);
        m_isOnHyperspeed = false;
        PlayAnimation("idle");
    }

    void Update() {
        CollisionCol.isTrigger = !m_isInputEnabled;
        if (!m_isInputEnabled) {
            m_player.Move(Vector3.down * 10f * Time.deltaTime);
            return;
        }
        moveLeft = (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || InputManager.Instance.SwipeLeft) && m_isInputEnabled;
        moveRight = (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || InputManager.Instance.SwipeRight) && m_isInputEnabled;
        moveUp = (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || InputManager.Instance.SwipeUp) && m_isInputEnabled;
        moveDown = (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || InputManager.Instance.SwipeDown) && m_isInputEnabled;
        Tap = (Input.GetKeyDown(KeyCode.Space) || InputManager.Instance.Tap) && m_isInputEnabled;

        if (Physics.Raycast(transform.position, -transform.right, out m_hitLeft, 3f)) {
            Debug.DrawRay(transform.position, -transform.right * m_hitLeft.distance, Color.red);
            if (m_hitLeft.collider.CompareTag("Wall")) {
                leftWallDetected = true;
            } else {
                leftWallDetected = false;
            }
        }
        if (Physics.Raycast(transform.position, transform.right, out m_hitRight, 3f)) {
            Debug.DrawRay(transform.position, transform.right * m_hitRight.distance, Color.red);
            if (m_hitRight.collider.CompareTag("Wall")) {
                rightWallDetected = true;
            } else {
                rightWallDetected = false;
            }
        }
        if (m_isInputEnabled) {
            if(!m_isOnTutorial)
            {
                if (moveLeft)
                {
                    if (m_side == SIDE.Middle)
                    {
                        m_lastSide = m_side;
                        m_side = SIDE.Left;
                        PlayAnimation("moveLeft");
                    }
                    else if (m_side == SIDE.Right)
                    {
                        m_lastSide = m_side;
                        m_side = SIDE.Middle;
                        PlayAnimation("moveLeft");
                    }
                    else if (m_side == SIDE.Left && leftWallDetected && !m_motorbikeActive && !m_wallOnCooldown)
                    {
                        m_lastSide = m_side;
                        m_side = SIDE.LeftWall;
                        m_isOnWall = true;
                        StartCoroutine(JumpOffWall(SIDE.Left));
                        PlayAnimation("wallRunLeftStart");
                    }
                    else if (m_side == SIDE.RightWall)
                    {
                        m_lastSide = m_side;
                        m_side = SIDE.Right;
                        m_isOnWall = false;
                        StartCoroutine(WallWalkCooldown());
                        PlayAnimation("wallRunRightEnd");
                    }
                    else if (m_side != m_lastSide)
                    {
                        m_lastSide = m_side;
                        //PlayAnimation("stumbleOffLeft");
                        PlayAnimation("stumble");
                    }
                }
                else if (moveRight)
                {
                    if (m_side == SIDE.Middle)
                    {
                        m_lastSide = m_side;
                        m_side = SIDE.Right;
                        PlayAnimation("moveRight");
                    }
                    else if (m_side == SIDE.Left)
                    {
                        m_lastSide = m_side;
                        m_side = SIDE.Middle;
                        PlayAnimation("moveRight");
                    }
                    else if (m_side == SIDE.Right && rightWallDetected && !m_motorbikeActive && !m_wallOnCooldown)
                    {
                        m_lastSide = m_side;
                        m_side = SIDE.RightWall;
                        m_isOnWall = true;
                        StartCoroutine(JumpOffWall(SIDE.Right));
                        PlayAnimation("wallRunRightStart");
                    }
                    else if (m_side == SIDE.LeftWall)
                    {
                        m_lastSide = m_side;
                        m_side = SIDE.Left;
                        m_isOnWall = false;
                        PlayAnimation("wallRunLeftEnd");
                        StartCoroutine(WallWalkCooldown());
                    }
                    else if (m_side != m_lastSide)
                    {
                        m_lastSide = m_side;
                        //PlayAnimation("stumbleOffRight");
                        PlayAnimation("stumble");
                    }
                }

                if (m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    //m_anim.SetLayerWeight(1, 0);
                    stopAllAnim = false;
                }

                stumbleTime = Mathf.MoveTowards(stumbleTime, stumbleTolerance, Time.deltaTime);

                transitionXPos = Mathf.Lerp(transitionXPos, (int)m_side, dodgeSpeed * Time.deltaTime);
                Vector3 moveVector;
                if (m_isOnWall)
                {
                    moveVector = new Vector3(transitionXPos - transform.position.x, 6f - transform.position.y, forwardSpeed * Time.deltaTime);
                }
                else
                {
                    moveVector = new Vector3(transitionXPos - transform.position.x, transitionYPos * Time.deltaTime, forwardSpeed * Time.deltaTime);
                }
                m_player.Move(moveVector);
                if (Tap)                                              // HERE WE DETECT THE FIRST TAP AND SAVE THE TIME WHEN IT HAS BEEN DONE
                {
                    if (Time.time - m_lastClickTime < m_doubleTapTime)// HERE WE CHECK IF ITS A DOUBLE TAP
                    {
                        StartCoroutine(m_motorbike.StartCountDown());
                        if (PlayerPrefs.GetInt("MotorbikeCharges") <= 0)//HERE WE CHECK IF MOTORBIKE IS AVAILABLE TO USE IT
                        {
                            if (PlayerPrefs.GetInt("MotorbikeCharges") < 0) PlayerPrefs.SetInt("MotorbikeCharges", 0);
                            Debug.Log("NO MOTORBIKE CHARGES");
                        }
                        else if (m_motorbikeActive)
                        {
                            Debug.Log("MOTORBIKE ALREADY ACTIVE");
                        }
                        else
                        {
                            ActivateMotorbike();
                            StartCoroutine(m_motorbike.StartCountDown());
                        }
                    }
                    m_lastClickTime = Time.time;
                }                                           // HERE WE DETECT THE FIRST TAP AND SAVE THE TIME WHEN IT HAS BEEN DONE

                //HERE WE ACTIVATE HYPERSPEED
                if (!m_isOnHyperspeed && Input.GetKeyDown(KeyCode.H) && GameManager.Instance.GetTraveledMeters() < 100.0f && PlayerPrefs.GetInt("HyperspeedCharges") > 0)
                {
                    m_hyperspeedPointEnd = GameManager.Instance.GetTraveledMeters() + m_hyperspeedAbility.GetMetersDuration();
                    ActivateHyperspeed();
                    PlayAnimation("flying");
                }

                if (m_isOnHyperspeed)
                {
                    if (GameManager.Instance.GetTraveledMeters() >= m_hyperspeedPointEnd)
                    {
                        ExitHyperspeed();
                        PlayAnimation("falling");
                    }
                    else
                    {
                        transitionYPos = 10.0f;
                    }
                }
                else if (!m_isOnWall)
                {
                    Slide();
                    Jump();
                }
            }
            else
            {
                if(m_tutorialActive == TutorialBlock.Slash)
                {
                    Slide();
                    if(isSliding)
                    {
                        m_isOnTutorial = false;
                        UIManager.Instance.OutOfTutorial();
                    }
                }
                else if(m_tutorialActive == TutorialBlock.Shield)
                {
                    Jump();
                    if (isJumping)
                    {
                        m_isOnTutorial = false;
                        UIManager.Instance.OutOfTutorial();
                    }
                }
                else if(m_tutorialActive == TutorialBlock.GroundWave)
                {
                    Jump();
                    if (isJumping)
                    {
                        m_isOnTutorial = false;
                        UIManager.Instance.OutOfTutorial();
                    }
                }
            }
        }
                
    }

    public IEnumerator DeathPlayer(string anim) {
        stopAllAnim = true;
        cameraController.ShakeCamera(0.5f, 0.2f);
        GameManager.Instance.GameOver();
        GameManager.Instance.ResetCombo();
        SfxMusicManager.Instance.PlaySfxMusic("HitSfx");
        if(ModuleManager.Instance.HasCollisionObject()) ModuleManager.Instance.DeactivateCollisionObject();
        //m_anim.SetLayerWeight(1, 0);
        m_anim.Play(anim);
        yield return new WaitForSeconds(0.2f);
        m_isInputEnabled = false;
    }

    public void PlayAnimation(string anim) {
        if (stopAllAnim) return;
        Debug.Log(anim);
        m_anim.Play(anim);
    }

    public void Stumble(string anim) {
        stopAllAnim = true;
        cameraController.ShakeCamera(0.5f, 0.2f);
        m_anim.Play(anim, 0, 0.0f);
        GameManager.Instance.ResetCombo();
        if (stumbleTime < stumbleTolerance / 2f) {
            //StartCoroutine(DeathPlayer("stumbleLow"));
            StartCoroutine(DeathPlayer("deathUpper"));
            return;
        }

        stumbleTime -= 6f;
        ResetCollision();
    }
    public void InitializePowerUpObjects() {
        m_hyperspeedAbility.setMetersDuration(PowerUpManager.Instance.GetHyperspeedPowerUp().GetHyperspeedMetersDuration());
        m_motorbike.SetDuration(PowerUpManager.Instance.GetMotorbikePowerUp().GetMotorbikeDuration());
    }
    private void ActivateMotorbike() {
        m_motorbike.ActivateMotorbike();
    }
    public void MotorbikeCrashed() {
        m_motorbike.DeactivateMotorbike();
    }
    private void ActivateHyperspeed() {
        m_hyperspeedAbility.ActivateHyperspeed();
    }
    private void ExitHyperspeed() {
        m_hyperspeedAbility.ExitHyperspeed();
    }

    private void Jump() {
        if (m_player.isGrounded) {
            if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("falling")) {
                PlayAnimation("landing");
                isJumping = false;
            }
            if (moveUp) {
                transitionYPos = jumpForce;
                m_anim.CrossFadeInFixedTime("jump", 0.1f);
                isJumping = true;
            }
        } else {
            transitionYPos -= jumpForce * 2f * Time.deltaTime;
            if (m_player.velocity.y < -0.1f && isJumping) {
                PlayAnimation("falling");
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
            m_anim.CrossFadeInFixedTime("sliding", 0.1f);
            isSliding = true;
            isJumping = false;
        }
    }
    //This corroutine controls the time the player can be walking on a wall without going down
    public IEnumerator JumpOffWall(SIDE destinySide) {
        yield return new WaitForSeconds(m_maxTimeOnWall);
        if (m_isOnWall) {
            m_isOnWall = false;
            m_side = destinySide;
            StartCoroutine(WallWalkCooldown());
        }
    }
    //This corroutine controls the cooldown the player has to be able to get up to a wall again
    public IEnumerator WallWalkCooldown() {
        m_wallOnCooldown = true;
        yield return new WaitForSeconds(m_wallWalkCooldown);
        m_wallOnCooldown = false;
    }
    public void OnPlayerColliderHit(Collider col) {
        m_hitBoxX = GetHitBoxX(col);
        m_hitBoxY = GetHitBoxY(col);
        m_hitBoxZ = GetHitBoxZ(col);
        if (m_hitBoxZ == HitBoxZ.Forward && m_hitBoxX == HitBoxX.Middle) {
            if (m_hitBoxY == HitBoxY.LowMiddle) {
                //Stumble("stumbleLow");
                Stumble("stumble");
            } else if (m_hitBoxY == HitBoxY.Down) {
                //StartCoroutine(DeathPlayer("deathLow"));
                StartCoroutine(DeathPlayer("deathUpper"));
                ResetCollision();
            } else if (m_hitBoxY == HitBoxY.Middle) {
                if (col.CompareTag("DynamicObstacle")) {
                    //StartCoroutine(DeathPlayer("deathDynamicObstacle"));
                    StartCoroutine(DeathPlayer("deathBounce"));
                    ResetCollision();
                } else if (!col.CompareTag("Ramp")) {
                    StartCoroutine(DeathPlayer("deathBounce"));
                    ResetCollision();
                }
            } else if (m_hitBoxY == HitBoxY.Up && !isSliding) {
                StartCoroutine(DeathPlayer("deathUpper"));
                ResetCollision();
            }
        } else if (m_hitBoxZ == HitBoxZ.Middle && m_hitBoxY != HitBoxY.LowMiddle) {
            if (m_hitBoxX == HitBoxX.Right) {
                m_side = m_lastSide;
                Stumble("stumble");
                //Stumble("stumbleSideRight");
            } else if (m_hitBoxX == HitBoxX.Left) {
                m_side = m_lastSide;
                Stumble("stumble");
                //Stumble("stumbleSideLeft");
            }
        } else if (m_hitBoxY != HitBoxY.LowMiddle) {
            if (m_hitBoxX == HitBoxX.Right) {
                //m_anim.SetLayerWeight(1, 1);
                Stumble("stumble");
                //Stumble("stumbleRightCorner");
            } else if (m_hitBoxX == HitBoxX.Left) {
                //m_anim.SetLayerWeight(1, 1);
                Stumble("stumble");
                //Stumble("stumbleLeftCorner");
            }
        }
    }
    private void ResetCollision() {
        m_hitBoxX = HitBoxX.None;
        m_hitBoxY = HitBoxY.None;
        m_hitBoxZ = HitBoxZ.None;
    }
    public HitBoxX GetHitBoxX(Collider col) {
        Bounds player_bounds = m_player.bounds;
        Bounds col_bounds = col.bounds;
        float min_x = Mathf.Max(col_bounds.min.x, player_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, player_bounds.max.x);
        float average_x = (min_x + max_x) / 2f - col_bounds.min.x;
        HitBoxX hitX;
        if (average_x > col_bounds.size.x - 0.33f) {
            hitX = HitBoxX.Right;
        } else if (average_x < 0.33f) {
            hitX = HitBoxX.Left;
        } else {
            hitX = HitBoxX.Middle;
        }
        return hitX;
    }
    public HitBoxY GetHitBoxY(Collider col) {
        Bounds player_bounds = m_player.bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, player_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, player_bounds.max.y);
        float average_y = ((min_y + max_y) / 2f - player_bounds.min.y) / player_bounds.size.y;
        HitBoxY hitY;
        if (average_y < 0.17f) {
            hitY = HitBoxY.LowMiddle;
        } else if (average_y < 0.33f) {
            hitY = HitBoxY.Down;
        } else if (average_y < 0.66f) {
            hitY = HitBoxY.Middle;
        } else {
            hitY = HitBoxY.Up;
        }
        return hitY;
    }
    public HitBoxZ GetHitBoxZ(Collider col) {
        Bounds player_bounds = m_player.bounds;
        Bounds col_bounds = col.bounds;
        float min_z = Mathf.Max(col_bounds.min.z, player_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, player_bounds.max.z);
        float average_z = ((min_z + max_z) / 2f - player_bounds.min.z) / player_bounds.size.z;
        HitBoxZ hitZ;
        if (average_z < 0.33f) {
            hitZ = HitBoxZ.Backward;
        } else if (average_z < 0.66f) {
            hitZ = HitBoxZ.Middle;
        } else {
            hitZ = HitBoxZ.Forward;
        }
        return hitZ;
    }


    public bool GetMotoActive() { return m_motorbikeActive; }
    public void SetMotoActive(bool Activate) { m_motorbikeActive = Activate; }
    public MotorbikeObject GetMotorbike() { return m_motorbike; }
    public void SetMesh(Mesh mesh) { m_currentPlayerModel.mesh = mesh; }
    public Mesh GetPlayerMesh() { return m_playerModel; }
    public void SetInvulneravility(bool invulnerability) { m_invulnerability = invulnerability; }
    public bool GetInvulneravility() { return m_invulnerability; }
    public void SetIsOnHyperspeed(bool isOnHS) { m_isOnHyperspeed = isOnHS; }
    public void SetOnWall(bool isOnWall) { m_isOnWall = isOnWall; }
    public bool getIsOnWall() { return m_isOnWall; }
    public void setSide(SIDE side) { m_side = side; }
    public SIDE getSide() { return m_side; }
    public void SetIsInputEnabled(bool inputEnabled) { m_isInputEnabled = inputEnabled; }
    public void SetTutorialActive(TutorialBlock tutorial)
    { 
        m_tutorialActive = tutorial;
        m_isOnTutorial = true; 
    }
}

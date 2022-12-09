using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform bulletParent;
    public Camera cam;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    Vector3 cameraRelativeMovement;
    bool isMovementPressed;
    bool isRunPressed;
    bool isShootingPressed;
    bool isSelectPressed;
    public float rotationFactorPerFrame = 15.0f;
    public float speed = 5.0f;
    public float runMultiplier = 3.0f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    public float maxJumpHeight = 2;
    public float maxJumpTime = 0.5f;
    bool isJumping = false;
    bool requireNewJumpPress = false;
    float gravity = -9.8f;
    float groundedGravity = -0.5f;
    public float fireRate = .5f;
    float nextFire;

    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    public PlayerBaseState CurrentState { get { return _currentState;  } set { _currentState = value; } }
    public CharacterController CharacterController { get { return characterController; } }
    public Animator Animator { get { return animator; } }
    public GameObject BulletPrefab { get { return bulletPrefab; } }
    public Transform FirePointTransform { get { return firePoint; } }
    public Transform BulletParent { get { return bulletParent; } }
    public Transform CameraTransform { get { return cam.transform; } }
    public int IsJumpingHash { get { return isJumpingHash; } }
    public int IsWalkingHash { get { return isWalkingHash; } }
    public int IsRunningHash { get { return isRunningHash; } }
    public bool RequireNewJumpPress { get { return requireNewJumpPress; } set { requireNewJumpPress = value; } }
    public bool IsJumping { set { isJumping = value; } }
    public bool IsJumpPressed { get { return isJumpPressed; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public bool IsRunPressed { get { return isRunPressed; } }
    public bool IsShootingPressed { get { return isShootingPressed; } }
    public bool IsSelectPressed {get { return isSelectPressed; } }
    public Vector2 CurrentMovementInput { get { return currentMovementInput; } }
    public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; } }
    public float AppliedMovementY { get { return appliedMovement.y; } set { appliedMovement.y = value; } }
    public float AppliedMovementX { get { return appliedMovement.x; } set { appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return appliedMovement.z; } set { appliedMovement.z = value; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float Speed { get { return speed; } }
    public float InitialJumpVelocity { get { return initialJumpVelocity; } }
    public float GroundedGravity { get { return groundedGravity; } }
    public float Gravity { get { return gravity; } }
    public float NextFire { get { return nextFire; } set { nextFire = value; } }
    public float FireRate { get { return fireRate; } }

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        playerInput.PlayerController.Move.started += OnMovementInput;
        playerInput.PlayerController.Move.canceled += OnMovementInput;
        playerInput.PlayerController.Move.performed += OnMovementInput;
        playerInput.PlayerController.Run.started += OnRun;
        playerInput.PlayerController.Run.canceled += OnRun;
        playerInput.PlayerController.Jump.started += OnJump;
        playerInput.PlayerController.Jump.canceled += OnJump;
        playerInput.PlayerController.Fire.started += OnShoot;
        playerInput.PlayerController.Fire.canceled += OnShoot;
        playerInput.PlayerController.Select.started += OnSelect;
        playerInput.PlayerController.Select.canceled += OnSelect;

        SetupJumpVariables();
    }

    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        _currentState.UpdateStates();

        cameraRelativeMovement = ConvertToCameraSpace(appliedMovement);
        characterController.Move(cameraRelativeMovement * Time.deltaTime);
    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate) 
    {
        float currentYValue = vectorToRotate.y;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;

        return vectorRotatedToCameraSpace;
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = cameraRelativeMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed) {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        requireNewJumpPress = false;
    }
    void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }
    void OnMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * speed;
        currentMovement.z = currentMovementInput.y * speed;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    void OnShoot(InputAction.CallbackContext context)
    {
        isShootingPressed = context.ReadValueAsButton();
    }

    void OnSelect(InputAction.CallbackContext context)
    {
        isSelectPressed = context.ReadValueAsButton();
    }

    void OnEnable()
    {
        playerInput.PlayerController.Enable();
    }

    void OnDisable()
    {
        playerInput.PlayerController.Disable();
    }
}


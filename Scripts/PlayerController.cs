using System.Collections.Generic;
using MLAPI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    private float Speed { get; } = 2.5f; 
    private float RunModifer { get; } = 2.2f;
    private float CrouchModifer { get; } = 0.6f;
    private float JumpPower { get; } = 6f;
    private float Mass { get; } = 1.5f;
    private float _gravityForce = 0.0f;

    private CharacterController _controller;
    private Animator _animator;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private List<GameObject> rotatingBones;
    
    private void Start()
    {
        #if UNITY_EDITOR
        if (fpsCamera == null)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            throw new MissingReferenceException("Ссылка на одну из камер не определена.");
        }
        #endif
        
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PlayerLooking();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }
    

    public void PlayerLooking()
    {
        var horizontal = Input.GetAxisRaw("Mouse X") + transform.eulerAngles.y;
        var vertical = Input.GetAxisRaw("Mouse Y") + fpsCamera.transform.eulerAngles.x;
        fpsCamera.transform.localRotation = Quaternion.Euler(vertical, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, horizontal, 0.0f);
    }
    

    public void PlayerMovement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var isCrouching = Input.GetButton("Crouch");
        var isRunning = Input.GetButton("Run");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized;
        if (isRunning) movement *= RunModifer;
        if (isCrouching) movement *= CrouchModifer;
        movement = transform.TransformDirection(movement) * Speed;
        if (_controller.isGrounded)
        {
            if (Input.GetButton("Jump")) _gravityForce = JumpPower;
            else _gravityForce = 0;
        }
        else _gravityForce += Physics.gravity.y * Time.deltaTime * Mass;
        movement.y = _gravityForce;
        _controller.Move(movement * Time.deltaTime);

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, movement, Color.red);
        #endif
    }
    
}

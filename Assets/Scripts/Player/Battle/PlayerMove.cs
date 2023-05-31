using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 12f;
    private Vector3 velocity;

    [SerializeField] private float gravity = -9.81f;
    private float groundDistance = 0.2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    [SerializeField] private float jumpHeight = 3f;
    private float mass = 3.0f;
    private Vector3 impact = Vector3.zero;

    [SerializeField] private float tapWindow = 0.2f;
    private float lastTapTime = 0;
    [SerializeField] private float dashDistance = 5f;
    private Vector2 previousInput;
    private float dashCooldown = 1f;
    private float elapsedDashTimer = 0f;

    private Vector2 GetInput()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        return input;
    }

    public void AddImpact(Vector3 dir, float force){
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            PauseManager.instance.MenuCheck();
        if(PauseManager.instance.paused)
            return;
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 input = GetInput();

        elapsedDashTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.W))
        {
            if((Time.time - lastTapTime) < tapWindow)
            {
                Dash(transform.forward);
            }
            
            lastTapTime = Time.time;
            
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            if((Time.time - lastTapTime) < tapWindow)
            {
                Dash(-transform.forward);
            }
            
            lastTapTime = Time.time;
            
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if((Time.time - lastTapTime) < tapWindow)
            {
                Dash(transform.right);
            }
            
            lastTapTime = Time.time;
            
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if((Time.time - lastTapTime) < tapWindow)
            {
                Dash(-transform.right);
            }
            
            lastTapTime = Time.time;
            
        }
        Move(input);


        
    }

    public void Move(Vector2 input)
    {
        float facing = Camera.main.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, facing, transform.eulerAngles.z);
        Vector3 move = transform.right * input.x + transform.forward * input.y; 

        if (impact.magnitude > 0.2)
            controller.Move((move*0.5f + impact) * speed * Time.deltaTime);
        else
            controller.Move(move * speed * Time.deltaTime);
        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);

        

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Dash(Vector3 direction)
    {
        if(elapsedDashTimer <= 0f)
        {
            float facing = Camera.main.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, facing, transform.eulerAngles.z);
            controller.Move(direction * dashDistance);
            elapsedDashTimer = dashCooldown;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController charController;
    [SerializeField] private float movementSpeed = 5;
    float vertInput;
    float horizInput;
    Vector3 forwardMovement;
    Vector3 rightMovement;
    private Animator anim;
    private Rigidbody rb;

    //Jumping vars
    [SerializeField] private float checkRadius=0.1f;
    [SerializeField] private float jumpForce=10;
    Vector3 movement;
    [SerializeField] private float rayCastJumpDistance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
       // Movement();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis)*movementSpeed;

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
        forwardMovement = transform.forward * vAxis;
        rightMovement = transform.right * hAxis;
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("speed", (forwardMovement.sqrMagnitude + rightMovement.sqrMagnitude));
    }
    private void Movement()
    {
        vertInput = Input.GetAxis("Vertical"); 
        horizInput = Input.GetAxis("Horizontal");
        
        movement = new Vector3(horizInput, 0, vertInput);
        rb.AddForce(movement);
        forwardMovement = transform.forward * vertInput;
        rightMovement = transform.right * horizInput;
       // charController.SimpleMove(forwardMovement + rightMovement);
        

        // anim.SetFloat("horizontal", (forwardMovement.x+rightMovement.x));
        // anim.SetFloat("vertical", (forwardMovement.z + rightMovement.z));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("speed", (forwardMovement.sqrMagnitude + rightMovement.sqrMagnitude));

       

    }
    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down*rayCastJumpDistance, Color.blue);
      return  (Physics.Raycast(transform.position, Vector3.down, rayCastJumpDistance));
        
    }
     

}

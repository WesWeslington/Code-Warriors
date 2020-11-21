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
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

     
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        vertInput = Input.GetAxis("Vertical") * movementSpeed; 
        horizInput = Input.GetAxis("Horizontal")*movementSpeed;
        forwardMovement = transform.forward * vertInput;
        rightMovement = transform.right * horizInput;
        charController.SimpleMove(forwardMovement + rightMovement);


        // anim.SetFloat("horizontal", (forwardMovement.x+rightMovement.x));
        // anim.SetFloat("vertical", (forwardMovement.z + rightMovement.z));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("speed", (forwardMovement.sqrMagnitude + rightMovement.sqrMagnitude));

    }
}

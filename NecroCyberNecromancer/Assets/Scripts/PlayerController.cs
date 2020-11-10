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
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
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
        
     
    }
}

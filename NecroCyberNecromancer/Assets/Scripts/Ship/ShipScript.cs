using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private GameObject openShip;
    [SerializeField] private GameObject closedShip;
    [SerializeField] private GameObject character;//To disable character at the beginning of liftoff script
    [SerializeField] private GameObject wholeCharacter;//to move the character instead of just disabling its sprites
    [SerializeField] private Animator shipAnim;
    bool playerInShip = false;

    [SerializeField] private Transform playerShipLandPos;//For the player to spawn in the ship after it lands
    private Rigidbody playerRB;

    [SerializeField] private GameObject flyPanel;

    [SerializeField] private List<Transform> shipLandLocations;

    public int flyInt;

    [SerializeField] private GameObject wholeShip;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = wholeCharacter.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            closedShip.SetActive(false);
            openShip.SetActive(true);
        }

     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            closedShip.SetActive(true);
            openShip.SetActive(false);
        }

 
    }

    public void LiftOff()
    {
        shipAnim.enabled = true;
        playerInShip = true;
        wholeCharacter.GetComponent<CapsuleCollider>().enabled = false;
        openShip.SetActive(false);
        playerRB.isKinematic = true;
        shipAnim.SetTrigger("LiftOff");
        flyPanel.SetActive(false);
        character.SetActive(false);
    }

    public void ShipLanding()
    {
        shipAnim.ResetTrigger("LiftOff");

        if (playerInShip)
        {
           
                character.SetActive(true);

                shipAnim.enabled = false;
                wholeCharacter.GetComponent<CapsuleCollider>().enabled = true;
                wholeCharacter.transform.position = playerShipLandPos.position;
                playerRB.isKinematic = false;
                playerInShip = false;
                shipAnim.enabled = false;
            closedShip.SetActive(false);
            openShip.SetActive(true);
            
        }
        else
        {
            shipAnim.enabled = false;
        }
    }


    public void MoveShipToLocation()
    {
        wholeShip.transform.position = shipLandLocations[flyInt].position;
        wholeCharacter.transform.position= shipLandLocations[flyInt].position;
    }
}

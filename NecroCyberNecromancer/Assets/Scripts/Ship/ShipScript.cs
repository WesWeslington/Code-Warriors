using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private GameObject openShip;
    [SerializeField] private GameObject closedShip;


    [SerializeField] private Animator shipAnim;
    // Start is called before the first frame update
    void Start()
    {
        
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
    public void StopAnimator()
    {
        shipAnim.enabled = false;
    }
  
}

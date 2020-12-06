using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private GameObject flyPanelUI;


    // Start is called before the first frame update
    void Start()
    {
        arrowObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            arrowObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                flyPanelUI.SetActive(true);

            }

        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            arrowObject.SetActive(false);
            flyPanelUI.SetActive(false);

        }
    }
}

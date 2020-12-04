using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffStorageScript : MonoBehaviour
{
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private GameObject stuffPanelUI;//Storage Unit UI


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
                stuffPanelUI.SetActive(true);

            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            arrowObject.SetActive(false);
            stuffPanelUI.SetActive(false);

        }
    }
}

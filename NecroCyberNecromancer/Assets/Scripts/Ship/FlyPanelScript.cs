using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlyPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private GameObject flyPanelUI;
    private int flyInt = 0;
    [SerializeField] private Text selectedText;
    // Start is called before the first frame update
    void Start()
    {
        arrowObject.SetActive(false);
        selectedText.text = "Selected: [0]";
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

    public void OnFlyOneButton()
    {
        selectedText.text = "Selected: [1]";
        flyInt = 1;

    }
    public void OnFlyTwoButton()
    {
        selectedText.text = "Selected: [2]";
        flyInt = 1;

    }
    public void OnFlyThreeButton()
    {
        selectedText.text = "Selected: [3]";
        flyInt = 1;

    }
    public void OnFlyFourButton()
    {
        selectedText.text = "Selected: [4]";
        flyInt = 1;

    }
}

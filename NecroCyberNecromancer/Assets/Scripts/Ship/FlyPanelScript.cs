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

    [SerializeField] private Animator shipAnim;
    private ShipScript shipScript;
    // Start is called before the first frame update
    void Start()
    {
        arrowObject.SetActive(false);
        selectedText.text = "Selected: [0]";
        shipScript = FindObjectOfType<ShipScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            arrowObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                flyPanelUI.SetActive(true);

            }

        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            arrowObject.SetActive(false);
            flyPanelUI.SetActive(false);

        }
    }

    public void OnFlyOneButton()
    {
        selectedText.text = "Selected: [1]";
        shipScript.flyInt = 1;

    }
    public void OnFlyTwoButton()
    {
        selectedText.text = "Selected: [2]";
        shipScript.flyInt = 2;

    }
    public void OnFlyThreeButton()
    {
        selectedText.text = "Selected: [3]";
        shipScript.flyInt = 3;

    }
    public void OnFlyFourButton()
    {
        selectedText.text = "Selected: [4]";
        shipScript.flyInt = 4;

    }

    public void OnFlyButton()
    {
        arrowObject.SetActive(false);
        flyPanelUI.SetActive(false);

        shipAnim.SetTrigger("LiftOff");
        shipAnim.SetInteger("flyInt", flyInt);
    }

}

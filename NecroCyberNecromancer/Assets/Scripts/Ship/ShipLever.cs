using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLever : MonoBehaviour
{
    [SerializeField] private GameObject arrowObject;
    private SpriteRenderer leverSprite;
    bool shipIsLanded=false;
    [SerializeField] private Animator shipAnim;
    private void Awake()
    {
        leverSprite = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player"&&!shipIsLanded)
        {
            arrowObject.SetActive(true);
            if (Input.GetKey(KeyCode.E)&&!shipIsLanded)
            {
                shipIsLanded = true;
                leverSprite.flipX = true;
                shipAnim.SetTrigger("ShipIsLanding");
            }

        }
    }//next work on ramp extending

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            arrowObject.SetActive(false);

        }
    }
}

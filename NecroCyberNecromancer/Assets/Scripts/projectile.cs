using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic attack and spells are in this script

public class projectile : MonoBehaviour
{
    [SerializeField] private GameObject proj;
    [SerializeField] private GameObject deathRing;

    // [SerializeField] private GameObject crosshairs;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject crossTest;

    private Vector3 target;
    public Vector3 location;
    public Vector3 direction;
    private GameObject player;

    private float angle;

    private Vector3 shootAngle;
    [SerializeField]  private float projSpeed = 1f;

    private string spellType;

    // Start is called before the first frame update
    void Start()
    {
         spellType = "blast";
        //spellType = "deathRing";

        player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 difference = location - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        //   player.transform.rotation = Quaternion.Euler(0.0f, 0f, rotationZ);
        if (Input.GetMouseButtonDown(0))
        {
            getPlayerZ();
            getShootPosition();
            direction = new Vector3((location.x - transform.position.x), 0, (location.z - transform.position.z));
            // direction.Normalize();
            angle = Vector3.Angle(direction, transform.forward);
            fireBullet(direction, rotationZ);
        }
        spells();

        


    }

    void fireBullet(Vector3 direction, float rotationZ)
    {
        GameObject b = Instantiate(proj, transform.position, Quaternion.identity) as GameObject;
        b.transform.position = player.transform.position;
        b.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        b.transform.position = Vector3.MoveTowards(transform.position, crossTest.transform.position, 2);


        // b.GetComponent<Rigidbody>().velocity = direction * projSpeed;
        //  b.GetComponent<Rigidbody>().AddForce( = direction * projSpeed;

    }


    void getPlayerZ()
    {
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance = 0;
        if (hPlane.Raycast(ray, out rayDistance))
        {
            
            location = ray.GetPoint(rayDistance);
            Debug.Log(location);
            crossTest.transform.position = new Vector3(location.x, crossTest.transform.position.y, location.z);
        }

    }

    void getShootPosition()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, crossTest.transform.position);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == ("shoot"))
            {
                Debug.Log("it work");
                //location = ray.GetPoint(hit);
            }

        }


    }
    void spells()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //heals player
            if (spellType == "heal")
            {
                
            }
            //DoT
            if (spellType == "poison")
            {

            }
            //AoE
            if (spellType == "deathRing")
            {
                GameObject b = Instantiate(deathRing, new Vector3(transform.position.x, transform.position.y-.3f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
                
            }
            //powerful projectile
            if (spellType == "blast")
            {
                getPlayerZ();
                getShootPosition();
                GameObject b = Instantiate(proj, transform.position, Quaternion.identity) as GameObject;
                b.transform.position = player.transform.position;
                b.transform.localScale *= 4;
                direction = new Vector3((location.x - transform.position.x), 0, (location.z - transform.position.z));

              //  b.GetComponent<Rigidbody>().velocity = direction * projSpeed;
                
            }

        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private GameObject proj;
    // [SerializeField] private GameObject crosshairs;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject crossTest;

    private Vector3 target;
    private Vector3 location;
    private Vector3 direction;
    private GameObject player;

    private Vector3 shootAngle;
    private float projSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {

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
            fireBullet(direction, rotationZ);
        }
    }

    void fireBullet(Vector3 direction, float rotationZ)
    {
        GameObject b = Instantiate(proj, transform.position, Quaternion.identity) as GameObject;
        b.transform.position = player.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0f, rotationZ);

        b.GetComponent<Rigidbody>().velocity = direction * projSpeed;
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

}



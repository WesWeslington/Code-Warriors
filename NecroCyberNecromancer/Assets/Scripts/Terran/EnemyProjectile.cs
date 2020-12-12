using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    float projectileLife = 1;
    float projectileTimeAlive = 0;

    public Vector3 speed = Vector3.zero;

    public float projectileDamage = 1;

    [SerializeField]
    BoxCollider hitBox = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().PlayerTakeDamage((int)projectileDamage);
            hitBox.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, Space.Self);
        DoProjectileLife();
    }

    void DoProjectileLife()
    {
        if(projectileTimeAlive >= projectileLife)
        {
            Destroy(this.gameObject);
        }else
        {
            projectileTimeAlive += Time.deltaTime;
        }
    }

    public void FlipSprite()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
    }
}

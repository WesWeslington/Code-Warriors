using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projTime : MonoBehaviour
{
    float projLife = .6f;
    [SerializeField] private int damage;
    // Update is called once per frame
    void Update()
    {
        projLife -= Time.deltaTime;
        if (projLife < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
        }
    }
}

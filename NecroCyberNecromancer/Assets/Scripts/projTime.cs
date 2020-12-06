using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projTime : MonoBehaviour
{
    float projLife = .6f;

    // Update is called once per frame
    void Update()
    {
        projLife -= Time.deltaTime;
        if (projLife < 0)
        {
            Destroy(gameObject);
        }
    }
}

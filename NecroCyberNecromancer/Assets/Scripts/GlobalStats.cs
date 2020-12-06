using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    [SerializeField] private Color primaryColor;
    [SerializeField] private List<GameObject> skeletonSprites;
    // Start is called before the first frame update
    void Start()
    {
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UpdateColor()
    {
        foreach(GameObject bone in skeletonSprites)
        {
            bone.GetComponent<SpriteRenderer>().color = primaryColor;
        }
    }
}

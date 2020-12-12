using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    private PlayerStats pStats;

    [SerializeField] private Color primaryColor;
    [SerializeField] private List<GameObject> skeletonSprites;
    [SerializeField] private Light playerLight;
    // Start is called before the first frame update
    void Start()
    {
        UpdateColor();
        playerLight.color = primaryColor;
        pStats = FindObjectOfType<PlayerStats>();
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


    public void LoadingTheSave()
    {

    }

    public void Save()
    {

    }

}

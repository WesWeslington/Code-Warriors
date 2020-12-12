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
        primaryColor = ChooseColor();
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
            playerLight.color = primaryColor;
        }
    }


    public void LoadingTheSave()
    {

    }

    public void Save()
    {

    }
    Color ChooseColor()
    {
        int randomNumber = Random.Range(0, 6);

        if (randomNumber == 0)
        {
            return Color.red;
        }
        else if (randomNumber == 1)
        {
            return Color.blue;
        }
        else if (randomNumber == 2)
        {
            return Color.cyan;
        }
        else if (randomNumber == 3)
        {
            return Color.magenta;
        }
        else if (randomNumber == 4)
        {
            return Color.green;
        }
        else if (randomNumber == 5)
        {
            return Color.yellow;
        }
        else
        {
            return Color.white;
        }

    }
}

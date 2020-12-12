using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyColorScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> flyColorChangeSprites;
    private Color flyColor;
    // Start is called before the first frame update
    void Start()
    {
        flyColor = ChooseColor();
        ColorTheFly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Color ChooseColor()
    {
        int randomNumber = Random.Range(0, 6);

        if (randomNumber==0)
        {
                return Color.red;
        }else if (randomNumber == 1)
        {
            return Color.blue;
        }else if (randomNumber == 2)
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
    void ColorTheFly()
    {
        foreach(GameObject flyPart in flyColorChangeSprites)
        {
            flyPart.GetComponent<SpriteRenderer>().color = flyColor;
        }
    }
}

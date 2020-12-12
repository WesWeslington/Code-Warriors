using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDisplay", menuName = "Enemies/EnemyDisplay", order = 1)]
public class EnemySprites : ScriptableObject
{
    [Header("Enemy Image Assets")]
    [Header("Enemy Head Sprites")]
    [SerializeField]
    private Sprite[] eHead = new Sprite[3]; //All sprites contain 3 images for different perspectives, front, side, and back.
    [SerializeField]
    private Sprite[] eEyes = new Sprite[3];
    [SerializeField]
    private Sprite[] eMouth = new Sprite[3];
    [SerializeField]
    private Sprite[] eEars = new Sprite[3]; //Used for other things such as horns or the angler bulb too
    [Header("Enemy Body Sprites")]
    [SerializeField]
    private Sprite[] eTorso = new Sprite[3];
    [SerializeField]
    private Sprite[] eWaist = new Sprite[3];
    [Header("Enemy Limb Sprites")]
    [SerializeField]
    private Sprite[] eArm = new Sprite[3];
    [SerializeField]
    private Sprite[] eHand = new Sprite[3];
    [SerializeField]
    private bool mirrorArms = true;
    [SerializeField]
    private Sprite[] eLeg = new Sprite[3];
    [SerializeField]
    private Sprite[] eFoot = new Sprite[3];
    [SerializeField]
    private bool mirrorLegs = true;

    public void ReplaceSprite(Sprite sprite, BodyParts part, int dirIndex)
    {
        switch(part)
        {
            case BodyParts.Head:
                {
                    eHead[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Eyes:
                {
                    eEyes[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Mouth:
                {
                    eMouth[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Ears:
                {
                    eEars[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Torso:
                {
                    eTorso[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Waist:
                {
                    eWaist[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Arm:
                {
                    eArm[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Hand:
                {
                    eHand[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Leg:
                {
                    eLeg[dirIndex] = sprite;
                    break;
                }
            case BodyParts.Foot:
                {
                    eFoot[dirIndex] = sprite;
                    break;
                }
        }
    }

    public void RenderEnemy(Transform[] _eSkeleton, int _dirIndex)
    {
        for(int i = 0; i < _eSkeleton.Length; i++)
        {
            SpriteRenderer _spriteRenderer = _eSkeleton[i].GetComponent<SpriteRenderer>();

            if(_spriteRenderer != null)
            {
                string _boneName = _eSkeleton[i].gameObject.name.ToLower();
                if(_boneName.Contains("head"))
                {
                    _spriteRenderer.sprite = eHead[_dirIndex];
                }else if(_boneName.Contains("eyes"))
                {
                    _spriteRenderer.sprite = eEyes[_dirIndex];
                }
                else if (_boneName.Contains("mouth"))
                {
                    _spriteRenderer.sprite = eMouth[_dirIndex];
                }
                else if (_boneName.Contains("ears"))
                {
                    _spriteRenderer.sprite = eEars[_dirIndex];
                }
                else if (_boneName.Contains("torso"))
                {
                    _spriteRenderer.sprite = eTorso[_dirIndex];
                }
                else if (_boneName.Contains("waist"))
                {
                    _spriteRenderer.sprite = eWaist[_dirIndex];
                }
                else if (_boneName.Contains("arm"))
                {
                    _spriteRenderer.sprite = eArm[_dirIndex];
                }
                else if (_boneName.Contains("hand"))
                {
                    _spriteRenderer.sprite = eHand[_dirIndex];
                }
                else if (_boneName.Contains("leg"))
                {
                    _spriteRenderer.sprite = eLeg[_dirIndex];
                }
                else if (_boneName.Contains("foot"))
                {
                    _spriteRenderer.sprite = eFoot[_dirIndex];
                }
            }
        }
    }
}

public enum BodyParts
{
    Head,
    Eyes,
    Mouth,
    Ears,
    Torso,
    Waist,
    Arm,
    Hand,
    Leg,
    Foot
}

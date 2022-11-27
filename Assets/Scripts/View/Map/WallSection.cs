using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSection : MonoBehaviour
{
    public enum Type
    {
        Solid,
        Door,
    }

    [SerializeField]
    Sprite _solidSprite;
    [SerializeField]
    Sprite _doorSprite;
    [SerializeField]
    Type _type;
    [SerializeField]
    SpriteRenderer _renderer;

    public Type WallType => _type;

    public void SetType(Type type)
    {
        _type = type;
        UpdateSprite();
    }

    private void Awake()
    {
        UpdateSprite();
    }

    void UpdateSprite()
    {
        switch (_type)
        {
            case Type.Solid:
                _renderer.sprite = _solidSprite;
                break;
            case Type.Door:
                _renderer.sprite = _doorSprite;
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [SerializeField]
    KeyAsset harvestableKey;
    [SerializeField]
    SpriteRenderer harvestableSprite;
    [SerializeField]
    Sprite harvestableImage;
    [SerializeField]
    Sprite notHarvestableImage;
    [SerializeField]
    InteractableTarget interactable;

    private void Start()
    {
        var id = GetComponent<IIdentifiable>().Id;
        Game.Do(new RegisterHarvestable(id, harvestableKey.Key, transform.position, OnRegistered));
    }

    void OnRegistered(IHarvestableModel harvestable)
    {
        harvestable.IsHarvestable.ValueChanged += UpdateSprite;
    }

    void UpdateSprite(bool _, bool isHarvestable)
    {
        interactable.IsInteractable = isHarvestable;
        if (isHarvestable)
        {
            harvestableSprite.sprite = harvestableImage;
        } else
        {
            harvestableSprite.sprite = notHarvestableImage;
        }
    }
}

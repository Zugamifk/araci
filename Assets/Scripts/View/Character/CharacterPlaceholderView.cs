using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CharacterPlaceholderView : MonoBehaviour
{
    public enum CharacterType
    {
        NPC,
        Demon,
        Animal
    }

    [SerializeField]
    SpriteRenderer shape;
    [SerializeField]
    TMP_Text displayName;
    [SerializeField]
    CharacterType characterType;

    public void Start()
    {
        var character = GetComponent<Character>();
        var model = character.GetModel();

        displayName.text = model.DisplayName;

        var dataCollection = DataService.GetData<PlaceholderViewDataCollection>();
        var data = dataCollection.Get(characterType.ToString());

        shape.color = data.BaseColor;
    }
}

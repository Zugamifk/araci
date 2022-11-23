using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField]
    Image _healthBar;
    [SerializeField]
    Image _experienceBar;
    [SerializeField]
    Image[] _skillChargeLevels;

    private void Update()
    {
        var player = Game.Model.Player;
        var character = Game.Model.Characters.GetItem(player.Id);

        if (character != null && character.Health.MaxHealth > 0)
        {
            _healthBar.fillAmount = (float)character.Health.CurrentHealth / (float)character.Health.MaxHealth;
        }

        if (player.Level.RequiredExperience > 0)
        {
            _experienceBar.fillAmount = (float)(player.Level.CurrentExperience - player.Level.LastLevelRequiredExperience)/ (float)player.Level.RequiredExperience;
        }
    }
}

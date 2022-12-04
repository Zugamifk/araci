using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField]
    Image _alertBar;
    [SerializeField]
    Image _experienceBar;
    [SerializeField]
    Image[] _skillChargeLevels;
    [SerializeField]
    HealthMarker[] _healthMarkers;

    private void Update()
    {
        var player = Game.Model.Player;
        var character = Game.Model.Characters.GetItem(player.Id);

        if (character != null && character.Health.MaxHealth > 0)
        {
            for(int i=0;i < _healthMarkers.Length; i++)
            {
                _healthMarkers[i].UpdateState(i < character.Health.CurrentHealth);
            }
        }

        if (player.Level.RequiredExperience > 0)
        {
            _experienceBar.fillAmount = (float)(player.Level.CurrentExperience - player.Level.LastLevelRequiredExperience)/ (float)player.Level.RequiredExperience;
        }
    }
}

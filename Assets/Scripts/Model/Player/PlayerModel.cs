using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : IPlayerModel
{
    public Guid Id { get; } = Guid.NewGuid();
    public LevelModel Level { get; } = new();
    public Dictionary<string, SkillModel> OwnedSkills = new();
    ILevelModel IPlayerModel.Level => Level;
}

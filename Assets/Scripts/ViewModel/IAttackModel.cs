using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackModel : IIdentifiable, IKeyHolder
{
    Guid SourceId { get; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemModel : IIdentifiable, IKeyHolder
{
    int Count { get; }
}

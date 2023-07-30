using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel : IItemModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Key { get; set; }
    public int Count { get; set; }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryModel
{
    IBindingCollection<Guid> Slots { get; }
}

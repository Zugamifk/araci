using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : IInventoryModel
{
    public BindingCollection<Guid> Slots { get; } = new ();

    IBindingCollection<Guid> IInventoryModel.Slots => Slots;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineModel : IShrineModel
{
    public Guid Id { get; set; }
    public bool HasBlessingAvailable { get; set; }
}

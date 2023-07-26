using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelIdentifiable : Identifiable
{
    public void SetModelId(Guid id)
    {
        SetId(id);
    }
}

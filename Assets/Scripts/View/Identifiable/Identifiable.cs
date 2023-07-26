using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Identifiable : MonoBehaviour, IIdentifiable
{
    public Guid Id { get; protected set; }
    public event Action<Guid> IdChanged;

    protected void SetId(Guid id)
    {
        Id = id;
        IdChanged?.Invoke(id);
    }
}

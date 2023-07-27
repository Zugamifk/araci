using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Identifiable : MonoBehaviour, IIdentifiable
{
    public Guid Id { get; protected set; }

    Action<Guid> idChanged;
    public event Action<Guid> IdChanged
    {
        add
        {
            idChanged += value;
            if (Id != Guid.Empty)
            {
                value.Invoke(Id);
            }
        }
        remove {
            idChanged -= value;
        }
    }

    protected void SetId(Guid id)
    {
        Id = id;
        idChanged?.Invoke(id);
    }
}

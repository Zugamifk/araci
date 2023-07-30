using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIModel
{
    IObservable<string> CurrentOpenWindow { get; }
}

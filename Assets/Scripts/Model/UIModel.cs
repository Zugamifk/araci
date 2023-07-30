using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel : IUIModel
{
    public Observable<string> CurrentOpenWindow { get; } = new();
    IObservable<string> IUIModel.CurrentOpenWindow => CurrentOpenWindow;
}

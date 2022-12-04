using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKeyHolderCollection<TModel>
    where TModel : IKeyHolder
{
    TModel this[string key] { get; }
    bool IsEmpty { get; }
    bool HasId(string key);
    TModel GetItem(string key);
    IEnumerable<TModel> AllItems { get; }
}

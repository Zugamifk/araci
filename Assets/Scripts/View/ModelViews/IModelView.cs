using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModelView<TModel>
    where TModel : IIdentifiable
{
    TModel GetModel();
    void InitializeFromModel(TModel model);
}

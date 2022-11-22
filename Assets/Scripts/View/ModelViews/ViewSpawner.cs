using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewSpawner<TIdentifiable, TView> : MonoBehaviour
    where TIdentifiable : IIdentifiable
    where TView : MonoBehaviour, IModelView<TIdentifiable>
{
    [SerializeField]
    protected Transform _viewParent;

    protected Dictionary<Guid, TView> _spawnedViews = new Dictionary<Guid, TView>();

    public TView GetView(Guid id) => _spawnedViews[id];

    private void Awake()
    {
        if(_viewParent==null)
        {
            _viewParent = transform;
        }
    }

    void Update()
    {
        List<Guid> toRemove = new List<Guid>();
        foreach (var id in _spawnedViews.Keys)
        {
            if (GetModel(id) == null)
            {
                DestroyedView(_spawnedViews[id]);
                GameObject.Destroy(_spawnedViews[id].gameObject);
                toRemove.Add(id);
            }
        }

        foreach (var id in toRemove)
        {
            _spawnedViews.Remove(id);
        }

        foreach (var m in AllModels())
        {
            if (!_spawnedViews.ContainsKey(m.Id))
            {
                var instance = InstantiateView(m);
                if (_viewParent != null)
                {
                    instance.transform.SetParent(_viewParent);
                    instance.SetLayerRecursively(_viewParent.gameObject.layer);
                }
                var view = instance.GetComponent<TView>();
                if (view == null)
                {
                    throw new InvalidOperationException($"Prefab {instance} doesn't contain a {typeof(TView)}!");
                }

                var identifiable = view.GetComponent<Identifiable>();
                if (identifiable != null)
                {
                    identifiable.Id = m.Id;
                }

                SpawnedView(m, view);
                view.InitializeFromModel(m);
                _spawnedViews.Add(m.Id, view);
            }
        }
    }

    protected abstract GameObject InstantiateView(TIdentifiable model);
    protected abstract TIdentifiable GetModel(Guid id);
    protected abstract IEnumerable<TIdentifiable> AllModels();
    protected virtual void SpawnedView(TIdentifiable model, TView view) { }
    protected virtual void DestroyedView(TView view) { }
}

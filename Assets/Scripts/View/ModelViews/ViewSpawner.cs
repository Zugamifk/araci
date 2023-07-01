using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewSpawner<TIdentifiable, TView> : MonoBehaviour
    where TIdentifiable : IIdentifiable
    where TView : MonoBehaviour, IModelView<TIdentifiable>
{
    [SerializeField]
    protected Transform viewParent;

    protected abstract IIdentifiableLookup<TIdentifiable> collection { get; }
    protected Dictionary<Guid, TView> spawnedViews = new Dictionary<Guid, TView>();

    public TView GetView(Guid id) => spawnedViews[id];

    private void Awake()
    {
        if(viewParent==null)
        {
            viewParent = transform;
        }
    }

    private void Start()
    {
        collection.AddedItem -= OnAddedItem;
        collection.AddedItem += OnAddedItem;

        collection.RemovedItem -= OnRemovedItem;
        collection.RemovedItem += OnRemovedItem;

        foreach(var item in collection.AllItems)
        {
            OnAddedItem(item);
        }
    }

    private void OnDestroy()
    {
        foreach(var view in spawnedViews.Values)
        {
            DestroyView(view);
        }
        spawnedViews.Clear();
    }

    void OnAddedItem(TIdentifiable item)
    {
        if (spawnedViews.ContainsKey(item.Id))
        {
            return;
        }

        var instance = InstantiateView(item);
        if (viewParent != null)
        {
            instance.transform.SetParent(viewParent);
            instance.SetLayerRecursively(viewParent.gameObject.layer);
        }
        var view = instance.GetComponent<TView>();
        if (view == null)
        {
            throw new InvalidOperationException($"Prefab {instance} doesn't contain a {typeof(TView)}!");
        }

        var identifiable = view.GetComponent<Identifiable>();
        if (identifiable != null)
        {
            identifiable.Id = item.Id;
        }

        SpawnedView(item, view);
        view.InitializeFromModel(item);
        spawnedViews.Add(item.Id, view);
    }

    void OnRemovedItem(TIdentifiable item)
    {
        var view = spawnedViews[item.Id];
        DestroyView(view);
        spawnedViews.Remove(item.Id);
    }

    void DestroyView(TView view)
    {
        DestroyedView(view);
        GameObject.Destroy(view.gameObject);
    }

    protected abstract GameObject InstantiateView(TIdentifiable model);
    protected virtual void SpawnedView(TIdentifiable model, TView view) { }
    protected virtual void DestroyedView(TView view) { }
}

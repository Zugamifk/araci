using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField]
    DataReferences gameData;

    GameModel model = new GameModel();

    static Game game;
    static Queue<ICommand> commandQueue = new Queue<ICommand>();
    static Dictionary<Guid, IUpdater> idToUpdater = new();
    static HashSet<IUpdater> uniqueUpdaters = new();
    static Stack<Guid> toRemove = new();

    public static IGameModel Model => game.model;

    public static void Do(ICommand command)
    {
        commandQueue.Enqueue(command);
    }

    internal static void AddUpdater(IUpdater updater)
    {
        uniqueUpdaters.Add(updater);
    }

    internal static void AddUpdater(Guid id, IUpdater updater)
    {
        idToUpdater.Add(id, updater);
    }

    internal static void RemoveUpdater(Guid id)
    {
        toRemove.Push(id);
    }

    void Awake()
    {
        if(game!=null)
        {
            throw new InvalidOperationException($"Second Game instance detected!");
        }

        game = this;

        Services.InitializeServices();

        InitializeTimeModel();
        InitializeInput();
    }

    private void Update()
    {
        while (commandQueue.Count > 0)
        {
            var command = commandQueue.Dequeue();
            command.Execute(model);
        }

        while(toRemove.Count > 0)
        {
            var id = toRemove.Pop();
            idToUpdater.Remove(id);
        }

        foreach (var updater in uniqueUpdaters)
        {
            updater.Update(model);
        }

        foreach (var updater in idToUpdater.Values)
        {
            updater.Update(model);
        }
    }

    void InitializeTimeModel()
    {
        var timeModel = model.TimeModel;
        var time = DateTime.Now;
        timeModel.RealTime = TimeSpan.FromSeconds(time.TimeOfDay.TotalSeconds / TimeModel.TIME_MULTIPLIER);

        AddUpdater(new TimeModelUpdater());
    }

    void InitializeInput()
    {
        AddUpdater(new InputUpdater());
    }


    #region Editor
#if UNITY_EDITOR 
    public static GameModel EditorModel => game.model;
#endif
    #endregion
}

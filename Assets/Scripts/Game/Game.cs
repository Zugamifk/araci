using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField]
    DataReferences _gameData;

    GameModel _model = new GameModel();

    static Game _game;
    static Queue<ICommand> _commandQueue = new Queue<ICommand>();
    static Dictionary<Guid, IUpdater> _idToUpdater = new();
    static HashSet<IUpdater> _uniqueUpdaters = new();
    static Stack<Guid> _toRemove = new();

    public static IGameModel Model => _game._model;

    public static void Do(ICommand command)
    {
        _commandQueue.Enqueue(command);
    }

    internal static void AddUpdater(IUpdater updater)
    {
        _uniqueUpdaters.Add(updater);
    }

    internal static void AddUpdater(Guid id, IUpdater updater)
    {
        _idToUpdater.Add(id, updater);
    }

    internal static void RemoveUpdater(Guid id)
    {
        _toRemove.Push(id);
    }

    void Awake()
    {
        if(_game!=null)
        {
            throw new InvalidOperationException($"Second Game instance detected!");
        }

        _game = this;

        InitializeTimeModel();
        InitializeInput();
    }

    private void Update()
    {
        while (_commandQueue.Count > 0)
        {
            var command = _commandQueue.Dequeue();
            command.Execute(_model);
        }

        while(_toRemove.Count > 0)
        {
            var id = _toRemove.Pop();
            _idToUpdater.Remove(id);
        }

        foreach (var updater in _uniqueUpdaters)
        {
            updater.Update(_model);
        }

        foreach (var updater in _idToUpdater.Values)
        {
            updater.Update(_model);
        }
    }

    void InitializeTimeModel()
    {
        var timeModel = _model.TimeModel;
        var time = DateTime.Now;
        timeModel.RealTime = TimeSpan.FromSeconds(time.TimeOfDay.TotalSeconds / TimeModel.TIME_MULTIPLIER);

        AddUpdater(new TimeModelUpdater());
    }

    void InitializeInput()
    {
        AddUpdater(new InputUpdater());
    }
}

using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A Task object represents a coroutine.  Tasks can be started, paused, and stopped.<para />
/// It is an error to attempt to start a task that has been stopped or which has naturally terminated.
/// </summary>
public class Task
{
    private TaskManager.TaskState taskState;

    //Returns true if and only if the coroutine is running.  Paused tasks are considered to be running.    
    public bool Running => taskState.Running;

    //Returns true if and only if the coroutine is currently paused.
    public bool Paused => taskState.Paused;

    #region Actions
    public Action OnStarted = null;
    public Action OnStop = null;
    public Action OnPause = null;
    public Action OnUnPause = null;
    //Delegate for termination subscribers.  manual is true if and only if the coroutine was stopped with an explicit call to Stop().    
    private Action<bool> _OnFinished = null;
    public Action<bool> OnFinished
    {
        get => _OnFinished;
        set { _OnFinished = value; taskState.OnFinished = _OnFinished; }
    }
    #endregion

    //Creates a new Task object for the given coroutine.    
    //If autoStart is true (default) the task is automatically started upon construction.
    public Task(IEnumerator c, bool autoStart = true)
    {
        taskState = TaskManager.CreateTaskState(c);
        if (autoStart)
            Start();
    }

    //Begins execution of the coroutine
    public void Start()
    {
        taskState.Start();
        OnStarted?.Invoke();
    }

    //Discontinues execution of the coroutine at its next yield.
    public void Stop()
    {
        taskState.Stop();
        OnStop?.Invoke();
    }

    public void Pause()
    {
        taskState.Pause();
        OnPause?.Invoke();
    }

    public void Unpause()
    {
        taskState.Unpause();
        OnUnPause?.Invoke();
    }
}

/// <summary>
/// There is no need to initialize or even refer to TaskManager.<para />
/// When the first Task is created in an application, a "TaskManager" GameObject will automatically be added to the scene root with the TaskManager component attached.<para />
/// This component will be responsible for dispatching all coroutines behind the scenes.
/// </summary>
class TaskManager : MonoBehaviour
{
    private static TaskManager singleton;
    public static TaskState CreateTaskState(IEnumerator coroutine)
    {
        if (singleton == null)
        {
            GameObject go = new GameObject("TaskManager");
            singleton = go.AddComponent<TaskManager>();
        }
        return new TaskState(coroutine);
    }

    public class TaskState
    {
        private bool _running;
        public bool Running { get => _running; set => _running = value; }
        private bool _paused;
        public bool Paused { get => _paused; set => _paused = value; }
        private bool stopped;

        public Action<bool> OnFinished = null;

        IEnumerator coroutine;

        public TaskState(IEnumerator c)
        {
            coroutine = c;
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }

        public void Start()
        {
            _running = true;
            singleton.StartCoroutine(CallWrapper());
        }

        public void Stop()
        {
            stopped = true;
            _running = false;
        }

        private IEnumerator CallWrapper()
        {
            IEnumerator e = coroutine;
            while (_running)
            {
                if (_paused)
                    yield return null;
                else
                {
                    if (e != null && e.MoveNext())
                        yield return e.Current;
                    else
                        _running = false;
                }
            }

            OnFinished?.Invoke(stopped);
        }
    }
}
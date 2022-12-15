using ProjectY;
using ScriptableObjectEvents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhackAMoleManager : MonoBehaviour
{
    [Header("Targets")]
    [ContextMenuItem("Get all targets in scene", nameof(GetAllTargetsInScene))]
    [SerializeField] private List<Mover> _targetPool = new();
    private readonly List<Mover> _currentMoles = new();
    [SerializeField] private Vector2Int _batchRange = new(1, 6);

    [Header("Timers")]
    [SerializeField] private DynamicTimer _endGameTimer;
    [SerializeField] private VoidEvent _gameEnded;
    [SerializeField] private FloatVariable _currentTime;
    [SerializeField] private Timer _flipTargets;

    private void OnEnable()
    {
        _endGameTimer.TimeEvent += EndGame;
        _flipTargets.TimeEvent += FlipTargets;
    }

    private void OnDisable()
    {
        _endGameTimer.TimeEvent -= EndGame;
        _flipTargets.TimeEvent -= FlipTargets;
    }

    private void Update()
    {
        UpdateTimeLeft();
    }

    private void UpdateTimeLeft()
    {
        if (!_endGameTimer.CanTick)
            return;
        float startTime = _endGameTimer.Time;
        _currentTime.SetValue(MathHelper.Map(_endGameTimer.ElapsedTime, 0, startTime, startTime, 0));
    }

    public void EndGame()
    {
        DisableTimers(_flipTargets);
        DisableTimers(_endGameTimer);
        _currentTime.SetValue(0);
        _gameEnded.Raise();
        gameObject.SetActive(false);
    }

    private void DisableTimers(Timer timer)
    {
        timer.StopAndReset();
        timer.enabled = false;
    }

    private void GetAllTargetsInScene()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(this);
#endif
        _targetPool = FindObjectsOfType<Mover>().ToList();
    }

    [ContextMenu("Flip")]
    public void FlipTargets()
    {
        _flipTargets.StopAndReset();

        int amountToFlip = Random.Range(_batchRange.x, _batchRange.y);

        _endGameTimer.Multiplier = amountToFlip;

        for (int i = 0; i < amountToFlip; i++)
        {
            int index = Random.Range(0, _targetPool.Count);

            Mover iFlipper = _targetPool[index];
            RemoveFromPool(iFlipper);

            iFlipper.Move();
        }
    }

    private void RemoveFromPool(Mover iFlipper)
    {
        _targetPool.Remove(iFlipper);
        _currentMoles.Add(iFlipper);
    }

    private void AddBackToPool(Mover iFlipper)
    {
        _targetPool.Add(iFlipper);
        _currentMoles.Remove(iFlipper);

    }

    //Event Listener
    public void AddBackToPoolPublic(Mover flipper)
    {
        AddBackToPool(flipper);
        if (_currentMoles.Count == 0)
        {
            _flipTargets.Continue();

            // Can Add A special score here 
            // Like if the player shot all targets before the timer to flip back
        }
    }

    //Event listener 
    public void AddSecondToTimer(float timeToAdd)
    {
        _endGameTimer.ChangeTime(timeToAdd);
    }
}


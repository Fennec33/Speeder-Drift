using UnityEngine;
using PathCreation;

public class LapTracker : MonoBehaviour
{
    private PathCreator _trackPath;
    private int _id;
    private float _positionOnPath;
    private bool _startedLap = true;
    private bool _finishingLap = false;
    private bool _pausePositionUpdate = false;

    void Start()
    {
        _id = RaceManager.Instance.RequestNewID(this.gameObject);
        _trackPath = FindObjectOfType<PathCreator>();
        InvokeRepeating("UpdatePositionOnPath", _id/100f, 0.1f);
    }

    private void UpdatePositionOnPath()
    {
        if (_pausePositionUpdate)
        {
            _positionOnPath += 0.01f;
            return;
        }

        _positionOnPath = _trackPath.path.GetClosestDistanceAlongPath(transform.position);
        RaceManager.Instance.UpdateCurrentPosition(_id, _positionOnPath);
        checkLap();
    }

    private void checkLap()
    {
        if (_positionOnPath < 5f && _startedLap && _finishingLap)
        {
            RaceManager.Instance.FinishedLap(_id);
            _startedLap = false;
            _finishingLap = false;
        }
        
        if (_positionOnPath < 5f && (!_startedLap || !_finishingLap))
        {
            _startedLap = false;
            _finishingLap = false;
        }

        if (_positionOnPath > 5f && _positionOnPath < 10f)
        {
            _startedLap = true;
        }

        if (_positionOnPath > 170f && _startedLap)
        {
            _finishingLap = true;
        }
    }

    public void PausePositionUpdate() => _pausePositionUpdate = true;
    public void UnpausePositionUpdate() => _pausePositionUpdate = false;
}

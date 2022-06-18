using UnityEngine;

public class AtBridgeCollider : MonoBehaviour
{
    private LapTracker _lapTracker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _lapTracker = collision.GetComponent<LapTracker>();
        _lapTracker.PausePositionUpdate();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _lapTracker = collision.GetComponent<LapTracker>();
        _lapTracker.UnpausePositionUpdate();
    }
}

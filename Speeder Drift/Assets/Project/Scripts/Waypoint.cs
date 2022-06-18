using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Transform nextWaypoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Speeder")
        {
            SpeederAI _ai = collision.GetComponent<SpeederAI>();

            if (_ai != null)
                _ai.SetNewWaypoint(nextWaypoint);
        }
    }
}

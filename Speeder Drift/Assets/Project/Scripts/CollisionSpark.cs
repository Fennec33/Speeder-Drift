using UnityEngine;

public class CollisionSpark : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private int _count = 0;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _count++;
        if (_count >= 5) _renderer.enabled = false;
    }

    public void Spark(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        _count = 0;
        _renderer.enabled = true;
    }
}

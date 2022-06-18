public class SpeederPlacementData
{
    private int _id;
    private float _position;
    private int _lap;

    public SpeederPlacementData(int id)
    {
        _id = id;
        _position = 0f;
        _lap = 0;
    }

    public int GetID() => _id;
    public float GetPosition() => _position;
    public void SetPosition(float newPosition) => _position = newPosition;
    public int GetLap() => _lap;
    public void IncreaseLap() => _lap++;
}

using UnityEngine;

public class SpeederVFX : MonoBehaviour
{
    [SerializeField] private float maxTiltAngle = 30f;
    [SerializeField] private float tiltSpeed = 150f;
    [SerializeField] private float delayTimeBetweenCrashSparks = 1;

    [SerializeField] private CollisionSpark collisionSpark;

    private GameObject _flames;

    private float _currentTurnAngle = 0f;
    private float _timeSinceLastCrash = 0f;

    public void TurnFlamesOn() => _flames.SetActive(true);
    public void TurnFlamesOff() => _flames.SetActive(false);

    private void Awake()
    {
        _flames = transform.Find("Flames").gameObject;
    }

    private void Update()
    {
        _timeSinceLastCrash += Time.deltaTime;
    }

    public void TurnSpeeder(float direction)
    {
        _currentTurnAngle += -direction * tiltSpeed * Time.deltaTime;

        if (_currentTurnAngle > maxTiltAngle) _currentTurnAngle = maxTiltAngle;
        if (_currentTurnAngle < -maxTiltAngle) _currentTurnAngle = -maxTiltAngle;

        transform.localRotation = Quaternion.Euler(0f, _currentTurnAngle, 0f);
    }

    public void TurnReset()
    {
        if (_currentTurnAngle > 0)
        {
            _currentTurnAngle += -1f * tiltSpeed * Time.deltaTime;
        }

        if (_currentTurnAngle < 0)
        {
            _currentTurnAngle += 1f * tiltSpeed * Time.deltaTime;
        }

        transform.localRotation = Quaternion.Euler(0f, _currentTurnAngle, 0f);
    }

    public void CollisionSparkEffect(ContactPoint2D contact)
    {
        if (_timeSinceLastCrash >= delayTimeBetweenCrashSparks)
        {
            collisionSpark.Spark(contact.point, Quaternion.identity);
            _timeSinceLastCrash = 0f;
        }
    }
}

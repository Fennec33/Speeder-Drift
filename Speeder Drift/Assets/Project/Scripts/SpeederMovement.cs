using UnityEngine;

public class SpeederMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 80f;
    [SerializeField] private float breakForce =3f;
    [SerializeField] private float turnSpeed = 150f;

    private Rigidbody2D _rigidbody;
    private SpeederVFX _speederVFX;
    private SpeederAudio _speederAudio;

    private bool _isAccelerating = false;
    private bool _isTurning = false;
    private float _turnDirection;
    private float _normalDrag;

    public float GetForwardSpeed() { return forwardSpeed; }
    public float GetBreakForce() { return breakForce; }
    public float GetTurnSpeed() { return turnSpeed; }

    public void StartAccelerating() => _isAccelerating = true;
    public void StopAccelerating() => _isAccelerating = false;
    public void StartTurning(float direction) { _isTurning = true; _turnDirection = direction; }
    public void StopTurning() => _isTurning = false;
    public void StartBreaking() => _rigidbody.drag = breakForce;
    public void StopBreaking() => _rigidbody.drag = _normalDrag;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _speederVFX = GetComponentInChildren<SpeederVFX>();
        _speederAudio = GetComponentInChildren<SpeederAudio>();
        _normalDrag = _rigidbody.drag;
    }

    private void FixedUpdate()
    {
        if (_isAccelerating)
            Accelerate();
        else
            NoAccelerate();

        if (_isTurning)
            Turn();
        else
            NoTurn();
    }

    private void Accelerate()
    {
        _rigidbody.AddForce(forwardSpeed * transform.up);
        _speederVFX.TurnFlamesOn();
        _speederAudio.EngineSound(true);
    }

    private void NoAccelerate()
    {
        _speederVFX.TurnFlamesOff();
        _speederAudio.EngineSound(false);
    }

    private void Turn()
    {
        transform.Rotate(Time.deltaTime * turnSpeed * _turnDirection * Vector3.back);
        _speederVFX.TurnSpeeder(_turnDirection);
    }

    private void NoTurn()
    {
        _speederVFX.TurnReset();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _speederAudio.playCrashSound();

        ContactPoint2D contact = collision.GetContact(0);
        _speederVFX.CollisionSparkEffect(contact);
    }
}

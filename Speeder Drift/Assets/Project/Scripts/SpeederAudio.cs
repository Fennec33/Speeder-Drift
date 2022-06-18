using UnityEngine;

public class SpeederAudio : MonoBehaviour
{
    [SerializeField] private float delayTimeBetweenCrashSounds = 1;

    private AudioSource _engineSound;
    private AudioSource _crashSound;

    private bool _playing = false;
    private float _timeSinceLastCrash = 0f;

    private void Awake()
    {
        _engineSound = transform.Find("EngineSound").GetComponent<AudioSource>();
        _crashSound = transform.Find("CrashSound").GetComponent<AudioSource>();
    }

    public void EngineSound(bool on)
    {
        if (on && !_playing)
        {
            _engineSound.Play();
            _playing = true;
        }
        else if (!on && _playing)
        {
            _engineSound.Stop();
            _playing = false;
        }
    }

    public void playCrashSound()
    {
        if (_timeSinceLastCrash >= delayTimeBetweenCrashSounds)
        {
            _crashSound.Play();
            _timeSinceLastCrash = 0f;
        }
    }

    private void Update()
    {
        _timeSinceLastCrash += Time.deltaTime;
    }
}

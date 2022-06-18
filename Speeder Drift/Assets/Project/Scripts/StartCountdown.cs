using System.Collections;
using UnityEngine;

public class StartCountdown : MonoBehaviour
{
    [SerializeField] private SpeederMovement playerMovement;
    [SerializeField] private SpeederAI[] speederAIs;

    [SerializeField] private float waitTime = 1f;

    [SerializeField] private GameObject num1;
    [SerializeField] private GameObject num2;
    [SerializeField] private GameObject num3;

    [SerializeField] private AudioSource countSound;
    [SerializeField] private AudioSource startSound;

    private bool _raceStarted = false;

    private void Start()
    {
        playerMovement.enabled = false;

        for (int i = 0; i < speederAIs.Length; i++)
        {
            speederAIs[i].enabled = false;
        }
    }

    public void StartRace()
    {
        if (_raceStarted) return;

        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(waitTime);

        num1.SetActive(true);
        countSound.Play();

        yield return new WaitForSeconds(waitTime);

        num1.SetActive(false);
        num2.SetActive(true);
        countSound.Play();

        yield return new WaitForSeconds(waitTime);

        num2.SetActive(false);
        num3.SetActive(true);
        countSound.Play();

        yield return new WaitForSeconds(waitTime);

        num3.SetActive(false);
        startSound.Play();
        CountdownOver();
    }
    
    private void CountdownOver()
    {
        TurnSpeederControllersOn();
    }

    private void TurnSpeederControllersOn()
    {
        playerMovement.enabled = true;

        for (int i = 0; i < speederAIs.Length; i++)
        {
            speederAIs[i].enabled = true;
        }
    }
}

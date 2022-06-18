using UnityEngine;

public class HeightChangerTrigger : MonoBehaviour
{
    [SerializeField] bool isHighTrigger;

    private SpeederHeightChanger _speeder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Speeder")
        {
            _speeder = collision.GetComponent<SpeederHeightChanger>();

            if (isHighTrigger) _speeder.SetHeightHigh();
            else _speeder.SetHeightLow();
        }
    }
}

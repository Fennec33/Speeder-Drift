using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    [SerializeField] private int numberOfLaps = 3;
    [SerializeField] private RaceUI raceUI;
    [SerializeField] private TextMeshProUGUI rankingtext;

    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject raceOverScreen;
    [SerializeField] private GameObject startScreen;

    public GameObject player;
    public int numberOfSpeeders = 6;

    private SpeederPlacementData[] _speeders;
    private int _nextID = 0;
    private int _playerID;
    private bool _raceOver = false;
    private int _playerRanking = 0;
    private int _playerRankUpdateTime = 0;


    private static bool _hasBeenRestarted = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _speeders = new SpeederPlacementData[6];
    }

    private void Start()
    {
        if (_hasBeenRestarted)
        {
            startScreen.SetActive(false);
            gameUI.SetActive(true);
            GetComponent<StartCountdown>().StartRace();
        }
    }

    private void FixedUpdate()
    {
        if(_playerRankUpdateTime < 25)
        {
            _playerRankUpdateTime++;
            return;
        }

        _playerRanking = GetPlayerRanking();
        raceUI.DisplayCurrentRanking(_playerRanking);
        _playerRankUpdateTime = 0;
    }

    public int RequestNewID(GameObject speeder)
    {
        int value = _nextID;
        _nextID++;

        if (speeder == player)
        {
            _playerID = value;
        }

        SpeederPlacementData temp = new SpeederPlacementData(value);
        _speeders[value] = temp;
        return value;
    }

    public void UpdateCurrentPosition(int id, float newDistance)
    {
        _speeders[id].SetPosition(newDistance);
    }

    public void FinishedLap(int id)
    {
        _speeders[id].IncreaseLap();

        if (_speeders[_playerID].GetLap() > numberOfLaps && !_raceOver) EndRace();

        raceUI.DisplayCurrentLap(_speeders[_playerID].GetLap(), numberOfLaps);
    }

    private int GetPlayerRanking()
    {
        int ranking = 1;
        float playerPosition = _speeders[_playerID].GetPosition();
        int playerLap = _speeders[_playerID].GetLap();
        float position;
        int lap;

        for (int i = 0; i < _speeders.Length; i++)
        {
            if (i == _playerID) continue;

            position = _speeders[i].GetPosition();
            lap = _speeders[i].GetLap();

            if (position > playerPosition && lap == playerLap || lap > playerLap)
            {
                ranking++;
            }
        }
        
        return ranking;
    }

    private void EndRace()
    {
        _raceOver = true;
        gameUI.SetActive(false);
        raceOverScreen.SetActive(true);

        int rank = GetPlayerRanking();

        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<SpeederAI>().enabled = true;

        rankingtext.text = "You Got " + rank + raceUI.GetSuffixForNumber(rank) + " Place";
    }

    public void RestartScene()
    {
        _hasBeenRestarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

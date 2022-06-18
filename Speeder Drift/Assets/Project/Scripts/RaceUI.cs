using UnityEngine;
using TMPro;

public class RaceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankingNumber;
    [SerializeField] private TextMeshProUGUI rankingSuffix;
    [SerializeField] private TextMeshProUGUI lapCounter;

    [SerializeField] private Color firstPlaceColor;
    [SerializeField] private Color secondPlaceColor;
    [SerializeField] private Color thirdPlaceColor;

    private int _playerRanking = 0;
    private string _playerRankingSuffix = "";

    public void DisplayCurrentRanking(int ranking)
    {
        if (ranking == _playerRanking) return;

        _playerRanking = ranking;

        _playerRankingSuffix = GetSuffixForNumber(_playerRanking);
        SetRankingNumberColor(_playerRanking);

        rankingNumber.text = _playerRanking.ToString();
        rankingSuffix.text = _playerRankingSuffix;
    }

    public string GetSuffixForNumber(int number)
    {
        string suffix = "";
        int num = number % 10;

        if (num == 1)
            suffix = "st";

        else if (num == 2)
            suffix = "nd";

        else if (num == 3)
            suffix = "rd";

        else
            suffix = "th";

        return suffix;
    }

    private void SetRankingNumberColor(int rank)
    {
        if (rank == 1)
            rankingNumber.color = firstPlaceColor;

        else if (rank == 2)
            rankingNumber.color = secondPlaceColor;

        else if (rank == 3)
            rankingNumber.color = thirdPlaceColor;

        else
            rankingNumber.color = Color.white;
    }

    public void DisplayCurrentLap(int currentlap, int totalLaps)
    {
        if (currentlap < 1) currentlap = 1;
        lapCounter.text = "Lap " + currentlap + "/" + totalLaps;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int[] playerScores;
    [SerializeField]
    private float gameTime;
    [SerializeField]
    private TextMeshProUGUI[] playerScoresText;
    [SerializeField]
    private TextMeshProUGUI timeText, playerWinText;
    [SerializeField]
    private GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        playerScores = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        //update timer text
        timeText.text = gameTime.ToString("00.0");

        if(gameTime <= 0)
        {
            //game over
            Time.timeScale = 0;
            int winner = playerScores[0] > playerScores[1] ? 1 : 2;
            gameOverPanel.SetActive(true);
            playerWinText.text = "Player " + winner + " wins!";
        }
    }

    void UpdateUI()
    {
        //update ui text
        playerScoresText[0].text = playerScores[0].ToString();
        playerScoresText[1].text = playerScores[1].ToString();
    }

    public void AddPoints(int player, int amount)
    {
        playerScores[player] += amount;
        UpdateUI();
    }
}

using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;

    public static void AddScore(int amount)
    {
        score += amount;
        SaveScore();
    }

    public static void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        UpdateRecordScores(); // Добавлен вызов функции обновления рекордов в таблице
    }

    public static void LoadScore()
    {
        score = PlayerPrefs.GetInt("Score", 0);
    }

    public static void ResetScore()
    {
        score = 0;
        SaveScore();
    }

    private static void UpdateRecordScores()
    {
        for (int i = 0; i < 10; i++)
        {
            int record = PlayerPrefs.GetInt("Record" + (i + 1), 0);
            if (score > record)
            {
                PlayerPrefs.SetInt("Record" + (i + 1), score);
                break;
            }
        }
    }
}
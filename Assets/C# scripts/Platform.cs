using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text bestRecordText;
    public Text[] recordTexts; // Добавлен массив для хранения ссылок на текстовые поля рекордов
    public Button resetButton; // Добавьте это поле в начало вашего класса
    private List<int> uniqueRecords = new List<int>();
    private void Start()
    {
        resetButton.onClick.AddListener(ResetRecords);
        ScoreManager.LoadScore();
        UpdateScoreText();
        UpdateBestRecordText();
        UpdateRecordTexts(); // Добавлен вызов функции обновления текстовых полей рекордов
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y < 0)
        {
            ScoreManager.AddScore(1);
            score++; // Увеличиваем счет здесь
            UpdateScoreText();
            UpdateBestRecordText(); // Обновляем лучший рекорд здесь
            ScoreManager.SaveScore();
            UpdateRecordTexts(); // Добавлен вызов функции обновления текстовых полей рекордов
            Doodle.instance.DoodleRigid.velocity = Vector2.up * 8;
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Просто обновляем текстовое поле
        }
    }

    private void UpdateBestRecordText()
    {
        int bestRecord = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestRecord) // Если текущий счет больше лучшего рекорда, обновляем лучший рекорд
        {
            PlayerPrefs.SetInt("HighScore", score);
            bestRecord = score;
        }
        if (bestRecordText != null)
        {
            bestRecordText.text = "" + bestRecord.ToString();
        }
    }

    private void UpdateRecordTexts()
    {
        uniqueRecords.Clear();

        for (int i = 0; i < recordTexts.Length; i++)
        {
            int record = PlayerPrefs.GetInt("Record" + (i + 1), 0);
            if (!uniqueRecords.Contains(record))
            {
                uniqueRecords.Add(record);
            }
        }

        for (int i = 0; i < recordTexts.Length; i++)
        {
            if (i < uniqueRecords.Count)
            {
                recordTexts[i].text = "Record " + (i + 1) + ": " + uniqueRecords[i].ToString();
            }
            else
            {
                recordTexts[i].text = ""; // Очищаем текстовое поле, если нет уникального рекорда для отображения
            }
        }
    }

    // Функция для сброса всех рекордов
    public void ResetRecords()
    {
        for (int i = 0; i < recordTexts.Length; i++)
        {
            PlayerPrefs.DeleteKey("Record" + (i + 1));
        }
        // Также сбрасываем лучший рекорд
        PlayerPrefs.DeleteKey("HighScore");
        UpdateRecordTexts(); // Обновляем отображение рекордов после сброса
    }
}
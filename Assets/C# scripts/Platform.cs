using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text bestRecordText;
    public Text[] recordTexts;
    public Button resetButton;
    private List<int> uniqueRecords = new List<int>();
    public PlatformGenerator platformGenerator;
    private AudioSource audioSource;
    public AudioClip jumpSound;

    private void Start()
    {
        resetButton.onClick.AddListener(ResetRecords);
        ScoreManager.LoadScore();
        UpdateScoreText();
        UpdateBestRecordText();
        UpdateRecordTexts();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y < 0)
        {
            ScoreManager.AddScore(1);
            score++;
            UpdateScoreText();
            UpdateBestRecordText();
            ScoreManager.SaveScore();
            UpdateRecordTexts();
            float angle = transform.rotation.eulerAngles.z;
            Vector2 jumpDirection = Quaternion.Euler(0, 0, angle) * Vector2.up;
            Doodle.instance.DoodleRigid.AddForce(jumpDirection * 525);
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
            platformGenerator.MovePlatform();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void UpdateBestRecordText()
    {
        int bestRecord = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestRecord)
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
                recordTexts[i].text = "";
            }
        }
    }

    public void ResetRecords()
    {
        for (int i = 0; i < recordTexts.Length; i++)
        {
            PlayerPrefs.DeleteKey("Record" + (i + 1));
        }
        PlayerPrefs.DeleteKey("HighScore");
        UpdateRecordTexts();
    }
}

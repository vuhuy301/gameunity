using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScore : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayScore Instance;

    private int score = 0;
    public Text scoreText; // Tham chiếu đến Text để hiển thị điểm số

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText(); // Cập nhật Text khi game bắt đầu
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText(); // Cập nhật Text sau khi thêm điểm

        // Các thao tác khác liên quan đến điểm số ở đây
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Hiển thị điểm số trên Text
        }
    }

    public int GetScore()
    {
        return score;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageTime = 1f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject gameOverScreenText;
    [SerializeField] private GameObject gameOverScreenImage;

    private int curentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;
    private Text score;
    private Image image;
    private bool gameOver = false;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        if (gameOverScreenText != null && gameOverScreenImage != null)
        {
            score = gameOverScreenText.GetComponentInChildren<Text>();
            image = gameOverScreenImage.GetComponentInChildren<Image>();
            image.enabled = false;
            score.enabled = false;

        }
    }

    private void Start()
    {
        curentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void Update()
    {
        // Kiểm tra nếu trò chơi kết thúc và người chơi nhấp chuột
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            RestartGame();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAi enemy = other.gameObject.GetComponent<EnemyAi>(); 
        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
        else
        {
            Debug.Log("Collision detected but no knockback applied.");
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if(!canTakeDamage) { return; }

        knockback.GetKnockBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        curentHealth -= damageAmount;
        UpdateHealthSlider();
        StartCoroutine(DamageRecoveryRoutine());

        if(curentHealth == 0)
        {
            GameOver();
        }

    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)curentHealth;
        }
    }

    public void IncreaseHealth(int health)
    {
        if(curentHealth < maxHealth)
        {
            curentHealth += health;
        }
        UpdateHealthSlider();
    }
    private void GameOver()
    {
        // Hiển thị màn hình kết thúc trò chơi
        score.enabled = true;
        image.enabled = true;
        score.text = "Score: " + PlayScore.Instance.GetScore();
        gameOver = true;
        // Dừng trò chơi
        Time.timeScale = 0f;
    }
    private void RestartGame()
    {
        // Khởi động lại trò chơi
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

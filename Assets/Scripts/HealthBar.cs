using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite[] healthSprites; // 체력 스프라이트 배열
    public Image healthBarImage;   // 체력 바 UI
    public player1Controller player;          // 캐릭터 참조

    void Start()
    {
        LoadHealthSprites();
    }

    void Update()
    {
        UpdateHealthBar(player.curHealth);
    }

    void LoadHealthSprites()
    {
        healthSprites = new Sprite[56];
        for (int i = 0; i < healthSprites.Length; i++)
        {
            healthSprites[i] = Resources.Load<Sprite>($"HealthSprites/{i+1}");
        }
    }

    void UpdateHealthBar(int currentHealth)
    {
        if (currentHealth >= 0 && currentHealth < healthSprites.Length)
        {
            healthBarImage.sprite = healthSprites[currentHealth];
        }
        else
        {
            Debug.LogError("Invalid currentHealth index: " + currentHealth);
        }
    }
}
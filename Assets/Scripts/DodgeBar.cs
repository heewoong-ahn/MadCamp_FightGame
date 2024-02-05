using UnityEngine;
using UnityEngine.UI;

public class DodgeBar : MonoBehaviour
{
    public Sprite[] dodgeSprites; // 체력 스프라이트 배열
    public Image dodgeBarImage;   // 체력 바 UI
    public player1Controller player;          // 캐릭터 참조
    private float dodgeCooldown = 1.0f; // 쿨타임

    void Start()
    {
        LoadDodgeSprites();
    }

    void Update()
    {
        UpdateDodgeBar();
    }

    void LoadDodgeSprites()
    {
        dodgeSprites = new Sprite[56];
        for (int i = 0; i < dodgeSprites.Length; i++)
        {
            dodgeSprites[i] = Resources.Load<Sprite>($"DodgeSprites/{i+1}");
        }
    }

    void UpdateDodgeBar()
    {
        float timeSinceLastDodge = Time.time - player.lastDodge;
        int spriteIndex = Mathf.FloorToInt((timeSinceLastDodge / dodgeCooldown) * dodgeSprites.Length);

        // 쿨다운이 끝난 경우, 가장 마지막 스프라이트를 표시
        if (spriteIndex >= dodgeSprites.Length)
        {
            spriteIndex = dodgeSprites.Length - 1;
        }

        if (spriteIndex >= 0 && spriteIndex < dodgeSprites.Length)
        {
            dodgeBarImage.sprite = dodgeSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Invalid sprite index: " + spriteIndex);
        }
    }
}
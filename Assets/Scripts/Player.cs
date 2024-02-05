using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int maxHealth = 55; // 최대 체력 설정
    public int currentHealth; // 현재 체력

    // 체력 변경 이벤트
    public event Action<int> OnHealthChanged;

    void Start()
    {
        // 게임 시작 시 최대 체력으로 초기화
        currentHealth = maxHealth;
        // 체력 변경 이벤트 발생
        OnHealthChanged?.Invoke(currentHealth);
    }

    // 체력을 변경하는 메소드
    public void ChangeHealth(int amount)
    {
        // 체력 변경
        currentHealth += amount;
        // 체력이 0 이하로 떨어지지 않도록 함
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // 체력 변경 이벤트 발생
        OnHealthChanged?.Invoke(currentHealth);

        // 체력이 0이 되면 플레이어 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 플레이어 사망 처리 로직
        // 예를 들어, 게임 오버 화면 표시, 캐릭터 애니메이션 재생 등
        Debug.Log("Player Died");
    }

    // 체력 테스트를 위한 임시 메소드 (실제 게임에서는 제거)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 스페이스 바를 누를 때마다 체력을 1 감소
            ChangeHealth(-1);
        }
    }
}
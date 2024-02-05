using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public float movementSpeed = 2.0f; // 이동 속도

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo currentAnimationState = animator.GetCurrentAnimatorStateInfo(0);
        // 애니메이션 상태에 따른 캐릭터 이동
        if (currentAnimationState.IsName("MoveForward"))
        {
            // "MoveForward" 애니메이션 상태인 경우
            MoveCharacter(Vector3.forward);
        }
        else if (currentAnimationState.IsName("MoveBackward"))
        {
            MoveCharacter(-Vector3.forward);
        }

        // 키 입력에 따른 애니메이션 상태 설정
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("IsMovingForward", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("IsMovingForward", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("IsMovingBackward", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("IsMovingBackward", false);
        }
    }

    void MoveCharacter(Vector3 direction)
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}
using System.Collections;
using UnityEngine;

public class sandbag : MonoBehaviour
{
    private bool isBeingHit = false; // 흔들림 상태를 나타내는 플래그
    public float shakeDuration = 0.5f; // 흔들림 지속 시간
    public float shakeIntensity = 0.1f; // 흔들림 강도

    public sounds soundControllerScript;
    public ParticleSystem blood;

    private Vector3 initialPosition = new Vector3(-0.8517556f, 1.49f, 6.88f); // 초기 위치
    private float timer = 0.0f;

    private void Start()
    {
        blood.Stop();
    }

    private void Update()
    {
        timer += Time.deltaTime; // 타이머 업데이트
        // 타이머가 checkInterval의 배수인지 확인하고 이동 처리
        if (timer>10.0f)
        {
            MoveToInitialPosition(); // 초기 위치로 이동
            timer = 0.0f;
        }
    }

    private void MoveToInitialPosition()
    {
        Debug.Log("back");
        transform.position = initialPosition; // 초기 위치로 이동
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee" || other.tag =="R_Melee")
        {

            shakeDuration = 0.25f;
            shakeIntensity = 0.1f;
            soundControllerScript.playPunched();
            StopCoroutine(Shake());
            StartCoroutine(Shake());


        }

        if (other.tag == "Foot" || other.tag == "L_Foot" || other.tag == "Knee")
        {
            shakeIntensity = 0.2f;
            shakeDuration = 0.4f;
            soundControllerScript.playKicked();
            StopCoroutine(Shake());
            StartCoroutine(Shake());


        }

        if (other.tag == "BigMelee")
        {

            shakeIntensity = 0.3f;
            shakeDuration = 0.6f;
            soundControllerScript.playUppered();
            StopCoroutine(Shake());
            StartCoroutine(Shake());


        }

    }

    // 흔들리는 코루틴
    private IEnumerator Shake()
    {
        isBeingHit = true;

        Vector3 originalPosition = transform.position;

        float elapsedTime = 0f;
        blood.Play();

        while (elapsedTime < shakeDuration)
        {
            // 랜덤한 흔들림 위치 생성
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;

            // 현재 위치에 흔들림 위치를 더하여 적용
            transform.position = originalPosition + shakeOffset;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        blood.Stop();

        // 흔들림이 끝나면 원래 위치로 복귀
        transform.position = originalPosition;

        isBeingHit = false;
    }
}
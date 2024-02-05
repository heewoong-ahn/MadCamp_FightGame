using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class player1Controller : MonoBehaviour
{
    float timer = 4f;

    public int maxHealth;
    public int curHealth;
    public float lastDodge;

    public Animator anim;
    Rigidbody rigid;
    BoxCollider boxCollider;
    enemyPlayer player;
    public TimeClock clock;

    float hAxis;
    Vector3 moveVec;
    public float speed = 5;

    float basicPunchTime = 0.7778f;
    float lastBasicPunchTime;
    float rightPunchTime = 0.9f;
    float lastRightPunchTime;
    private float rightPunchStartTime; // 두 번째 펀치의 시작 시간을 추적
    private float upperCutTime = 1.33f; // uppercut 애니메이션의 지속 시간

    float rightKickTime = 1.112f;
    float lastRightKickTime;

    float leftKickTime = 0.96f;
    float lastLeftKickTime;

    float guardTime = 0.3f;
    float lastGuardTime; 

    float kickedTime = 1.112f;
    float lastKickedTime;

    float basicPunchedTime = 0.53333f;
    float lastBasicPunchedTime;
    private float lastPressTime = 0f;
    private float lastPressTimeBack = 0f;
    private int pressCount = 0; // 키 눌림 횟수
    private int pressCountBack = 0;
    private float doublePressInterval = 0.2f; // 회피기 작동 시간

    /*    float walkTime = 0.9f;
        float lastWalkTime;*/

    public leftPunch leftPunchScript;
    public rightKick rightKickScript;
    public leftKick leftKickScript;
    public rightPunch rightPunchScript;
    public upperCut upperCutScript;
    public underKick underKickScript;
    private bool isRightPunchActive = true;
    private bool isUpperCutActive = true;
    private bool isRightKickActive = true;
    private float leftPunchStartTime;
    private float leftKickStartTime;
    private float rightKickStartTime;
    public camera cameraScript;
    public Camera mainCam;
    public Camera victoryCam;
    public EndMenuController endmenu;
    public GameObject middleBar;

    public sounds soundControllerScript;
    public MusicController music;

    public ParticleSystem particle;
    public Light light;

    public ParticleSystem guard;
    public ParticleSystem afterI;
    public TrailRenderer dodgeLeft;
    public TrailRenderer dodgeRight;

    bool gFlag;
    bool hitByUpperCut;

    public Light winLight; 
    public bool isEnd;

    // Start is called before the first frame update
    void Start()
    {
        isEnd = false;
        gFlag = false; 
        hitByUpperCut = false;
        music = FindObjectOfType<MusicController>();
        clock = FindObjectOfType<TimeClock>();
        endmenu = FindObjectOfType<EndMenuController>();
        victoryCam.gameObject.SetActive(false);
        player = GameObject.FindObjectOfType<enemyPlayer>();
        maxHealth = 55;
        particle.Stop();
        light.enabled = false;
        guard.Stop();
        afterI.Stop();
        dodgeLeft.enabled = false;
        dodgeRight.enabled = false;
        winLight.enabled = false;

        // 게임 시작 시 최대 체력으로 초기화
        curHealth = maxHealth;

        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();

        lastBasicPunchTime = -basicPunchTime;
        lastRightPunchTime = -rightPunchTime;
        rightPunchStartTime = -rightPunchStartTime;
        lastRightKickTime = -rightKickTime;
        lastLeftKickTime = -leftKickTime;
        lastGuardTime = -guardTime;
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(IsIdlePlaying())
        {
            hitByUpperCut = false;
            anim.SetBool("hitByUpperCut", false);
        }
        if(IsLying())
        {
            hitByUpperCut = true;
            anim.SetBool("hitByUpperCut", true);
        }

        if (Input.GetKey(KeyCode.D) && !hitByUpperCut && !isEnd)
        {
            MoveForward();
        }
        else if(Input.GetKey(KeyCode.A) && !hitByUpperCut && !isEnd)
        {
            MoveBackward();       
        }
        else
        {
            anim.SetBool("stepForward", false);
            anim.SetBool("stepBackward", false);
        }

        if (Input.GetKeyDown(KeyCode.T) && !hitByUpperCut && !isEnd)
        {
            if (Time.time - lastBasicPunchTime >= basicPunchTime && CanStartPunch())
            {
                leftPunchScript.Use();
                anim.SetTrigger("basicPunch");
                lastBasicPunchTime = Time.time; 
                leftPunchStartTime = Time.time;
                anim.SetBool("rightPunch", false);
                isRightPunchActive = false;
            }
            else if (!isRightPunchActive && Time.time - leftPunchStartTime > basicPunchTime - 0.3f && Time.time - leftPunchStartTime < basicPunchTime)
            {
                anim.SetBool("rightPunch", true);
                rightPunchScript.Use();
                rightPunchStartTime = Time.time;
                isRightPunchActive = true;
                isUpperCutActive = false;
                StartCoroutine(SetRightPunchFalseAfterAnimation()); // 코루틴 호출
            }
            else if (!isUpperCutActive && Time.time - rightPunchStartTime > rightPunchTime - 0.3f && Time.time - rightPunchStartTime < rightPunchTime)
            {
                anim.SetBool("upperCut", true);
                upperCutScript.Use();
                isUpperCutActive = true;
                StartCoroutine(SetUpperCutFalseAfterAnimation());
            }
        }

        IEnumerator SetRightPunchFalseAfterAnimation()
        {
            yield return new WaitForSeconds(rightPunchTime); // 후속 펀치 애니메이션의 지속 시간
            anim.SetBool("rightPunch", false);
        }

        IEnumerator SetUpperCutFalseAfterAnimation()
        {
            yield return new WaitForSeconds(upperCutTime);
            anim.SetBool("upperCut", false);
        }


        if (Input.GetKeyDown(KeyCode.Y) && !hitByUpperCut && !isEnd)
        {
            if (Time.time - lastLeftKickTime >= leftKickTime && CanStartPunch())
            {
                leftKickScript.Use();
                anim.SetTrigger("leftKick");
                lastLeftKickTime = Time.time; 
                leftKickStartTime = Time.time;
                anim.SetBool("rightKick", false);
                isRightKickActive = false;
            }
            else if (!isRightKickActive && Time.time - leftKickStartTime > leftKickTime - 0.3f && Time.time - leftKickStartTime < leftKickTime)
            {
                anim.SetBool("rightKick", true);
                rightKickScript.Use();
                rightKickStartTime = Time.time;
                isRightKickActive = true;
                StartCoroutine(SetRightKickFalseAfterAnimation()); // 코루틴 호출
            }
        }

        IEnumerator SetRightKickFalseAfterAnimation()
        {
            yield return new WaitForSeconds(rightKickTime); // 후속 펀치 애니메이션의 지속 시간
            anim.SetBool("rightKick", false);
        }

        if (Input.GetKeyDown(KeyCode.U) && !isEnd && !hitByUpperCut && !IsGuardAnimationPlaying() && !IsBasicPunchAnimationPlaying() && !IsLeftKickAnimationPlaying() && !IsRightKickAnimationPlaying() && !IsUnderKickAnimationPlaying() && !IsBasicPunchedAnimationPlaying() && !IsKickedAnimationPlaying() && !IsUnderKickedAnimationPlaying())
        {
            underKickScript.Use();
            anim.SetTrigger("underKick");
        }

        if (Input.GetKeyDown(KeyCode.G) && !isEnd && !hitByUpperCut && Time.time - lastGuardTime >= guardTime && !IsGuardAnimationPlaying() && !IsBasicPunchAnimationPlaying() && !IsLeftKickAnimationPlaying() && !IsRightKickAnimationPlaying() && !IsUnderKickAnimationPlaying() && !IsBasicPunchedAnimationPlaying() && !IsKickedAnimationPlaying() && !IsUnderKickedAnimationPlaying())
        {
            /*rightKickScript.Use();*/
            gFlag = true;
            StartCoroutine(ActivateGuard(0.3f));
            anim.SetTrigger("guard");
            lastGuardTime = Time.time;
        }
    }
    private bool CanStartPunch()
    {
        return !IsGuardAnimationPlaying() && !IsBasicPunchAnimationPlaying() && !IsRightPunchAnimationPlaying() && !IsUpperCutAnimationPlaying() && !IsLeftKickAnimationPlaying() && !IsRightKickAnimationPlaying() && !IsUnderKickAnimationPlaying() && !IsBasicPunchedAnimationPlaying() && !IsKickedAnimationPlaying() && !IsUnderKickedAnimationPlaying();
    }

    private void MoveForward()
    {
        if (Input.GetKeyDown(KeyCode.D) && timer < 0) 
        {
            float timeSinceLastPress = Time.time - lastPressTime;
            lastPressTime = Time.time;

            if (timeSinceLastPress < doublePressInterval) 
            {
                pressCount++;
            } 
            else 
            {
                pressCount = 0; // 시간 간격이 너무 길면 횟수 리셋
            }

            if (pressCount >= 1) // 두 번째 키 눌림 감지
            {
                if(!NoDodge() && Time.time - lastDodge > 1)
                {
                    StartCoroutine(DodgeCoroutine(0.2f)); // 0.2초 동안 회피 동작 실행
                }
            }
        }

        // 키가 눌린 상태이면 걷기 동작
        if (Input.GetKey(KeyCode.D) && !IsBasicPunchAnimationPlaying() && !IsLeftKickAnimationPlaying() && !IsRightKickAnimationPlaying() && !IsUnderKickAnimationPlaying() && !IsBasicPunchedAnimationPlaying() && !IsKickedAnimationPlaying() && !IsUnderKickedAnimationPlaying() && !IsGuardAnimationPlaying() && !IsRightPunchAnimationPlaying() && !IsUpperCutAnimationPlaying() && timer<0) 
        {
            Vector3 moveVec = new Vector3(0, 0, 1);
            transform.position += moveVec * speed * Time.deltaTime;

            anim.SetBool("stepForward", moveVec.z > 0);
            anim.SetBool("stepBackward", moveVec.z < 0);
        }

        // 키에서 손을 떼면
        if (Input.GetKeyUp(KeyCode.D))
        {
            pressCount = 0; // 키 눌림 횟수 리셋
        }
        // 첫 번째 키 눌림 시간 기록
        if (pressCount == 1)
        {
            lastPressTime = Time.time;
        }
        // 키 눌림 간격이 너무 길면 키 눌림 횟수 리셋
        if (Time.time - lastPressTime > doublePressInterval && pressCount > 0)
        {
            pressCount = 0;
        }

        IEnumerator DodgeCoroutine(float duration)
        {
            lastDodge = Time.time;
            anim.SetTrigger("dodge");
            Vector3 startPostion = transform.position;
            Vector3 moveVec = new Vector3(0, 0, 2);
            Vector3 targetPosition = startPostion + moveVec;

            dodgeLeft.enabled = true;
            dodgeRight.enabled = true; 
            afterI.Play();

            float endTime = Time.time + duration;
            while (Time.time < endTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5*speed * Time.deltaTime);
                
                yield return null;
                
            }
            pressCount = 0; // 회피 동작 종료 후 횟수 리셋
            afterI.Stop();
            dodgeLeft.enabled = false;
            dodgeRight.enabled = false; 
        }
    }

    private void MoveBackward()
    {
        if (Input.GetKeyDown(KeyCode.A) && timer < 0) 
        {
            float timeSinceLastPress = Time.time - lastPressTimeBack;
            lastPressTimeBack = Time.time;
            // 회피 동작 조건 체크
            if (timeSinceLastPress < doublePressInterval) 
            {
                pressCountBack++;
            } 
            else 
            {
                pressCountBack = 0; // 시간 간격이 너무 길면 횟수 리셋
            }

            if (pressCountBack >= 1)
            {
                if(!NoDodge() && Time.time - lastDodge> 1)
                {
                    StartCoroutine(DodgeCoroutine(0.2f)); // 0.2초 동안 회피 동작 실행
                }
            }
        }

        // 키가 눌린 상태이면 걷기 동작
        if (Input.GetKey(KeyCode.A) && !IsBasicPunchAnimationPlaying() && !IsRightKickAnimationPlaying() && !IsLeftKickAnimationPlaying() && !IsUnderKickAnimationPlaying() && !IsBasicPunchedAnimationPlaying() && !IsKickedAnimationPlaying() && !IsUnderKickedAnimationPlaying() && !IsGuardAnimationPlaying() && !IsRightPunchAnimationPlaying() && !IsUpperCutAnimationPlaying() && timer<0)
        {
            moveVec = new Vector3(0, 0, -1);
            transform.position += moveVec * speed * Time.deltaTime;

            anim.SetBool("stepForward", moveVec.z > 0);
            anim.SetBool("stepBackward", moveVec.z < 0);

        }

        // 키에서 손을 떼면
        if (Input.GetKeyUp(KeyCode.A))
        {
            pressCountBack = 0; // 키 눌림 횟수 리셋
        }
        // 첫 번째 키 눌림 시간 기록
        if (pressCountBack == 1)
        {
            lastPressTimeBack = Time.time;
        }
        // 키 눌림 간격이 너무 길면 키 눌림 횟수 리셋
        if (Time.time - lastPressTimeBack > doublePressInterval && pressCountBack > 0)
        {
            pressCountBack = 0;
        }
        IEnumerator DodgeCoroutine(float duration)
        {
            lastDodge = Time.time;
            anim.SetTrigger("dodge");
            Vector3 startPostion = transform.position;
            Vector3 moveVec = new Vector3(0, 0, 2);
            Vector3 targetPosition = startPostion - moveVec;

            dodgeLeft.enabled = true;
            dodgeRight.enabled = true;
            afterI.Play();

            float endTime = Time.time + duration;
            while (Time.time < endTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5*speed * Time.deltaTime);
                yield return null;
            }
            pressCountBack = 0; // 회피 동작 종료 후 횟수 리셋
            afterI.Stop();
            dodgeLeft.enabled = false;
            dodgeRight.enabled = false; 
        }
    }

    public void ChangeHealth(int amount)
    {
        // 체력 변경
        curHealth += amount;
        // 체력이 0 이하로 떨어지지 않도록 함
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);

        // 체력이 0이 되면 플레이어 사망 처리
        if (curHealth <= 0)
        {
            anim.SetBool("Died", true);
            Die();
            player.Win();
        }
    }
    public void Win()
    {
        StartCoroutine(SlowMotionWin());
        
    }

    public void Win0()
    {
        middleBar.SetActive(false);
        winLight.enabled = true;
        isEnd = true;
        music.PlayWinnerMusic();
        clock.EndOfGame();
        endmenu.WhenGameEnd();
        mainCam.gameObject.SetActive(false);
        victoryCam.gameObject.SetActive(true);
        anim.SetTrigger("isWin");
    }

    public void Die0()
    {
        anim.SetTrigger("isLose");
        middleBar.SetActive(false);
        isEnd = true;
        clock.EndOfGame();
        mainCam.gameObject.SetActive(false);
    }

    IEnumerator SlowMotionWin()
    {
        Time.timeScale = 0.3f;

        yield return new WaitForSecondsRealtime(2.5f);

        Time.timeScale = 1.0f;

        middleBar.SetActive(false);
        winLight.enabled = true;
        isEnd = true;
        music.PlayWinnerMusic();
        clock.EndOfGame();
        endmenu.WhenGameEnd();
        mainCam.gameObject.SetActive(false);
        victoryCam.gameObject.SetActive(true);
        anim.SetTrigger("isWin");

    }

    public void Die()
    {
        StartCoroutine(SlowMotionDie());
    }

    IEnumerator SlowMotionDie()
    {
        Time.timeScale = 0.3f;
        anim.SetTrigger("isLose");
        yield return new WaitForSecondsRealtime(2.5f);

        Time.timeScale = 1.0f;

        middleBar.SetActive(false);
        isEnd = true;
        clock.EndOfGame();
        mainCam.gameObject.SetActive(false);
        
        // 플레이어 사망 처리 로직
        // 예를 들어, 게임 오버 화면 표시, 캐릭터 애니메이션 재생 등
        //SceneManager.LoadScene("EndingScene");

    }

    bool NoDodge()
    {
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");
        return (anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("rightKick")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("leftKick")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("underKick")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("basicPunch")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("kicked")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("underKicked")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("basicPunched")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("guard")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("rightPunch")
                || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("upperCut"));
    }

    bool IsIdlePlaying()
    {
        // "Base Layer" is the name of basic layer of Animation Controller 
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        // check whether basicPunch animation is playing or not. 
        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("idle");
    }

    bool IsLying()
    {
        // "Base Layer" is the name of basic layer of Animation Controller 
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        // check whether basicPunch animation is playing or not. 
        return (anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("upperCutted")
            || anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("standup"));
    }


    bool IsBasicPunchAnimationPlaying()
    {
        // "Base Layer" is the name of basic layer of Animation Controller 
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        // check whether basicPunch animation is playing or not. 
        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("basicPunch");
    }

    bool IsRightPunchAnimationPlaying()
    {
        // "Base Layer" is the name of basic layer of Animation Controller 
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        // check whether RightPunch animation is playing or not. 
        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("rightPunch");
    }

    bool IsUpperCutAnimationPlaying()
    {
        // "Base Layer" is the name of basic layer of Animation Controller 
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        // check whether RightPunch animation is playing or not. 
        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("upperCut");
    }

    bool IsRightKickAnimationPlaying()
    {
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("rightKick");
    }
    
    bool IsLeftKickAnimationPlaying()
    {
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("leftKick");
    }

    bool IsUnderKickAnimationPlaying()
    {
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("underKick");
    }

    bool IsBasicPunchedAnimationPlaying()
    {
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("basicPunched");
    }

    bool IsKickedAnimationPlaying()
    {
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("kicked");
    }

    bool IsUnderKickedAnimationPlaying()
    {
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("underKicked");
    }

    bool IsGuardAnimationPlaying()
    {
        int BaseLayerIndex = anim.GetLayerIndex("Base Layer");

        return anim.GetCurrentAnimatorStateInfo(BaseLayerIndex).IsName("guard");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            leftPunch leftPunch = other.GetComponent<leftPunch>();

            //when not guarded
            if (!gFlag && !hitByUpperCut)
            {
                anim.SetTrigger("basicPunched");

                cameraScript.punched();
                soundControllerScript.playPunched();

                ChangeHealth(-leftPunch.damage);

                StartCoroutine(ActivateParticleAndLight(particle, light, 0.3f));
            }
            else if(gFlag)
            {
                if (curHealth == 1)
                {
                    soundControllerScript.guarded();
                    StartCoroutine(ActivateGuard(guard, 0.5f));
                }
                else { 
                ChangeHealth(-1);
                soundControllerScript.guarded();
                StartCoroutine(ActivateGuard(guard, 0.5f));
                }
            } 
        }

        else if (other.tag == "R_Melee")
        {
            rightPunch rightPunch = other.GetComponent<rightPunch>();

            //when not guarded
            if (!gFlag && !hitByUpperCut)
            {
                anim.SetTrigger("basicPunched");

                cameraScript.punched();
                soundControllerScript.playPunched();

                ChangeHealth(-rightPunch.damage);

                StartCoroutine(ActivateParticleAndLight(particle, light, 0.3f));
            }
            else if(gFlag)
            {

                if (curHealth == 1)
                {
                    soundControllerScript.guarded();
                    StartCoroutine(ActivateGuard(guard, 0.5f));
                }
                else
                {
                    ChangeHealth(-1);
                    soundControllerScript.guarded();
                    StartCoroutine(ActivateGuard(guard, 0.5f));
                }
            } 
        }

        else if (other.tag == "BigMelee")
        {
            if(!hitByUpperCut)
            {
                upperCut upperCut = other.GetComponent<upperCut>();
                anim.SetTrigger("upperCutted");
                anim.SetBool("hitByUpperCut", true);
                hitByUpperCut = true;
                cameraScript.uppered();
                soundControllerScript.playUppered();

                ChangeHealth(-upperCut.damage);

                StartCoroutine(ActivateParticleAndLight(particle, light, 0.3f));
            }
        }

        else if (other.tag == "Foot")
        {

            rightKick rightKick = other.GetComponent<rightKick>();

            //when not guarded
            if (!gFlag && !hitByUpperCut)
            { 
            anim.SetTrigger("kicked");

            cameraScript.kicked();
            soundControllerScript.playKicked();

            
            ChangeHealth(-rightKick.damage);

            StartCoroutine(ActivateParticleAndLight(particle, light, 0.3f));
            }
            else if(gFlag)
            {
                if (curHealth == 1)
                {
                    soundControllerScript.guarded();
                    StartCoroutine(ActivateGuard(guard, 0.5f));
                }
                else
                {
                    ChangeHealth(-1);
                    soundControllerScript.guarded();
                    StartCoroutine(ActivateGuard(guard, 0.5f));
                }
            }

        }

        else if (other.tag == "L_Foot")
        {

            leftKick leftKick = other.GetComponent<leftKick>();

            //when not guarded
            if (!gFlag && !hitByUpperCut)
            { 
            anim.SetTrigger("kicked");

            cameraScript.kicked();
            soundControllerScript.playKicked();

            
            ChangeHealth(-leftKick.damage);

            StartCoroutine(ActivateParticleAndLight(particle, light, 0.3f));
            }
            else if(gFlag)
            {
                if (curHealth == 1)
                {
                    soundControllerScript.guarded();
                    StartCoroutine(ActivateGuard(guard, 0.5f));
                }
                else
                {
                    ChangeHealth(-1);
                    soundControllerScript.guarded();
                    StartCoroutine(ActivateGuard(guard, 0.5f));
                }
            }

        }

        else if (other.tag == "Knee")
        {

            underKick underKick = other.GetComponent<underKick>();

            //when not guarded
            if (!hitByUpperCut)
            {
                ChangeHealth(-underKick.damage);
                cameraScript.kicked();
                soundControllerScript.playKicked();
                StartCoroutine(ActivateParticleAndLight(particle, light, 0.3f));

                if (curHealth > 0)
                {
                    anim.SetTrigger("underKicked");

                }

                if (curHealth <= 0)
                {
                    anim.SetTrigger("isLose");
                }
            }

        }

    }

    IEnumerator ActivateGuard(float duration)
    {
        
        yield return new WaitForSeconds(duration);
        gFlag = false;
    }

    IEnumerator ActivateGuard(ParticleSystem guard, float duration)
    {
        guard.Play();
      
        yield return new WaitForSeconds(duration);

        particle.Stop();
     
    }

    IEnumerator ActivateParticleAndLight(ParticleSystem particle, Light light, float duration)
    {
        particle.Play();
        light.enabled = true;

        yield return new WaitForSeconds(duration);

        particle.Stop();
        light.enabled = false;
    }

    //limit the orientation of the charater? 
    void FixedUpdate()
    {
        FreezeRotation();
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
}

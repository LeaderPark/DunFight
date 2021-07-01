using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimSc : MonoBehaviour
{
    bool isNext = false;
    bool isMove = true;

    public Text mainText;
    public GameObject mainCamera;
    public GameObject backBlock;
    public AudioClip typing;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer sprite;
    CameraMove cameraMove;
    AudioSource audioSource;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        cameraMove = mainCamera.GetComponent<CameraMove>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(Anim());
    }
    
    void Update()
    {
        if(isMove)
            move();
    }

    IEnumerator Anim()
    {
        DataManager.Instance.isDie = true;
        yield return new WaitForSeconds(5f);
        DoText("안녕 내 이름은 용사야", 1.5f);
        yield return new WaitForSeconds(3.5f);
        DoText("난 도움이 필요한 사람들을 위해 모험을 하고 있지", 2f);
        yield return new WaitForSeconds(3.5f);
        DoText("오옹 넌 이번모험이 처음인거 같은데?", 1.5f); 
        yield return new WaitForSeconds(3.5f);   
        DoText("내가 처음인 너를 위해 특별히 간단한 동작들을 알려줄께.", 2f);
        yield return new WaitForSeconds(3.5f);
        DoText("이번 한번만이라고", 1f);
        yield return new WaitForSeconds(3.5f);
        DoText("잇차... 일단 멈추고", 1f);
        isMove = false;
        animator.SetBool("isMoveing", false);
        cameraMove.limitMinX = 198;
        backBlock.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        DoText("자 우선 방향키를 눌러서 이동을 해봐.", 1.5f);
        yield return new WaitForSeconds(3.5f);
        DoText("또 SPACE바를 누르면 점프를 할 수 있어.", 1.5f);
        DataManager.Instance.isDie = false;
        yield return new WaitForSeconds(3.5f);
        yield return new WaitUntil(() => isNext);
        isNext = false;
        DoText("이번에는 SPACE바를 연속 2번 눌러서 이단점프를 해보자.", 2f);
        yield return new WaitForSeconds(3.5f);
        yield return new WaitUntil(() => isNext);
        isNext = false;
        DoText("앞에 보이는 벽에 붙으면 벽을 타면서 내려갈 수 있어.", 2f);
        yield return new WaitForSeconds(3.5f);
        yield return new WaitUntil(() => isNext);
        isNext = false;
        DoText("A키를 눌러서 앞에 적을 공격해 보자.", 1.5f);
        yield return new WaitForSeconds(3.5f);
        yield return new WaitUntil(() => isNext);
        isNext = false;
        DoText("이번에는 점프후 A키를 눌러 점프 공격을 해보자.", 1.5f);
        yield return new WaitForSeconds(3.5f);
        yield return new WaitUntil(() => isNext);
        isNext = false;
        DoText("잘했어 이제 잘 알겠지? 이제 가던 길이나 가자", 1.5f);
        isMove = false;
        animator.SetBool("isMoveing", true);
        yield return new WaitForSeconds(3.5f);
        DoText("어 뭐라고?", 1f);
        yield return new WaitUntil(() => isNext);
        DoText("으아아악...!", 1f);
        DataManager.Instance.isFade = true;
    }

    void move()
    {
        Vector3 moveVelcity = Vector3.zero;
        moveVelcity = Vector3.right;
        sprite.flipX = false;
        animator.SetBool("isMoveing", true);
        transform.position += moveVelcity * 10 * Time.deltaTime;
    }

    void DoText(string text,float time)
    {
        mainText.DOText(" ", 0);
        Sequence dleay = DOTween.Sequence();
        dleay.OnStart(() =>mainText.DOText(text,time));
        dleay.SetDelay(3f);
        dleay.Append(mainText.DOText(" ",0));
    }

    void typingS()
    {
        audioSource.PlayOneShot(typing);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("NEXT"))
        {
            isNext = true;
            Destroy(col.gameObject);
        }
    }
}

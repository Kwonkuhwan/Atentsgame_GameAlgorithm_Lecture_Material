using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Player : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    private PlayerInputAction actions = null;
    private Animator ani = null;
    private Rigidbody2D rigid = null;
    private Light2D spotLight = null;

    private Vector2 inputDir = Vector2.zero;
    private Vector2 oldDir = Vector2.zero;

    public float sightRange = 3.0f;
    public float sightAngle = 90.0f;

    private Slime seenslime = null;

    private Vector2Int currentMap = Vector2Int.one;
    private Vector2 mapSize = new Vector2(20, 20);
    private Vector2Int mapCount = new Vector2Int(3, 3);
    private Vector2 offset = Vector2.zero;

    private AttackArea attackArea = null;

    public Slider lifeSlider;
    private const float MaxLifeTime = 10.0f;
    public float lifeTime = MaxLifeTime;
    public float LifeTime
    {
        get { return lifeTime; }
        set
        {
            if (value < 0.0f && !isDead)
            {
                Die();
            }
            else
            {
                lifeTime = Mathf.Clamp(value, 0.0f, MaxLifeTime);
                lifeSlider.value = LifeTime;
                lifeTimeText.text = $"{lifeTime:F2} 초";

                vignette.intensity.value = (MaxLifeTime - lifeTime) / MaxLifeTime;
            }
        }
    }

    private float playTime = 0.0f;
    public float PlayTime
    {
        get { return playTime; }
        set
        {
            playTime = value;
        }
    }

    public Text lifeTimeText;

    private Vignette vignette;

    public Vector2Int CurrentMap
    {
        get => currentMap;
        set
        {
            if(currentMap != value)
            {
                currentMap = value;
                onMapChange?.Invoke(currentMap);
                Debug.Log($"현재 맵의 위치 : {currentMap}");
            }
        }
    }

    public Action<Vector2Int> onMapChange;

    private bool isDead = false; 
    public GameOver gameOver;

    private void Awake()
    {
        actions = new PlayerInputAction();
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        spotLight = transform.GetChild(1).GetComponent<Light2D>();

        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        circle.radius = sightRange;

        attackArea = GetComponentInChildren<AttackArea>();

        offset = new Vector2(mapSize.x * mapCount.x * -0.5f, mapSize.y * mapCount.y * -0.5f);
    }

    private void Start()
    {
        GameManager.Inst.PostProcessVolume.profile.TryGet<Vignette>(out vignette);

        if (lifeSlider == null)
            lifeSlider = FindObjectOfType<Slider>();

        if (lifeTimeText == null)
            lifeTimeText = FindObjectOfType<Text>();

        lifeSlider.minValue = 0;
        lifeSlider.maxValue = MaxLifeTime;
        lifeSlider.value = MaxLifeTime;

        lifeTimeText.text = $"{lifeTime:F2}초";

        if(gameOver == null)
            gameOver = FindObjectOfType<GameOver>();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnStop;
        actions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        actions.Player.Attack.performed -= OnAttack;
        actions.Player.Move.canceled -= OnStop;
        actions.Player.Move.performed -= OnMove;
        actions.Player.Disable();
    }

    private void Update()
    {
        if (!isDead)
        {
            LifeTime -= Time.deltaTime;
            PlayTime += Time.deltaTime;
        }
    }

    private void Die()
    {
        isDead = true;
        actions.Player.Disable();
        gameOver.GameOverLoading(PlayTime);
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * walkSpeed * inputDir);

        Vector2 pos = (Vector2)transform.position - offset;
        CurrentMap = new Vector2Int((int)(pos.x / mapSize.x), (int)(pos.y / mapSize.y));
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
        List<ContactPoint2D> temp = new List<ContactPoint2D>();

        if (rigid.GetContacts(temp) == 0)
        {
            inputDir = inputDir.normalized;
        }

        oldDir = inputDir;
        attackArea.transform.localPosition = inputDir * 0.8f;

        // 바라보는 방향을 스포트 라이트 회전시키기
        //float angle = Vector3.SignedAngle(Vector3.up, (Vector3)inputDir, Vector3.forward);
        //spotLight.transform.rotation = Quaternion.Euler(0, 0, angle);
        spotLight.transform.up = inputDir;
        spotLight.color = new Color(1.0f, 0.9f, 0.8f);

        ani.SetBool("IsMove", true);
        ani.SetFloat("InputX", inputDir.x);
        ani.SetFloat("InputY", inputDir.y);
    }


    private void OnStop(InputAction.CallbackContext context)
    {
        //inputDir = context.ReadValue<Vector2>();
        inputDir = Vector2.zero;
        ani.SetBool("IsMove", false);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        ani.SetTrigger("IsAttack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
             if (IsInSight(collision.transform.position))
            {
                //Debug.Log("적이 보인다.");
                seenslime = collision.gameObject.GetComponent<Slime>();
                seenslime.OutlineOnOff(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (IsInSight(collision.transform.position))
            {
                //Debug.Log("적이 보인다.");
                seenslime = collision.gameObject.GetComponent<Slime>();
                seenslime.OutlineOnOff(false);
            }
            else
            {
                seenslime?.OutlineOnOff(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            seenslime = null;
        }        
    }

    bool IsInSight(Vector3 targetPos)
    {
        float angle = Vector2.Angle(oldDir, (targetPos - transform.position));
        return angle <= sightAngle * 0.5f;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.forward, sightRange);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, transform.forward, 
            Quaternion.Euler(0, 0, -sightAngle * 0.5f) * oldDir, sightAngle, sightRange, 3f);

        Handles.DrawLine(transform.position, 
            transform.position + Quaternion.Euler(0, 0, -sightAngle * 0.5f) * oldDir * sightRange);
        Handles.DrawLine(transform.position, 
            transform.position + Quaternion.Euler(0, 0, sightAngle * 0.5f) * oldDir * sightRange);
    }
#endif
}

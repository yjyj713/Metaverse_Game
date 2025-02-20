using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;  // 이동 속도

    private SpriteRenderer backgroundRenderer;  // 배경의 SpriteRenderer
    private Vector2 backgroundSize;             // 배경의 크기

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Sprite upSprite;  // 위 방향 스프라이트
    public Sprite downSprite;  // 아래 방향 스프라이트
    public Sprite leftSprite;  // 왼쪽 방향 스프라이트
    public Sprite rightSprite;  // 오른쪽 방향 스프라이트

    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;  // 스프라이트 렌더러

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 컴포넌트 할당
        spriteRenderer = GetComponent<SpriteRenderer>();  // 스프라이트 렌더러 초기화

        // 플레이어가 회전하지 않도록 설정
        rb.freezeRotation = true;  // 회전 방지

        // 배경 오브젝트의 SpriteRenderer 찾기
        backgroundRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        backgroundSize = backgroundRenderer.bounds.size;  // 배경 크기 계산

        // Player 오브젝트의 SpriteRenderer 찾기
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Player 오브젝트에 SpriteRenderer가 없습니다.");
        }

        // 플레이어가 처음 시작할 때 맵 내에 있도록 위치 제한
        RestrictPlayerToBounds();
    }

    void Update()
    {
        // WASD 입력 받기
        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D (좌우) 입력
        float vertical = Input.GetAxisRaw("Vertical");  // W/S (상하) 입력

        // 이동 방향 계산
        moveDirection = new Vector2(horizontal, vertical).normalized;

        // 이동 방향에 맞는 스프라이트 변경
        if (moveDirection.x > 0)  // 오른쪽으로 이동
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (moveDirection.x < 0)  // 왼쪽으로 이동
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (moveDirection.y > 0)  // 위로 이동
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (moveDirection.y < 0)  // 아래로 이동
        {
            spriteRenderer.sprite = downSprite;
        }

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    void FixedUpdate()
    {
        // 플레이어의 새로운 위치 계산
        Vector2 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        // 배경의 크기를 기준으로 플레이어 위치 제한
        float minX = -backgroundSize.x / 2 + 0.5f;  // 맵의 왼쪽 끝
        float maxX = backgroundSize.x / 2 - 0.5f;   // 맵의 오른쪽 끝
        float minY = -backgroundSize.y / 2 + 0.5f;  // 맵의 아래쪽 끝
        float maxY = backgroundSize.y / 2 - 0.5f;   // 맵의 위쪽 끝

        // 경계 내로 플레이어 위치 제한
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // Rigidbody2D에 위치 업데이트
        rb.MovePosition(targetPosition);
    }

    // 충돌 발생 시 처리
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Minigame"))
        {
            // 집, 나무, 호수와 충돌 시
            // 충돌 방향에 따라 플레이어의 이동을 멈춤
            rb.velocity = Vector2.zero;  // 이동을 멈추게 함
            // 집과 충돌하면 씬을 미니게임 씬으로 변경
            StartMiniGame();  // 미니게임 시작
        }
    }

    // 미니게임 씬으로 전환
    private void StartMiniGame()
    {
        SceneManager.LoadScene("MiniGameScene");
    }

    // 플레이어가 처음 게임을 시작할 때, 맵 내로 제한
    private void RestrictPlayerToBounds()
    {
        // 배경의 크기를 기준으로 플레이어 초기 위치를 제한
        float minX = -backgroundSize.x / 2 + 0.5f;
        float maxX = backgroundSize.x / 2 - 0.5f;
        float minY = -backgroundSize.y / 2 + 0.5f;
        float maxY = backgroundSize.y / 2 - 0.5f;

        // 플레이어의 위치를 제한된 범위 내로 설정
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // 제한된 위치로 이동
        transform.position = new Vector2(clampedX, clampedY);
    }
}

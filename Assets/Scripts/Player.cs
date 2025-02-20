using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;  // �̵� �ӵ�

    private SpriteRenderer backgroundRenderer;  // ����� SpriteRenderer
    private Vector2 backgroundSize;             // ����� ũ��

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Sprite upSprite;  // �� ���� ��������Ʈ
    public Sprite downSprite;  // �Ʒ� ���� ��������Ʈ
    public Sprite leftSprite;  // ���� ���� ��������Ʈ
    public Sprite rightSprite;  // ������ ���� ��������Ʈ

    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;  // ��������Ʈ ������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D ������Ʈ �Ҵ�
        spriteRenderer = GetComponent<SpriteRenderer>();  // ��������Ʈ ������ �ʱ�ȭ

        // �÷��̾ ȸ������ �ʵ��� ����
        rb.freezeRotation = true;  // ȸ�� ����

        // ��� ������Ʈ�� SpriteRenderer ã��
        backgroundRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        backgroundSize = backgroundRenderer.bounds.size;  // ��� ũ�� ���

        // Player ������Ʈ�� SpriteRenderer ã��
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Player ������Ʈ�� SpriteRenderer�� �����ϴ�.");
        }

        // �÷��̾ ó�� ������ �� �� ���� �ֵ��� ��ġ ����
        RestrictPlayerToBounds();
    }

    void Update()
    {
        // WASD �Է� �ޱ�
        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D (�¿�) �Է�
        float vertical = Input.GetAxisRaw("Vertical");  // W/S (����) �Է�

        // �̵� ���� ���
        moveDirection = new Vector2(horizontal, vertical).normalized;

        // �̵� ���⿡ �´� ��������Ʈ ����
        if (moveDirection.x > 0)  // ���������� �̵�
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (moveDirection.x < 0)  // �������� �̵�
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (moveDirection.y > 0)  // ���� �̵�
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (moveDirection.y < 0)  // �Ʒ��� �̵�
        {
            spriteRenderer.sprite = downSprite;
        }

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    void FixedUpdate()
    {
        // �÷��̾��� ���ο� ��ġ ���
        Vector2 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        // ����� ũ�⸦ �������� �÷��̾� ��ġ ����
        float minX = -backgroundSize.x / 2 + 0.5f;  // ���� ���� ��
        float maxX = backgroundSize.x / 2 - 0.5f;   // ���� ������ ��
        float minY = -backgroundSize.y / 2 + 0.5f;  // ���� �Ʒ��� ��
        float maxY = backgroundSize.y / 2 - 0.5f;   // ���� ���� ��

        // ��� ���� �÷��̾� ��ġ ����
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // Rigidbody2D�� ��ġ ������Ʈ
        rb.MovePosition(targetPosition);
    }

    // �浹 �߻� �� ó��
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Minigame"))
        {
            // ��, ����, ȣ���� �浹 ��
            // �浹 ���⿡ ���� �÷��̾��� �̵��� ����
            rb.velocity = Vector2.zero;  // �̵��� ���߰� ��
            // ���� �浹�ϸ� ���� �̴ϰ��� ������ ����
            StartMiniGame();  // �̴ϰ��� ����
        }
    }

    // �̴ϰ��� ������ ��ȯ
    private void StartMiniGame()
    {
        SceneManager.LoadScene("MiniGameScene");
    }

    // �÷��̾ ó�� ������ ������ ��, �� ���� ����
    private void RestrictPlayerToBounds()
    {
        // ����� ũ�⸦ �������� �÷��̾� �ʱ� ��ġ�� ����
        float minX = -backgroundSize.x / 2 + 0.5f;
        float maxX = backgroundSize.x / 2 - 0.5f;
        float minY = -backgroundSize.y / 2 + 0.5f;
        float maxY = backgroundSize.y / 2 - 0.5f;

        // �÷��̾��� ��ġ�� ���ѵ� ���� ���� ����
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // ���ѵ� ��ġ�� �̵�
        transform.position = new Vector2(clampedX, clampedY);
    }
}

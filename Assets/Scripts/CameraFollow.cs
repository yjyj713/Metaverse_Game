using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // ���� �÷��̾�
    public float smoothSpeed = 0.125f;  // ī�޶� �̵� �ӵ�
    public Vector3 offset;           // ī�޶��� �ʱ� ������ (�÷��̾���� �Ÿ�)

    private SpriteRenderer backgroundRenderer;  // ����� SpriteRenderer
    private Vector2 backgroundSize;             // ����� ũ��

    void Start()
    {
        // ��� ũ�� ���
        backgroundRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        backgroundSize = backgroundRenderer.bounds.size;  // ��� ũ�� ���
    }

    void LateUpdate()
    {
        // ī�޶� ���� ��ǥ ��ġ
        Vector3 desiredPosition = player.position + offset;

        // ����� ũ�� �� ī�޶��� ũ�� ��� (ī�޶� ũ�⸸ŭ ����)
        float cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float cameraHalfHeight = Camera.main.orthographicSize;

        // ī�޶��� �̵� ���� ����
        float minX = -backgroundSize.x / 2 + cameraHalfWidth;  // ���� ���� �� (ī�޶� ũ�� ���)
        float maxX = backgroundSize.x / 2 - cameraHalfWidth;   // ���� ������ �� (ī�޶� ũ�� ���)
        float minY = -backgroundSize.y / 2 + cameraHalfHeight; // ���� �Ʒ��� �� (ī�޶� ũ�� ���)
        float maxY = backgroundSize.y / 2 - cameraHalfHeight;  // ���� ���� �� (ī�޶� ũ�� ���)

        // ī�޶� ��ġ�� ����
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // ī�޶��� ��ġ�� z���� �����Ǿ� �����Ƿ�, z�� �״�� �ΰ� x, y�� ����
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);

        // ī�޶� �ε巴�� �̵���Ű��
        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }
}

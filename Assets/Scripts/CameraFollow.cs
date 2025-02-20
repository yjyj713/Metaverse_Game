using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // 따라갈 플레이어
    public float smoothSpeed = 0.125f;  // 카메라 이동 속도
    public Vector3 offset;           // 카메라의 초기 오프셋 (플레이어와의 거리)

    private SpriteRenderer backgroundRenderer;  // 배경의 SpriteRenderer
    private Vector2 backgroundSize;             // 배경의 크기

    void Start()
    {
        // 배경 크기 계산
        backgroundRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        backgroundSize = backgroundRenderer.bounds.size;  // 배경 크기 계산
    }

    void LateUpdate()
    {
        // 카메라가 따라갈 목표 위치
        Vector3 desiredPosition = player.position + offset;

        // 배경의 크기 및 카메라의 크기 계산 (카메라 크기만큼 제한)
        float cameraHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float cameraHalfHeight = Camera.main.orthographicSize;

        // 카메라의 이동 범위 설정
        float minX = -backgroundSize.x / 2 + cameraHalfWidth;  // 맵의 왼쪽 끝 (카메라 크기 고려)
        float maxX = backgroundSize.x / 2 - cameraHalfWidth;   // 맵의 오른쪽 끝 (카메라 크기 고려)
        float minY = -backgroundSize.y / 2 + cameraHalfHeight; // 맵의 아래쪽 끝 (카메라 크기 고려)
        float maxY = backgroundSize.y / 2 - cameraHalfHeight;  // 맵의 위쪽 끝 (카메라 크기 고려)

        // 카메라 위치를 제한
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // 카메라의 위치는 z값은 고정되어 있으므로, z는 그대로 두고 x, y만 제한
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);

        // 카메라를 부드럽게 이동시키기
        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }
}

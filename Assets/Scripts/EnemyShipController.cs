using System.Collections;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    private Vector2 originalPosition;
    private bool isMovingInHexagonPattern = false;
    private bool shouldRearrangeGrid = false;

    public void MoveInHexagonPattern(float moveDuration, int index)
    {
        originalPosition = transform.position;

        // Tính toán vị trí hình lục giác
        Vector2[] hexagonPositions = CalculateHexagonPositions();

        // Di chuyển theo hình lục giác với index làm offset thời gian
        StartCoroutine(MoveInHexagonPatternCoroutine(hexagonPositions, moveDuration, index));
    }

    private Vector2[] CalculateHexagonPositions()
    {
        Vector2[] hexagonPositions = new Vector2[12];
        float radius = 2f;
        float angleIncrement = 2 * Mathf.PI / hexagonPositions.Length;

        for (int i = 0; i < hexagonPositions.Length; i++)
        {
            float angle = i * angleIncrement;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            hexagonPositions[i] = originalPosition + new Vector2(x, y);
        }

        return hexagonPositions;
    }

    private IEnumerator MoveInHexagonPatternCoroutine(Vector2[] positions, float moveDuration, int index)
    {
        isMovingInHexagonPattern = true;

        int positionIndex = index % positions.Length;
        Vector2 targetPosition = positions[positionIndex];
        float startTime = Time.time;
        float endTime = startTime + moveDuration;
        Vector2 startPosition = transform.position;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / moveDuration;

            // Di chuyển từ vị trí ban đầu đến vị trí đích
            transform.position = Vector2.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        if (index == positions.Length - 1)
        {
            shouldRearrangeGrid = true;
        }

        // Tiếp tục di chuyển đến vị trí kế tiếp trong hình lục giác
        int nextIndex = (index + 1) % positions.Length;
        MoveInHexagonPattern(moveDuration, nextIndex);
    }

    private void Update()
    {
        if (shouldRearrangeGrid && !isMovingInHexagonPattern)
        {
            // Nếu đã hoàn thành di chuyển theo hình lục giác và cần xếp lại thành hình 4x3
            shouldRearrangeGrid = false;
            StartCoroutine(RearrangeToGridPattern());
        }
    }

    private IEnumerator RearrangeToGridPattern()
    {
        // Xếp lại thành hình 4x3
        int rowSize = 4;
        int columnSize = 3;
        float offsetX = 2f;
        float offsetY = 2f;

        // Tính toán số hàng và số cột dựa trên tổng số enemy
        int totalEnemies = transform.parent.childCount;
        int numRows = Mathf.CeilToInt((float)totalEnemies / columnSize);
        int numColumns = Mathf.Min(totalEnemies, columnSize);

        for (int i = 0; i < totalEnemies; i++)
        {
            int rowIndex = i / numColumns;
            int columnIndex = i % numColumns;

            float x = originalPosition.x + columnIndex * offsetX;
            float y = originalPosition.y - rowIndex * offsetY;

            // Di chuyển enemy đến vị trí mới
            Vector2 targetPosition = new Vector2(x, y);
            float startTime = Time.time;
            float endTime = startTime + 1f;
            Vector2 startPosition = transform.parent.GetChild(i).position;

            while (Time.time < endTime)
            {
                float t = (Time.time - startTime) / 1f;

                transform.parent.GetChild(i).position = Vector2.Lerp(startPosition, targetPosition, t);

                yield return null;
            }
        }
    }
}

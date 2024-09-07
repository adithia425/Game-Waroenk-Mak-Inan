using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUIMiniGame : MonoBehaviour
{
    public RectTransform content; // Referensi ke RectTransform dari konten Scroll View
    public float moveDistance; // Jarak yang akan digerakkan setiap kali tombol spasi ditekan
    public float moveDuration = 1f; // Durasi pergerakan dalam detik

    public void MoveContent()
    {
        StartCoroutine(ActionMoveContent());
    }

    private IEnumerator ActionMoveContent()
    {
        Vector2 startPosition = content.anchoredPosition;
        Vector2 targetPosition = startPosition - new Vector2(moveDistance, 0);

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            content.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        content.anchoredPosition = targetPosition;
    }
}

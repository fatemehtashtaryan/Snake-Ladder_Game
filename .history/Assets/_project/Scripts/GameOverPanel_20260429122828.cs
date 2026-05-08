using TMPro;
using UnityEngine;
using System.Collections;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private RectTransform targetImage;

    public void SetGameOverText(int winnerIndex)
    {
        gameOverText. SetText($"Winner:\n Player{winnerIndex}");
    }
    

    public void PlayIntroAnimation()
    {
        AudioManager.Instance.PlayGameOver_Audio();
        StartCoroutine(AnimateImage());
    }

    IEnumerator AnimateImage()
    {
        Vector2 startPos = new Vector2(targetImage.anchoredPosition.x, 2000);
        targetImage.anchoredPosition = startPos;

        yield return MoveUI(targetImage, new Vector2(startPos.x, -400), 0.9f);

        yield return MoveUI(targetImage, new Vector2(startPos.x, -100), 0.25f);
    }

    IEnumerator MoveUI(RectTransform rect, Vector2 target, float duration)
    {
        Vector2 start = rect.anchoredPosition;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            float smooth = Mathf.SmoothStep(0f, 1f, t);

            rect.anchoredPosition = Vector2.Lerp(start, target, smooth);
            yield return null;
        }

        rect.anchoredPosition = target;
    }

}

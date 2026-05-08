using UnityEngine;
using System.Collections;

public class pawn : MonoBehaviour
{
    private int currentPos = -1;

    public float moveSpeed = 4f;
    public float hopHeight = 20f;

    public float returnSpeed = 2f;
    public float returnHopHeight = 20f;

    [SerializeField] float scaleSpeed = 4f;
    [SerializeField] float smallScaleFactor = 0.5f;

    [SerializeField] private RectTransform firstPos;
    [SerializeField] float moveDuration = 0.35f;


    private RectTransform pawnRect;

    private void Start()
    {
        pawnRect = GetComponent<RectTransform>();
    }

    public int GetPos()
    {
        return currentPos;
    }

    public void MoveStep(RectTransform nextTile, int nextPos)
    {
        StartCoroutine(MoveOneTile(nextTile, nextPos));
    }

    public void MoveStep_blackHole(RectTransform nextTile, int nextPos)
    {
        StartCoroutine(ShrinkThenMove(nextTile, nextPos));
    }

    IEnumerator MoveOneTile(RectTransform targetTile, int newPos)
    {
        AudioManager.Instance.PlayStep_Audio();

        Vector2 startPos = pawnRect.anchoredPosition;

        Vector2 endPos;
        RectTransform parentRect = pawnRect.parent as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            RectTransformUtility.WorldToScreenPoint(null, targetTile.position),
            null,
            out endPos
        );

        endPos.y -= 10f;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / moveDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            float hop = Mathf.Sin(smoothT * Mathf.PI) * hopHeight;

            Vector2 pos = Vector2.Lerp(startPos, endPos, smoothT);
            pos.y += hop;

            pawnRect.anchoredPosition = pos;

            yield return null;
        }

        pawnRect.anchoredPosition = endPos;
        currentPos = newPos;
    }




    public void ReturnToStart()
    {
        StartCoroutine(ReturnSmooth());
    }

    IEnumerator ReturnSmooth()
    {
        AudioManager.Instance.Play_ReturnToStart_Audio();

        Vector2 startPos = pawnRect.anchoredPosition;
        Vector2 targetPos = firstPos.anchoredPosition;

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * returnSpeed;

            float hop = Mathf.Sin(t * Mathf.PI) * returnHopHeight;

            Vector2 pos = Vector2.Lerp(startPos, targetPos, t);
            pos.y += hop;

            pawnRect.anchoredPosition = pos;

            yield return null;
        }

        pawnRect.anchoredPosition = targetPos;
        currentPos = -1;
    }

    IEnumerator ShrinkThenMove(RectTransform nextTile, int targetIndex)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 smallScale = originalScale * smallScaleFactor;

        yield return StartCoroutine(ScaleTo(smallScale));
        yield return StartCoroutine(MoveOneTile(nextTile, targetIndex));
        yield return StartCoroutine(ScaleTo(originalScale));
    }

    IEnumerator ScaleTo(Vector3 target)
    {
        Vector3 start = transform.localScale;

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.localScale = target;
    }
}

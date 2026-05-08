using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour
{
    private int currentPos = -1;

    public float moveSpeed = 6f;
    public float hopHeight = 40f;

    public float returnSpeed = 2f;
    public float returnHopHeight = 20f;

    [SerializeField] float scaleSpeed = 4f;
    [SerializeField] float smallScaleFactor = 0.5f;

    [SerializeField] private RectTransform firstPos;

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
        Vector2 endPos = targetTile.anchoredPosition;

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;

            float hop = Mathf.Sin(t * Mathf.PI) * hopHeight;

            Vector2 pos = Vector2.Lerp(startPos, endPos, t);
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

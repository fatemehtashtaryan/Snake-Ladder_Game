using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Collections;

public class pawn : MonoBehaviour
{
    private int currentPos = -1;
    public float moveSpeed = 700f;        
    public float hopHeight = 40f;         
    public float hopSpeed = 6f;         
    private float returnSpeed = 2f;   
    private float returnHopHeight = 20f; 
    [SerializeField] private Transform firstPos;
    [SerializeField] float scaleSpeed = 4f;
    [SerializeField] float smallScaleFactor = 0.5f;
    RectTransform pawnRect;


    private void Start()
    {
        pawnRect = GetComponent<RectTransform>();
    }

    public void MoveStep(Transform nextTile, int nextPos)
    {
        StartCoroutine(MoveOneTile(nextTile.GetComponent<RectTransform>(), nextPos));
    }
    
    public void MoveStep_blackHole(Transform nextTile, int nextPos)
    {
       
        StartCoroutine(ShrinkThenMove(nextTile, nextPos));
    }

    IEnumerator MoveOneTile(RectTransform targetTile, int newPos)
    {
        AudioManager.Instance.PlayStep_Audio();
        Vector2 startPos = pawnRect.anchoredPosition;
        Vector2 endPos = targetTile.anchoredPosition;

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * 5f;
            pawnRect.anchoredPosition = Vector2.Lerp(startPos, endPos, time);
            yield return null;
        }
        pawnRect.anchoredPosition = endPos;
        currentPos = newPos;
    }

    public int GetPos()
    {
        return currentPos;
    }

    
    public void ReturnToStart()
    {
        StartCoroutine(ReturnSmooth());
    }
    
    IEnumerator ReturnSmooth()
    {

        Vector3 startPos = transform.position;
        Vector3 targetPos = firstPos.position;
        AudioManager.Instance.Play_ReturnToStart_Audio();

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * returnSpeed;  

            Vector3 pos = Vector3.Lerp(startPos, targetPos, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * returnHopHeight;

            transform.position = pos;
            yield return null;
        }

        transform.position = targetPos;
        currentPos = -1;
    }

    private IEnumerator ShrinkThenMove(Transform nextTile, int targetIndex)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 smallScale = originalScale * smallScaleFactor;
        

        yield return StartCoroutine(ScaleTo(smallScale));

        yield return StartCoroutine(MoveOneTile(nextTile.GetComponent<RectTransform>(), targetIndex));

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

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Collections;

public class pawn : MonoBehaviour
{
    private int currentPos = 0;
    public float moveSpeed = 700f;        
    public float hopHeight = 40f;         
    public float hopSpeed = 6f;         
    private float returnSpeed = 2f;   
    private float returnHopHeight = 20f; 
    [SerializeField] private Transform firstPos;
    [SerializeField] float scaleSpeed = 4f;
    [SerializeField] float smallScaleFactor = 0.5f;




    public void MoveStep(Transform nextTile, int nextPos)
    {
        StartCoroutine(MoveOneTile(nextTile.position, nextPos));
    }
    
    public void MoveStep_blackHole(Transform nextTile, int nextPos)
    {
       
        StartCoroutine(ShrinkThenMove(nextTile, nextPos));
    }

    IEnumerator MoveOneTile(Vector3 targetPos, int newPos)
    {
        targetPos-= new Vector3(0, 10f, 0);
        AudioManager.Instance.PlayStep_Audio();
        

        Vector3 startPos = transform.position;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * hopSpeed;
            Vector3 pos = Vector3.Lerp(startPos, targetPos, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * hopHeight;

            transform.position = pos;
            yield return null;
        }
        transform.position = targetPos;
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
        currentPos = 0;
    }

    private IEnumerator ShrinkThenMove(Transform nextTile, int targetIndex)
    {
         AudioManager.Instance.Play_BlackHole_Audio();
        Vector3 originalScale = transform.localScale;
        Vector3 smallScale = originalScale * smallScaleFactor;
        

        yield return StartCoroutine(ScaleTo(smallScale));

        yield return StartCoroutine(MoveOneTile(nextTile.position, targetIndex));

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

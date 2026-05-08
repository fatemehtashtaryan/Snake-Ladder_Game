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
    [SerializeField] private AudioSource stepAudio;
    [SerializeField] private AudioSource fallAudio;
    [SerializeField] private Transform firstPos;
    [SerializeField] float scaleSpeed = 4f;
    [SerializeField] float smallScaleFactor = 0.5f;




    public void MoveStep(Transform nextTile, int nextPos)
    {
        StartCoroutine(MoveOneTile(nextTile.position, nextPos));
    }

    IEnumerator MoveOneTile(Vector3 targetPos, int newPos)
    {
        targetPos-= new Vector3(0, 10f, 0);

        if (stepAudio != null)
            stepAudio.pitch = Random.Range(1.1f, 1.8f);
            stepAudio.Play();  
            stepAudio.SetScheduledEndTime(AudioSettings.dspTime + 0.7);

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
        if (fallAudio != null)
        {
            fallAudio.time = 0.9f;
            fallAudio.pitch = 0.9f; 
            fallAudio.Play();
        }

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
        Vector3 originalScale = transform.localScale;
        Vector3 smallScale = originalScale * smallScaleFactor;

        yield return StartCoroutine(ScaleTo(smallScale));

        yield return StartCoroutine(MoveOneTile(nextTile.position, targetIndex));

        yield return StartCoroutine(ScaleTo(originalScale));
    }


    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float t = 0;

        while(t < 1)
        {
            t += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        transform.localScale = targetScale;
    }



}

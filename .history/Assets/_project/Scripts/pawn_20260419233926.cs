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
    [SerializeField] private AudioSource stepAudio;

    public void MoveStep(Transform nextTile, int nextPos)
    {
        StartCoroutine(MoveOneTile(nextTile.position, nextPos));
    }

    IEnumerator MoveOneTile(Vector3 targetPos, int newPos)
    {
        if (stepAudio != null)
            stepAudio.Play();  

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
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;

public class pawn : MonoBehaviour
{
    private int currentPos =1; 

    public void MovePawn(Transform newTransform, int newPos)
    {
        transform.position = newTransform.position;
        currentPos = newPos;
    }
    public int GetPos(){
        return currentPos;
    }
}

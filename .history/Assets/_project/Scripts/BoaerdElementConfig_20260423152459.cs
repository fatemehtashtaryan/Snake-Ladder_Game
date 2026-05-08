using UnityEngine;

public class BoardElementConfig : MonoBehaviour
{
    [SerializeField] private int hitIndex;
    [SerializeField] private int forwardedIndex;

    public void Initialize(int hit, int forward)
    {
        hitIndex = hit;
        forwardedIndex = forward;
        Debug.Log(hit);
    }

    public bool IsHitIndex(int index){
        return index == hitIndex;
    }
    public int IsForwardedIndex(){
        return forwardedIndex;
    }
    public int getHitInd(){
        return hitIndex;
    }
    
}
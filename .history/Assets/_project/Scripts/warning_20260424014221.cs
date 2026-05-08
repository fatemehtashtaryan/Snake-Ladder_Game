using TMPro;
using UnityEngine;

public class warning : MonoBehaviour
{
    public TMP_Text alert;

    public void ShowText(string message, float duration = 8f, Color? color = null)
    {
        Color finalColor = color ?? Color.red; 
        StartCoroutine(ShowTextCoroutine(message, duration, finalColor));
    }

    private System.Collections.IEnumerator ShowTextCoroutine(string message, float duration, Color color)
    {
        alert.enabled = true;
        alert.text = message;
        alert.color = color;

        yield return new WaitForSeconds(duration);

        alert.enabled = false;
    }
    
}

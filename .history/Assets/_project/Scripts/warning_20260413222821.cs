using TMPro;
using UnityEngine;

public class warning : MonoBehaviour
{
    public TMP_Text myText;

    public void ShowText(string message, float duration = 8f)
    {
        StartCoroutine(ShowTextCoroutine(message, duration));
    }

    private System.Collections.IEnumerator ShowTextCoroutine(string message, float duration, Color color)
    {
        myText.gameObject.SetActive(true);
        myText.text = message;
        myText.color = color;

        yield return new WaitForSeconds(duration);

        myText.gameObject.SetActive(false);
    }
}

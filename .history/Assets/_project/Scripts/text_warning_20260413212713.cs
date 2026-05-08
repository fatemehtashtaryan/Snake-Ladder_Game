using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TMP_Text myText;

    public void ShowText(string message, float duration = 3f)
    {
        StartCoroutine(ShowTextCoroutine(message, duration));
    }

    private System.Collections.IEnumerator ShowTextCoroutine(string message, float duration)
    {
        myText.gameObject.SetActive(true);
        myText.text = message;

        yield return new WaitForSeconds(duration);

        myText.gameObject.SetActive(false);
    }
}

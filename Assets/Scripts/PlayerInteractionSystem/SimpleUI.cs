using UnityEngine;
using TMPro;
using System.Collections;

public class SimpleUI : MonoBehaviour
{
    public static SimpleUI Instance;

    public TMP_Text objectiveText;
    public TMP_Text interactText;
    public TMP_Text messageText;

    void Awake()
    {
        Instance = this;

        interactText.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
    }

    public void ShowInteract()
    {
        interactText.gameObject.SetActive(true);
        messageText.gameObject.SetActive(false);
        interactText.text = "[E] Interact";
    }

    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        interactText.gameObject.SetActive(false);
        messageText.gameObject.SetActive(true);
        messageText.text = message;

        StartCoroutine(HideMessageAfterDelay());
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        messageText.gameObject.SetActive(false);
    }

    public void HidePrompt()
    {
        interactText.gameObject.SetActive(false);
    }

    public void SetObjective(string objective)
    {
        objectiveText.text = objective;
    }
}
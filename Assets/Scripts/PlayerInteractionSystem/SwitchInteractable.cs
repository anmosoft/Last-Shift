using UnityEngine;

public class SwitchInteractable : MonoBehaviour, IInteractable
{
    [Header("Switch Settings")]
    public GameObject objectToChange;
    public bool itemRequired = false;

    [Header("Audio Settings")]
    public AudioClip interactionSound;
    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource audioSource;

    public enum RequiredItem
    {
        Gasoline,
        Keycard
    }

    public RequiredItem requiredItem;

    private bool isOn = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    public void Interact()
    {
        if (itemRequired)
        {
            if (requiredItem == RequiredItem.Gasoline &&
                !PlayerInventory.hasGasoline)
            {
                Debug.Log("You need Gasoline!");
                SimpleUI.Instance.ShowMessage("You need Gasoline!");
                return;
            }

            if (requiredItem == RequiredItem.Keycard &&
                !PlayerInventory.hasKeycard)
            {
                Debug.Log("You need a Keycard!");
                SimpleUI.Instance.ShowMessage("You need a Keycard!");
                return;
            }
        }

        isOn = !isOn;

        // Play sound after successful interaction
        if (interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound, volume);
        }

        if (objectToChange != null)
        {
            objectToChange.SetActive(isOn);

            if (isOn)
            {
                SimpleUI.Instance.ShowMessage("You Won!");
            }
        }

        Debug.Log(isOn ? "Switch turned ON!" : "Switch turned OFF!");
    }
}
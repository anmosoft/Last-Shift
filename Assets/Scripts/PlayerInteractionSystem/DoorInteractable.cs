
using UnityEngine;
using System.Collections;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Sliding")]
    public float slideDistance = 2f;
    public float slideSpeed = 2f;

    public Vector3 slideDirection = Vector3.right;
    public bool invertDirection = false;

    [Header("Keycard")]
    public bool keycardRequired = true;

    [Header("Audio Settings")]
    public AudioClip openSound;
    public AudioClip closeSound;

    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource audioSource;

    private bool isOpen = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    void Start()
    {
        closedPosition = transform.localPosition;

        Vector3 direction = slideDirection.normalized;

        if (invertDirection)
            direction = -direction;

        openPosition = closedPosition + direction * slideDistance;
    }

    public void Interact()
    {
        if (keycardRequired && !PlayerInventory.hasKeycard)
        {
            Debug.Log("Keycard required!");
            SimpleUI.Instance.ShowMessage("Keycard required!");
            return;
        }

        isOpen = !isOpen;

        // Play the appropriate sound
        AudioClip sound = isOpen ? openSound : closeSound;

        if (sound != null)
            audioSource.PlayOneShot(sound, volume);

        StopAllCoroutines();
        StartCoroutine(SlideDoor());
    }

    IEnumerator SlideDoor()
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;

        float time = 0f;
        float duration = 1f / Mathf.Max(slideSpeed, 0.01f);

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(
                startPosition,
                targetPosition,
                time / duration
            );

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }
}
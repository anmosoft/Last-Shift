
using UnityEngine;

public class PickupInteractable : MonoBehaviour, IInteractable
{
    public enum ItemType
    {
        Default,
        Keycard,
        Gasoline
    }

    [Header("Item Settings")]
    public ItemType itemType = ItemType.Default;

    [Header("Audio Settings")]
    public AudioClip pickupSound;

    [Range(0f, 1f)]
    public float volume = 1f;

    public void Interact()
    {
        switch (itemType)
        {
            case ItemType.Default:
                Debug.Log("Item got picked up!");
                SimpleUI.Instance.ShowMessage("Item got picked up!");
                break;

            case ItemType.Keycard:
                PlayerInventory.hasKeycard = true;
                Debug.Log("Keycard collected!");
                SimpleUI.Instance.ShowMessage("Keycard collected!");
                SimpleUI.Instance.SetObjective("Find Gasoline");
                break;

            case ItemType.Gasoline:
                PlayerInventory.hasGasoline = true;
                Debug.Log("Gasoline collected!");
                SimpleUI.Instance.ShowMessage("Gasoline collected!");
                SimpleUI.Instance.SetObjective("Start The Generator!");
                break;
        }

        // Play the pickup sound even after the item is destroyed
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(
                pickupSound,
                transform.position,
                volume
            );
        }

        Destroy(gameObject);
    }
}
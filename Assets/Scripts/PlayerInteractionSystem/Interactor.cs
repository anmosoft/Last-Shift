using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 3f;
    public Color highlightColor = Color.yellow;

    IInteractable currentObject;
    Renderer[] currentRenderers;
    Color[][] originalColors;

   

    void Update()
    {
        CheckObject();

        if (Input.GetKeyDown(KeyCode.E) && currentObject != null)
        {
            currentObject.Interact();
        }
    }

    void CheckObject()
    {
        IInteractable foundObject = null;

        if (InteractorSource != null)
        {
            Ray ray = new Ray(
                InteractorSource.position,
                InteractorSource.forward
            );

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, InteractRange))
            {
                foundObject =
                    hit.collider.GetComponentInParent<IInteractable>();
            }
        }

        //when you can interact

        if (foundObject != currentObject)
        {
            RemoveHighlight();
            currentObject = foundObject;
            AddHighlight();

            if (currentObject != null)
            {
                SimpleUI.Instance.ShowInteract();
                Debug.Log("INTERACT");
            }
            else
            {
                SimpleUI.Instance.HidePrompt();
            }
        }
    }

    void AddHighlight()
    {
        if (currentObject == null)
            return;

        Component obj = currentObject as Component;

        if (obj == null)
            return;

        currentRenderers = obj.GetComponentsInChildren<Renderer>();
        originalColors = new Color[currentRenderers.Length][];

        for (int i = 0; i < currentRenderers.Length; i++)
        {
            Material[] materials = currentRenderers[i].materials;
            originalColors[i] = new Color[materials.Length];

            for (int j = 0; j < materials.Length; j++)
            {
                originalColors[i][j] = materials[j].color;
                materials[j].color = highlightColor;
            }
        }
    }

    void RemoveHighlight()
    {
        if (currentRenderers == null || originalColors == null)
            return;

        for (int i = 0; i < currentRenderers.Length; i++)
        {
            if (currentRenderers[i] == null)
                continue;

            Material[] materials = currentRenderers[i].materials;

            for (int j = 0; j < materials.Length &&
                            j < originalColors[i].Length; j++)
            {
                materials[j].color = originalColors[i][j];
            }
        }

        currentRenderers = null;
        originalColors = null;
    }
}
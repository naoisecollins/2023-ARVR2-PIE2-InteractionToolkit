using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RaycastExample : MonoBehaviour
{
    public float raycastDistance = 10f;
    public Material highlightMaterial; // The material to use for highlighting the object
private XRRayInteractor rayInteractor;
private GameObject lastHighlightedObject; // The last object that was highlighted

private void Start()
{
    // Get the XR Ray Interactor component on this GameObject
    rayInteractor = GetComponent<XRRayInteractor>();
}

private void Update()
{
    // Get the current RaycastHit
    RaycastHit hitInfo;
    bool hit = rayInteractor.TryGetCurrent3DRaycastHit(out hitInfo);

    if (hit)
    {
        // Highlight the object that was hit, if it is not the same object as the last highlighted object
        if (hitInfo.collider.gameObject != lastHighlightedObject)
        {
            // Remove the highlight from the last highlighted object, if there was one
            if (lastHighlightedObject != null)
            {
                RemoveHighlight(lastHighlightedObject);
            }

            // Add the highlight to the new object
            AddHighlight(hitInfo.collider.gameObject);

            // Remember the new object as the last highlighted object
            lastHighlightedObject = hitInfo.collider.gameObject;
        }

        // Do something with the hit information, such as print the name of the GameObject that was hit
        Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
    }
    else
    {
        // Remove the highlight from the last highlighted object, if there was one
        if (lastHighlightedObject != null)
        {
            RemoveHighlight(lastHighlightedObject);
            lastHighlightedObject = null;
        }
    }
}

private void AddHighlight(GameObject obj)
{
    // Add a highlight to the object
    Renderer renderer = obj.GetComponent<Renderer>();
    if (renderer != null)
    {
        renderer.material = highlightMaterial;
    }
}

private void RemoveHighlight(GameObject obj)
{
    // Remove the highlight from the object
    Renderer renderer = obj.GetComponent<Renderer>();
    if (renderer != null)
    {
        renderer.material = null;
    }
}
}
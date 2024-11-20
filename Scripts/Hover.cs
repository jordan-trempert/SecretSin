using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Hover : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    public List<GameObject> draggables = new List<GameObject>();
    public bool isInside = false;
    public List<GameObject> storedObjects = new List<GameObject>();
    public GameObject[] draggablePrefabs;
    public Countdown countdownTimer; // Reference to the countdown timer

    private void OnMouseExit()
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.enabled = false;
        }
        isInside = false;
    }

    private void OnMouseEnter()
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.enabled = true;
        }
        isInside = true;
    }

    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Populate draggables list
        GameObject[] draggableArray = GameObject.FindGameObjectsWithTag("Draggable");
        draggables.AddRange(draggableArray);

        // Ensure countdownTimer is set
        if (countdownTimer == null)
        {
            Debug.LogError("Countdown timer not assigned!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is a draggable
        if (other.CompareTag("Draggable"))
        {
            // Enable the SpriteRenderer when a draggable object hovers over
            if (spriteRenderers != null)
            {
                foreach (var renderer in spriteRenderers)
                {
                    renderer.enabled = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is a draggable
        if (other.CompareTag("Draggable"))
        {
            // Disable the SpriteRenderer when the draggable object leaves
            if (spriteRenderers != null)
            {
                foreach (var renderer in spriteRenderers)
                {
                    renderer.enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        // Check for mouse release
        if (Input.GetMouseButtonUp(0) && isInside) // 0 is the left mouse button
        {
            // Loop through all draggables to see if any are being dragged
            for (int i = draggables.Count - 1; i >= 0; i--) // Iterate backwards to safely remove items
            {
                GameObject obj = draggables[i];

                // Check if the object is null or has been destroyed
                if (obj == null)
                {
                    draggables.RemoveAt(i); // Remove null references
                    continue;
                }

                if (obj.GetComponent<Draggable>().isDragging)
                {
                    foreach (GameObject pref in draggablePrefabs)
                    {
                        if (pref.GetComponent<Tags>().name == obj.GetComponent<Tags>().name)
                        {
                            storedObjects.Add(pref);

                            // Stop countdown if 3 items are placed
                            if (storedObjects.Count == 3)
                            {
                                countdownTimer.stopCountdown();
                            }

                            Destroy(gameObject.GetComponent<BoxCollider2D>());
                            Destroy(obj); // Destroy the object if it's being dragged and released
                            draggables.RemoveAt(i); // Remove the destroyed object from the list
                            break;
                        }
                    }
                }
            }
        }
    }
}

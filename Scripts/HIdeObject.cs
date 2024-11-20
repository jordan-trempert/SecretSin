using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    public List<GameObject> objectsInside = new List<GameObject>(); // List to store objects that enter the container

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + " entered chest trigger");
    }
    private void OnTriggerStay(Collider other)
    {
        // Check if the object in the trigger has the 'Draggable' tag
        if (other.CompareTag("Draggable"))
        {
            Debug.Log("Draggable object is inside the trigger area: " + other.name);

            // Only add and destroy if the left mouse button is released
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Mouse button released over container. Adding object to list and destroying it: " + other.name);

                // Add the object to the list
                objectsInside.Add(other.gameObject);

                // Destroy the draggable object from the scene
                Destroy(other.gameObject);
            }
        }
        else
        {
            Debug.Log("An object inside the trigger is not draggable: " + other.name);
        }
    }
}

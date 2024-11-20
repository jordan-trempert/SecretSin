using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]

public class Draggable : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    public GameObject textBox;
    public bool isDragging;

    void OnMouseDown()
    {
        if (!textBox.activeSelf)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        }

    }

    private void OnMouseUp()
    {
        //isDragging = false;
    }

    void OnMouseDrag()
    {
        if (!textBox.activeSelf)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            isDragging = true;
        }


    }

}
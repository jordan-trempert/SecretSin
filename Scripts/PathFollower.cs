using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public List<Transform> nodes; // List of nodes to follow
    public float speed = 5f; // Movement speed
    public float fadeSpeed = 0.01f; // Speed at which the sprite fades out
    public GameObject man;
    public GameObject textBox;
    public string[] msgAfterFade;

    private Transform currentTarget; // Current target node
    private int currentNodeIndex = -1; // Index of the current node
    private Color tmp; // Temporary color variable for fading
    private bool isFading = false; // Flag to ensure FadeOut is only called once
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        if (nodes == null || nodes.Count == 0)
        {
            Debug.LogWarning("No nodes assigned for path following.");
            return;
        }

        // Find the closest node to start with
        currentNodeIndex = FindClosestNodeIndex();
        if (currentNodeIndex != -1)
        {
            currentTarget = nodes[currentNodeIndex];
        }

        // Initialize the color to the sprite's current color
        tmp = GetComponentInChildren<SpriteRenderer>().color;

        // Get the Animator component
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No Animator component found on the GameObject.");
        }
    }

    void Update()
    {
        if (currentTarget == null)
        {
            // Start fading out only once
            if (!isFading)
            {
                isFading = true;
                StopWalkingAnimation(); // Stop the walking animation immediately
                StartCoroutine(FadeOut());
            }
            return;
        }

        // Move towards the current target node
        MoveTowardsNode();

        // Check if the object reached the node
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            Destroy(currentTarget.gameObject); // Destroy the reached node
            SetNextNode(); // Move to the next node
        }
    }

    private int FindClosestNodeIndex()
    {
        int closestNodeIndex = -1;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < nodes.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, nodes[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNodeIndex = i;
            }
        }

        return closestNodeIndex;
    }

    private void MoveTowardsNode()
    {
        if (animator != null && !animator.GetBool("isWalking"))
        {
            StartWalkingAnimation(); // Start the walking animation only once
        }

        Vector3 direction = (currentTarget.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void SetNextNode()
    {
        nodes.RemoveAt(currentNodeIndex); // Remove the reached node

        if (nodes.Count == 0)
        {
            currentTarget = null; // No more nodes to follow
            return;
        }

        // Find the closest node from remaining nodes
        currentNodeIndex = FindClosestNodeIndex();
        currentTarget = nodes[currentNodeIndex];
    }

    private void StartWalkingAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }

    private void StopWalkingAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }

    IEnumerator FadeOut()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Fade out by gradually reducing the alpha value
        while (tmp.a > 0)
        {
            tmp.a -= 0.05f; // Increase the alpha decrement to make fading faster
            spriteRenderer.color = tmp; // Apply the new color
            man.GetComponentInChildren<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(fadeSpeed);
        }

        // Activate the text box and start typing the new messages
        textBox.SetActive(true); // Activate the text box
        textBox.GetComponentInChildren<Typewriter>().ResetText();

        // Destroy the current game objects
        Destroy(gameObject);
        Destroy(man);
    }


}

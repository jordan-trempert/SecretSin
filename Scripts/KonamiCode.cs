using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    private string konamiCode = "up up down down left right left right b a";
    private string userInput = "";
    private float inputDelay = 0.5f; // Delay between inputs to prevent too fast inputs
    private float lastInputTime;
    public Sprite secretSprite;

    void Update()
    {
        if (Time.time - lastInputTime > inputDelay)
        {
            userInput = ""; // Reset user input if the delay has passed
        }

        // Check for inputs
        if (Input.GetKeyDown(KeyCode.UpArrow))
            AddInput("up");
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            AddInput("down");
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            AddInput("left");
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            AddInput("right");
        else if (Input.GetKeyDown(KeyCode.B))
            AddInput("b");
        else if (Input.GetKeyDown(KeyCode.A))
            AddInput("a");
    }

    private void AddInput(string input)
    {
        lastInputTime = Time.time;
        userInput += input + " ";

        if (userInput.TrimEnd() == konamiCode)
        {
            Debug.Log("Konami Code Entered!");
            // Call any method you want to execute upon entering the code
            ActivateCheat();
            userInput = ""; // Reset the input after successful entry
        }
    }

    private void ActivateCheat()
    {
        // Implement the actions to take when the Konami Code is successfully entered
        //Debug.Log("Cheat activated!");
        this.GetComponent<SpriteRenderer>().sprite = secretSprite;
    }
}

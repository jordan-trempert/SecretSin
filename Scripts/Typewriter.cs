using TMPro;
using UnityEngine;
using System.Collections;

public class Typewriter : MonoBehaviour
{
    public TMP_Text txt;
    string story;
    public float delay = 0.125f;
    public bool isDone = false;
    public TMP_Text emoji;
    public string[] msgs;
    public int currentIndex = 0;
    public GameObject cont;
    public GameObject textBox;
    public GameObject infoBox;
    public GameObject woman;
    public Countdown countdownTimer;

    void Awake()
    {
        txt = GetComponent<TMP_Text>();
        story = msgs[currentIndex];
        txt.text = "";

        StartCoroutine(PlayText());
    }

    public void ResetText()
    {
        if (msgs != null && msgs.Length > 0 && currentIndex < msgs.Length)
        {
            story = msgs[currentIndex];
            txt.text = "";
            isDone = false;
            StartCoroutine(PlayText());
        }
    }
    public void StartTyping(string[] newMessages)
    {
        Debug.Log(msgs);
        msgs = newMessages; // Set the new messages
        Debug.Log(msgs);
        currentIndex = 0; // Reset current index to start from the first message
        txt.text = ""; // Clear the current text
        StartCoroutine(PlayText()); // Start typing the new messages
    }


    public void Update()
    {
        

        // Advance messages on Z key
        if (Input.GetKeyUp(KeyCode.Z) && isDone && (emoji.gameObject.activeSelf || currentIndex > 0))
        {
            currentIndex++;
            if (currentIndex < msgs.Length && currentIndex != 5 && this.isActiveAndEnabled)
            {
                ResetText();
            }
            else if (currentIndex >= msgs.Length)
            {
                textBox.SetActive(false);
                infoBox.SetActive(true);

            }
            else
            {
                textBox.SetActive(false);
                woman.GetComponentInChildren<SpriteRenderer>().flipX = false;
                woman.GetComponent<PathFollower>().enabled = true;
            }
        }
        // Color update based on index
        if (currentIndex == 0 || currentIndex == 2)
        {
            SetTextColor("#FF015C");
        }
        else
        {
            SetTextColor("#019CFF");
        }

        // Display emoji after the first message
        if (isDone && currentIndex == 0)
        {
            StartCoroutine(SetEmoji());
        }
        else
        {
            emoji.gameObject.SetActive(false);
        }
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(delay);
        }
        isDone = true;
    }

    IEnumerator SetEmoji()
    {
        yield return new WaitForSeconds(delay);
        emoji.gameObject.SetActive(true);
    }

    private void SetTextColor(string colorCode)
    {
        if (ColorUtility.TryParseHtmlString(colorCode, out Color newColor))
        {
            txt.color = newColor;
            emoji.color = newColor;
            cont.GetComponent<SpriteRenderer>().color = newColor;
            cont.GetComponentInChildren<TMP_Text>().color = newColor;
        }
    }
}

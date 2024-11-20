using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI tmpText; // Reference to TMP text object
    private float countdownTime = 10f; // Starting countdown time in seconds
    public bool countdownStarted = false;
    public GameObject infoBox;
    public TextMeshProUGUI bestTime;

    void Start()
    {
        if (tmpText == null)
        {
            Debug.LogError("TMP Text reference not set!");
        }

        string filePath = Path.Combine(Application.persistentDataPath, "save.txt");
        if (File.Exists(filePath))
        {
            bestTime.text = File.ReadAllText(filePath);
        }
    }

    public void setCountdownState(bool isOn)
    {
        countdownStarted = isOn;
    }

    void Update()
    {
        if (countdownStarted)
        {
            infoBox.SetActive(false);
            // Reduce countdown time by the elapsed time
            countdownTime -= Time.deltaTime;

            // Ensure countdown time doesn't go below 0
            countdownTime = Mathf.Max(countdownTime, 0);

            // Convert time to seconds and fractional part
            int seconds = Mathf.FloorToInt(countdownTime);
            int fractional = Mathf.FloorToInt((countdownTime - seconds) * 1000); // Milliseconds as a whole number

            // Combine into 3 digits
            if (seconds > 0)
            {
                // Show 1 second digit + 2 fractional digits
                tmpText.text = $"{seconds}.{fractional / 10:D2}";
            }
            else
            {
                // Show 3 fractional digits
                tmpText.text = $"0.{fractional:D3}";
                SceneManager.LoadScene("GameOver");
            }
        }
       
    }

    public void stopCountdown()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "save.txt");

        // Try to parse the current best time, if it's not a valid float, treat it as an invalid time.
        float bestTimeNum = 0f;
        if (bestTime.text != "-.--" && float.TryParse(bestTime.text, out bestTimeNum))
        {
            bestTimeNum = float.Parse(bestTime.text); // Parsing valid best time
        }
        else
        {
            bestTimeNum = float.MaxValue; // Initialize to a large number if no valid best time
        }

        // Parse the current countdown time
        float currentTimeNum = 0f;
        if (float.TryParse(tmpText.text, out currentTimeNum))
        {
            // If current time is valid, compare with best time
            setCountdownState(false);
            if (bestTime.text == "-.--" || bestTimeNum < currentTimeNum)
            {
                bestTime.text = tmpText.text;
                File.WriteAllText(filePath, bestTime.text);
            }
        }
        else
        {
            Debug.LogError("Current time is not a valid float.");
        }
        SceneManager.LoadScene("Win");
    }


}

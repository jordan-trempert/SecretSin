using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameObject heldObject;
    public List<GameObject> hideableObjects;
    public Countdown countdown;

    private void Awake()
    {
        // Singleton pattern for GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(int sceneNum)
    {
        if(sceneNum == 0)
        {
            SceneManager.LoadScene("Main Menu");
        }
        if (sceneNum == 1)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (sceneNum == 2)
        {
            SceneManager.LoadScene("Win");
        }
        if (sceneNum == 3)
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void SetHeldObject(GameObject obj)
    {
        heldObject = obj;
    }

    public GameObject GetHeldObject()
    {
        return heldObject;
    }

    private void Update()
    {
        foreach(GameObject hideable in hideableObjects)
        {
            if (hideable == null)
            {
                hideableObjects.Remove(hideable);
            }
        }
        if(hideableObjects.Count == 0)
        {
            countdown.stopCountdown();
        }
    }
}

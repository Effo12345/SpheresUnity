using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    Button button;
    public string buttonType;
    int nextBuildIndex;
    [SerializeField] List<int> lockIconIndex;
    public List<GameObject> lockIcons;
    public List<GameObject> levelSelectButtons;
    public List<GameObject> mainMenuButtons;
    public GameObject lockContainer;

    public string levelToSelect;
    public GameObject fadeOutCanvas;

    public GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonFunctions);
        //serialization = GameObject.Find("Serializer").GetComponent<Serialization>();
        nextBuildIndex = Serialization.nextBuildIndex;

        int i = 0;
        while (i <= 5)
        {
            lockIconIndex.Add(i);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonFunctions()
    {
        Invoke(buttonType, 0.0f);
    }

    void Play()
    {
        FadeOut();
        SceneManager.LoadScene(nextBuildIndex, LoadSceneMode.Single);
    }

    void LevelSelect()
    {
        lockContainer.SetActive(true);

        foreach (var number in lockIconIndex)
        {
            if (number <= nextBuildIndex)
            {
                GameObject button = levelSelectButtons[number];
                button.SetActive(true);
            }

            if (number < nextBuildIndex - 1)
            {
                GameObject icon = lockIcons[number];
                icon.SetActive(false);
            }
        }

        foreach (var button in mainMenuButtons)
        {
            button.SetActive(false);
        }

        backButton.SetActive(true);
    }

    void Tutorial()
    {

    }


 void LevelSelectNumber()
    {
        FadeOut();

        SceneManager.LoadScene(levelToSelect, LoadSceneMode.Single);
    }

    void BackButton()
    {
        lockContainer.SetActive(false);

        foreach (var button in levelSelectButtons)
        {
            button.SetActive(false);
        }

        foreach (var button in mainMenuButtons)
        {
            button.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    void BackToGame()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.UndoMenu();
    }

    void MainMenu()
    {
        FadeOut();

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    void Close()
    {
        FadeOut();

        Application.Quit();
    }    


    void FadeOut()
    {
        fadeOutCanvas.SetActive(true);
        StartCoroutine(waitForFade());
    }


    IEnumerator waitForFade()
    {
        yield return new WaitForSeconds(3);
    }
}

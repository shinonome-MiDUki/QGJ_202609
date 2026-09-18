using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;


public interface IExecutable
{
    void First_BTN();
    void Second_BTN();
    void Third_BTN();
    List<string> GetUiStr();

    void Quit_BTN()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
    #else
        Application.Quit(); 
    #endif 
    }

    void Tutorial_Mode_BTN()
    {
        TutorialMode.is_tutorial_mode = true;
        SceneManager.LoadScene("MainScene");
    }
}

public class FirstStart : IExecutable
{
    private readonly Tutorial tutorial;

    public FirstStart(Tutorial tutorial)
    {
        this.tutorial = tutorial;
    }

    public void First_BTN()
    {
        DifficultyControl.difficulty = 0;
        TutorialMode.is_tutorial_mode = false;
        SceneManager.LoadScene("MainScene");
    }

    public void Second_BTN()
    {
        TutorialMode.is_tutorial_mode = true;
        SceneManager.LoadScene("MainScene");
    }

    public void Third_BTN()
    {
        if (tutorial == null)
        {
            return;
        }
        tutorial.ShowTutorial();
    }

    public List<string> GetUiStr()
    {
        return new List<string>()
        {
            "スタート", "チュートリアルモード", "チュートリアル"
        };
    }
}

public class LaterStart : IExecutable
{
    public void First_BTN()
    {
        DifficultyControl.difficulty = 0;
        SceneManager.LoadScene("MainScene");
    }

    public void Second_BTN()
    {
        DifficultyControl.difficulty = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void Third_BTN()
    {
        DifficultyControl.difficulty = 2;
        SceneManager.LoadScene("MainScene");
    }

    public List<string> GetUiStr()
    {
        return new List<string>()
        {
            "易", "中", "難"
        };
    }
}

public class Starting : MonoBehaviour
{    
    private static bool is_first_start = true;
    [SerializeField] private AudioSource bgm_audiosource;
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private Button quit_btn;
    [SerializeField] private Button tutorial_mode_btn;

    void Start()
    {
        bgm_audiosource.Stop();
        bgm_audiosource.Play();
        bgm_audiosource.loop = true;

        IExecutable obj = is_first_start ? new FirstStart(tutorial) : new LaterStart();
        
        Button first_btn = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
        first_btn.onClick.AddListener(obj.First_BTN);
        first_btn.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetUiStr()[0];
        Button second_btn = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
        second_btn.onClick.AddListener(obj.Second_BTN);
        second_btn.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetUiStr()[1];
        Button third_btn = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
        third_btn.onClick.AddListener(obj.Third_BTN);
        third_btn.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetUiStr()[2];
        quit_btn.onClick.AddListener(obj.Quit_BTN);
        if (!is_first_start)
        {
            tutorial_mode_btn.enabled = true;
            tutorial_mode_btn.onClick.AddListener(obj.Tutorial_Mode_BTN);
        }
        else
        {
            tutorial_mode_btn.enabled = false;
        }

        is_first_start = false;
    }
}

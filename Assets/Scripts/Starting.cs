using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;


public interface IExecutable
{
    void First_BTN();
    void Second_BTN();
    void Third_BTN();
    List<string> GetUiStr();
}

public class FirstStart : IExecutable
{
    public void First_BTN()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Second_BTN()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Third_BTN()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
    #else
        Application.Quit(); 
    #endif 
    }

    public List<string> GetUiStr()
    {
        return new List<string>()
        {
            "スタート", "チュートリアル", "終了"
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

    void Start()
    {
        IExecutable obj = is_first_start ? new FirstStart() : new LaterStart();
        
        Button first_btn = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
        first_btn.onClick.AddListener(obj.First_BTN);
        first_btn.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetUiStr()[0];
        Button second_btn = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
        second_btn.onClick.AddListener(obj.Second_BTN);
        second_btn.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetUiStr()[1];
        Button third_btn = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
        third_btn.onClick.AddListener(obj.Third_BTN);
        third_btn.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetUiStr()[2];

        is_first_start = false;
    }
}


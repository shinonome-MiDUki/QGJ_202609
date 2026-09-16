
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Ending : MonoBehaviour
{
    [SerializeField] private Sprite game_over_sprite;
    [SerializeField] private Sprite game_success_sprite;
    [SerializeField] private UnityEngine.UI.Image bg;
    [SerializeField] private TMP_Text result_text;

    public void GoToEndScene()
    {
        float current_rating = ((float)ScoreSystem.current_comments[0] / ScoreSystem.current_comments[1]);
        print(UtilVar.is_success);
        if (UtilVar.is_success)
        {
            bg.sprite = game_success_sprite;
            result_text.text = "おめでとう!!!\n\n" 
                + "残高 : " + ScoreSystem.current_money.ToString() + "円\n"
                + "口コミ : " + current_rating.ToString("F1");
        }
        else
        {
            bg.sprite = game_over_sprite;
            result_text.text = "GAME OVER\n\n" 
                + "残高 : 0円" + "\n"
                + "口コミ : " + current_rating.ToString("F1");
        }
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Starting");
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // エディタ実行時は停止
    #else
            Application.Quit(); // 本番ビルド時はアプリ終了
    #endif
    }
}

using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class Ending : MonoBehaviour
{
    [SerializeField] private Sprite game_over_sprite;
    [SerializeField] private Sprite game_success_sprite;
    [SerializeField] private Sprite game_success_withloss_sprite;
    [SerializeField] private UnityEngine.UI.Image bg;
    [SerializeField] private TMP_Text result_text;
    [SerializeField] private AudioSource bgm_audiosource;
    [SerializeField] private SoundAssetRef soundAssetRef;

    public void Start()
    {
        bgm_audiosource.Stop();
        float current_rating = ((float)ScoreSystem.current_comments[0] / ScoreSystem.current_comments[1]);
        print(UtilVar.is_success);
        if (UtilVar.is_success)
        {
            bgm_audiosource.clip = soundAssetRef.ending_bgm;
            bgm_audiosource.Play();
            bgm_audiosource.loop = false;
            StartCoroutine(PlayNextBgm());
            if (ScoreSystem.current_money > UtilVar.init_capital)
            {
                bg.sprite = game_success_sprite;
                result_text.text = "おめでとう!!!\n\n" 
                    + "残高 : " + ScoreSystem.current_money.ToString() + "円\n"
                    + "口コミ : " + current_rating.ToString("F1"); 
            }
            else
            {
                bg.sprite = game_success_withloss_sprite;
                result_text.text = "なんとか生き延びた\n\n" 
                    + "残高 : " + ScoreSystem.current_money.ToString() + "円\n"
                    + "口コミ : " + current_rating.ToString("F1"); 
            }
            
        }
        else
        {
            bgm_audiosource.clip = soundAssetRef.game_over_bgmse;
            bgm_audiosource.Play();
            bg.sprite = game_over_sprite;
            result_text.text = "GAME OVER\n\n" 
                + "残高 : 0円" + "\n"
                + "口コミ : " + current_rating.ToString("F1");
        }
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // エディタ実行時は停止
    #else
            Application.Quit(); // 本番ビルド時はアプリ終了
    #endif
    }

    IEnumerator PlayNextBgm()
    {
        float first_bgm_len = soundAssetRef.ending_bgm.length;
        yield return new WaitForSeconds(first_bgm_len);
        bgm_audiosource.Stop();
        bgm_audiosource.clip = soundAssetRef.ending_loop_bgm;
        bgm_audiosource.Play();
        bgm_audiosource.loop = true;
    }
}

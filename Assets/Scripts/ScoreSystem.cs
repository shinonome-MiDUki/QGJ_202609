using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreSystem : MonoBehaviour
{
    float score = 0f;
    float money = 0f;
    float star = 0f;

    // ランクごとの報酬金額を定義
    Dictionary<int, int> ranktomoney = new Dictionary<int, int>()
    {
        {1, 100},
        {2, 200},
        {3, 300},
        {4, 400},
        {5, 500}
    };
    // 調理完了時に呼び出し
    public void checkscore(int rank, float order_time)
    {
        AddMoney(rank);
        if (order_time > 0)
        {
            RemoveScore(rank, order_time);
        }
        else
        {
            AddScore(rank);
        }
        AddScore(rank);
        
    }
    void AddScore(int rank)
    {
        score += rank;
        PlayerPrefs.SetFloat("score", score);
        PlayerPrefs.Save(); 

        Judgescore(score);
    }

    void AddMoney(int rank)
    {
        if (ranktomoney.ContainsKey(rank))
        {
            money += ranktomoney[rank];
            PlayerPrefs.SetFloat("money", money);
            PlayerPrefs.Save(); 
        }
        else
        {
            Debug.LogWarning("存在しないランクです: " + rank);
        }
    }
    void RemoveScore(int rank, float order_time)
    {
        score -= rank;
        if (score < 0) score = 0; // スコアが負にならないようにする
        PlayerPrefs.SetFloat("score", score);
        PlayerPrefs.Save(); 
    }
    //スコアから星の数を判定する関数
    void Judgescore(float currentScore)
    {
        if (currentScore >= 1000)
        {
            star = 3f;
        }
        else if (currentScore >= 800)
        {
            star = 2.5f;
        }
        else if (currentScore >= 600)
        {
            star = 2f;
        }
        else if (currentScore >= 400)
        {
            star = 1f;
        }
        else
        {
            star = 0.5f;
        }

        PlayerPrefs.SetFloat("star", star);
        PlayerPrefs.Save();
    }
}
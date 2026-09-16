using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;

public class ScoreSystem : MonoBehaviour
{

    private static float score = 0.0f;
    private static float money = 0.0f;
    private static float star = 0.0f;
    private static int[] comments = new int[2];
    [SerializeField] private UtilVar utilVar;

    public void SolveResult(
        List<int> ordered_toppings,
        List<int> applied_toppings,
        int ordered_status,
        int applied_status,
        float waiting_time
    )
    {
        List<int>[] toppings_comparison = new List<int>[2];
        toppings_comparison = CompareToppings(ordered_toppings, applied_toppings);
        if (toppings_comparison[0].Count > 0 
            || toppings_comparison[1].Count > 0
            || !CompareEggStatus(ordered_status, applied_status)
        )
        {
            return;
        }
        float income = 0.0f;
        foreach (int x in ordered_toppings)
        {
            EggCommonParam.ToppingsType toppingsType = (EggCommonParam.ToppingsType)Enum.ToObject(typeof(EggCommonParam.ToppingsType), x);
            income += utilVar.topping_price[toppingsType];
        }
        EggCommonParam.EggStatusIndex eggStatusIndex = (EggCommonParam.EggStatusIndex)Enum.ToObject(typeof(EggCommonParam.EggStatusIndex), ordered_status);
        income += utilVar.egg_status_price[eggStatusIndex];

        money += income;

        comments[0] = comments[0] + CompareTime(waiting_time, ordered_status);
        comments[1] = comments[1] + 1;
    }

    private List<int>[] CompareToppings(
        List<int> ordered_toppings,
        List<int> applied_toppings
    )
    {
        List<int>[] rtn = new List<int>[2];
        rtn[0] = ordered_toppings.Except(applied_toppings).ToList();
        rtn[1] = applied_toppings.Except(ordered_toppings).ToList();
        return rtn;
    }

    private bool CompareEggStatus(
        int ordered_status,
        int applied_status
    )
    {
        return ordered_status == applied_status;
    }
    
    private int CompareTime(
        float waiting_time,
        int ordered_status
    )
    {
        float standard_time = (utilVar.play_time / 4) * ordered_status;
        int multiply = Convert.ToInt32(Math.Floor(waiting_time / standard_time));
        multiply = Math.Clamp(multiply, 2, 6);
        int personal_star = 7 - multiply;
        return personal_star;
    }

    


    Dictionary<int, int> rank2money = new Dictionary<int, int>()
    {
        {1, 100},
        {2, 200},
        {3, 300},
        {4, 400},
        {5, 500}
    };

    public void CheckScore(int rank, float order_time)
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
        if (rank2money.ContainsKey(rank))
        {
            money += rank2money[rank];
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

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;

public class ScoreSystem : MonoBehaviour
{

    protected static float current_money = 500.0f;
    protected static int[] current_comments = new int[2]{5, 1};
    [SerializeField] private UtilVar utilVar;

    protected void SolveResult(
        List<int> ordered_toppings,
        List<int> applied_toppings,
        int ordered_status,
        int applied_status,
        float waiting_time
    )
    {
        List<int>[] toppings_comparison = new List<int>[2];
        toppings_comparison = CompareToppings(ordered_toppings, applied_toppings);
        float income = 0.0f;

        if (!(toppings_comparison[0].Count > 0 
            || toppings_comparison[1].Count > 0
            || !CompareEggStatus(ordered_status, applied_status))
        )
        {
            foreach (int x in ordered_toppings)
            {
                EggCommonParam.ToppingsType toppingsType = (EggCommonParam.ToppingsType)Enum.ToObject(typeof(EggCommonParam.ToppingsType), x);
                income += utilVar.topping_price[toppingsType];
            }
            EggCommonParam.EggStatusIndex eggStatusIndex = (EggCommonParam.EggStatusIndex)Enum.ToObject(typeof(EggCommonParam.EggStatusIndex), ordered_status);
            income += utilVar.egg_status_price[eggStatusIndex];
        }

        current_money += income - utilVar.egg_cost;

        current_comments[0] = current_comments[0] + CompareTime(waiting_time, ordered_status);
        current_comments[1] = current_comments[1] + 1;
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
        float multiply = Convert.ToInt32(Math.Floor(waiting_time / standard_time));
        multiply = Math.Clamp(multiply, 1, 2);
        int personal_star = (int)Math.Round((5 - ((multiply - 1) * 5)));
        print(personal_star);
        return personal_star;
    }

    public void LoseNEggs(int n)
    {
        current_money -= utilVar.egg_cost * n;
    }

}

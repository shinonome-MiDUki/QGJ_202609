using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;
using Unity.VisualScripting;

public class ScoreSystem : MonoBehaviour
{

    public static float current_money = 500.0f;
    public static int[] current_comments = new int[2]{5, 1};
    [SerializeField] protected UtilVar utilVar;
    [SerializeField] private AudioSource se_audiosource;
    [SerializeField] private SoundAssetRef soundAssetRef;

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
            if (ordered_toppings != null)
            {
                foreach (int x in ordered_toppings)
                {
                    EggCommonParam.ToppingsType toppingsType = (EggCommonParam.ToppingsType)Enum.ToObject(typeof(EggCommonParam.ToppingsType), x);
                    income += utilVar.topping_price[toppingsType];
                }
            }
            EggCommonParam.EggStatusIndex eggStatusIndex = (EggCommonParam.EggStatusIndex)Enum.ToObject(typeof(EggCommonParam.EggStatusIndex), ordered_status);
            income += utilVar.egg_status_price[eggStatusIndex];
            se_audiosource.PlayOneShot(soundAssetRef.offering_se);
        }
        else
        {
            se_audiosource.PlayOneShot(soundAssetRef.offering_se);
        }

        current_money += income ;

        current_comments[0] = current_comments[0] + CompareTime(waiting_time, ordered_status);
        current_comments[1] = current_comments[1] + 1;
    }

    private List<int>[] CompareToppings(
        List<int> ordered_toppings,
        List<int> applied_toppings
    )
    {
        List<int> safeOrdered = ordered_toppings ?? new List<int>();
        List<int> safeApplied = applied_toppings ?? new List<int>();

        List<int>[] rtn = new List<int>[2];
        rtn[0] = safeOrdered.Except(safeApplied).ToList();
        rtn[1] = safeApplied.Except(safeOrdered).ToList();
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
        float standard_time = (utilVar.per_egg_time / 4) * ordered_status;
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

    public void LoseRating(int added_rating)
    {
        current_comments[0] = current_comments[0] + added_rating;
        current_comments[1] = current_comments[1] + 1;
    }

}

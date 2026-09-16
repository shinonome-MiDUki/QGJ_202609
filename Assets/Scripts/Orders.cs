using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Orders : MonoBehaviour
{
    private EggCommonParam.EggStatusIndex ordered_egg_status;
    private List<EggCommonParam.ToppingsType> ordered_toppings_list;
    private List<int> ordered_toppings_idx_list;
    private float time_elapsed = 0.0f;
    private bool is_timer_working = false;
    void Start()
    {
        ordered_egg_status = GetRandomEggStatus();
        ordered_toppings_list = GetRandomToppingTypes();
        TMP_Text order_text = this.transform.Find("order_content").GetComponent<TMP_Text>();
        order_text.text = ordered_egg_status + "\n\n" + String.Join("\n", ordered_toppings_list);
        time_elapsed = 0.0f;
        is_timer_working = true;
    }

    void Update()
    {
        if (!is_timer_working)
        {
            return;
        }
        time_elapsed += Time.deltaTime;
    }

    private EggCommonParam.EggStatusIndex GetRandomEggStatus()
    {
        int ordered_egg_status_idx = UnityEngine.Random.Range(
            0, Enum.GetNames(typeof(EggCommonParam.EggStatusIndex)).Length
        );
        while(ordered_egg_status_idx == 0 || ordered_egg_status_idx == 5)
        {
            ordered_egg_status_idx = UnityEngine.Random.Range(
                0, Enum.GetNames(typeof(EggCommonParam.EggStatusIndex)).Length
            );
        }
        EggCommonParam.EggStatusIndex ordered_egg_status = (EggCommonParam.EggStatusIndex)Enum.ToObject(typeof(EggCommonParam.EggStatusIndex), ordered_egg_status_idx);
        return ordered_egg_status;
    }

    private List<EggCommonParam.ToppingsType> GetRandomToppingTypes()
    {
        List<EggCommonParam.ToppingsType> ordered_toppings_list = new List<EggCommonParam.ToppingsType>();
        List<int> ordered_already = new List<int>();
        int toppings_count = UnityEngine.Random.Range(
            0, Enum.GetNames(typeof(EggCommonParam.ToppingsType)).Length - 1
        );

        if (toppings_count == 0)
        {
            return ordered_toppings_list;
        }

        for (int i = 0; i < toppings_count; i++)
        {
            int ordered_topping_idx = UnityEngine.Random.Range(
                0, Enum.GetNames(typeof(EggCommonParam.ToppingsType)).Length
            );
            while(ordered_topping_idx == 5 
                || ordered_already.Contains(ordered_topping_idx))
            {
                ordered_topping_idx = UnityEngine.Random.Range(
                    0, Enum.GetNames(typeof(EggCommonParam.ToppingsType)).Length
                );
            }
            ordered_already.Add(ordered_topping_idx);
            ordered_toppings_idx_list.Add(ordered_topping_idx);
            ordered_toppings_list.Add((EggCommonParam.ToppingsType)Enum.ToObject(typeof(EggCommonParam.ToppingsType), ordered_topping_idx));
        }
           
        return ordered_toppings_list;
    }

    public List<int> GetOrderedToppings()
    {
        return ordered_toppings_idx_list;
    }

    public EggCommonParam.EggStatusIndex GetOrderedEggStatus()
    {
        return ordered_egg_status;
    }

    public float StopAndGetTimerTime()
    {
        is_timer_working = false;
        return time_elapsed;
    }
}

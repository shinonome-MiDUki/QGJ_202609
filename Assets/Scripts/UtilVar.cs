using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilVar", menuName = "UtilVar")]
public class UtilVar : ScriptableObject
{
    public Dictionary<EggCommonParam.ToppingsType, string> toppings_name_e2j= new Dictionary<EggCommonParam.ToppingsType, string>()
    {
       { EggCommonParam.ToppingsType.KETCHUP, "ケチャップ"},
       { EggCommonParam.ToppingsType.MAYONAISE, "マヨネーズ"},
       { EggCommonParam.ToppingsType.PEPPER, "黒こしょう"},
       { EggCommonParam.ToppingsType.SOYSAUCE, "醤油"},
       { EggCommonParam.ToppingsType.SYRUP, "メープル"}
    };

    public Dictionary<EggCommonParam.EggStatusIndex, string> egg_status_name_e2j= new Dictionary<EggCommonParam.EggStatusIndex, string>()
    {
       {EggCommonParam.EggStatusIndex.RAW, "生"},
       {EggCommonParam.EggStatusIndex.HALF, "半熟"},
       {EggCommonParam.EggStatusIndex.COOKED, "かため"},
       {EggCommonParam.EggStatusIndex.BURNT, "焦げ"}
    };
    public Dictionary<EggCommonParam.ToppingsType, float> topping_price = new Dictionary<EggCommonParam.ToppingsType, float>()
    {
       { EggCommonParam.ToppingsType.KETCHUP, 25.0f},
       { EggCommonParam.ToppingsType.MAYONAISE, 25.0f},
       { EggCommonParam.ToppingsType.PEPPER, 25.0f},
       { EggCommonParam.ToppingsType.SOYSAUCE, 25.0f},
       { EggCommonParam.ToppingsType.SYRUP, 25.0f}
    };
    public Dictionary<EggCommonParam.EggStatusIndex, float> egg_status_price= new Dictionary<EggCommonParam.EggStatusIndex, float>()
    {
       {EggCommonParam.EggStatusIndex.RAW, 130.0f},
       {EggCommonParam.EggStatusIndex.HALF, 160.0f},
       {EggCommonParam.EggStatusIndex.COOKED, 200.0f},
       {EggCommonParam.EggStatusIndex.BURNT, 100.0f}
    };
    public float egg_cost = 30.0f;
    public float play_time = 60.0f;
    public float game_time_lim = 180.0f;
    [HideInInspector] public static bool is_success = true;
}

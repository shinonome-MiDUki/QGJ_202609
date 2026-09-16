using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilVar", menuName = "UtilVar")]
public class UtilVar : ScriptableObject
{
    public Dictionary<EggCommonParam.ToppingsType, string> toppings_name_e2j= new Dictionary<EggCommonParam.ToppingsType, string>()
    {
       { EggCommonParam.ToppingsType.KETCHUP, "ケチャップ"},
       { EggCommonParam.ToppingsType.MAYONAISE, "マヨネーズ"},
       { EggCommonParam.ToppingsType.PEPPER, "ブラックペッパー"},
       { EggCommonParam.ToppingsType.SOYSAUCE, "醤油"},
       { EggCommonParam.ToppingsType.SYRUP, "シロップ"}
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
       { EggCommonParam.ToppingsType.KETCHUP, 10.0f},
       { EggCommonParam.ToppingsType.MAYONAISE, 10.0f},
       { EggCommonParam.ToppingsType.PEPPER, 10.0f},
       { EggCommonParam.ToppingsType.SOYSAUCE, 10.0f},
       { EggCommonParam.ToppingsType.SYRUP, 10.0f}
    };
    public Dictionary<EggCommonParam.EggStatusIndex, float> egg_status_price= new Dictionary<EggCommonParam.EggStatusIndex, float>()
    {
       {EggCommonParam.EggStatusIndex.RAW, 40.0f},
       {EggCommonParam.EggStatusIndex.HALF, 50.0f},
       {EggCommonParam.EggStatusIndex.COOKED, 60.0f},
       {EggCommonParam.EggStatusIndex.BURNT, 30.0f}
    };
    public float egg_cost = 30.0f;
    public float play_time = 60.0f;
}

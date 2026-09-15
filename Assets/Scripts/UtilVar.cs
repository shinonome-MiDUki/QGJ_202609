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

    public Dictionary<Egg.EggStatusIndex, string> egg_status_name_e2j= new Dictionary<Egg.EggStatusIndex, string>()
    {
       {Egg.EggStatusIndex.RAW, "生"},
       {Egg.EggStatusIndex.HALF, "半熟"},
       {Egg.EggStatusIndex.COOKED, "かため"},
       {Egg.EggStatusIndex.BURNT, "焦げ"}
    };
}

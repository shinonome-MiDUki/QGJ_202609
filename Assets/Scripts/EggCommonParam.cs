using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EggCommonParam", menuName = "EggCommonParam")]
public class EggCommonParam : ScriptableObject
{
    public List<Sprite> topping_list;
    public enum ToppingsType
    {
        KETCHUP = 0,
        SOYSAUCE = 1,
        PEPPER = 2,
        MAYONAISE = 3,
        SYRUP = 4,
        NONE = 5
    };
    public ToppingsType current_active_topping = ToppingsType.NONE;
    public Sprite[] egg_status_image = new Sprite[5];
}

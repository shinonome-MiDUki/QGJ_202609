using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ToppingsScatterManager : Egg
{
    protected override void Start()
    {

        Transform children = this.gameObject.GetComponentInChildren<Transform>();
        if (children.childCount == 0) {
            return;
        }
        foreach(Transform ob in children) {
            ob.gameObject.SetActive(false);
        }

        this.transform.parent.gameObject.GetComponent<Button>().onClick.AddListener(OnEggClicked);
    }

    public void AddTopping(EggCommonParam.ToppingsType toppings_type)
    {
        int toppings_idx = (int)toppings_type;
        GameObject topping_gobj = this.transform.GetChild(toppings_idx).gameObject;
        topping_gobj.SetActive(true);
    }

    private void OnEggClicked()
    {
        string current_parent_gobj_name = this.transform.parent.parent.gameObject.name;
        bool is_parsed = int.TryParse(current_parent_gobj_name.Split('_').Last(), out int my_idx);
        my_idx = is_parsed ? my_idx : 0;

        if (applied_toppings.Contains((int)param.current_active_topping)){
            return;
        }
        if (egg_status[my_idx] == EggCommonParam.EggStatusIndex.NO_EGG || egg_status[my_idx] == EggCommonParam.EggStatusIndex.UNBROKEN)
        {
            return;
        }

        if (!is_focused[my_idx])
        {
            return;
        }
        if (param.current_active_topping == EggCommonParam.ToppingsType.NONE)
        {
            return;
        }

        AddTopping(param.current_active_topping);
        applied_toppings.Add((int)param.current_active_topping);
        param.current_active_topping = EggCommonParam.ToppingsType.NONE;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ToppingsScatterManager : Egg
{
    private EggCommonParam param;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        //base.Start();

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
        if (param.current_active_topping == EggCommonParam.ToppingsType.NONE)
        {
            return;
        }
        AddTopping(param.current_active_topping);
        applied_toppings.Add((int)param.current_active_topping);
    }
}

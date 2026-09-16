using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToppingsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject toppings_prefab;
    [SerializeField] private EggCommonParam param;

    protected virtual void Start()
    {
        for (int i = 0; i < param.topping_list.Count; i++)
        {
            GameObject topping_inst = Instantiate(
                toppings_prefab, Vector3.zero, Quaternion.identity, this.transform
            );
            EggCommonParam.ToppingsType toppingsType = (EggCommonParam.ToppingsType)i;
            topping_inst.name = toppingsType.ToString();
            Image btn_image = topping_inst.GetComponent<Button>().image;
            btn_image.sprite = param.topping_list[i];
            btn_image.SetNativeSize();
            RectTransform btn_rt = topping_inst.GetComponent<RectTransform>();
            if (btn_rt != null)
            {
                btn_rt.anchoredPosition = new Vector3(130.0f, i * -150.0f + 960.0f, 0.0f);
                btn_rt.localScale = new Vector3(0.07f, 0.07f, 0.07f);
            }
        }
    }

}

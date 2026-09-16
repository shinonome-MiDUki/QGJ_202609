using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class Toppings : MonoBehaviour
{
    public EggCommonParam param;
    void Start()
    {
        Button topping_btn = this.GetComponent<Button>();
        topping_btn.onClick.AddListener(OnClicked);

        EventTrigger topping_button_evt = topping_btn.gameObject.GetComponent<EventTrigger>();
        if (topping_button_evt == null)
        {
            topping_button_evt = topping_btn.gameObject.AddComponent<EventTrigger>();
        }
        if (topping_button_evt.triggers == null)
        {
            topping_button_evt.triggers = new List<EventTrigger.Entry>();
        }

        // EventTrigger.Entry entry_enter = new EventTrigger.Entry();
        // if (entry_enter == null)
        // {
        //     entry_enter = new EventTrigger.Entry();
        // }
        // entry_enter.eventID = EventTriggerType.PointerEnter;
        // entry_enter.callback.AddListener((data) => ShowNote((PointerEventData)data));
        // topping_button_evt.triggers.Add(entry_enter);

        // EventTrigger.Entry entry_exit = new EventTrigger.Entry();
        // if (entry_exit == null)
        // {
        //     entry_exit = new EventTrigger.Entry();
        // }
        // entry_enter.eventID = EventTriggerType.PointerExit;
        // entry_exit.callback.AddListener((data) => HideNote((PointerEventData)data));
        // topping_button_evt.triggers.Add(entry_exit);

        TMP_Text btn_text = topping_btn.GetComponentInChildren<TMP_Text>();
        btn_text.enabled = false;
    }

    private void OnClicked()
    {
        EggCommonParam.ToppingsType clicked_topping = (EggCommonParam.ToppingsType)Enum.Parse(typeof(EggCommonParam.ToppingsType), this.name);
        param.current_active_topping = clicked_topping;        
    }

    private void ShowNote(PointerEventData data)
    {
        Button topping_btn = this.GetComponent<Button>();
        TMP_Text btn_text = topping_btn.GetComponentInChildren<TMP_Text>();
        btn_text.text = this.name;
        btn_text.enabled = true;
    }

    private void HideNote(PointerEventData data)
    {
        Button topping_btn = this.GetComponent<Button>();
        TMP_Text btn_text = topping_btn.GetComponentInChildren<TMP_Text>();
        btn_text.enabled = false;
    }

}

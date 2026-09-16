using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Toppings : MonoBehaviour
{
    public EggCommonParam param;
    [SerializeField] private SoundAssetRef soundAssetRef;
    [SerializeField] private AudioSource se_audiosource;

    private UnityEngine.UI.Image w_back;

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

        // w_back = topping_btn.GetComponentInChildren<UnityEngine.UI.Image>();
        // if (w_back != null)
        // {
        //     w_back.enabled = false;
        // }

        // EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        // enterEntry.eventID = EventTriggerType.PointerEnter;
        // enterEntry.callback.AddListener((data) => { EnterArea((PointerEventData)data); });
        // topping_button_evt.triggers.Add(enterEntry);

        // EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        // exitEntry.eventID = EventTriggerType.PointerExit;
        // exitEntry.callback.AddListener((data) => { ExitArea((PointerEventData)data); });
        // topping_button_evt.triggers.Add(exitEntry);
    }

    private void OnClicked()
    {
        if (se_audiosource != null && soundAssetRef != null)
        {
            se_audiosource.PlayOneShot(soundAssetRef.select_topping_se);
        }

        if (Enum.TryParse(this.name, out EggCommonParam.ToppingsType clicked_topping))
        {
            param.current_active_topping = clicked_topping;
        }
    }

    private void EnterArea(PointerEventData data)
    {
        print("X");
    }

    private void ExitArea(PointerEventData data)
    {
        print("O");
    }
}
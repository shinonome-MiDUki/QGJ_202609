using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem.Controls;

public class Egg : MonoBehaviour
{
    protected enum EggStatusIndex
    {
        UNBROKEN = 0,
        RAW = 1,
        HALF = 2,
        COOKED = 3,
        BURNT = 4, 
        NO_EGG = 5
    }
    [SerializeField] private Sprite[] egg_status_image = new Sprite[5];
    private List<int> topping; 
    protected EggStatusIndex egg_status; 
    private float time_elapsed; 
    private bool is_frypan_available; 
    private float cooking_speed; 
    private bool is_toppings_selectable = false;
    private GameObject egg_gobj;
    private bool is_egg_prepared = false;
    private bool is_new_egg_usable = false;
    private bool is_timer_working = false;
    protected EggSystemData eggSystemData;

    void Awake()
    {
        eggSystemData = new EggSystemData();
    }

    void Start()
    {
        egg_gobj = this.gameObject.transform.Find("egg").gameObject;
        SwitchEggStatus(EggStatusIndex.NO_EGG);
        is_egg_prepared = false;
        is_new_egg_usable = true;
        is_toppings_selectable = false;
        is_timer_working = false;
        eggSystemData.is_cooking = false;
        eggSystemData.is_focused = false;
        
    }

    void Update()
    {
        if (!eggSystemData.is_focused)
        {
            return;
        }

        if (!is_new_egg_usable)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (is_egg_prepared)
            {
                SwitchEggStatus(EggStatusIndex.RAW);
                is_egg_prepared = false;
                is_new_egg_usable = false;
                is_toppings_selectable = false;
                is_timer_working = true;
                eggSystemData.is_cooking = true;
                //StartCoroutine(Timer());
            }
            else
            {
                SwitchEggStatus(EggStatusIndex.UNBROKEN);
                is_egg_prepared = true;
            }
        }

    }

    protected void SwitchEggStatus(EggStatusIndex new_status){
        egg_status = new_status;
        if (!egg_gobj){
            egg_gobj = this.gameObject.transform.Find("egg").gameObject;
        }
        UnityEngine.UI.Image egg_img = egg_gobj.GetComponent<UnityEngine.UI.Image>();
        RectTransform egg_rt = egg_gobj.GetComponent<RectTransform>();
        egg_img.SetNativeSize();
        if (new_status == EggStatusIndex.NO_EGG)
        {
            egg_img.enabled = false;
        }
        else
        {
            egg_img.enabled = true;
            egg_img.sprite = egg_status_image[(int)new_status];
            print("Reached ln91");
            if (egg_rt != null)
            {
                egg_rt.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }
    }

    private IEnumerator Timer()
    {
        float starting_time = Time.time;
        // while (is_timer_working){
        //     time_elapsed = Time.time - starting_time;
        // }
        yield return null;
    }

    public void SetFocus(bool do_focus)
    {
        this.gameObject.transform.Find("ray").gameObject.SetActive(do_focus);
        eggSystemData.is_focused = do_focus;
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class Egg : MonoBehaviour
{
    public SoundAssetRef soundAssetRef;
    public AudioSource se_audiosource;
    [SerializeField] private ShowScore showScore;
    [SerializeField] private NoticeAnimation noticeAnimation;
    public UtilVar utilVar;
    public enum EggStatusIndex
    {
        UNBROKEN = 0,
        RAW = 1,
        HALF = 2,
        COOKED = 3,
        BURNT = 4, 
        NO_EGG = 5
    }

    public EggCommonParam param;
    protected static List<int>[] applied_toppings = new List<int>[3]; 

    private GameObject egg_gobj;
    private bool is_egg_prepared = false;
    private bool is_new_egg_usable = false;
    protected static bool[] is_cooking = new bool[3];
    public static bool[] is_focused = new bool[3];
    protected static EggCommonParam.EggStatusIndex[] egg_status = new EggCommonParam.EggStatusIndex[3]; 
    public struct EggSystemInfo
    {
        public EggCommonParam.EggStatusIndex egg_final_status;
        public List<int> egg_applied_toppings;
    }

    protected virtual void Start()
    {
        for (int i = 0; i < 3; i++){
            applied_toppings[i] = new List<int>();
        }
        egg_gobj = this.gameObject.transform.Find("egg").gameObject;
        SwitchEggStatus(EggCommonParam.EggStatusIndex.NO_EGG);
        is_egg_prepared = false;
        //is_new_egg_usable = true;
        is_cooking[GetMyIdx()] = false;
        is_focused[GetMyIdx()] = GetMyIdx() == 0 ? true : false;
        
    }

    protected virtual void Update()
    {
        if (!is_focused[GetMyIdx()])
        {
            return;
        }
        if (is_cooking[GetMyIdx()]){
            return;
        }
        if (!this.name.Contains("egg_system"))
        {
            return;
        }

        // if (!is_new_egg_usable)
        // {
        //     return;
        // }
        if (TutorialMode.is_lock_e_key)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (is_egg_prepared)
            {
                is_egg_prepared = false;
                //is_new_egg_usable = false;
                is_cooking[GetMyIdx()] = true;
                showScore.UiLoseNEggs(1);
                noticeAnimation.TriggerAnimation("-" + utilVar.egg_cost.ToString());
            }
            else
            {
                SwitchEggStatus(EggCommonParam.EggStatusIndex.UNBROKEN);
                is_egg_prepared = true;
                se_audiosource.PlayOneShot(soundAssetRef.call_egg_se);
            }
        }

    }

    protected int GetMyIdx()
    {
        bool is_parsed = int.TryParse(this.name.Split('_').Last(), out int my_idx);
        my_idx = is_parsed ? my_idx : 0;
        return my_idx;
    }

    protected void SwitchEggStatus(EggCommonParam.EggStatusIndex new_status){
        if (!this.name.Contains("egg_system"))
        {
            return;
        }
        egg_status[GetMyIdx()] = new_status;
        if (!egg_gobj){
            egg_gobj = this.gameObject.transform.Find("egg").gameObject;
        }
        UnityEngine.UI.Image egg_img = egg_gobj.GetComponent<UnityEngine.UI.Image>();
        RectTransform egg_rt = egg_gobj.GetComponent<RectTransform>();
        egg_img.SetNativeSize();
        if (new_status == EggCommonParam.EggStatusIndex.NO_EGG)
        {
            egg_img.enabled = false;
        }
        else
        {
            egg_img.enabled = true;
            egg_img.sprite = param.egg_status_image[(int)new_status];
            if (egg_rt != null)
            {
                egg_rt.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }
    }

    public void SetFocus(bool do_focus)
    {
        float alpha = do_focus ? 0.28f : 0.06f;
        Color color = new Color(0.96f, 0.94f, 0.78f, alpha);
        this.gameObject.transform.Find("ray").gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        is_focused[GetMyIdx()] = do_focus;
    }

    public bool GetFocusStatus()
    {
        return is_focused[GetMyIdx()];
    }

    public EggSystemInfo GetEggSystemInfo()
    {
      
        EggSystemInfo eggSystemInfo = new EggSystemInfo()
        {
            egg_final_status = egg_status[GetMyIdx()],
            egg_applied_toppings = applied_toppings[GetMyIdx()]
        };
        return eggSystemInfo;
    }

    public void ResetEggSystem(){
        applied_toppings[GetMyIdx()] = new List<int>();
        SwitchEggStatus(EggCommonParam.EggStatusIndex.NO_EGG);
        GameObject parentObj = GameObject.Find("egg_system_" + GetMyIdx().ToString());

        if (parentObj != null) {
            GameObject[] topping_gobjs = GameObject.FindGameObjectsWithTag("topping");
            foreach (GameObject x in topping_gobjs){
                if (x.transform.IsChildOf(parentObj.transform)) {
                    x.SetActive(false);
                }
            }
        }

        is_cooking[GetMyIdx()] = false;
    }
}

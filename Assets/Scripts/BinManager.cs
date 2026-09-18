using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BinManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] private Sprite bin_opened;
    [SerializeField] private Sprite bin_closed;
    [SerializeField] private SoundAssetRef soundAssetRef;
    [SerializeField] private AudioSource se_audiosource;
    [SerializeField] private ShowScore showScore;
    [SerializeField] private TMP_Text foodloss_warning;
    [SerializeField] private float foodloss_warning_fadeout_speed = 0.7f;
    private Vector3 drag_start_pos;
    private bool is_run_animation = false;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = bin_closed;
        foodloss_warning.enabled = false;
        is_run_animation = false;
    }

    void Update()
    {
        if (!is_run_animation)
        {
            return;
        }
        foodloss_warning.enabled = true;
        Color current_color = foodloss_warning.color;
        Color new_color = new Color(
            current_color.r, current_color.g, current_color.b,
            current_color.a - Time.deltaTime * foodloss_warning_fadeout_speed
        );
        foodloss_warning.color = new_color;
        if (foodloss_warning.color.a <= 0.0f)
        {
            foodloss_warning.enabled = false;
            foodloss_warning.color = new Color(
                foodloss_warning.color.r, foodloss_warning.color.g, foodloss_warning.color.b, 1.0f
            );
            is_run_animation = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        drag_start_pos = this.transform.position;
        this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = bin_opened;
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = bin_closed;
        canvasGroup.blocksRaycasts = true;
        GameObject target_gobj = eventData.pointerEnter;
        if (target_gobj == null)
        {
            this.transform.position = drag_start_pos;
            return;
        }
        GameObject target_parent_gobj = target_gobj.transform.parent.gameObject;
        string target_parent_gobj_name = target_parent_gobj.name;
        if (target_parent_gobj_name.Contains("egg_system"))
        {
            int egg_applied_status = (int)target_parent_gobj.GetComponent<Egg>().GetEggSystemInfo().egg_final_status;
            if (egg_applied_status == 5)
            {
                this.transform.position = drag_start_pos;
                return;
            }
            showScore.RatingPanalty(2);
            foodloss_warning.transform.position = eventData.position;
            is_run_animation = true;
            target_parent_gobj.GetComponent<Egg>().ResetEggSystem();
        }
        else
        {
            this.transform.position = drag_start_pos;
        }
        this.transform.position = drag_start_pos;
        se_audiosource.PlayOneShot(soundAssetRef.throw_to_bin_se);
    }
    
}

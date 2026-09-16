using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private Vector3 drag_start_pos;
    [SerializeField] private OrderSpawner orderSpawner;
    [SerializeField] private ShowScore showScore;
    [SerializeField] private SoundAssetRef soundAssetRef;
    [SerializeField] private AudioSource se_audiosource;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        drag_start_pos = this.transform.position;
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
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
            List<int> egg_applied_toppings = target_parent_gobj.GetComponent<Egg>().GetEggSystemInfo().egg_applied_toppings;
            int egg_applied_status = (int)target_parent_gobj.GetComponent<Egg>().GetEggSystemInfo().egg_final_status;
            if (egg_applied_status == 0 || egg_applied_status == 5)
            {
                this.transform.position = drag_start_pos;
                return;
            }
            target_parent_gobj.GetComponent<Egg>().ResetEggSystem();
            List<int> egg_ordered_toppings = this.gameObject.GetComponent<Orders>().GetOrderedToppings();
            int egg_ordered_status = (int) this.gameObject.GetComponent<Orders>().GetOrderedEggStatus();
            float time_elapsed = this.gameObject.GetComponent<Orders>().StopAndGetTimerTime();
            orderSpawner.PopReceipt(this.name);
            showScore.UpdateResultSystem(
                egg_ordered_toppings,
                egg_applied_toppings,
                egg_ordered_status,
                egg_applied_status,
                time_elapsed
            );
            se_audiosource.PlayOneShot(soundAssetRef.offering_se);
            return;
        }
        else
        {
            this.transform.position = drag_start_pos;
            return;
        }
    }
    
}

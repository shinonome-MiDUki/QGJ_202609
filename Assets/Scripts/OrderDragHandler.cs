using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class OrderDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private Vector3 drag_start_pos;
    [SerializeField] private UtilVar utilVar;
    [SerializeField] private OrderSpawner orderSpawner;
    [SerializeField] private ShowScore showScore;
    [SerializeField] private NoticeAnimation noticeAnimation;

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
        float drop_point_x = eventData.position.x;
        float drop_point_y = eventData.position.y;
        float[][] available_collision_targets = utilVar.collision_area;
        bool is_collided = false;
        int collided_idx = -1;
        for (int i = 0; i < available_collision_targets.Length; i++)
        {
            if (drop_point_x >= available_collision_targets[i][0]
                && drop_point_x <= available_collision_targets[i][1]
                && drop_point_y >= available_collision_targets[i][2]
                && drop_point_y <= available_collision_targets[i][3]
            )
            {
                is_collided = true;
                collided_idx = i;
                break;
            }
        }
        if (!is_collided)
        {
            this.transform.position = drag_start_pos;
            return;
        }
        string target_parent_gobj_name = "egg_system_" + collided_idx.ToString();
        GameObject target_parent_gobj = GameObject.Find(target_parent_gobj_name);
        if (target_parent_gobj == null)
        {
            this.transform.position = drag_start_pos;
            return;
        }
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
        float before_money = showScore.GetCurrent("money");
        showScore.UpdateResultSystem(
            egg_ordered_toppings,
            egg_applied_toppings,
            egg_ordered_status,
            egg_applied_status,
            time_elapsed
        );
        float gain = showScore.GetCurrent("money") - before_money;
        if (gain > 0.0f)
        {
            noticeAnimation.TriggerAnimation("+" + gain.ToString());
        }
    }
}

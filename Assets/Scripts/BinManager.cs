using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BinManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] private Sprite bin_opened;
    [SerializeField] private Sprite bin_closed;
    [SerializeField] private ShowScore showScore;
    [SerializeField] private SoundAssetRef soundAssetRef;
    [SerializeField] private AudioSource se_audiosource;
    private Vector3 drag_start_pos;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = bin_closed;
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
        int egg_applied_status = (int)target_parent_gobj.GetComponent<Egg>().GetEggSystemInfo().egg_final_status;
        if (egg_applied_status == 5)
        {
            this.transform.position = drag_start_pos;
            return;
        }
        string target_parent_gobj_name = target_parent_gobj.name;
        if (target_parent_gobj_name.Contains("egg_system"))
        {
            target_parent_gobj.GetComponent<Egg>().ResetEggSystem();
            showScore.UiLoseNEggs(1);
        }
        else
        {
            this.transform.position = drag_start_pos;
        }
        this.transform.position = drag_start_pos;
        se_audiosource.PlayOneShot(soundAssetRef.throw_to_bin_se);
    }
    
}

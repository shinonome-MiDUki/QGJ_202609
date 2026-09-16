using System;
using Unity.VisualScripting;
using UnityEngine;

public class LensOperation : EggStatusSlider
{
    [SerializeField] private float max_height = 400.0f;
    [SerializeField] private float min_height = 30.0f;
    [SerializeField] private float key_sensitivity = 0.6f;
    [SerializeField] private float factor_sensitivity = 1.0f;
    [SerializeField] private RaySpreading raySpreading;
    private GameObject lens_gobj;

    protected override void Start()
    {
        base.Start();

        lens_gobj = this.gameObject.transform.Find("lens").gameObject;
        RectTransform lens_rt = lens_gobj.GetComponent<RectTransform>();
        Vector3 current_pos = lens_rt.anchoredPosition;
        float height = current_pos.y;
        CookingSpeedControl(height);
    }

    protected override void Update()
    {
        base.Update();

        if (!is_focused[GetMyIdx()])
        {
            return;
        }

        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            return;
        }

        if (!lens_gobj)
        {
            lens_gobj = this.gameObject.transform.Find("lens").gameObject;
        }

        RectTransform lens_rt = lens_gobj.GetComponent<RectTransform>();
        Vector3 current_pos = lens_rt.anchoredPosition;
        float height = current_pos.y;
        if (Input.GetKey(KeyCode.W))
        {
            if (height >= 400.0f)
            {
                return;
            }
            height += key_sensitivity;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (height <= min_height)
            {
                return;
            }
            height -= key_sensitivity;
        }
        lens_rt.anchoredPosition = new Vector3(current_pos.x, height, current_pos.z);
        CookingSpeedControl(height);
    }

    private void CookingSpeedControl(float height)
    {
        float factor = Math.Abs(height - min_height) / Math.Abs(max_height - min_height) ;
        factor = -0.3f + 1.8f * factor;
        raySpreading.RefreshMesh(height - 200.0f, factor * 3.2f);

        factor = Math.Abs(max_height - height) / Math.Abs(max_height - min_height) + 1.0f;
        factor += factor_sensitivity;
        AdjustCookingSpeed(factor);
    }
}

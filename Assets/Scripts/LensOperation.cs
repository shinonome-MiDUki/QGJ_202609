using System;
using Unity.VisualScripting;
using UnityEngine;

public class LensOperation : EggStatusSlider
{
    [SerializeField] private readonly float max_height;
    [SerializeField] private readonly float min_height;
    private GameObject lens_gobj;
    private RectTransform lens_rt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lens_gobj = this.gameObject.transform.Find("lens").gameObject;
        lens_rt = lens_gobj.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lens_gobj)
        {
            lens_gobj = this.gameObject.transform.Find("lens").gameObject;
        }

        RectTransform lens_rt = lens_gobj.GetComponent<RectTransform>();
        Vector3 current_pos = lens_rt.position;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
        }

    }
}

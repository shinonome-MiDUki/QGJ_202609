using System;
using TMPro;
using UnityEngine;

public class NoticeAnimation : MonoBehaviour
{
    private TMP_Text notice_text_obj;
    private Vector3 init_pos;
    private bool is_run_animation = false;
    [SerializeField] private float animation_speed = 300.0f;
    [SerializeField] private float start_fade_out_y_pos = 800.0f;
    [SerializeField] private float fading_speed = 1.0f;
    [SerializeField] private float stop_y_pos = 700.0f;
    void Start()
    {
        notice_text_obj = this.gameObject.GetComponent<TMP_Text>();
        init_pos = this.gameObject.transform.position;
        is_run_animation = false;
    }

    void Update()
    {
        if (!is_run_animation)
        {
            return;
        }
        Vector3 current_pos = this.gameObject.transform.position;
        float moved_factor = (init_pos.y - current_pos.y) / (init_pos.y - stop_y_pos);
        Vector3 new_pos = new Vector3(
            current_pos.x, 
            current_pos.y + Time.deltaTime * animation_speed * (0.8f * (float)Math.Pow(1 - moved_factor, 1.5) + 0.2f) * -1, 
            current_pos.z
        );
        this.gameObject.transform.position = new_pos;
        if (new_pos.y < start_fade_out_y_pos)
        {
            Color current_color = notice_text_obj.color;
            Color new_color = new Color(
                current_color.r, current_color.g, current_color.b,
                current_color.a - fading_speed * Time.deltaTime
            );
            notice_text_obj.color = new_color;
        }
        if (new_pos.y < stop_y_pos || notice_text_obj.color.a <= 0.0f)
        {
            this.gameObject.transform.position = init_pos;
            Color current_color = notice_text_obj.color;
            notice_text_obj.color = new Color(
                current_color.r, current_color.g, current_color.b, 1.0f
            );
            is_run_animation = false;
        }
    }

    public void TriggerAnimation(string notice_content)
    {
        notice_text_obj.text = notice_content;
        is_run_animation = true;
    }
}

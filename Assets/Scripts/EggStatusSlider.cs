using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EggStatusSlider : Egg
{
    [SerializeField] private float play_time = 60.0f;
    private Slider status_slider;
    private float speed_factor;
    

    void Start()
    {
        speed_factor = 1.0f;
        status_slider = this.gameObject.transform.Find("status_bar").GetComponent<Slider>();
    }

    void Update()
    {
        if (!eggSystemData.is_focused)
        {
            return;
        }

        if (!eggSystemData.is_cooking)
        {
            return;
        }
        float standard_increment = Time.deltaTime * (1 / play_time);
        status_slider.value += standard_increment * speed_factor;

        float current_factor = status_slider.value;
        if (current_factor >= 0.0f && current_factor < 0.25f && egg_status != EggStatusIndex.RAW){
            SwitchEggStatus(EggStatusIndex.RAW);
        }
        else if (current_factor >= 0.25f && current_factor < 0.5f && egg_status != EggStatusIndex.HALF){
            SwitchEggStatus(EggStatusIndex.HALF);
        }
        else if (current_factor >= 0.5f && current_factor < 0.75f && egg_status != EggStatusIndex.RAW){
            SwitchEggStatus(EggStatusIndex.COOKED);
        }
        else if (current_factor >= 0.75f && current_factor < 1.0f && egg_status != EggStatusIndex.RAW){
            SwitchEggStatus(EggStatusIndex.BURNT);
        }
        else if (current_factor >= 1.0f){
            eggSystemData.is_cooking = false;
        }
    }

    protected void AdjustCookingSpeed(float new_speed_factor)
    {
        speed_factor = new_speed_factor;
    }
}

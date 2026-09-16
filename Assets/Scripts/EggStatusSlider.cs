using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EggStatusSlider : Egg
{
    [SerializeField] private UtilVar utilVar;
    private Slider status_slider;
    private float speed_factor;
    

    protected override void Start()
    {
        base.Start();
        
        speed_factor = 1.0f;
        status_slider = this.gameObject.transform.Find("status_bar").GetComponent<Slider>();
    }

    protected override void Update()
    {
        base.Update();

        if (!is_cooking[GetMyIdx()])
        {
            status_slider.value = 0.0f;
            return;
        }
        float standard_increment = Time.deltaTime * (1 / utilVar.play_time);
        status_slider.value += standard_increment * speed_factor;

        float current_factor = status_slider.value;
        if (current_factor >= 0.0f && current_factor < 0.25f && egg_status[GetMyIdx()] != EggCommonParam.EggStatusIndex.RAW){
            se_audiosource.PlayOneShot(soundAssetRef.egg_status_proceed_se);
            SwitchEggStatus(EggCommonParam.EggStatusIndex.RAW);
        }
        else if (current_factor >= 0.25f && current_factor < 0.5f && egg_status[GetMyIdx()] != EggCommonParam.EggStatusIndex.HALF){
            se_audiosource.PlayOneShot(soundAssetRef.egg_status_proceed_se);
            SwitchEggStatus(EggCommonParam.EggStatusIndex.HALF);
        }
        else if (current_factor >= 0.5f && current_factor < 0.75f && egg_status[GetMyIdx()] != EggCommonParam.EggStatusIndex.COOKED){
            se_audiosource.PlayOneShot(soundAssetRef.egg_status_proceed_se);
            SwitchEggStatus(EggCommonParam.EggStatusIndex.COOKED);
        }
        else if (current_factor >= 0.75f && current_factor < 1.0f && egg_status[GetMyIdx()] != EggCommonParam.EggStatusIndex.BURNT){
            se_audiosource.PlayOneShot(soundAssetRef.egg_status_proceed_se);
            SwitchEggStatus(EggCommonParam.EggStatusIndex.BURNT);
        }
        else if (current_factor >= 1.0f){
            //is_cooking[GetMyIdx()] = false;
        }
    }

    protected void AdjustCookingSpeed(float new_speed_factor)
    {
        speed_factor = new_speed_factor;
    }
}

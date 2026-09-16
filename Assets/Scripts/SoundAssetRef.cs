using UnityEngine;

[CreateAssetMenu(fileName = "SoundAssetRef", menuName = "SoundAssetRef")]
public class SoundAssetRef : ScriptableObject
{

    [Tooltip("Narration")]
    public AudioClip[] toppings_tsumugi = new AudioClip[5];
    public AudioClip[] egg_status_tsumugi = new AudioClip[4];
    public AudioClip[] toppings_metan = new AudioClip[5];
    public AudioClip[] egg_status_metan = new AudioClip[4];
     
    [Tooltip("BGM")]
    public AudioClip main_bgm;

    [Tooltip("SE")]
    public AudioClip[] toppings_se = new AudioClip[5];
    public AudioClip select_topping_se;
    public AudioClip egg_status_proceed_se;
    public AudioClip offering_se;
    public AudioClip call_egg_se;
    public AudioClip throw_to_bin_se;
    
}

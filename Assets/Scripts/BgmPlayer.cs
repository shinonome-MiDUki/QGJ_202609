using System;
using System.Collections;
using UnityEngine;

public class BgmPlayer : MonoBehaviour
{
    private AudioSource bgm_audioSource;
    private float loop_prep_timer = 0.0f;
    private bool is_first_time;
    [SerializeField] private float loop_start_point = 43.20f;
    [SerializeField] private SoundAssetRef soundAssetRef;

    void Start()
    {
        bgm_audioSource = this.gameObject.GetComponent<AudioSource>();
        bgm_audioSource.clip = soundAssetRef.main_bgm;
        bgm_audioSource.volume = 0.7f;
        is_first_time = true;
    }

    void Update()
    {
        if (bgm_audioSource.isPlaying)
        {
            return;
        }
        loop_prep_timer += Time.deltaTime;
        if (loop_prep_timer > 1.0f)
        {
            loop_prep_timer = 0.0f;
            if (!is_first_time)
            {
                bgm_audioSource.timeSamples = (int)(loop_start_point * bgm_audioSource.clip.frequency);
            }
            bgm_audioSource.Play();
            is_first_time = false;
        }
    }

}

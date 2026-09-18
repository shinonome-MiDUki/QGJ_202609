using System;
using System.Collections;
using UnityEngine;

public class BgmPlayer : MonoBehaviour
{
    private AudioSource bgm_audiosource;
    [SerializeField] private float loop_start_point = 43.20f;
    [SerializeField] private SoundAssetRef soundAssetRef;

    void Start()
    {
        bgm_audiosource = this.gameObject.GetComponent<AudioSource>();
        bgm_audiosource.clip = soundAssetRef.main_bgm;
        bgm_audiosource.loop = false;
        bgm_audiosource.Play();
        StartCoroutine(MakeLoopFromMSec());
    }

    IEnumerator MakeLoopFromMSec()
    {
        yield return new WaitForSeconds(loop_start_point);
        bgm_audiosource.Stop();
        bgm_audiosource.clip = soundAssetRef.main_bgm;
        bgm_audiosource.loop = true;
        bgm_audiosource.Play();
    }

}

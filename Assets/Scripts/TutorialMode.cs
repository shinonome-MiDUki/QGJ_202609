using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TutorialMode : MonoBehaviour
{
    public static bool is_tutorial_mode = false;
    [HideInInspector] public static bool is_lock_w_s_key = false;
    [HideInInspector] public static bool is_lock_e_key = false;
    [SerializeField] private List<GameObject> shielding_list;
    //0 topping ; 1 frypan ; 2 receipt ; 3 bin
    [SerializeField] private List<Canvas> tutorial_slides;
    private bool is_proceed = false;

    void Start()
    {
        SetAllUi(is_tutorial_mode);
        if (!is_tutorial_mode)
        {
            return;
        }
        StartCoroutine(RunTutorialMode());
    }

    void Update()
    {
        if (!is_tutorial_mode)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            is_proceed = true;
        }
    }

    private void SetAllUi(bool is_lock)
    {
        foreach (GameObject x in shielding_list)
        {
            x.SetActive(is_lock);
        }
        is_lock_e_key = is_lock;
        is_lock_w_s_key = is_lock;
        foreach (Canvas x in tutorial_slides)
        {
            x.enabled = false;
        }
    }

    IEnumerator RunTutorialMode()
    {
        tutorial_slides[0].enabled = true;

        is_proceed = false;
        yield return new WaitUntil(() => is_proceed);
        SetAllUi(true);
        tutorial_slides[1].enabled = true;
        is_lock_e_key = false;

        is_proceed = false;
        yield return new WaitUntil(() => is_proceed);
        SetAllUi(true);
        tutorial_slides[2].enabled = true;
        shielding_list[0].SetActive(false);
        shielding_list[1].SetActive(false);

        is_proceed = false;
        yield return new WaitUntil(() => is_proceed);
        SetAllUi(true);
        tutorial_slides[3].enabled = true;
        is_lock_w_s_key = false;

        is_proceed = false;
        yield return new WaitUntil(() => is_proceed);
        SetAllUi(true);
        tutorial_slides[4].enabled = true;
        shielding_list[2].SetActive(false);

        is_proceed = false;
        yield return new WaitUntil(() => is_proceed);
        SetAllUi(true);
        tutorial_slides[5].enabled = true;
        shielding_list[1].SetActive(false);
        shielding_list[3].SetActive(false);

        is_proceed = false;
        yield return new WaitUntil(() => is_proceed);
        SetAllUi(true);
        tutorial_slides[6].enabled = true;

        is_proceed = false;
        yield return new WaitUntil(() => is_proceed);
        is_tutorial_mode = false;
        is_proceed = false;
        SetAllUi(false);
    }

}

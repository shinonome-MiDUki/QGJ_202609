using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class EggSystemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject egg_system_prefab;
    [SerializeField] private int set_num = 3;
    private List<GameObject> egg_systems;
    private int current_focus_idx = 0;
    

    void Start()
    {
        egg_systems = new List<GameObject>();
        for (int i = 0; i < set_num; i++)
        {
            GameObject egg_system_inst = Instantiate(
                egg_system_prefab, 
                new Vector3(520.0f + (i * 530.0f), 550.0f, 0.0f), 
                Quaternion.identity, this.transform
            );
            egg_system_inst.name = "egg_system_" + i.ToString();
            egg_systems.Add(egg_system_inst);

            if (i != 0)
            {
                egg_system_inst.GetComponent<Egg>().SetFocus(false);
            }
        }
        current_focus_idx= 1;
        SwitchFocus(false);
        // egg_systems[current_focus_idx].GetComponent<Egg>().SetFocus(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SwitchFocus(true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SwitchFocus(false);
        }
    }

    private void SwitchFocus(bool is_forward)
    {
        if ((is_forward && current_focus_idx >= egg_systems.Count - 1)
            || (!is_forward && current_focus_idx <= 0))
        {
            return;
        }
        GameObject current_focus = egg_systems[current_focus_idx];
        current_focus.GetComponent<Egg>().SetFocus(false);
        int movement = is_forward ? 1 : -1;
        current_focus_idx += movement;
        current_focus = egg_systems[current_focus_idx];
        current_focus.GetComponent<Egg>().SetFocus(true);
    }
}

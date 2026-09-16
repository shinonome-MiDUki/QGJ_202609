using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject receipt_prefab;
    [SerializeField] private int max_order_num = 6;
    private int current_order_count = 0;
    private int accum_order_count = 0;
    private static Dictionary<Vector3, GameObject> position_existance;

    void Start()
    {
        position_existance = new Dictionary<Vector3, GameObject>();
        float interval = 1450 / (max_order_num - 1);
        for (int i = 0; i < max_order_num; i++)
        {
            Vector3 pos = new Vector3(235.0f + (i * interval), 112.0f, 0.0f);
            position_existance[pos] = null;
        }
    }
    public void PopulateReceipt()
    {
        if (current_order_count >= max_order_num)
        {
            return;
        }
        Vector3 inst_pos = new Vector3();
        foreach (var pair in position_existance)
        {
            if (pair.Value != null)
            {
                continue;
            }
            inst_pos = pair.Key;
            break;
        }
        GameObject receipt_inst = Instantiate(
            receipt_prefab, 
            inst_pos,
            Quaternion.identity, this.transform
        );
        receipt_inst.name = "receipt" + accum_order_count.ToString();
        position_existance[inst_pos] = receipt_inst;
        accum_order_count++;
        current_order_count++;
    }

    public void PopReceipt(string receipt_name)
    {
        GameObject target_receipt_gobj = GameObject.Find(receipt_name);
        Vector3 target_receipt_pos = new Vector3();
        foreach (var pair in position_existance)
        {
            if (pair.Value != target_receipt_gobj)
            {
                continue;
            }
            print("Hello");
            target_receipt_pos = pair.Key;
            break;
        }
        if (target_receipt_gobj != null)
        {
            print(target_receipt_pos);
            position_existance[target_receipt_pos] = null;
            Destroy(target_receipt_gobj);
            current_order_count--;
        }
    }

    public void TestBtn()
    {
        PopulateReceipt();
    }
}
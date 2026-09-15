using UnityEngine;
using UnityEngine.UI;

public class Toppings : MonoBehaviour
{
    void Start()
    {
        Button topping_btn = this.GetComponent<Button>();
        topping_btn.onClick.AddListener(OnClicked);
    }

    void Update()
    {
        
    }

    void OnClicked()
    {
        Debug.Log("My name is " + this.name);
    }
}

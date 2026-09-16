using UnityEngine;

public class DifficultyControl : MonoBehaviour
{
    public static int difficulty = 0; // 0: easy - 2: difficult

    public static int[] GetOrderInterval()
    {
        int[] min_max = new int[2];
        switch (difficulty)
        {
            case 0:
                min_max[0] = 7; min_max[1] = 9;
                break;
            case 1:
                min_max[0] = 5; min_max[1] =7;
                break;
            case 2:
                min_max[0] = 3; min_max[1] = 5;
                break;
            default:
                min_max[0] = 7; min_max[1] = 9;
                break;
        }
        return min_max;
    }

    public static int GetMaxToppingNum()
    {
        int max_topping_num = 2;
        switch (difficulty)
        {
            case 0:
                max_topping_num = 2;
                break;
            case 1:
                max_topping_num = 3;
                break;
            case 2:
                max_topping_num = 5;
                break;
            default:
                max_topping_num = 2;
                break;
        }
        return max_topping_num;
    }
}

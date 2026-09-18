using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ShowScore : ScoreSystem
{
    private TMP_Text money_text;
    private TMP_Text star_text;

    void Start()
    {
        current_money = UtilVar.init_capital;
        current_comments = new int[2]{5, 1};
        money_text = this.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        star_text = this.gameObject.transform.GetChild(2).GetComponent<TMP_Text>();
        money_text.text = "残高 : " + current_money.ToString();
        star_text.text = "口コミ : " + ((double)current_comments[0] / current_comments[1]).ToString("F1");
    }

    public void UpdateResultSystem(
        List<int> ordered_toppings,
        List<int> applied_toppings,
        int ordered_status,
        int applied_status,
        float waiting_time
    )
    {
        SolveResult(
            ordered_toppings,
            applied_toppings,
            ordered_status,
            applied_status,
            waiting_time
        );

        money_text.text = "残高 : " + current_money.ToString();
        if (current_comments[1] != 0){
            star_text.text = "口コミ : " + ((double)current_comments[0] / current_comments[1]).ToString("F1");
        }
    }
    
    public void UiLoseNEggs(int n)
    {
        LoseNEggs(n);
        money_text.text = "残高 : " + current_money.ToString();

        if (current_money <= 0.0f)
        {
            UtilVar.is_success = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
            return;
        }
    }

    public void RatingPanalty(int added_rating)
    {
        LoseRating(added_rating);
        star_text.text = "口コミ : " + ((double)current_comments[0] / current_comments[1]).ToString("F1");
    }

    public float GetCurrent(string mode)
    {
        if (mode == "money")
        {
            return current_money;
        }
        else
        {
            float current_rating = (float)current_comments[0] / current_comments[1];
            return current_rating;
        }
    }
}

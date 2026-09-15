using UnityEngine;
using TMPro;
using System.Collections.Generic;
using JetBrains.Annotations;

public class ShowScore : MonoBehaviour
{
    public bool show_score;
    public float star;
    [SerializeField] private TextMeshProUGUI scoreTextField;
    Dictionary<float, string> starstring = new Dictionary<float, string>()
    {
        {0f,"☆"},
        {0.5f,"☆"},
        {1f,"★"},
        {1.5f,"★☆"},
        {2f,"★★"},
        {2.5f,"★★☆"},
        {3f,"★★★"},
        {3.5f,"★★★☆"},
        {4f,"★★★★"},
        {4.5f,"★★★★☆"},
        {5f,"★★★★★"}
    };

    void Update()
    {
        if (scoreTextField == null)
        {
            scoreTextField = GetComponent<TextMeshProUGUI>();
        }
        if (scoreTextField != null)
        {
            float star = PlayerPrefs.GetFloat("star", 0f);
            float money = PlayerPrefs.GetFloat("money", 0f);
            if (show_score == false)
            {
                scoreTextField.text = "所持金: " + money;
            }
            else
            {
            scoreTextField.text = "評価: " + starstring[star] + "\n所持金: " + money;
            }
        }
        else
        {
            Debug.LogError("ScoreTextFieldが割り当てられていません！");
        }
    }
}
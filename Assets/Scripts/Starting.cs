using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Starting : MonoBehaviour
{
    [Header("UI要素の参照")]
    [SerializeField] private Button firstButton;    // 1番上のボタン（切替用）
    [SerializeField] private Button secondButton1;   // 2番目のボタン
    [SerializeField] private Button thirdButton2;   // 3番目のボタン

    [SerializeField] private TextMeshProUGUI firstButtonText;
    [SerializeField] private TextMeshProUGUI secondButtonText;
    [SerializeField] private TextMeshProUGUI thirdButtonText;
private int currentMode = 0;

    void Start()
    {
        // ボタンがクリックされたときの処理をコード側で登録
        firstButton.onClick.AddListener(OnfirstButtonClicked);
        secondButton1.onClick.AddListener(OnsecondButtonClicked);
        thirdButton2.onClick.AddListener(OnthirdButtonClicked);

        // 最初の状態をセット
        UpdateUI();
    }

    // 1番上のボタンを押したとき（モード切替）
    void OnfirstButtonClicked()
    {
        if (currentMode == 0) {
            currentMode = (currentMode + 1) % 2;
        }
        else
        {
            PlayerPrefs.SetInt("difficulty", 1);
            SceneManager.LoadScene("Game");
        }
        Debug.Log("モードを切り替えました: " + currentMode);
        UpdateUI();
    }

    
    void UpdateUI()
    {
        if (currentMode == 0)
        {
            firstButtonText.text = "スタート";
            secondButtonText.text = "チュートリアル";
            thirdButtonText.text = "終了";
        }
        else if (currentMode == 1)
        {
            firstButtonText.text = "イージー";
            secondButtonText.text = "ノーマル";
            thirdButtonText.text = "ハード";
        }
    }

    // 2番目のボタンを押したときの実装内容
    void OnsecondButtonClicked()
    {
        if (currentMode == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (currentMode == 1)
        {
            PlayerPrefs.SetInt("difficulty", 2);
            SceneManager.LoadScene("Game");
        }
    }

    // 3番目のボタンを押したときの実装内容
    void OnthirdButtonClicked()
    {
        if (currentMode == 0)
        {
            QuitGame();
        }
        else if (currentMode == 1)
        {
            PlayerPrefs.SetInt("difficulty", 3);
            SceneManager.LoadScene("Game");
        }
    }
    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // エディタ実行時は停止
#else
        Application.Quit(); 
#endif
    }
}


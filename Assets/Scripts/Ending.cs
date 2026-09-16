using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement; 
using TMPro;



public enum StructureType
{
    None,      // 何もしない
    Restart,  // タイトル画面,
    QuitApp     // アプリを終了
}

public class Ending : MonoBehaviour, IPointerClickHandler
{
    
    [Header("この構造物の種類を設定")]
    public StructureType structureType; // インスペクターで選択可能にする

    public void Start()
    {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        Debug.Log(gameObject.name + "is clicked.");

        ExecuteStructureAction();
    }

    void ExecuteStructureAction()
    {
        switch (structureType)
        {
            case StructureType.Restart:
                Debug.Log("button restart");
                SceneManager.LoadScene("Start");
                break;


            case StructureType.QuitApp:
                Debug.Log("button quit");
                QuitGame();
                break;

            default:
                Debug.LogWarning("Unknown structure type: " + structureType);
                break;
        }
    }

    // アプリ終了処理（エディタ対策込み）
    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // エディタ実行時は停止
#else
        Application.Quit(); // 本番ビルド時はアプリ終了
#endif
    }
}

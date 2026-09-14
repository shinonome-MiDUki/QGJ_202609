using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Starting : MonoBehaviour, IPointerClickHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "was clicked.");
        SceneManager.LoadScene("Game");
    }
}

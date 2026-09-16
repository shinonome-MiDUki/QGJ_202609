using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite[] tutorial_slides = new Sprite[4];
    private UnityEngine.UI.Image self_img;
    private int current_page;

    void Start()
    {
        self_img = this.gameObject.GetComponent<UnityEngine.UI.Image>();
        self_img.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            if (current_page >= tutorial_slides.Length - 1)
            {
                return;
            }
            current_page++;
            self_img.sprite = tutorial_slides[current_page];
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (current_page <= 0)
            {
                return;
            }
            current_page--;
            self_img.sprite = tutorial_slides[current_page];
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideTutorial();
        }
    }

    public void ShowTutorial()
    {
        current_page = 0;
        self_img.sprite = tutorial_slides[current_page];
        self_img.enabled = true;
    }

    public void HideTutorial()
    {
        self_img.enabled = false;
    }
}

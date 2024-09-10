using UnityEngine.SceneManagement;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject WinCanvas;
    public GameObject LoseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        CheckLevelForCanvasState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
		//Debug.Log(LevelController.GetActiveLevel() + " is ActiveScene");
		GameManager.Instance.LoadLevel("Level3");
    }

    public void CheckLevelForCanvasState()
    {
        Debug.Log("Active LEVEL IS " + SceneManager.GetActiveScene().name);
        if(LevelController.Instance.GetActiveLevel() == "MainMenu")
        {
            SetButtons();
        }
    }

    public void SetButtons()
    {
        Debug.Log("won and lost in respect: " + GameManager.Instance.lastLevelWon + GameManager.Instance.lastLevelLost);
        string lastLevelLoststr = PlayerPrefs.GetString("lastLevelLost");
        if(GameManager.Instance.lastLevelWon)
        {
            SetFirstCanvasActive(WinCanvas, LoseCanvas, MainCanvas);
        }
        else if(GameManager.Instance.lastLevelLost)
        {
            Debug.Log("Bura mi calisti");
            SetFirstCanvasActive(LoseCanvas, WinCanvas, MainCanvas);
        }
        else
        {
            SetFirstCanvasActive(MainCanvas, WinCanvas, LoseCanvas);
        }
    }

    public void SetFirstCanvasActive(GameObject toSetActive, GameObject toSetInactive, GameObject toSetInactive2)
    {
        Debug.Log("Setting two of 'em inactive");
        toSetActive.SetActive(true);
        toSetInactive.SetActive(false);
        toSetInactive2.SetActive(false);
    }
}

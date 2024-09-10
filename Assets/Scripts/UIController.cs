using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject WinCanvas;
    public GameObject LoseCanvas;
    public Text debugText;
    
    [SerializeField]
    private Dropdown levelsDropdown;
    private int selectedLevel;
    private int lastUnlockedLevel=0;

    // Start is called before the first frame update
    void Start()
    {
        CheckLevelForCanvasState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
		//Debug.Log(LevelController.GetActiveLevel() + " is ActiveScene");
		GameManager.Instance.LoadLevel("Level1");
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
            SetFirstCanvasActive(LoseCanvas, WinCanvas, MainCanvas);
        }
        else
        {
            SetFirstCanvasActive(MainCanvas, WinCanvas, LoseCanvas);
            GetDebugText();
        }
    }

    public void SetFirstCanvasActive(GameObject toSetActive, GameObject toSetInactive, GameObject toSetInactive2)
    {
        Debug.Log("Setting two of 'em inactive");
        toSetActive.SetActive(true);
        toSetInactive.SetActive(false);
        toSetInactive2.SetActive(false);
    }

    public void ChangeDebugMode()
    {
        GameManager.Instance.debugModeOn =! GameManager.Instance.debugModeOn;
        GetDebugText();
    }

    void GetDebugText()
    {
        if(GameManager.Instance.debugModeOn)
        {
            debugText.text = "Debug Mode: On";
        }
        else
        {
            debugText.text = "Debug Mode: Off";
        }
    }

    void SelectLevel()
    {
        //Code for dropdown
        //requestedLevel = dropdown.choice;
        
        //unlock all levels
        if(GameManager.Instance.debugModeOn)
        {
            lastUnlockedLevel = 8;
        }

        //try and load selected level
        if(selectedLevel <= lastUnlockedLevel)
        {
            GameManager.Instance.LoadLevel("Level" + selectedLevel);
        }
    }
}

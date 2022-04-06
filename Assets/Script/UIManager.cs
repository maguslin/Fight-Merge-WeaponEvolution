using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject OnboardText;
    public Text levelText;
    float speed=20;
    [Header("Bot UI")]
    public float Bothealth = 100;
    public Text damageText;
    public Slider slider;
    public Text barText;
    public GameObject BotCanvas;

    [Header("Bot UI Sate 2")]
    public float Bothealthst2 = 100;
    public Text damageTextst2;
    public Slider sliderst2;
    public Text barTextst2;
    public GameObject BotCanvasst2;

    [Header("Player UI")]
    public float PlayerHealth = 100;
    public Text PlayerDamageText;
    public Slider PlayerSlider;
    public Text PlayerBarText;
    public GameObject PlayerCanvas;

    [Header("Player UI State 2")]
    public float PlayerHealthst2 = 100;
    public Text PlayerDamageTextst2;
    public Slider PlayerSliderst2;
    public Text PlayerBarTextst2;
    public GameObject PlayerCanvasst2;

    [Header("Game Panels")]
    public GameObject gamePanel;
    public GameObject gameWinPanel;
    public GameObject gameFailPanel;

    [Header("Win Panel")]
    public GameObject rotateImage;

    [Header("State Blok")]
    public GameObject State1;
    public GameObject State2;
    public GameObject StateAnim;

    public void Start()
    {
        instance = this;

        Application.targetFrameRate = 90;
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        if (PlayerPrefs.GetInt("Level") < Application.levelCount)
        {
            if (Application.loadedLevel != PlayerPrefs.GetInt("Level"))
            {
                Application.LoadLevel(PlayerPrefs.GetInt("Level"));
            }
        }
        levelText.text = "LEVEL " + PlayerPrefs.GetInt("Level");
        //ElephantSDK.Elephant.LevelStarted(PlayerPrefs.GetInt("Level"));

        Destroy(OnboardText, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        rotateImage.transform.Rotate(0, 0, speed * Time.deltaTime);
    }


   public void NextLevel()
   {
        // ElephantSDK.Elephant.LevelCompleted(PlayerPrefs.GetInt("Level"));
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        if (PlayerPrefs.GetInt("Level") >= Application.levelCount)
        {
            Application.LoadLevel(Random.Range(1, Application.levelCount));
        }
        else
        {
            Application.LoadLevel(PlayerPrefs.GetInt("Level"));
        }
    }
    public void Retry()
    {
        // ElephantSDK.Elephant.LevelFailed(PlayerPrefs.GetInt("Level"));
        Application.LoadLevel(Application.loadedLevel);

    }
}

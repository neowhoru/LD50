using System;
using Platformer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int levelNumber = 1;
    public string LevelFinishedScene = "FinishedScene";

    public int currentHealth = 100;
    
    // Ui stuff
    public Text healthText;
    public Image healthPercentImage;

    public Animator helpPanelAnimator;
    
    [SerializeField] private RectTransform fader;

    private PlayerAudioManager _audioManager;
    void Start()
    {
        UpdateUI();
        _audioManager = GetComponent<PlayerAudioManager>();
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(3,3,3), 0f);
        LeanTween.scale(fader, Vector3.zero, 1f).setEaseInOutQuad() .setOnComplete(() =>
        {
            Debug.Log("Fader finished");
            fader.gameObject.SetActive(false);
            InitDecreaseHealthTimer();
            Invoke(nameof(DisableHelpPanel), 5);
        });
        
        
    }

    private void InitDecreaseHealthTimer()
    {
        InvokeRepeating(nameof(DecreaseHealthByTimer), 1f, 1f);
    }

    public void DisableHelpPanel()
    {
        helpPanelAnimator.SetBool("FadeOut",true);
    }

    public void OnEnable()
    {
        // Add Event Listener
        PlayerColliderPlatformer.RestartLevelNotification += RestartCurrentLevel;
        PlayerColliderPlatformer.AddHealth += AddHealth;
        PlayerColliderPlatformer.PlayClip += PlayAudioClip;
        PlayerColliderPlatformer.LevelFinished += OnLevelFinished;
        Movement.DecreaseHealth += DecreaseHealth;
        Weapon.DecreaseHealth += DecreaseHealth;
        Movement.PlayClip += PlayAudioClip;
    }

    public void OnDisable()
    {
        // Remove Event Listener
        PlayerColliderPlatformer.RestartLevelNotification -= RestartCurrentLevel;
        PlayerColliderPlatformer.AddHealth -= AddHealth;
        PlayerColliderPlatformer.PlayClip -= PlayAudioClip;
        PlayerColliderPlatformer.LevelFinished -= OnLevelFinished;
        Weapon.DecreaseHealth -= DecreaseHealth;
        Movement.DecreaseHealth -= DecreaseHealth;
        Movement.PlayClip -= PlayAudioClip;
        
    }
    
    private void PlayAudioClip(string clipType)
    {
        _audioManager.PlayClip(clipType);
    }

    public void AddHealth(int amount)
    {
        if ((currentHealth + amount) > 100)
        {
            currentHealth = 100;
        }
        else
        {
            currentHealth += amount;    
        }
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthText.text = "Health:" + currentHealth.ToString();
        healthPercentImage.fillAmount = (float)currentHealth / 100;
    }

    public void DecreaseHealthByTimer()
    {
        if (currentHealth >= 0)
        {
            DecreaseHealth(1);    
        }
        else
        {
            CancelInvoke(nameof(DecreaseHealthByTimer));
        }
        
    }
    
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            FindObjectOfType<PlayerColliderPlatformer>().HandleDeath();
            //RestartCurrentLevel();
        }
        UpdateUI();
    }

    public void OnGameStart()
    {
        // Behaviors for Game Starts 
    }

    public void OnLevelFinished()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(3,3,3), 1f).setEaseInOutQuad() .setOnComplete(() =>
        {
            Invoke(nameof(OnLoadNextLevelIfExists), 2);
        });
    }

    public void OnLoadNextLevelIfExists()
    {
        levelNumber =  Int16.Parse(SceneManager.GetActiveScene().name.Substring(5));
        int nextLevelNumber = levelNumber + 1;

        // Check if an Scene with the next level exisits - otherwise load the finish scene
        Debug.Log("Check if scene exists : Level" + nextLevelNumber );
        if (SceneUtility.GetBuildIndexByScenePath("Level" + nextLevelNumber) != -1)
        {
            SceneManager.LoadScene("Level" + nextLevelNumber);
        }
        else
        {
            // Load the "finished" Scene
            SceneManager.LoadScene(LevelFinishedScene);
        }

    }

    public void RestartCurrentLevel()
    {
        Invoke(nameof(RestartLevel), 2);
    }

    public void RestartLevel()
    {
        // Some Fancy effects would be nice 
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(3,3,3), 1f).setEaseInOutQuad() .setOnComplete(() =>
        {
            Invoke(nameof(ReloadCurrentLevel), 2);
        }); 
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

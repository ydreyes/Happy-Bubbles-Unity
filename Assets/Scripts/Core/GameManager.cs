using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  //Convert to singleton
  public static GameManager instance;
  //References in the editor
  [SerializeField] private GameObject restartPanel;
  [SerializeField] private GameObject pauseButton;
  [SerializeField] private Animator camAnime;
  [SerializeField] private Animator timeAnime;
  [SerializeField] private Text timeText;
  [SerializeField] private float timeRemaining = 0;
  [SerializeField] private float slowDownValue = 0.5f;
  [SerializeField] private float slowDownLenght = 2f;
  //private variables
  private bool timeIsRunning = false;

  private void Awake() {
    instance = this;
  }
  private void Start()  {
    AudioManager.instance.PlaylevelMusic();
    timeIsRunning = true;
    UIManager.instance.PauseGame();
  }
  
  private void Update() {
    //Call the method to reduce time
    CountDown();
    
    if (UIManager.isPaused == false) {
      Time.timeScale += (1f / slowDownLenght) * Time.unscaledDeltaTime;
      Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
  }
  
  
  public void AddTime(float addTime) {
    this.timeRemaining += addTime;
    timeAnime.SetTrigger("CanvasTimeAdd");
  }
  
  public void ReduceTime(float decreaseTime) {
    this.timeRemaining -= decreaseTime;
    timeAnime.SetTrigger("CanvasTimeLose");
  }
  
  public void SlowMo() {
    //change the default value of time scale to slow the time
    Time.timeScale = slowDownValue;
    Time.fixedDeltaTime = Time.timeScale * 0.02f;
  }
  
  public void CameraShake() {
    //Call the animation controller and trigger the animation
    camAnime.SetTrigger("CameraShake");
  }
  
  public void CountDown() {
    //If the time is running
    if (timeIsRunning == true) {
      
      if(timeRemaining > 0) {
        timeRemaining -= Time.deltaTime;
        timeText.text = timeRemaining.ToString("0");
      }
      else {
        Time.timeScale = 0;
        timeText.text = "0";
        restartPanel.SetActive(true);
        pauseButton.SetActive(false);
        GameObject.Find("SpawnManager").SetActive(false);
    	GameObject.Find("SpawnWaveManager").SetActive(false);
      }
    }
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
  //editor reference
  [SerializeField] private AudioClip popSFX;
  [SerializeField] private BallSize ballSize;
  //Game Objects reference
  [SerializeField] private GameObject smallBall;
  [SerializeField] private GameObject midBall;
  [SerializeField] private GameObject bigBall;
  [SerializeField] private GameObject collectible;
  [SerializeField] private GameObject explotionFX;
  //Editor variables
  [SerializeField] private float moveSpeed = 0;
  [SerializeField] private float selfDestroyTime = 0;
  [SerializeField] private float changeDirectionTime = 0;
  //private variables
  private float rotateSpeed;
  private float saveDirectionTime;
  private float localSpeed;
  private Vector2 moveDirection;
  private GameManager gameManager;
  [Range(0, 100)] public float chanceToDrop;

  private void Start() {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    this.localSpeed = moveSpeed;
    this.rotateSpeed = Random.Range(30f, 60f);
    ChangeDirection();
    saveDirectionTime = changeDirectionTime;
  }
  
  private void FixedUpdate() {
    this.RotateBall();
    this.DestroyBallAfterTime();
    this.TranslateBall();
  }
  
  private void Update() {
    if(changeDirectionTime > 0) {
      changeDirectionTime -= Time.deltaTime;
    }
    else {
      ChangeDirection();
      changeDirectionTime = saveDirectionTime;
    }
  }
  
  private void OnMouseDown() {
    AudioManager.instance.PlaySFX(0);
      DestroyBall();
  }
  
  //Rotate te ball around itself
  private void RotateBall() {
    this.transform.Rotate(0f, 0f, this.rotateSpeed * Time.deltaTime, Space.Self);
  }
  
  //translate the ball arround the scene
  private void TranslateBall() {
    this.transform.Translate(this.moveDirection * this.moveSpeed * Time.deltaTime, Space.World);
  }
  
  //Change the direction of the ball
  private void ChangeDirection() {
    this.moveDirection = Random.insideUnitCircle.normalized;
  }
  
  //Destroy the ball after a certain time
  private void DestroyBallAfterTime() {
    selfDestroyTime -= Time.deltaTime;
    float timeToReduce = Random.Range(1, 5);
    if(selfDestroyTime <= 0) {
      Destroy(this.gameObject);
      gameManager.ReduceTime(Mathf.RoundToInt(timeToReduce));
      Instantiate(explotionFX, this.transform.position, this.transform.rotation);
    }
  }
  
  //Change to Spawn a bonus time object
  private void ChanceToDrop() {
    float dropSelect = Random.Range(0, 100f);
    if (dropSelect <= chanceToDrop) {
      Instantiate(collectible, this.transform.position, this.transform.rotation);
    }
  }
  
  //Destroy the balls
  private void DestroyBall() {
    //Switch between cases of the ball size
    switch (ballSize) {
      //if is big size
    case BallSize.big:
        GameObject.Find("SpawnWaveManager").GetComponent<SpawnWaveManager>().EnemyDestroyed();
        Destroy(this.gameObject);
        Instantiate(explotionFX, this.transform.position, this.transform.rotation);
        ChanceToDrop();
        break;
      //If is mid size
    case BallSize.mid:
        GameObject.Find("SpawnWaveManager").GetComponent<SpawnWaveManager>().EnemyDestroyed();
        Destroy(this.gameObject);
        Instantiate(explotionFX,this.transform.position, this.transform.rotation);
        ChanceToDrop();
        break;
      //If if a small ball
    case BallSize.small:
        GameObject.Find("SpawnWaveManager").GetComponent<SpawnWaveManager>().EnemyDestroyed();
        Destroy(this.gameObject);
        Instantiate(explotionFX, this.transform.position, this.transform.rotation);
        ChanceToDrop();
        break;
      //Debug log error message
      default:
        Debug.LogError("Error Instantiating balls");
        break;
    }
  }
}
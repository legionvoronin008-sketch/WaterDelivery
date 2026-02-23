using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player_control : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;
    public float BoostTime = 5f;
    public float SpeedBoostAmount = 2f;
    public float JumpBoostAmount = 2f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sprite;
    public Image img;
    private float currentTimeUI;
    public float MaxTime;
    public Transform FirePoint2 ; 
    public Transform FirePoint1 ; 
    public Transform FirePoint_Waterbomb ; 
    public GameObject bulletPrefab;
    public GameObject WaterbombPrefab;
    public float BulletSpeed;
    private bool IsdoubleJumped = true;
    private bool Imaging = false;
    private Transform currentPlatform;
    private Vector2 lastPLposition;
    private Coroutine doubleJumpCoroutine;
    public GameObject WaterGenerator_Hint;
    private  static Vector2  spawn;
    public  static bool HasSessionSpawn = false;
    private Collider2D currentWaterBox;
    private bool WaterTriggerFlag = false;
    private bool WaterTriggerFlag1 = false;
    private bool WaterTriggerFlag2 = false;
    private bool HelpTPFlag = false;
    private bool HelpTPFlag1 = false;
    private bool HelpTPFlag2 = false;
    private static int GeneratorCounter = 0;
    private static int TiredPersonCounter = 0;
    private int SavedPeople = 0;
    public TextMeshProUGUI  NumberGenerators;
    public TextMeshProUGUI  NumberTiredPerson;
    public TextMeshProUGUI  NumberSavedPeople;
    public GameObject JumpPT;
    public GameObject EnergyPT;
    private bool isAttacking = false;
    public GameObject Win_Pannel;
    public TextMeshProUGUI  TotalNumberSavedPeople;
    private int currentIndex;
    private int totalScenes;

    private bool isGrounded = true;
    private float moveInput;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (HasSessionSpawn == false)
        {
           spawn = transform.position;
           HasSessionSpawn = true;
        }
        rb.position = spawn;
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        totalScenes = SceneManager.sceneCountInBuildSettings;
    }
    void Start()
    {
        //if (rb == null) rb = GetComponent<Rigidbody2D>();
        //if (animator == null) animator = GetComponent<Animator>();
        //if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        rb.velocity = Vector2.zero;
        //rb.position = Reload_Level.SpawnPoint;
        currentTimeUI = 0;
        NumberGenerators.text = GeneratorCounter.ToString();
        NumberTiredPerson.text = TiredPersonCounter.ToString();
    }

    void Update()
    {
        // Получаем Input
        moveInput = Input.GetAxisRaw("Horizontal");

        if (isAttacking == false)
        {
            animator.SetBool("isMoving", moveInput != 0);
        }

        if (moveInput < 0) sprite.flipX = true;
        if (moveInput > 0) sprite.flipX = false;

        if (Imaging == true)
        {
            currentTimeUI += Time.deltaTime;
            img.fillAmount = currentTimeUI / MaxTime;
        }
  
        if(Input.GetButtonDown(Constants.FIRE_1))
        {
                StartCoroutine(timer());   
        }
        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            currentPlatform = null;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false)
        {
            if (IsdoubleJumped == true)
            {
                StartDoubleJumpTimer();
            }
        }
        if (Input.GetKeyDown(KeyCode.E) &&  WaterTriggerFlag == true)
        {
                Debug.Log("Click");
                spawn = currentWaterBox.transform.position;
                Debug.Log("Spwan up");
                Constants.WaterGeneratorFlag = true;
                GeneratorCounter++;
                NumberGenerators.text = GeneratorCounter.ToString();
                Debug.Log(Constants.WaterGeneratorFlag);
        }
        else if (Input.GetKeyDown(KeyCode.E) &&  WaterTriggerFlag1 == true)
        {
            Debug.Log("Click");
            spawn = currentWaterBox.transform.position;
            Debug.Log("Spwan up");
            Constants.WaterGeneratorFlag1 = true;
            GeneratorCounter++;
            NumberGenerators.text = GeneratorCounter.ToString();
            Debug.Log(Constants.WaterGeneratorFlag1);
        }
        else if (Input.GetKeyDown(KeyCode.E) &&  WaterTriggerFlag2 == true)
        {
            Debug.Log("Click");
            spawn = currentWaterBox.transform.position;
            Debug.Log("Spwan up");
            Constants.WaterGeneratorFlag2 = true;
            GeneratorCounter++;
            NumberGenerators.text = GeneratorCounter.ToString();
            Debug.Log(Constants.WaterGeneratorFlag2);
        }
        else if (Input.GetKeyDown(KeyCode.E) &&  HelpTPFlag == true)
        {
            Constants.TiredPerson = true;
            TiredPersonCounter++;
            NumberTiredPerson.text = TiredPersonCounter.ToString();
            StartCoroutine(Boost());
        }
        else if (Input.GetKeyDown(KeyCode.E) &&  HelpTPFlag1 == true)
        {
            Constants.TiredPerson1 = true;
            TiredPersonCounter++;
            NumberTiredPerson.text = TiredPersonCounter.ToString();
            StartCoroutine(Boost());
        }
        else if (Input.GetKeyDown(KeyCode.E) &&  HelpTPFlag2 == true)
        {
            Constants.TiredPerson2 = true;
            TiredPersonCounter++;
            NumberTiredPerson.text = TiredPersonCounter.ToString();
            StartCoroutine(Boost());
        }
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput * speed;
        float currentY = rb.velocity.y;
        // Движение через velocity (правильно для Rigidbody2D)
        rb.velocity = new Vector2(
        Mathf.Lerp(rb.velocity.x, targetSpeed, 0.2f),
        currentY);
        if (currentPlatform != null)
        {
            Vector2 platformDelta = (Vector2)currentPlatform.position - lastPLposition;
            rb.MovePosition(rb.position + (Vector2)platformDelta);
            lastPLposition = currentPlatform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if  (collision.gameObject.CompareTag("Movingplatform") || collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("ISJump",false);
        }
        if (collision.gameObject.CompareTag("Movingplatform"))
        {
            currentPlatform = collision.transform;
            lastPLposition = currentPlatform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Movingplatform") )
        {
            isGrounded = true;
            animator.SetBool("ISJump",false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Movingplatform"))
        {
            isGrounded = false;
            animator.SetBool("ISJump",true);
        }
        if (collision.gameObject.CompareTag("Movingplatform"))
        {
            currentPlatform = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            HasSessionSpawn = false;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Constants.WaterGeneratorFlag2 = false;
            Constants.WaterGeneratorFlag1 = false;
            Constants.WaterGeneratorFlag = false;
            Constants.TiredPerson = false;
            Constants.TiredPerson1 = false;
            Constants.TiredPerson2 = false;
            GeneratorCounter = 0;
            Constants.SavedPeoplenumber += SavedPeople;
            NumberGenerators.text = GeneratorCounter.ToString();
            TiredPersonCounter = 0;
            NumberTiredPerson.text = TiredPersonCounter.ToString();
            if (currentIndex + 1 < totalScenes)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Win_Pannel.SetActive(true);
                TotalNumberSavedPeople.text = Constants.SavedPeoplenumber.ToString();
                Time.timeScale = 0f;
            }
        }
        else if (collision.CompareTag("WaterGenerator_box1"))
        {
            WaterTriggerFlag1 = true;
            currentWaterBox = collision;
            WaterGenerator_Hint.SetActive(true);
        }
        else if (collision.CompareTag("WaterGenerator_box"))
        {
            WaterTriggerFlag = true;
            currentWaterBox = collision;
            WaterGenerator_Hint.SetActive(true);
        }
        else if (collision.CompareTag("WaterGenerator_box2"))
        {
            WaterTriggerFlag2 = true;
            currentWaterBox = collision;
            WaterGenerator_Hint.SetActive(true);
        }
        else if (collision.CompareTag("TiredPerson"))
        {
            WaterGenerator_Hint.SetActive(true);
            HelpTPFlag = true;
        }
        else if (collision.CompareTag("TiredPerson1"))
        {
            WaterGenerator_Hint.SetActive(true);
            HelpTPFlag1 = true;
        }
        else if (collision.CompareTag("TiredPerson2"))
        {
            WaterGenerator_Hint.SetActive(true);
            HelpTPFlag2 = true;
        }
 
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
        //if (collision.CompareTag("WaterGenerator_box1"))
        //{
            //if (Input.GetKeyDown(KeyCode.E))
            //{
                 //Debug.Log("Click");
                //spawn = collision.transform.position;
                //Debug.Log("Spwan up");
           // }
        //}

   // }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterGenerator_box1"))
        {
            WaterTriggerFlag1 = false;
            currentWaterBox = null;
            WaterGenerator_Hint.SetActive(false);
        }
        else if (collision.CompareTag("WaterGenerator_box"))
        {
             WaterTriggerFlag = false;
            currentWaterBox = null;
            WaterGenerator_Hint.SetActive(false);
        }
        else if (collision.CompareTag("WaterGenerator_box2"))
        {
            
            WaterTriggerFlag2 = false;
            currentWaterBox = null;
            WaterGenerator_Hint.SetActive(false);
        }
        else if (collision.CompareTag("TiredPerson"))
        {
            WaterGenerator_Hint.SetActive(false);
            HelpTPFlag = false;
        }
        else if (collision.CompareTag("TiredPerson1"))
        {
            WaterGenerator_Hint.SetActive(false);
            HelpTPFlag1 = false;
        }
        else if (collision.CompareTag("TiredPerson2"))
        {
            WaterGenerator_Hint.SetActive(false);
            HelpTPFlag2 = false;
        }
   }
    private void StartDoubleJumpTimer()
    {
        if (doubleJumpCoroutine != null)
        {
            StopCoroutine(doubleJumpCoroutine);
        }
        currentTimeUI = 0f;
        img.fillAmount = 0f;
        doubleJumpCoroutine = StartCoroutine(timerDBjump());
    }
    private void Shoot()
    {
        if (sprite.flipX == true)
        {
            GameObject bullet = Instantiate(bulletPrefab, FirePoint1.position, Quaternion.identity);
            Rigidbody2D rbbullet = bullet.GetComponent<Rigidbody2D>();
            rbbullet.velocity = Vector2.left * BulletSpeed;
        }
        else if (sprite.flipX == false)
        {
            GameObject bullet = Instantiate(bulletPrefab, FirePoint2.position, Quaternion.identity);
            Rigidbody2D rbbullet = bullet.GetComponent<Rigidbody2D>();
            rbbullet.velocity = Vector2.right * BulletSpeed;
        }
    }
    public void AddPerson()
    {
        SavedPeople++;
        NumberSavedPeople.text = SavedPeople.ToString();
    }
    //public void WinCond()
    //{
        //if (SceneManager.GetActiveScene().buildIndex == 3 && Constants.WaterGeneratorFlag2 == true && Constants.WaterGeneratorFlag1 == true && Constants.WaterGeneratorFlag == true)
        //{
            //Win_Pannel.SetActive(true);
            //TotalNumberSavedPeople.text = Constants.SavedPeoplenumber.ToString();
        //}
    //}
   
    //void SetSpawnPoint()
    //{
        
        //if (PlayerPrefs.GetInt("HasSpawn",0) ==1)
        //{
            //float x = PlayerPrefs.GetFloat("SpawnX");
            //float y = PlayerPrefs.GetFloat("SpawnY");
            //rb.position = new Vector2(x,y);
       // }
     
    //}
    private IEnumerator timer()
  {
      isAttacking = true;
      animator.SetBool("isMoving", false);
      animator.SetBool("ISJump",false);
      animator.SetTrigger("Attacking");
      yield return new WaitForSeconds(0.67f);
      Shoot();
      yield return new WaitForSeconds(0.15f);
       //animator.SetTrigger("AnimationBack");
       isAttacking = false;

  }
  //private IEnumerator timer1()
  //{
      //animator.SetBool("isMoving", false);
      //animator.SetBool("ISJump",false);
      //animator.SetTrigger("Attacking");
      
  //}
  private IEnumerator timerDBjump()
  {
      Imaging = true;
      IsdoubleJumped = false;
      rb.AddForce(Vector2.up * jumpForce * 1.45f, ForceMode2D.Impulse);
      Instantiate(JumpPT ,FirePoint_Waterbomb.position, Quaternion.identity);
      Instantiate(WaterbombPrefab, FirePoint_Waterbomb.position, Quaternion.identity);
      float duration = 5f;
      while (currentTimeUI < duration)
      {
          currentTimeUI += Time.deltaTime;
          img.fillAmount = currentTimeUI/ duration;
          yield return null;
      }
      img.fillAmount = 1f;
      IsdoubleJumped = true;
      Imaging = false;
  }
  private IEnumerator Boost()
  {
      Instantiate(EnergyPT ,transform.position, Quaternion.identity);
      speed = speed * SpeedBoostAmount;
      jumpForce = jumpForce * JumpBoostAmount;
      yield return new WaitForSeconds(BoostTime);
      speed = speed / SpeedBoostAmount;
      jumpForce = jumpForce / JumpBoostAmount;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    private int _health = 10;
    private NewSpawnManager _spawnManager;
   

    [SerializeField]
    private TextMeshProUGUI _livesText;
   
    private statsbar statsBar;

    public GameObject healthbarVisual; 


    void Start()
    {

        statsBar = FindObjectOfType<statsbar>();

        UpdateLivesText();
       
        transform.position = new Vector3(2.25f, 9.45f, 0);

        _spawnManager = GameObject.Find("spawnManager").GetComponent<NewSpawnManager>();
   

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

    }

   
    void Update()
    {

        CalulateMovement();

        //if i hit the space key 
        //spawn gameObject

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "eLaser")
        {
            TakeDamage();
            Destroy(other.gameObject);
        }

        if(other.tag == "Knight")
        {
            _health = _health - 3;
            statsBar.KnightHit();
            Debug.Log("Player Health:" + _health);
            Debug.Log("hit knight");
        }
    }

    void CalulateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //new vector3(1, 0, 0) * 5 * real time 
      
        Vector3 Direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(Direction * _speed * Time.deltaTime);

        //if player postition on the y is greater than 0
        //y position = 0 
        //else if position on the y is less than -3.8f
        //y pos = 3.8f 

       //Mathf.Clamp allows for a value that passes through a min / max value 
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 3.9f, 10f), 0);

        float minX = 2f;
        float maxX = 16f;

        // Clamp the player's x position between minX and maxX
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);


        // Set the new position based on the clamped x value
        transform.position = new Vector3(clampedX, transform.position.y, 0);

       // if (transform.position.x >= 12.1)
        //{
            //transform.position = new Vector3(12.1f, transform.position.y, 0);

       // }

       // else if (transform.position.x <= -12.1)
       // {
           // transform.position = new Vector3(-12.1f, transform.position.y, 0);
       // }
    }

    void FireProjectile()
    {
        _canFire = Time.time + _fireRate;
        // Quaternion.identity means keeping default rotation
        Instantiate(_laserPrefab, transform.position + new Vector3(0.8f, -0.1f, 0), Quaternion.identity);
        
    }

    public void LoseLife()
    {
        _lives--;
        UpdateLivesText();
        //_lives = _lives -1;
        //_lives -= 1;

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath1();
            _spawnManager.OnPlayerDeath2();
            Destroy(this.gameObject);
            healthbarVisual.SetActive(false);
            _livesText.text = "0";
            
        }
    }

    public void TakeDamage()
    {
        _health--;
        UpdateLivesText();

        if(_health <= 0)
        {
            LoseLife();
            _health = 10;
            statsBar.health = 1;
        }

        if (statsBar != null)
        {
            statsBar.DecreaseHealth();
        }
    }

    void UpdateLivesText()
    {
        if (_livesText != null)
        {
            _livesText.text =  _lives.ToString();
        }

       
        
    }
}



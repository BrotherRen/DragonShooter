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
    //private float _speedBoostMultiplier = 2;
    private float _speedBoost = 10.0f;

    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleshotPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    private int _health = 10;
    private NewSpawnManager _spawnManager;

    [SerializeField]
    private bool _tripleShotON = false;
    [SerializeField]
    private bool _speedBoostON = false;

    [SerializeField]
    private bool _shieldBoostON = false;
   

    [SerializeField]
    private TextMeshProUGUI _livesText;
   
    private statsbar statsBar;

    public GameObject healthbarVisual;

    [SerializeField]
    private GameObject _onFireVisualPrefab;

    private EvolutionManager evolutionmanager;

    private Coroutine _tripleShotCoroutine;
    private Coroutine _speedBoostCoroutine;
    private Coroutine _shieldBoostCoroutine;


    void Start()
    {
        _shieldPrefab.SetActive(false);
        _onFireVisualPrefab.SetActive(false);
        statsBar = FindObjectOfType<statsbar>();
        evolutionmanager = FindObjectOfType<EvolutionManager>();
        if (evolutionmanager == null)
        {
            Debug.LogError("EvolutionManager not found");
        }

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

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            FireProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    void CalulateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //new vector3(1, 0, 0) * 5 * real time 
      
        Vector3 Direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_speedBoostON == false)
        {

            transform.Translate(Direction * _speed * Time.deltaTime);
        }

        else
        {
            transform.Translate(Direction * _speedBoost * Time.deltaTime);
        }
       
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

        if (_tripleShotON == true)
        {
            Instantiate(_tripleshotPrefab, transform.position , Quaternion.identity);
        }

        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0.8f, -0.1f, 0), Quaternion.identity);
        }

           
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
            
            Destroy(this.gameObject);
            healthbarVisual.SetActive(false);
            _livesText.text = "0";
            
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (_shieldBoostON == true)
        {
           
            

            return;
        }
        else
        {
            evolutionmanager.PlayerDamaged();
            _health -= damageAmount;
            UpdateLivesText();
            Debug.Log("TAKING DAMAGE");
        }


        if (statsBar != null)
        {
            statsBar.DecreaseHealth(damageAmount);
        }

        if (_health <= 0)
        {
            LoseLife();
            _health = 10;
            statsBar.ResetHealth();
        }
    }

    void UpdateLivesText()
    {
        if (_livesText != null)
        {
            _livesText.text =  _lives.ToString();
        }

       
        
    }

    public void TripleShotActive()
    {
        _tripleShotON = true;

        if (_tripleShotCoroutine != null)
        {
            StopCoroutine(_tripleShotCoroutine);
        }
       
        _tripleShotCoroutine = StartCoroutine(TripleShotPowerDown());
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotON = false;
        _tripleShotCoroutine = null;
    }

    public void SpeedBoostActive()
    {
        _speedBoostON = true;

        if (_speedBoostCoroutine != null)
        {
            // Stop the existing coroutine to reset its duration
            StopCoroutine(_speedBoostCoroutine);
        }
        _speedBoostCoroutine = StartCoroutine(SpeedBoostPowerDownRoutine());


        //StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldsActive()
    {
        _shieldBoostON = true;
        _shieldPrefab.SetActive(true);
        Debug.Log("SHIELD ON");

        if (_shieldBoostCoroutine != null)
        {
            // Stop the existing coroutine to reset its duration
            StopCoroutine(_shieldBoostCoroutine);
        }

        _shieldBoostCoroutine = StartCoroutine(ShieldBoostPowerDownRoutine());
    }


    public void EvolutionOneActive()
    {
        _onFireVisualPrefab.SetActive(true);
        
    }

    public void EvolutionOneOff()
    {
        _onFireVisualPrefab.SetActive(false);
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostON = false;
        _speedBoostCoroutine = null;
        
    }


    IEnumerator ShieldBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldBoostON = false;
        _shieldPrefab.SetActive(false);
        _shieldBoostCoroutine = null;
        Debug.Log("SHIELD OFF");
    }

}




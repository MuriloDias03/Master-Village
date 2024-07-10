using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isPaused;

    public float speed = 3;
    public float runSpeed = 6;
    public float speedRoll = 7;
    public float health = 20;
    public float totalHealth = 20;
    public Image healthBar;
    public bool isDeath;

    private Rigidbody2D rig;
    private PlayerItens playerItems;
    
    private float initialSpeed;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;
    private bool _isAttack;

    private Vector2 _direction;

    [HideInInspector] public int handlingObj;

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }

    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }

    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }

    public bool isAttack
    {
        get { return _isAttack; }
        set { _isAttack = value; }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItens>();

        initialSpeed = speed;
    }

    private void Update()
    {
        if (!isPaused && !isDeath)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                handlingObj = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingObj = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingObj = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                handlingObj = 3;
            }

            OnInput();
            OnRun();
            OnRolling();
            OnCutting();
            OnDig();
            OnWatering();
            OnAttack();
            Cure();
        }
        Exit();
    }
    private void FixedUpdate()
    {
        if (!isPaused && !isDeath)
        {
            OnMove();
        }
        }

    #region Movement

    void OnAttack()
    {
        if (handlingObj == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isAttack = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isAttack = false;
                speed = initialSpeed;
            }
        }
        else
        {
            isAttack = false;
        }
    }

    void OnWatering()
    {
        if (handlingObj == 2)
        {
            if (Input.GetMouseButtonDown(0) && playerItems.currentWater > 0)
            {     
                _isWatering = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0) || playerItems.currentWater < 0)
            {
                _isWatering = false;
                speed = initialSpeed;
            }

            if (isWatering)
            {
                playerItems.currentWater -= 0.01f;
            }
        }
        else
        {
            isWatering = false;
        }
    }

    void OnDig()
    {
        if(handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }
        else
        {
            isDigging = false;
        }
    }

    void OnCutting()
    {
        if (handlingObj == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
        else
        {
            isCutting = false;
        }
    }

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);

    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    void OnRolling()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = speedRoll;
            isRolling = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = initialSpeed;
            isRolling = false;
        }
    }

    #endregion

    void Cure()
    {
        if (Input.GetKey(KeyCode.Q) && playerItems.carrots > 0)
        {
            playerItems.carrots--;
            health += 2;

            healthBar.fillAmount = health / totalHealth;
        }

        if (Input.GetKey(KeyCode.R) && playerItems.fishes > 0)
        {
            playerItems.fishes--;
            health += 4;

            healthBar.fillAmount = health / totalHealth;
        }
    }

    void Exit()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Saiu do jogo");
            Application.Quit();
        }
    }
}
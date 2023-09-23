using UnityEngine;

public class Player_Movement : MonoBehaviour, ISaveManager
{
    public static Player_Movement Instance = null;

    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    bool facingRight = true;

    bool isInteracting;

    [SerializeField] Animator animator;

    public void LoadData(GameData data)
    {
        transform.position = data.currentPos;
    }

    public void SaveData(GameData data)
    {
        data.currentPos = transform.position;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Creating PlayerMovement");
            Instance = this;
        }
        else
        {
            Debug.Log("Destroying duplicate PlayerMovement");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isInteracting = false;
    }

    // Update is called once per frame
    void Update() //Inputs
    {
        if (isInteracting)
        {
            return;
        }

        Pause();
        Inventory();
        ProcessInputs();
    }

    void FixedUpdate() //Physics Calculations
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        if (isInteracting)
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(moveDirection.x), Mathf.Abs(moveDirection.y)));
            if (moveDirection.x > 0 && !facingRight)
            {
                flip();
            } 
            else if (moveDirection.x < 0 && facingRight)
            {
                flip();
            }
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.Instance.setPauseMenuAs(true);
        }
    }

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public bool getIsInteracting()
    {
        return isInteracting;
    }

    public void setIsInteracting(bool interacting)
    {
        isInteracting = interacting;
    }

    void Inventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            FindObjectOfType<InventoryMenu>().setInventoryMenuAs(true);
        }
    }

}
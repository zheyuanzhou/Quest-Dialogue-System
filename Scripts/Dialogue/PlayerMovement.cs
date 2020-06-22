using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private float moveSpeed;
    private float moveH, moveV;

    public bool isTalking = false;

    //MARKER ADD Jump Feature
    [Header("Jump")]
    [SerializeField] private float jumpForce;

    [SerializeField] private bool isGround;
    public Transform checkPoint;
    [SerializeField] private Vector2 checkBoxSize;
    public LayerMask layerMask;

    [Header("Second Jump")]
    [SerializeField] private float jumpTimes;
    [SerializeField] private bool canJump;

    [Header("Better Jump")]
    [SerializeField] private float fallFactor;
    [SerializeField] private float shortJumpFactor;

    //MARKER Scene Transition
    public string scenePassword;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        Physics2D.queriesStartInColliders = false;
        isGround = false;
        canJump = false;
    }

    private void Update()
    {
        #region
        //moveH = Input.GetAxis("Horizontal") * moveSpeed;
        //moveV = Input.GetAxis("Vertical") * moveSpeed;

        //if(canMove)
        //Flip();
        #endregion

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            canJump = true;
            jumpTimes--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && jumpTimes > 0 && !canJump)
        {
            jumpTimes--;
            canJump = true;
        }

        moveH = Input.GetAxis("Horizontal") * moveSpeed;

        if (!isTalking)
            Flip();

        CheckGround();

        if (isGround && !canJump)
            jumpTimes = 1;
    }

    private void FixedUpdate()
    {
        #region
        //if (canMove)
        //    rb.velocity = new Vector2(moveH, moveV);
        //else
        //rb.velocity = Vector2.zero;
        #endregion

        if (canJump && !isTalking)
        {
            rb.velocity = Vector2.up * jumpForce;
            canJump = false;
        }

        if (!isTalking)
            rb.velocity = new Vector2(moveH, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, -rb.gravityScale * 9.8f);

        BetterJump();
    }

    private void Flip()
    {
        if(moveH > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (moveH < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void CheckGround()
    {
        Collider2D collider = Physics2D.OverlapBox(checkPoint.position, checkBoxSize, 0, layerMask);

        if (collider != null)//I have detected we are ON THE GROUND
        {
            isGround = true;
            jumpTimes = 1;//MARKER Double Jump
        }
        else
        {
            isGround = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPoint.position, checkBoxSize);
        Gizmos.color = Color.green;
    }

    private void BetterJump()
    {
        if (rb.velocity.y < 0)//MARKER 角色下落时，速度会越来越快 Player Falling down, Faster Speed!
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallFactor * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * shortJumpFactor * Time.deltaTime;
        }
    }

}

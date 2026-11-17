using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")]
    private PlayerInputs inputs;

    private Rigidbody2D rb;

    [Header("Movements")]
    public float speed;
    private Vector2 moveDir;
    public Vector2 lastMoveDir;

    [Header("Animations")]
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        inputs = new PlayerInputs();
    }

    void OnEnable()
    {
        inputs.Enable();
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    void Update()
    {
        moveDir = inputs.Player.Move.ReadValue<Vector2>();

        if (moveDir != Vector2.zero) lastMoveDir = moveDir;
        
        anim.SetFloat("moveX", lastMoveDir.x);
        anim.SetFloat("moveY", lastMoveDir.y);
        anim.SetBool("isMoving", moveDir != Vector2.zero);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDir * Time.fixedDeltaTime * (speed * 10);
    }
}

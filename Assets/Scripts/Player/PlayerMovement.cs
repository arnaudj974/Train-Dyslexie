using System.Collections.Generic;
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
    public List<bool> cantMoveReasons = new List<bool>();

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

        if (MoveStatus())
        {
            anim.SetFloat("moveX", lastMoveDir.x);
            anim.SetFloat("moveY", lastMoveDir.y);
            anim.SetBool("isMoving", moveDir != Vector2.zero);
        }
    }

    void FixedUpdate()
    {
        if (MoveStatus())
            rb.linearVelocity = moveDir * Time.fixedDeltaTime * (speed * 10);
    }

    public PlayerInputs GetInputs()
    {
        return inputs;
    }

    public void CanMove(bool value)
    {
        if (value)
            if (cantMoveReasons.Count > 0) cantMoveReasons.Remove(true);
            else
                cantMoveReasons.Add(true);
    }

    public bool MoveStatus()
    {
        if (cantMoveReasons.Count > 0) return false;
        return true;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
    }

    public void Load()
    {
        float x = PlayerPrefs.GetFloat("PlayerPosX");
        float y = PlayerPrefs.GetFloat("PlayerPosY");

        transform.position = new Vector3(x, y, 0f);
    }
}

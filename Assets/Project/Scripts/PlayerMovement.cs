using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : LifeController
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float dashForce;
    [SerializeReference] private float dashDuration;
    public bool canMove;
    private Vector2 dir;
    private bool facingLeft;
    private bool onDash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();

        rb.gravityScale = 0;

        canMove = true;
    }

    private void Update()
    {
        PlayerInputs();
        Animations();

        if (dir.x > 0 && facingLeft || dir.x < 0 && !facingLeft) Flip();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInputs()
    {
        if (!canMove) return;

        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");

        dir = new Vector2(_x, _y);
        dir.Normalize();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !onDash) StartCoroutine(Dash());
    }

    private void Move()
    {
        rb.velocity = dir * speed;
    }

    private IEnumerator Dash()
    {
        onDash = true;
        canMove = false;
        canTakeDamage = false;

        yield return new WaitForSeconds(dashDuration);

        onDash = false;
        canMove = true;
        canTakeDamage = true;
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(Vector2.up * 180);
    }

    private void Animations()
    {
        if (dir.x != 0) anim.SetFloat("Speed_X", 1);
        else anim.SetFloat("Speed_X", 0);

        anim.SetFloat("Speed_Y", dir.y);
    }
}

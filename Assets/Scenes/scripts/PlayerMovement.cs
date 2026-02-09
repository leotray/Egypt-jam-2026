using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private float horizontal;
    private float speed = 8f;
    private float acceleration = 7f; // لتسريع تدريجي
    private float deceleration = 7f; // لتباطؤ تدريجي
    private float jumpingPower = 16f;
    private float currentVelocity; // للمساعدة في التنعيم
    private bool isFacingRight = true;

    [Header("Double Jump")]
    private bool canDoubleJump;
    [SerializeField] private float doubleJumpTrailTime = 0.3f; // مدة أثر القفزة الثانية

    [Header("Dash Settings")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 18f;
    private float dashingTime = 0.15f;
    private float dashingCooldown = 0.2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    void Update()
    {
        if (isDashing) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        // القفز المزدوج مع تأثير بصري
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower * 0.85f); // القفزة الثانية أخف قليلاً للواقعية
                canDoubleJump = false;
                StartCoroutine(DoubleJumpEffect()); // تفعيل الأثر للقفزة الثانية
            }
        }

        // قفزة سلسة (قصيرة عند الضغط الخفيف)
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        // جعل الحركة الأفقية سلسة جداً (Smooth Acceleration/Deceleration)
        float targetSpeed = horizontal * speed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 0.9f) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // أثر بصري للقفزة المزدوجة
    private IEnumerator DoubleJumpEffect()
    {
        if (tr != null)
        {
            tr.emitting = true;
            yield return new WaitForSeconds(doubleJumpTrailTime);
            if (!isDashing) tr.emitting = false; // لا نغلقه إذا كان اللاعب قد بدأ داش فجأة
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        if (tr != null) tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        if (tr != null) tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
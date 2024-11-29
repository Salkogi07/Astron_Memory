using System.Collections;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{
    Player_Move playerMove;
    Rigidbody2D rb;
    TrailRenderer tr;

    [Header("Dash info")]
    [SerializeField] public bool canDash = true;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    void Awake()
    {
        playerMove = GetComponent<Player_Move>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        playerMove.isDashing = true;
        //playerAnimator.PlayAnimation("Dash");
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        int dir = playerMove.isFacingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower * dir, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        playerMove.isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

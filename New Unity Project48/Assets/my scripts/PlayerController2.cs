using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : Unit
{

    [SerializeField]
    private int lives = 5;
    [SerializeField]
    private float playerspeed = 3.0f;
    private float jumpForce = 7.0F;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isGrounded = false;
    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }


    private void Awake()
    {
       
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

      ;
    }

    private void FixedUpdate()
    {
     CheckGround();
    }

    private void Update()
    {
      if (isGrounded) State = CharState.Idle;

       // if (Input.GetButtonDown("Fire1")) Fire();
        if (Input.GetButton("Horizontal")) Run();
       if (isGrounded && !isLadder && Input.GetButtonDown("Jump")) Jump();
        if (isLadder && Input.GetButton("Vertical")) Climb();
        if (isLadder && Input.GetButton("Vertical")) State = CharState.Climbed;


    }
   
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, playerspeed * Time.deltaTime);

        sprite.flipX = direction.x > 0.0F;

        if (isGrounded) State = CharState.Run;

         
         }

        private void Jump()
        {
           rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }


        //  private void Shoot()
        // {
        //    Vector3 position = transform.position; position.y += 0.8F;
        //   Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        //  newBullet.Parent = gameObject;
        // newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
   // }

    // public override void ReceiveDamage()
    // {
    //    Lives--;
    //
    //   rigidbody.velocity = Vector3.zero;
    //   rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);

    //   Debug.Log(lives);
    // }

    private void CheckGround()
     {
         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

   if (!isGrounded) State = CharState.Jump;
       
        if(!isGrounded) rigidbody.gravityScale = 3;

    }

    // private void OnTriggerEnter2D(Collider2D collider)
    //  {

    //     Bullet bullet = collider.gameObject.GetComponent<Bullet>();
    //    if (bullet && bullet.Parent != gameObject)
    //    {
    //        ReceiveDamage();
    //     }
    //  }
    private void Climb()
    {
        Vector3 direction = transform.up * Input.GetAxis("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, playerspeed * Time.deltaTime);
        rigidbody.gravityScale = 0;

        if (isLadder) State = CharState.Climbed;
    }

}
public enum CharState
{
    Idle,
    Run,
    Fire,
    Climbed,
    Jump
}
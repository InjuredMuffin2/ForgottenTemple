using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerWithRaycasting : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float speed;
    public float jumpForce = 70;
    public Animator theAnimator;
    public bool isGrounded;

    public Transform groundRayOrigin;
    public float groundRayLength;
    public LayerMask groundLayer;


    void CheckForGround()
    {
        // The following line isn't really neccessary, it simply draws the ray that is being cast. It is
        // simply to allow us to see it, it actually does nothing.
        Debug.DrawRay(groundRayOrigin.position, Vector2.down * groundRayLength, Color.yellow);

        if (theRB.velocity.y < 0)
        {
            // The following line of code casts a ray. 
            // See https://docs.unity3d.com/ScriptReference/Physics2D.Raycast.html

            bool rayHit = Physics2D.Raycast(groundRayOrigin.position, Vector2.down, groundRayLength, groundLayer);

            // If rayHit is true then the ray we just must have hit something in the "groundLayer". If there are
            // only "ground" objects in the "groundLayer" then the ray must have hit a piece of ground.
            if (rayHit == true)
            {
                isGrounded = true;
                theAnimator.SetBool("jump", false);
            }
            else
            {
                isGrounded = false;
            }
        }

    }

    void Update()
    {
        CheckForGround();

        if (Input.GetKey(KeyCode.RightArrow) == true && isGrounded)
        {
            theRB.velocity = Vector2.right * speed;
            theAnimator.SetFloat("walkSpeed", speed);
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) == true && isGrounded)
        {
            theRB.velocity = Vector2.left * speed;
            theAnimator.SetFloat("walkSpeed", speed);
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }
        else if (isGrounded)
        {
            theRB.velocity = new Vector2(0, 0);
            theAnimator.SetFloat("walkSpeed", 0);
        }


        if ((Input.GetKeyDown(KeyCode.Space) == true) && (isGrounded == true))
        {
            Vector2 forceToApply = Vector2.up * jumpForce;
            theRB.AddForce(forceToApply);

            // Technicially I don't need the following line as, via the update function, we are
            // constantly casting a ray to check if we are on the ground and if we are not we
            // set isGrounded to false. But the following line is doing no harm here.
            isGrounded = false;
            theAnimator.SetBool("jump", true);
        }
    }

    

}

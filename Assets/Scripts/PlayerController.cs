using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //prevent player from moving diagonally
            if (input.x != 0) input.y = 0;

            //begin movement
            if (input != Vector2.zero)
            {
                //set directions for animation
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        //interact input
        if (Input.GetKeyDown(KeyCode.J))
        {
            Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        //move player one space in a direction based on set moveSpeed
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        //check if the bottom half of the player will collide next move
        Vector3 checkPos = targetPos + new Vector3(0, -0.5f, 0);
        if (Physics2D.OverlapCircle(checkPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }

        return true;
    }

    void Interact()
    {
        //get position in front of player
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var InteractPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(InteractPos, 0.2f, interactableLayer);
        
        //do interaction stuff idk
        if (collider != null)
        {

        }
    }
}
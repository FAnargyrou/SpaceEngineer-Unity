using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float interactRadius = 5f;
    public LayerMask shipComponentsMasks;
    public Inventory inventory;
    public ProgressBar progressBar;

    Rigidbody2D rb;
    Animator animator;
    ShipComponent focus;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (!rb)
            Debug.LogWarning($"{name} does not have a RigidBody2D component");
        if (!animator)
            Debug.LogWarning($"{name} does not have an Animator component");
        progressBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }

        // If Player is interacting with a ShipComponent object
        if (focus)
        {
            BreakableComponent breakable = focus.GetComponent<BreakableComponent>();
            if (breakable)
            {
                float progress = breakable.GetProgress();
                progressBar.SetProgress(progress);
            }

            // Check if it's too far away and cancel current progress
            if (Vector2.Distance(focus.transform.position, transform.position) > 1f)
            {
                if (breakable)
                {
                    progressBar.SetProgress(0f);
                    progressBar.gameObject.SetActive(false);
                }
                focus.Cancel();
                focus = null;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement;
        movement.x = Input.GetAxis("Horizontal") * movementSpeed;
        movement.y = Input.GetAxis("Vertical") * movementSpeed;
        rb.velocity = new Vector2(movement.x, movement.y);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);

        if (movement.x != 0f)
        {
            int direction = movement.x < 0f ? 0 : 1;
            animator.SetInteger("Direction", direction);
        }
    }

    void Interact()
    {
        Collider2D hitComponent = Physics2D.OverlapCircle(transform.position, interactRadius, shipComponentsMasks);

        if (hitComponent == null) return;

        ShipComponent shipComponent = hitComponent.GetComponent<ShipComponent>();
        if (shipComponent == null)
        {
            Debug.LogWarning($"Player interacted with object {hitComponent.name} but it did not have a ShipComponent attached to it.");
            return;
        }

        shipComponent.Interact();
        BreakableComponent breakable = shipComponent.GetComponent<BreakableComponent>();
        if (breakable)
            progressBar.gameObject.SetActive(true);
        focus = shipComponent;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float interactRadius = 5f;
    public float o2TotalCapacity = 100f;
    public LayerMask shipComponentsMasks;
    public Inventory inventory;
    public ProgressBar progressBar;
    public ProgressBar o2Bar;

    Rigidbody2D rb;
    Animator animator;
    ShipComponent focus;
    bool gravity = true;
    bool o2Active = true;
    [HideInInspector] public float o2Current = 0f;
    int o2Multiplier = 0;

    bool playingWalkingSound;

    AudioManager audioManager;
    GameMode gameMode;

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
        o2Current = o2TotalCapacity;

        audioManager = FindObjectOfType<AudioManager>();
        if (!audioManager)
            Debug.LogWarning("Audio Manager not found!!!");
        gameMode = FindObjectOfType<GameMode>();
        if (!gameMode)
            Debug.LogWarning("Game Mode not found!!! Game will NOT function properly.");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            gameMode.PauseGame();
            rb.velocity = Vector2.zero;
            if (playingWalkingSound)
            {
                audioManager.Stop("PlayerSteps");
                playingWalkingSound = false;
            }
        }
        if (!PauseMenu.isGamePaused) UpdatePlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PauseMenu.isGamePaused) UpdateMovement();
    }

    void UpdatePlayer()
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

        if (!o2Active)
        {
            o2Current -= o2Multiplier * Time.deltaTime;
        }
        o2Bar.SetProgress(o2Current / o2TotalCapacity);
    }

    void UpdateMovement()
    {
        Vector2 movement;
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (gravity)
        {
            rb.velocity = new Vector2(movement.x * movementSpeed, movement.y * movementSpeed);
            if (rb.velocity.magnitude > 0f && audioManager && !playingWalkingSound)
            {
                audioManager.Play("PlayerSteps");
                playingWalkingSound = true;
            }
            else if (rb.velocity.magnitude <= 0f && audioManager && playingWalkingSound)
            {
                audioManager.Stop("PlayerSteps");
                playingWalkingSound = false;
            }

        }
        else if (!gravity)
        {
            if (playingWalkingSound)
            {
                audioManager.Stop("PlayerSteps");
                playingWalkingSound = false;
            }

            Vector2 currentVelocity = rb.velocity;
            currentVelocity.x += Time.deltaTime * movement.x;
            currentVelocity.y += Time.deltaTime * movement.y;
            rb.velocity = currentVelocity;

        }

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

    public void ToggleGravity(bool toggle)
    {
        gravity = toggle;
    }

    public void ToggleO2(bool toggle)
    {
        o2Active = toggle;
    }
    
    public void SetO2Multiplier(int multiplier)
    {
        o2Multiplier = multiplier;
    }

    public void RefillO2()
    {
        o2Current = o2TotalCapacity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}

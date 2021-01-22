using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float interactRadius = 5f;
    public LayerMask shipComponentsMasks;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement;
        movement.x = Input.GetAxis("Horizontal") * movementSpeed;
        movement.y = Input.GetAxis("Vertical") * movementSpeed;
        rb.velocity = new Vector2(movement.x, movement.y);
    }

    void Interact()
    {
        Collider2D hitComponent = Physics2D.OverlapCircle(transform.position, interactRadius);

        if (hitComponent == null) return;

        ShipComponent shipComponent = hitComponent.GetComponent<ShipComponent>();
        if (shipComponent == null)
        {
            Debug.LogWarning($"Player interacted with object {hitComponent.name} but it did not have a ShipComponent attached to it.");
            return;
        }

        shipComponent.Fix();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}

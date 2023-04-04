using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDetection : MonoBehaviour
{
    public Transform[] feet; // both feet of the player
    [SerializeField] private LayerMask LayerMask; // layer to detect raycast on
    [SerializeField] private float rayDistance; // length of ray
    private Transform GroundedObject; // object standing on
    private Vector3? groundedObjLastPosition; // last known position of the grounded obj
    public bool isGrounded; // is player grounded
    public Vector2 groundedObjDirection;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public bool GetIsGrounded()
    {
        return isGrounded;
    }
    private void Update()
    {
        foreach (var pos in feet) // loop through feet
        {
            CheckForGrounding(pos); // checks grounding on each foot
            if (isGrounded)
                break;
        }

            stickToMovingObjects(); // makes the player move when on top of moving objects.  
    }

    void stickToMovingObjects()
    {
        if (GroundedObject != null) // if player is standing on a platform
        {
            if (groundedObjLastPosition.HasValue && groundedObjLastPosition.Value != GroundedObject.position) // if player has moved
            {
                Vector3 delta = GroundedObject.position - groundedObjLastPosition.Value; // get delta value between current position and last known position
                transform.position += delta; // add it to player position
            }
            groundedObjLastPosition = GroundedObject.position;
        }
        else
        {
            groundedObjLastPosition = null;
        }
    }
    void CheckForGrounding(Transform foot)
    {
        var hit = Physics2D.Raycast(foot.position, foot.forward, rayDistance, LayerMask);
        if (hit.collider != null) // if raycast cant detect floor
        {
            groundedObjDirection = foot.forward;
            if (GroundedObject != hit.collider.transform)
            {
                if (anim.GetBool("grounded") == false)
                {
                    anim.SetBool("grounded", true); // triggers jump animation
                }
                GroundedObject = hit.collider.transform; // set object player is standing on
                isGrounded = true;
                groundedObjLastPosition = GroundedObject.position;
            }

        }
        else
        {
            GroundedObject = null;
            isGrounded = false;
        }
    }
}

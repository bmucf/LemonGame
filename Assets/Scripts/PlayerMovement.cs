using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;
    Vector3 m_Movement;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>(); // Attaches reference to animator component of this game object. This allows us to control animations.
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Assigns a value equal to horizontal input. Ranges from -1 for full left to 1 for full right.
        float vertical = Input.GetAxis("Vertical"); // Assigns a value equal to vertical input. Ranges from -1 for full down to 1 for full up.

        m_Movement.Set(horizontal, 0f, vertical); // Sets direction of vector.
        m_Movement.Normalize(); // Sets magnitude of vector to 1, including diagonal movement which would naturally have a higher magnitude than axial movement.

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f); // Checks if there is non-zero horizontal input.
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f); // Checks if there is non-zero vertical input.
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking); // When isWalking is true, the IsWalking parameter on the animator of this game object becomes true.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity; // Creates variable storing no rotation.
    bool speeding = false;
    public bool isSpeeding()
    {
        return speeding;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>(); // Attaches reference to animator component of this game object. This allows control of animations.
        m_Rigidbody = GetComponent<Rigidbody>(); // Attaches reference to rigidbody component of this game object. This allows control of rigidbody of this game object, which would otherwise be overridden by animations.
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Assigns a value equal to horizontal input. Ranges from -1 for full left to 1 for full right.
        float vertical = Input.GetAxis("Vertical"); // Assigns a value equal to vertical input. Ranges from -1 for full down to 1 for full up.

        m_Movement.Set(horizontal, 0f, vertical); // Sets direction of movement vector.
        m_Movement.Normalize(); // Sets magnitude of movement vector to 1, including diagonal movement which would otherwise have a higher magnitude than axial movement.

        if (speeding)
        {
            // double the speed if you're running
            m_Movement *= 4;
        }

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f); // Checks if there is non-zero horizontal input.
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f); // Checks if there is non-zero vertical input.
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking); // When isWalking is true, the IsWalking parameter on the animator of this game object becomes true.

        if (isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        // Takes current rotation of this gameobject and sets new vector to the direction of m_Movement, then rotates this game object to meet that vector at a rate of radians equal to turnSpeed per frame.
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed , 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    private void LateUpdate()
    {
        SpeedUpdate();
    }
    
    // Update speed based on user input
    void SpeedUpdate()
    {
        // switch speed mode when user presses G
        if (Input.GetKeyDown(KeyCode.G))
        {
            speeding = !speeding; // Toggle
        }
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}

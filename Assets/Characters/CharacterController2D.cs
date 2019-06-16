using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

    public enum State
        {
            Moving,
            Pushing,
            WaitingForInput
        }

    private const float MovementBlockSize = 0.32f;
    private const float MovingSpeedPPS = 1.0f;
    private const float TimeToMoveOneBlock = MovementBlockSize / MovingSpeedPPS;

    public State m_State;

    //Character starts of facing downward
    public Vector2 m_Facing = Vector2.down;

    private Animator m_Animator;

    public float m_MoveTimer;
    public Vector2 m_MovingFrom;
    public Vector2 m_MovingTo;
    private Vector2 m_SpawnPoint;

    private SpriteRenderer m_Renderer;

    private void Awake()
    {
        m_Animator = gameObject.GetComponentInChildren<Animator>();
        m_Animator.SetFloat("Dir_x", m_Facing.x);
        m_Animator.SetFloat("Dir_y", m_Facing.y);

        m_Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        m_Animator.SetFloat("Dir_x", m_Facing.x);
        m_Animator.SetFloat("Dir_y", m_Facing.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_State = State.WaitingForInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_State == State.Moving)
        {
            MoveUpdate();
        }

        if (m_State == State.WaitingForInput)
        {
            InputUpdate();
        }
    }

    private void MoveUpdate()
    {
        // Move towards our target position
        m_MoveTimer += Time.deltaTime;

        var ratio = m_MoveTimer / TimeToMoveOneBlock;

        var pos = Vector2.Lerp(m_MovingFrom, m_MovingTo, ratio);
        gameObject.transform.position = pos;

        if (ratio >= 1.0f)
        {
            m_State = State.WaitingForInput;
            m_MoveTimer = Mathf.Repeat(m_MoveTimer, TimeToMoveOneBlock);
        }
    }

    private void InputUpdate()
    {
        Vector2 dv = Vector2.zero;

        if((!GameObject.Find("Cutscene Trigger") || (GameObject.Find("Cutscene Trigger").GetComponent<cutscene>().cutsceneCompleted || !GameObject.Find("Cutscene Trigger").GetComponent<cutscene>().cutsceneInitiated))) {
            dv.x = Input.GetAxisRaw("Horizontal");
            dv.y = Input.GetAxisRaw("Vertical");
        }

        // Favor horizontal movement over vertical
        if (dv.x != 0)
        {
            m_Facing.x = dv.x;
            m_Facing.y = 0;
        }
        else if (dv.y != 0)
        {
            m_Facing.x = 0;
            m_Facing.y = dv.y;
        }

        m_Facing.Normalize();
        m_MovingFrom = gameObject.transform.position;

        if (dv.SqrMagnitude() > 0 && !Input.GetKey(KeyCode.LeftControl))
        {
            // We are attempting to move so we want to animate
            m_Animator.SetBool("Moving", true);

            // We may not be allowed to move, however, if that would cause a collision with the default colliders
            var pos = gameObject.transform.position;
            var hit = Physics2D.Raycast(pos, m_Facing, MovementBlockSize, 1 << LayerMask.NameToLayer("Default"));

            if (hit)
            {
                m_State = State.WaitingForInput;
                m_MoveTimer = 0.0f;
            }
            else
            {
                m_State = State.Moving;
                m_MovingTo = m_MovingFrom + m_Facing * MovementBlockSize;
            }
        }
        else
        {
            // No input means we aren't even trying to move
            m_MoveTimer = 0.0f;
            m_Animator.SetBool("Moving", false);
        }
    }

    private int RoundToGrid(float value)
    {
        if (value < 0)
        {
            return (int)(((value - MovementBlockSize * 0.5f) / MovementBlockSize)) * (int)MovementBlockSize;
        }

        return (int)(((value + MovementBlockSize * 0.5f) / MovementBlockSize)) * (int)MovementBlockSize;
    }

}

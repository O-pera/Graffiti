using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_Board : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 10f;
    public float speeds = 0;
    public float jumpHeight = 3f;
    public float jumpHeights = 0f;
    public float dash = 2f;
    public float rotSpeed = 6f;
    public float animMoveWeightSpeed;
    public float dash_jump = 5;

    private Vector3 dir = Vector3.zero;

    private bool ground = false;
    private float animationMoveWeight;
    public LayerMask layer;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animationMoveWeight = 0f;
        speeds = speed;
        jumpHeights=jumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();

        CheckGround();

        if (Input.GetKeyDown(KeyCode.W) && ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeights;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("TRUE");
            jumpHeights = jumpHeight * dash_jump;
        }
        else
            jumpHeights = jumpHeight;

        if (Input.GetKey(KeyCode.LeftShift))
            speeds = speed * dash;
        else
            speeds = speed;
        //AnimationUpdate();
    }

    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            /*if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x)
                || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0, 1, 0);
            }*/
            //transform.forward = Vector3.Lerp(transform.forward, dir.normalized, rotSpeed * Time.fixedDeltaTime);
        }

        rigidbody.MovePosition(this.gameObject.transform.position + dir.normalized * speeds * Time.fixedDeltaTime);
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.6f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }

   /* void AnimationUpdate()
    {
        bool isSprint = Input.GetKey(KeyCode.LeftShift);
        bool isMove = dir.x != 0 || dir.z != 0;

        if (isMove)
        {
            if (isSprint)
            {
                animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight > 1f)
                    animationMoveWeight = 1f;
            }
            else
            {
                if (animationMoveWeight > 0.5f)
                {
                    animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
                    if (animationMoveWeight < 0.5f)
                        animationMoveWeight = 0.5f;
                }
                else if (animationMoveWeight < 0.5f)
                {
                    animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
                    if (animationMoveWeight > 0.5f)
                        animationMoveWeight = 0.5f;
                }
            }
        }
        else
        {
            animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
            if (animationMoveWeight < 0f)
                animationMoveWeight = 0f;
        }

        animator.SetFloat("moveWeight_Side", animationMoveWeight);
    }*/
}
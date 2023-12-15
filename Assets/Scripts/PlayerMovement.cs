using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

	private Vector2 _moveInput;


    private const float MAXSPEED = 4;
    const float runAccelAmount = 0.1f;
    const float runDeccelAmount = 0.1f;

    public Rigidbody2D RB;    
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        RB.velocity = new Vector2(_moveInput.x,_moveInput.y);
    }

    private void run(float lerpAmount)
    {
		float targetSpeed = _moveInput.x * MAXSPEED;
        targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;

    }
}

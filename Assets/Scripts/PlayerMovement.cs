using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

	private Vector2 _moveInput;


    private const float MAXSPEED = 25;
    const float runAccelAmount = 2f;
    const float runDeccelAmount = 10f;
    private const bool doConserveMomentum = true;

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
        //RB.velocity = new Vector2(_moveInput.x,_moveInput.y);
        run(1);
    }

    private void run(float lerpAmount)
    {
        var targetSpeedX = _moveInput.x * MAXSPEED;
        var targetSpeedY = _moveInput.y * MAXSPEED;

        targetSpeedX = Mathf.Lerp(RB.velocity.x, targetSpeedX, lerpAmount);
        targetSpeedY = Mathf.Lerp(RB.velocity.y, targetSpeedY, lerpAmount);
        
        var accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? runAccelAmount : runDeccelAmount;
        var accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? runAccelAmount : runDeccelAmount;

        if(doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeedX) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeedX) && Mathf.Abs(targetSpeedX) > 0.01f)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRateX = 0; 
        }
        
        if(doConserveMomentum && Mathf.Abs(RB.velocity.y) > Mathf.Abs(targetSpeedY) && Mathf.Sign(RB.velocity.y) == Mathf.Sign(targetSpeedY) && Mathf.Abs(targetSpeedY) > 0.01f)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRateY = 0; 
        }
        var speedDifX = targetSpeedX - RB.velocity.x;
        var speedDifY = targetSpeedY - RB.velocity.y;
        
        var movementX = speedDifX * accelRateX;
        var movementY = speedDifY * accelRateY;

        RB.AddForce(new Vector2(movementX,movementY), ForceMode2D.Force);

    }
}

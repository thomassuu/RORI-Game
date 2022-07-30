using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private float speed = Stats.Hero.SPEED;
    Vector2 inputMovement;
    Vector2 cursorPos;

    enum BulletTypes {Normal, Special, Type3, Type4};
    public Transform firingPoint;
    public GameObject[] bulletPrefabs = new GameObject[Enum.GetNames(typeof(BulletTypes)).Length];
    private float bulletForce = Stats.Hero.BULLET_SPEED;
    private float fireDelay = Stats.Hero.FIRE_DELAY;
    public TrailRenderer gunTrailFx;


    // Update is called once per frame
    void Update()
    {
        this.UpdateInput();
        this.UpdateDelays();
    }

    void FixedUpdate() 
    {
       this.ApplyMovement();
    }

    void UpdateDelays()
    {
        this.fireDelay -= Time.deltaTime;
    }

    void UpdateInput() 
    {
        this.CheckKeyboardInputMovement();
        this.UpdateCursorPosition();
        this.CheckMouseInput();
    }

    void CheckKeyboardInputMovement() 
    {
        this.inputMovement.x = Input.GetAxisRaw("Horizontal"); // 1: right, -1: left, 0: no input
        this.inputMovement.y = Input.GetAxisRaw("Vertical"); // 1: up, -1: down, 0: no input
    }

    void UpdateCursorPosition()
    {
        this.cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void CheckMouseInput() 
    {
        if (Input.GetButton("Fire1")) 
        {
            if (this.fireDelay <= 0) 
            {
                this.Shoot(BulletTypes.Normal);
                this.fireDelay = Stats.Hero.FIRE_DELAY;
            }
        }
    }

    void Shoot(BulletTypes type)
    {   
        GameObject bullet = Instantiate(bulletPrefabs[(int) type], firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firingPoint.right * this.bulletForce, ForceMode2D.Impulse);
        this.gunTrailFx.emitting = true;
        CancelInvoke();
        Invoke("DeactivateTrail", 1.0f);
    }

    void DeactivateTrail() 
    {
        this.gunTrailFx.emitting = false;
    }

    void ApplyMovement() 
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.MovePosition(rb.position + this.inputMovement * this.speed * Time.fixedDeltaTime);
        Vector2 lookingDirection = cursorPos - rb.position;
        rb.rotation = this.vector2DToDeg(lookingDirection);
    }
    float vector2DToDeg(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90f;
    }

    
}

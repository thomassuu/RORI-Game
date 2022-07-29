using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum BulletTypes {Normal, Special, Type3, Type4};
    public float SPEED = 50f;
    Vector2 inputMovement;
    Vector2 cursorPos;

    public Transform firingPoint;
    public GameObject[] bulletPrefabs = new GameObject[4];
    public float bulletForce = 20f;




    // Update is called once per frame
    void Update()
    {
        this.UpdateInput();
    }

    void FixedUpdate() 
    {
       this.ApplyMovement();
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
        if (Input.GetButtonDown("Fire1")) this.Shoot(BulletTypes.Normal);
    }

    void Shoot(BulletTypes type)
    {   
        GameObject bullet = Instantiate(bulletPrefabs[(int) type], firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firingPoint.right * this.bulletForce, ForceMode2D.Impulse);
    }

    void ApplyMovement() 
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.MovePosition(rb.position + this.inputMovement * this.SPEED * Time.fixedDeltaTime);
        Vector2 lookingDirection = cursorPos - rb.position;
        rb.rotation = this.vector2DToDeg(lookingDirection);
    }
    float vector2DToDeg(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90f;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int id;
    private Vector3 startPosition;
    private int direction = 0;
    private float moveSpeedMutiplier = 1f;
    [SerializeField] float aiDeadzone = 1f;
    [SerializeField] float moveSpeed;
    private void Start(){
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPaddlePosition;
    }
    private void ResetPaddlePosition(){
        transform.position = startPosition;
    }
    private void Update(){
        if(id == 2 && GameManager.instance.IsPlay2Ai()){
            MoveAi();
        }
        else{
            float movement = ProcessInput();
            Move(movement);
        }
    }
    private void MoveAi(){
        Vector2 ballPos = GameManager.instance.ball.transform.position;
        if(Mathf.Abs(ballPos.y - transform.position.y) > aiDeadzone){
            direction = ballPos.y > transform.position.y ? 1 : -1; 
        }
        if(Random.value < 0.01f){
            moveSpeedMutiplier = Random.Range(0.5f, 1.5f);
        }
        Move(direction);
    }
    private float ProcessInput(){
        float movement = 0;
        switch(id){
            case 1:
              movement = Input.GetAxis("MovePlayer1");
              break;
            case 2:
              movement = Input.GetAxis("MovePlayer2");
                break;
        }
        return movement;
    }
    private void Move(float value){
        Vector2 velo = rb2d.velocity;
        velo.y = value * moveSpeed * moveSpeedMutiplier;
        rb2d.velocity = velo;
    }

}

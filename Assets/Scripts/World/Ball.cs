using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public BallAudio ballAudio;
    public ParticleSystem particleSystem;
    public SpriteRenderer spriteRenderer;
    //Xem lại particleSystem đi
    
    //public GameManager gameManager;
    [SerializeField] float maxInitialAngle = 0.67f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float starX = 0;
    [SerializeField] float maxStartY = 4;
    [SerializeField] float speedMutipler = 1.1f;
    private void Start(){
        //InitialPush();
        GameManager.instance.onReset += ResetBall;
        //Đăng ký hàm ResetBall vào sự kiện onReset
        GameManager.instance.gameUI.onStartGame += ResetBall;
    }
    private void ResetBall(){
        ResetBallPosition();
        InitialPush();
    }
    private void InitialPush(){

        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        // Nếu 1 giá trị bất kì nhỏ hơn 0.5f thì Vector2.left ko thì Vector2.right
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;

        EmitParticle(32);
    }
    private void ResetBallPosition(){
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(starX, posY);
        transform.position = position;
    }
    private void OnTriggerEnter2D(Collider2D other){
        ScoreZone scoreZone = other.GetComponent<ScoreZone>();
        if(scoreZone != null){
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
            //
            GameManager.instance.screenShake.StartShake(0.33f, 0.1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){

        Paddle paddle = collision.collider.GetComponent<Paddle>();
        if(paddle != null){
            ballAudio.PlayPaddleSound();
            rb2d.velocity *= speedMutipler;
            EmitParticle(16);
            GameManager.instance.screenShake.StartShake(0.1f, 0.05f);
            //GameManager.instance.screenShake.StartShake(Mathf.Sqrt(rb2d.velocity.magnitude) * 0.02f, 0.075f);
        }

        Wall wall = collision.collider.GetComponent<Wall>();
        if(wall != null){
            ballAudio.PlayWallSound();
            EmitParticle(8);
            GameManager.instance.screenShake.StartShake(0.033f, 0.033f);
        }
        AdjustSpriteRotation();
    }
    private void EmitParticle(int amount){
        particleSystem.Emit(amount);
        // Phát ra số lượng hạt đúng bằng giá trị amount
    }
    private void AdjustAngle(Paddle paddle, Collision2D collision){
        
    }
    private void AdjustSpriteRotation(){
        spriteRenderer.flipY = rb2d.velocity.x < 0f;
    }
    //Khi đối tượng di chuyển sang trái (vận tốc âm), 
    //sprite sẽ được lật ngược theo trục Y. Nếu đối tượng di chuyển sang phải (vận tốc dương), sprite sẽ không bị lật.
}

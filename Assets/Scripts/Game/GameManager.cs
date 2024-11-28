using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] int scorePlayer1 = 0, scorePlayer2 = 0;
    [SerializeField] int maxScore = 4;
    public static GameManager instance;
    // biến static tạo ra GameManager để dùng chung cho nhiều lớp
    public ScoreText scoreTextLeft, scoreTextRight;
    public System.Action onReset;
    public GameUI gameUI;
    public GameAudio gameAudio;
    public Shake screenShake;
    public PlayMode playMode;
    public Ball ball;
    public enum PlayMode{
        PlayerVsPlayer,
        PlayerVsAi
    }
    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
        }//Nếu đã có một phiên bản khác của đối tượng này tồn tại (nghĩa là instance không phải là null), thì đối tượng hiện tại (đối tượng đang gọi Awake()) sẽ bị phá hủy bằng cách sử dụng Destroy(gameObject);. 
         //gameObject ở đây đại diện cho toàn bộ đối tượng mà script này được gắn vào.
        else{
            instance = this;
            //instance = this; thiết lập biến instance thành đối tượng hiện tại
            //Đảm bảo rằng đối tượng này sẽ là phiên bản duy nhất trong trò chơi.
            gameUI.onStartGame += OnStartGame;
        }
    }
    // Awake() đảm bảo chỉ có 1 đối tượng duy nhất như thế xuất hiện trong game
    private void OnDestroy(){
        gameUI.onStartGame -= OnStartGame;
    }
    public void OnScoreZoneReached(int id){
        //onReset?.Invoke();
        //Khi mà quả bóng bay vào khung thành thì sẽ kích hoạt các sự kiện trong onReset
        if(id == 1){
            scorePlayer1++;
        }
        if(id == 2){
            scorePlayer2++;
        }
        gameUI.UpdateScore(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);
        CheckWin();
        //CheckWin nó phải là 1 hàm gọi OnGameEnds thôi chứ ko đc phép gọi các sự kiện của onReset để game ko thể tiếp tục sau khi có 1 người thắng

    }
    private void CheckWin(){
        int winnerId = scorePlayer1 == maxScore ? 1 : scorePlayer2 == maxScore ? 2 : 0;
        if(winnerId != 0){
            gameUI.OnGameEnds(winnerId);
            gameAudio.PlayWinSound();
        }
        else{
            gameAudio.PlayScoreSound();
            onReset?.Invoke();
        }
    }
    private void OnStartGame(){
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.UpdateScore(scorePlayer1, scorePlayer2);
    }
    public void SwitchPlayMode(){
        switch(playMode){
            case PlayMode.PlayerVsPlayer:
              playMode = PlayMode.PlayerVsAi;
              break;
            case PlayMode.PlayerVsAi:
              playMode = PlayMode.PlayerVsPlayer;
              break;
        }
    }
    public bool IsPlay2Ai(){
        return playMode == PlayMode.PlayerVsAi; 
    }
}

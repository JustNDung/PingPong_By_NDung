using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreTextPlayer1, scoreTextPlayer2;
    public GameObject menuObject; 
    public GameObject quitButton;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI volumeValuetext;
    public TextMeshProUGUI playModeButtonText;
    public System.Action onStartGame;
    private void Start(){
        AdjustPlayModeButtonText();
        CheckDisableQuitButton();
    }
    public void UpdateScore(int scorePlayer1, int scorePlayer2){
        scoreTextPlayer1.GetScore(scorePlayer1);
        scoreTextPlayer2.GetScore(scorePlayer2);
    }
    public void HighlightScore(int id){
        if(id == 1){
            scoreTextPlayer1.Highlight();
        }
        else{
            scoreTextPlayer2.Highlight();
        }
    }
    public void OnStartButtonClicked(){
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }
    public void OnGameEnds(int winnerId){
        menuObject.SetActive(true);
        winText.text = $"Player {winnerId} wins";
        //winText.text = "Player" + winnerId + "wins";
    }
    public void OnVolumeChanged(float value){
        AudioListener.volume = value;
        volumeValuetext.text = $"{Mathf.RoundToInt(value * 100)}%";
    }
    // Hàm kiểm soát âm thanh
    public void OnSwitchPlayModeButton(){
        GameManager.instance.SwitchPlayMode();
        AdjustPlayModeButtonText();
    }
    private void AdjustPlayModeButtonText(){
        string s = string.Empty;
        switch(GameManager.instance.playMode){
            case GameManager.PlayMode.PlayerVsPlayer:
              s = "2 Players";
              break;
            case GameManager.PlayMode.PlayerVsAi:
              s = "Player vs AI";
              break;
        }
        playModeButtonText.text = s;
    }
    public void OnQuitButtonClicked(){
        Application.Quit();
        // Quit khỏi game;s
    }
    private void CheckDisableQuitButton(){
        #if UNITY_WEBGL
        quitButton.SetActive(false);
        #endif
    }
}

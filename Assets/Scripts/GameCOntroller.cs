using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int scorePlayer1, scorePlayer2;

    [SerializeField] GameObject fightGO;
    [SerializeField] GameObject lastMessageCanvasa;
    [SerializeField] TMP_Text winner, player1Score, player2Score;
    

    public bool gameStarted = false;

    [SerializeField] PlayerController player1, player2;

    [SerializeField] TMP_Text timerTxt;
    float timer;

    void restart(){
        SceneManager.LoadScene("Main");
    }

    void start(){
    }

    public void play(){
        if(player1.ready && player2.ready){
            // reset score and laaunch timer
            player1.score = 0;
            player2.score = 0;
            
            timer = 0;
            timerTxt.text = "0";

            fightGO.SetActive(true);
            gameStarted = true;
        }
    }
    
    /// for animation
    public void playBis(){
        gameStarted = true;
    }

    public bool finished;

    void Update(){
        if(gameStarted && !finished){
            timer += Time.deltaTime;
            timerTxt.text = Math.Round(timer).ToString();

            if(timer > 10){
                // end game
                finished = true;

                if(player1.score > player2.score){
                    player1.score += 1000;
                    player2.score+= 200;

                    winner.text = "Le joueur Rouge est vainqueur !";
                }
                else{
                    player2.score += 1000;
                    player1.score+= 200;

                    winner.text = "Le joueur Bleu est vainqueur !";
                }

                player1Score.text = "Joueur rouge : " + player1.score + "pt (" + player1.score/1000 + ")";
                player2Score.text = "Joueur bleu : " + player2.score + "pt (" + player2.score/1000 + ")";

                lastMessageCanvasa.SetActive(true);

                //display last message

            }
        }
    }
}

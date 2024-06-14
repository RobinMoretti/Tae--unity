using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyButtons;

public class GameController : MonoBehaviour
{
    int scorePlayer1, scorePlayer2;

    [SerializeField] GameObject fightGO;
    [SerializeField] GameObject lastMessageCanvasa;
    [SerializeField] GameObject fightGO2;
    [SerializeField] GameObject lastMessageCanvasa2;
    [SerializeField] GameObject TargetGruop;

    [SerializeField] TMP_Text winner, player1Score, player2Score;
    [SerializeField] TMP_Text winner2, player1Score2, player2Score2;


    public bool gameStarted = false;

    [SerializeField] PlayerController player1, player2;

    [SerializeField] TMP_Text timerTxt;

    float timer;

    [SerializeField] Transform player1InitialPos, player2InitialPos, targetGroupInitialPos;


    [Button]
    public void restart()
    {
        player1.ready = false;
        player2.ready = false;
        player1.score = 0;
        player2.score = 0;
        player1.gameObject.transform.position = player1InitialPos.position;
        player2.gameObject.transform.position = player2InitialPos.position;
        TargetGruop.transform.position = targetGroupInitialPos.position;

        player1.usedCards.Clear();
        player2.usedCards.Clear();

        timer = 120;
        timerTxt.text = "120";
        gameStarted = false;
        finished = false;

        lastMessageCanvasa.SetActive(false);
        lastMessageCanvasa2.SetActive(false);
        fightGO.SetActive(false);
        fightGO2.SetActive(false);
    }


    void Start()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

    public void play(){
        if(player1.ready && player2.ready){
            // reset score and laaunch timer
            player1.resetScore();
            player2.resetScore();
            
            timer = 0;
            timerTxt.text = (120 - Math.Round(timer)).ToString();

            fightGO.SetActive(true);
            fightGO2.SetActive(true);
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
            timerTxt.text = (120 - Math.Round(timer)).ToString();

            if(timer >= 120){
                // end game
                finished = true;

                if(player1.score > player2.score){
                    player1.score += 1000;
                    player2.score+= 200;

                    winner.text = "Le joueur Rouge est vainqueur !";
                    winner2.text = "Le joueur Rouge est vainqueur !";
                }
                else{
                    player2.score += 1000;
                    player1.score+= 200;

                    winner.text = "Le joueur Bleu est vainqueur !";
                    winner2.text = "Le joueur Bleu est vainqueur !";
                }

                player1Score.text = "Joueur rouge : " + player1.score + "pt (" + player1.score / 100 + ")";
                player2Score.text = "Joueur bleu : " + player2.score + "pt (" + player2.score / 100 + ")";
                player1Score2.text = "Joueur rouge : " + player1.score + "pt (" + player1.score / 100 + ")";
                player2Score2.text = "Joueur bleu : " + player2.score + "pt (" + player2.score / 100 + ")";

                lastMessageCanvasa.SetActive(true);
                lastMessageCanvasa2.SetActive(true);

                //display last message

            }
        }
    }
}

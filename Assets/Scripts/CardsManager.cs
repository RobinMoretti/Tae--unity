using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    List<string> jumpLeftCards;
    List<string> jumpRightCards;
    List<string> jumpFrontCards;
    List<string> jumpBackCards;

    List<string> salutationCards;
    List<string> frontKickCards;
    List<string> sideKickCards;
    List<string> Combo1Cards;
    List<string> RunAndKickCards;
    List<string> BackKickCards;
    
        
    // Start is called before the first frame update
    void Start()
    {
        jumpLeftCards = new List<string>();
        jumpLeftCards.Add("0x40x3F0x930xEB0x780x00x0");
        jumpLeftCards.Add("0x40xA00x930xEB0x780x00x0"); 
        jumpLeftCards.Add("0x40xC20x920xEB0x780x00x0");
        jumpLeftCards.Add("0x40x630xAF0xEB0x780x00x0");
        jumpLeftCards.Add("0x40x620xAF0xEB0x780x00x0");
        jumpLeftCards.Add("0x40xBB0x930xEB0x780x00x0");

        jumpRightCards = new List<string>();
        jumpRightCards.Add("0x40xBF0x930xEB0x780x00x0");
        jumpRightCards.Add("0x40xC10x930xEB0x780x00x0");
        jumpRightCards.Add("0x40xC00x930xEB0x780x00x0");
        jumpRightCards.Add("0x40x360x930xEB0x780x00x0");
        jumpRightCards.Add("0x40x350x930xEB0x780x00x0");
        jumpRightCards.Add("0x40x370x930xEB0x780x00x0");
        jumpRightCards.Add("0x40x610xAF0xEB0x780x00x0");
        jumpRightCards.Add("0x40xF40xAF0xEB0x780x00x0");
        jumpRightCards.Add("0x40x600xAF0xEB0x780x00x0");

        jumpFrontCards = new List<string>();
        jumpFrontCards.Add("0x40xF50xAF0xEB0x780x00x0");
        jumpFrontCards.Add("0x40xBC0x920xEB0x780x00x0");
        jumpFrontCards.Add("0x40xBD0x930xEB0x780x00x0");
        jumpFrontCards.Add("0x40x280x930xEB0x780x00x0");
        jumpFrontCards.Add("0x40x3D0x930xEB0x780x00x0");
        jumpFrontCards.Add("0x40x290x930xEB0x780x00x0");
        jumpFrontCards.Add("0x40x3B0x930xEB0x780x00x0");
        jumpFrontCards.Add("0x40x3C0x930xEB0x780x00x0");
        jumpFrontCards.Add("0x40x380x930xEB0x780x00x0");

        jumpBackCards = new List<string>();
        jumpBackCards.Add("0x40x340x920xEB0x780x00x0");
        jumpBackCards.Add("0x40xC70x920xEB0x780x00x0");
        jumpBackCards.Add("0x40xC60x920xEB0x780x00x0");
        jumpBackCards.Add("0x40xC50x920xEB0x780x00x0");
        jumpBackCards.Add("0x40xC40x920xEB0x780x00x0");
        jumpBackCards.Add("0x40xC30x920xEB0x780x00x0");
        jumpBackCards.Add("0x40xC00x920xEB0x780x00x0");
        jumpBackCards.Add("0x40x3E0x930xEB0x780x00x0");
        jumpBackCards.Add("0x40xC10x920xEB0x780x00x0");

        salutationCards = new List<string>();
        salutationCards.Add("0x40xB90x930xEB0x780x00x0");

        frontKickCards = new List<string>();
        frontKickCards.Add("0x40xB70x930xEB0x780x00x0");
        frontKickCards.Add("0x40xB60x930xEB0x780x00x0");
        frontKickCards.Add("0x40xBE0x930xEB0x780x00x0");
        frontKickCards.Add("0x40xF30xAF0xEB0x780x00x0");
        frontKickCards.Add("0x40xBC0x930xEB0x780x00x0");
        frontKickCards.Add("0x40xB80x930xEB0x780x00x0");
        frontKickCards.Add("0x40x320x930xEB0x780x00x0"); 

        sideKickCards = new List<string>(); 
        sideKickCards.Add("0x40xB60x920xEB0x780x00x0");
        sideKickCards.Add("0x40xB70x920xEB0x780x00x0");
        sideKickCards.Add("0x40x3A0x930xEB0x780x00x0");
        sideKickCards.Add("0x40xBF0x920xEB0x780x00x0");
        sideKickCards.Add("0x40x330x930xEB0x780x00x0");
        sideKickCards.Add("0x40x430x930xEB0x780x00x0");

        Combo1Cards = new List<string>();
        Combo1Cards.Add("0x40xBA0x930xEB0x780x00x0");
        Combo1Cards.Add("0x40x640xAF0xEB0x780x00x0");

        RunAndKickCards = new List<string>();
        RunAndKickCards.Add("0x40x390x930xEB0x780x00x0");
        RunAndKickCards.Add("0x40x340x930xEB0x780x00x0");

        BackKickCards = new List<string>();
        BackKickCards.Add("0x40xBA0x920xEB0x780x00x0");
        BackKickCards.Add("0x40xB80x920xEB0x780x00x0");
        BackKickCards.Add("0x40x420x930xEB0x780x00x0");
        BackKickCards.Add("0x40x410x930xEB0x780x00x0");
        BackKickCards.Add("0x40xB90x920xEB0x780x00x0");
        BackKickCards.Add("0x40xBB0x920xEB0x780x00x0");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getCardAction(string cardId)
    {
        if (jumpLeftCards.Contains(cardId))
        {
            return "JumpLeft";
        }
        if (jumpRightCards.Contains(cardId))
        {
            return "JumpRight";
        }
        if (jumpFrontCards.Contains(cardId))
        {
            return "JumpFront";
        }
        if (jumpBackCards.Contains(cardId))
        {
            return "JumpBack";
        }
        if (salutationCards.Contains(cardId))
        {
            return "Salu";
        }
        if (frontKickCards.Contains(cardId))
        {
            return "FrontKick";
        }
        if (sideKickCards.Contains(cardId))
        {
            return "SideKick";
        }
        if (Combo1Cards.Contains(cardId))
        {
            return "Combo1";
        }
        if (RunAndKickCards.Contains(cardId))
        {
            return "RunAndKick";
        }
        if (BackKickCards.Contains(cardId))
        {
            return "BackKick";
        }

        return "false";
    }
}

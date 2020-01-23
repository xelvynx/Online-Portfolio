using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class CardTemplate : MonoBehaviour {
    public Card card;
    CardManager cm;

    public void Start()
    {
        cm = GameObject.Find("GameManager").GetComponent<CardManager>();
    }
    public void Update()
    {
        LoadCard(card);
    }
    public void LoadCard(Card c)
    {
        if (c == null)
            return;
        card = c;
        CardProperties cp = c.cardProperties;
        gameObject.name = cp.title;
        gameObject.GetComponent<Image>().sprite = cp.cardPic;
        if (c.cardType == null)
        { gameObject.GetComponent<Image>().sprite = null; }
    }
    public void LoadForgeCard(int i)
    {
        if (i == 1) {
            cm.forge1Display.GetComponent<CardTemplate>().LoadCard(card);
        }
    }
}

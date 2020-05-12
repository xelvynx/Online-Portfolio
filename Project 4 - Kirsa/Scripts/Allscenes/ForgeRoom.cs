﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeRoom : MonoBehaviour
{

    public GameObject forgePanel;
    bool activePanel = false;
    public CardManager cm;
    public GameObject cardButton;
    public Transform cardButtonParent;
    public GameObject recipeButton;
    public Transform recipeButtonParent;
    public List<Card> forgeDeck = new List<Card>();

    public Card forge1;
    public Card forge2;
    public Text tokenText;
    int token;

    public Recipe recipe;
    // Use this for initialization
    void Start()
    {
        cm = GameObject.Find("GameManager").GetComponent<CardManager>();
        //forgePanel = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetForgePanel(activePanel);
            //cm.Forging();
            //Time.timeScale = 1;
            //for (int i = 0; i < forgeDeck.Count; i++)
            //{
            //    cm.playerDeck.Add(forgeDeck[i]);

            //}
            //foreach (Transform child in recipeButtonParent)
            //{
            //    GameObject.Destroy(child.gameObject);
            //}
            //foreach (Transform child in cardButtonParent)
            //{
            //    GameObject.Destroy(child.gameObject);
            //}
            //forgeDeck.Clear();
        }
    }

    void SetForgePanel(bool b)
    {
        activePanel = !b;
        forgePanel.SetActive(activePanel);
        //cm.Forging();
        Time.timeScale = 0;
        CreateForgeDeck();
        RecipeButtonLoad();
        if (activePanel == false)
        {
            Time.timeScale = 1;
            for (int i = 0; i < forgeDeck.Count; i++)
            {
                cm.playerDeck.Add(forgeDeck[i]);

            }
            foreach (Transform child in recipeButtonParent)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (Transform child in cardButtonParent)
            {
                GameObject.Destroy(child.gameObject);
            }
            forgeDeck.Clear();
            cm.SetForgeDisplay();
            tokenText.text = "";
            forge1 = null;
            forge2 = null;
        }

    }
    public void RecipeButtonLoad()
    {
        for (int i = 0; i < cm.recipeList.Count; i++)
        {
            GameObject o = Instantiate(recipeButton, recipeButton.transform.parent);
            o.transform.SetParent(recipeButtonParent, false);
            o.GetComponent<RecipeButton>().SetRecipe(cm.recipeList[i]);
        }
    }
    public void CreateForgeDeck()
    {

        for (int i = 0; i < cm.playerDeck.Count; i++)
        {
            if (cm.playerDeck[i] != null)
                forgeDeck.Add(cm.playerDeck[i]);
        }
        //if (cm.pHand.Count > 0)
        //{
        //    for (int i = 0; i < cm.pHand.Count; i++)
        //    {
        //        Card c = cm.pHand[i].transform.GetChild(0).GetComponent<CardTemplate>().card;
        //        if (c != null)
        //            forgeDeck.Add(c);

        //    }
        //}
        if (cm.discardPile.Count > 0)
        {
            for (int i = 0; i < cm.discardPile.Count; i++)
            {
                if (cm.discardPile[i] != null)
                    forgeDeck.Add(cm.discardPile[i]);
            }
        }
        cm.discardPile.Clear();
        cm.playerDeck.Clear();
        CreateCardButton();

    }
    public void Forge()
    {
        cm.ForgeCard(forge1, forge2,recipe);
        Debug.Log("Forging");
    }
    public void CreateCardButton()
    {
        for (int i = 0; i < forgeDeck.Count; i++)
        {
            GameObject o = Instantiate(cardButton, cardButton.transform.parent);
            //o.transform.parent = cardButtonParent.transform;
            o.transform.SetParent(cardButtonParent, false);
            o.GetComponent<CardTemplate>().LoadCard(forgeDeck[i]);
        }
    }
    public void UpdateToken(int i)
    {
        token = i;
        tokenText.text = token.ToString();
    }
    public void ResetForgeCards(Card card1, Card card2)
    {
        forgeDeck.Remove(card1);
        forgeDeck.Remove(card2);
        forge1 = null;
        forge2 = null;
        //add fused card to 
        foreach (Transform child in cardButtonParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        CreateForgeDeck();

    }
}

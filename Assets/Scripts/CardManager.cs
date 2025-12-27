using UnityEngine;
using System.Collections.Generic;

public class CardManager
{
    private BattleManager battleManager;
    public static List<Cards> deck;
    public List<Cards> drawPile;
    public List<Cards> hand;
    public List<Cards> discardPile;
    public CardManager(BattleManager b)
    {
        battleManager = b;
        battleManager.OnBattleStart += () =>
        {
            drawPile = new List<Cards>(deck);
            Shuffle(drawPile);
            hand = new List<Cards>();
            discardPile = new List<Cards>();
            DrawCards(5); // ここの値は適当
        };
    }
    public void Shuffle(List<Cards> listToShuffle)
    {
        List<Cards> shuffledList = new List<Cards>(listToShuffle);
        int n = shuffledList.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + Random.Range(0, n - i);
            Cards temp = shuffledList[r];
            shuffledList[r] = shuffledList[i];
            shuffledList[i] = temp;
        }
        listToShuffle.Clear();
        listToShuffle.AddRange(shuffledList);
    }
    public void DrawCards(int number)
    {
        for (int i = 0; i < number; i++)
        {
            if (drawPile.Count == 0)
            {
                // ドローパイルが空の場合、捨て札をシャッフルしてドローパイルに戻す
                drawPile.AddRange(discardPile);
                discardPile.Clear();
                Shuffle(drawPile);
            }
            if (drawPile.Count > 0)
            {
                Cards drawnCard = drawPile[0];
                drawPile.RemoveAt(0);
                hand.Add(drawnCard);
            }
        }
    }
    public void DiscardCard(Cards card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            discardPile.Add(card);
        }
    }
}

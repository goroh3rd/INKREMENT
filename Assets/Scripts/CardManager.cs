using UnityEngine;
using System.Collections.Generic;

public class CardManager
{
    public static List<Cards> deck;
    public List<Cards> drawPile;
    public List<Cards> hand;
    public List<Cards> discardPile;
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
}

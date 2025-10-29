using UnityEngine;

public class Playerinventory : MonoBehaviour
{
    public Coinmanager coinmanager;
    
    int coins = 0;  

    public void AddCoins(int amount)
    {
        coins += amount;
        coinmanager.UpdateCoinUi(coins);
    }
}

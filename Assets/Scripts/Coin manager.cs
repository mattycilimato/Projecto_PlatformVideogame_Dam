using TMPro;
using UnityEngine;

public class Coinmanager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public void UpdateCoinUi(int coin)
    {
        coinText.text = coin.ToString();
    }
}

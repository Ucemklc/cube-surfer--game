using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coinCount = 0;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        if (coinText != null)
            coinText.text = "Coin: " + coinCount;
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int coins = 0;

    public GameObject[] coinObjects;

    public TMPro.TextMeshProUGUI coinText;

    public void AddCoin()
    {
        coins++;
        coinText.text = "Coins: " + coins.ToString();
        if(coins >= coinObjects.Length)
        {
            Time.timeScale = 0;
        }
    }
}

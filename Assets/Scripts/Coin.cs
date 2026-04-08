using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.AddCoin();
        Destroy(gameObject);
    }
}

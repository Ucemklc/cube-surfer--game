using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float floatSpeed = 2f;
    public float floatHeight = 0.2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<topalabilirküp>() != null)
        {
            GameManager.instance.AddCoin(1);

            PlayerAutoBackward_XYZ player = FindObjectOfType<PlayerAutoBackward_XYZ>();
            if (player != null)
            {
                player.AddCoinEffect();
            }

            Destroy(gameObject);
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAutoBackward_XYZ : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    [SerializeField] private float autoXSpeed = 4f;
    [SerializeField] private float controlSpeed = 10f;
    [SerializeField] private float clampY = 5f;
    [SerializeField] private float clampZ = 5f;

    private float targetY;
    private float targetZ;
    private bool isDead = false;

    private AudioSource audioSource;
    public CameraShake camShake;
    public GameOverUI gameOverUI;

    // 🔥 TRAIL
    private TrailRenderer tr;
    float baseTrailTime = 0.2f;
    float maxTrailTime = 0.6f;

    // 💨 NITRO (DENGELİ)
    [Header("Nitro")]
    public float normalSpeed = 3f;
    public float nitroSpeed = 3.5f;   // ✅ ARTIK YAVAŞ
    public float nitroDuration = 0.3f;
    private bool isNitro = false;

    // 🎯 NITRO BAR
    public Image nitroBar;
    private int coinCount = 0;
    public int coinForNitro = 5;

    // 🎥 KAMERA
    public Camera mainCamera;
    private float normalFOV = 60f;

    // 🔥 PARTICLE
    public ParticleSystem nitroEffect;

    void Start()
    {
        targetY = transform.position.y;
        targetZ = transform.position.z;
        audioSource = GetComponent<AudioSource>();
        tr = GetComponent<TrailRenderer>();

        autoXSpeed = normalSpeed;

        if (mainCamera != null)
            normalFOV = mainCamera.fieldOfView;

        if (nitroEffect != null)
            nitroEffect.Stop();
    }

    void Update()
    {
        if (isDead) return;

        HandleInput();

        float newX = transform.position.x - autoXSpeed * Time.deltaTime;
        float newY = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * controlSpeed);
        float newZ = Mathf.Lerp(transform.position.z, targetZ, Time.deltaTime * controlSpeed);

        transform.position = new Vector3(newX, newY, newZ);

        // 🔥 Trail uzunluğu
        if (tr != null)
        {
            float t = Mathf.InverseLerp(3f, 6f, autoXSpeed);
            tr.time = Mathf.Lerp(baseTrailTime, maxTrailTime, t);
        }
    }

    void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float normalizedY = touch.position.y / Screen.height;
            float normalizedZ = touch.position.x / Screen.width;

            targetY = Mathf.Lerp(-clampY, clampY, normalizedY);
            targetZ = Mathf.Lerp(-clampZ, clampZ, normalizedZ);
        }

        if (Input.GetMouseButton(0))
        {
            float normalizedY = Input.mousePosition.y / Screen.height;
            float normalizedZ = Input.mousePosition.x / Screen.width;

            targetY = Mathf.Lerp(-clampY, clampY, normalizedY);
            targetZ = Mathf.Lerp(-clampZ, clampZ, normalizedZ);
        }
    }

    // 💰 COIN GELİNCE
    public void AddCoinEffect()
    {
        coinCount++;

        // 🌈 Trail renk
        Color randomColor = Random.ColorHSV();
        ChangeTrailColor(randomColor);

        // 🎯 UI
        if (nitroBar != null)
            nitroBar.fillAmount = (float)coinCount / coinForNitro;

        if (coinCount >= coinForNitro)
        {
            ActivateNitro();
            coinCount = 0;

            if (nitroBar != null)
                nitroBar.fillAmount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("engel") && !isDead)
        {
            isDead = true;
            if (audioSource != null) audioSource.Play();
            if (camShake != null) camShake.Shake();

            Time.timeScale = 0.5f;
            Invoke("GameOver", 0.5f);
        }
    }

    void GameOver()
    {
        Time.timeScale = 1f;
        if (gameOverUI != null) gameOverUI.ShowGameOver();
    }

    // 🌈 TRAIL RENK
    public void ChangeTrailColor(Color color)
    {
        if (tr != null)
        {
            Gradient gradient = new Gradient();

            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(color, 0.0f),
                    new GradientColorKey(color, 1.0f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1.0f, 0.0f),
                    new GradientAlphaKey(0.0f, 1.0f)
                }
            );

            tr.colorGradient = gradient;
        }
    }

    // 💨 NITRO
    public void ActivateNitro()
    {
        if (!isNitro)
            StartCoroutine(NitroCoroutine());
    }

    IEnumerator NitroCoroutine()
    {
        isNitro = true;

        float elapsed = 0f;

        while (elapsed < nitroDuration)
        {
            elapsed += Time.deltaTime;

            float wave = Mathf.Sin((elapsed / nitroDuration) * Mathf.PI);

            autoXSpeed = Mathf.Lerp(normalSpeed, nitroSpeed, wave);

            if (mainCamera != null)
                mainCamera.fieldOfView = Mathf.Lerp(normalFOV, normalFOV + 5f, wave);

            yield return null;
        }

        autoXSpeed = normalSpeed;

        if (mainCamera != null)
            mainCamera.fieldOfView = normalFOV;

        isNitro = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topalabilirküp : MonoBehaviour
{
    bool ToplandiMi;
    int index;
    public toplayıcı toplayıcı;

    [Header("Ses Ayarları")]
    public AudioClip collectClip;  // Küp toplama sesi
    public AudioClip hitObstacleClip; // Engel sesi
    public AudioSource audioSource;   // Ses çalacak AudioSource (Player veya küp)

    void Start()
    {
        ToplandiMi = false;

        // Eğer AudioSource atanmadıysa, Player üzerindeki AudioSource’u al
        if (audioSource == null && toplayıcı != null)
        {
            audioSource = toplayıcı.GetComponent<AudioSource>();
        }
        if (audioSource == null && toplayıcı != null)
        {
            audioSource = toplayıcı.GetComponent<AudioSource>();
        }

        // 🔥 Trail ekle
        TrailRenderer tr = gameObject.AddComponent<TrailRenderer>();
        tr.time = 0.3f;
        tr.startWidth = 0.4f;
        tr.endWidth = 0f;
    }

    void Update()
    {
        if (ToplandiMi)
        {
            if (transform.parent != null)
            {
                transform.localPosition = new Vector3(0, -index, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "engel")
        {
            // Yukseklik azalt
            toplayıcı.yukseklikazalt();

            // Ses çal
            if (audioSource != null && hitObstacleClip != null)
            {
                audioSource.PlayOneShot(hitObstacleClip, 1f);
            }

            transform.parent = null;
            GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public bool GetToplandiMi()
    {
        return ToplandiMi;
    }

    public void toplandiYap()
    {
        ToplandiMi = true;

        // 🔊 Küp toplandığında ses çal
        if (audioSource != null && collectClip != null)
        {
            audioSource.PlayOneShot(collectClip, 1f);
        }
    }

    public void IndexAyarla(int index)
    {
        this.index = index;
    }
}
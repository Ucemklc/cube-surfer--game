using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toplayıcı : MonoBehaviour
{
    GameObject anakup;
    int yukseklik;
    void Start()
    {
        anakup = GameObject.Find("anaküp");
    }

    // Update is called once per frame
    void Update()
    {
        anakup.transform.position = new Vector3(transform.position.x, yukseklik + 1, transform.position.z);
        this.transform.localPosition = new Vector3(0, -yukseklik, 0);
    }
    public void yukseklikazalt()
    {
        yukseklik--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "topla"&&other.gameObject.GetComponent<topalabilirküp>().GetToplandiMi()==false)
        {
            yukseklik += 1;
            other.gameObject.GetComponent<topalabilirküp>().toplandiYap();
            other.gameObject.GetComponent<topalabilirküp>().IndexAyarla(yukseklik);
            other.gameObject.transform.parent = anakup.transform;


        }
    }
}

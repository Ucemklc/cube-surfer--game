using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class hareketettirmeü : MonoBehaviour
{
    [SerializeField]
    private float ilerigitmehizi;
    [SerializeField]
    private float sagasolagitmehızı;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float yatayEksen = Input.GetAxis("Horizontal") * sagasolagitmehızı * Time.deltaTime;
        this.transform.Translate(yatayEksen, 0, ilerigitmehizi * Time.deltaTime);


    }
}
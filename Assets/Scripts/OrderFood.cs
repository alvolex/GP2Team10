using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderFood : MonoBehaviour
{
    [SerializeField] private GameObject orderUIImage;

    private SphereCollider sCollider;
    
    private bool readyToOrder;

    public bool ReadyToOrder => readyToOrder;
    public GameObject OrderUIImage
    {
        get => orderUIImage;
        set => orderUIImage = value;
    }

    public SphereCollider SCollider
    {
        set => sCollider = value;
    }

    public void Order()
    {
        StartCoroutine(TimeToOrder());
    }

    IEnumerator TimeToOrder()
    {
        yield return new WaitForSeconds(Random.Range(5f,8f));
        orderUIImage.SetActive(true);
        readyToOrder = true;
        //Make the collider bigger again when the alien is seated so that we can handle orders
        sCollider.radius = 3f;
    }
}
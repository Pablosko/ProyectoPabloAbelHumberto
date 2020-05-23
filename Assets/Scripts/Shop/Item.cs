using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 10,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        GoToInventoryObj();
    }
    public void GoToInventoryObj()
    {
        Vector3 dir = Gamecontroller.instance.ItemTigger.transform.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * 10);
    }
 }

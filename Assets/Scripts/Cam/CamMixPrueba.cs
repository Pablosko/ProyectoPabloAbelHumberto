using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMixer))]
public class CamMixPrueba : MonoBehaviour
{
    [SerializeField]
    Camera[] cameras;
    public int value = 0;
    CameraMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        mixer = GetComponent<CameraMixer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AddValue(-1);
            mixer.blendCamera(cameras[value], 1, Interpolators.bounceInOut);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AddValue(-1);
            AddValue(-1);

            mixer.blendCamera(cameras[value], 1, Interpolators.circularIn);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AddValue(1);

            mixer.blendCamera(cameras[value], 1, Interpolators.expoIn);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddValue(1);
            AddValue(1);

            mixer.blendCamera(cameras[value], 1, Interpolators.expoIn);
        }
    }
    public void AddValue(int add)
    {
        value += add;
        if (value >= cameras.Length)
            value = 0;
        if (value < 0)
            value = cameras.Length - 1;
    }
}

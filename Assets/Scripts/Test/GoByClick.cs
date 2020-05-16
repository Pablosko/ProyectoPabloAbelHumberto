using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoByClick : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Board generate;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 position = new Vector3(999,999,999);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                for(int i = 0; i < generate.tiles.Count; i++)
                {
                    if(Vector3.Distance(hit.point, generate.tiles[i].gameObject.transform.position) < Vector3.Distance(position, generate.tiles[i].gameObject.transform.position))
                    {
                        position = generate.tiles[i].gameObject.transform.position;
                    }
                }
                print(position);
                agent.SetDestination(position);
            }
        }
    }
}

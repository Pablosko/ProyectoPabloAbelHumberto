using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHero : MonoBehaviour
{
    public bool drag;
    Heroe heroe;
    Tile previousTile;
    // Start is called before the first frame update
    void Start()
    {
        heroe = transform.parent.GetComponent<Heroe>();
    }

    void Update()
    {
        if (drag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.RaycastAll(ray, 100);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<Tile>() != null)
                {
                    if (previousTile == null)
                    {
                        previousTile = hit.transform.GetComponent<Tile>();
                    }
                    if ((previousTile != null) && (previousTile != hit.transform.GetComponent<Tile>()))
                    {
                        previousTile.AnimateBorde(false);
                        previousTile = hit.transform.GetComponent<Tile>();
                    }
                    heroe.transform.position = hit.point;
                    if(Gamecontroller.instance.stageController.state == State.Start)
                         hit.transform.GetComponent<Tile>().AnimateBorde(true);

                }
            }
        }
    }
    private void OnMouseDown()
    {
        drag = true;
        if (Gamecontroller.instance.stageController.state == State.Fight && heroe.currentTile.type == TileType.board)
        {
            drag = false;
        }
        if(drag)
        Gamecontroller.instance.board.ActiveCasillasBorde(true);
    }
    private void OnMouseUp()
    {
       
        Gamecontroller.instance.board.ActiveCasillasBorde(false);
        drag = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, 100);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.GetComponent<Tile>() != null)
            {
                previousTile.AnimateBorde(false);
                if (Gamecontroller.instance.stageController.state == State.Fight)
                {
                    if (Gamecontroller.instance.board.tiles.Contains(hit.transform.GetComponent<Tile>()))
                    {
                        heroe.transform.localPosition = Vector3.zero;
                        return;
                    }
                }
                Tile tile = hit.transform.gameObject.GetComponent<Tile>();
                heroe.SwitchTile(tile);
                return;
            }
            heroe.transform.localPosition = Vector3.zero;
        }
       
    }

}

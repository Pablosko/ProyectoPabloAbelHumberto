using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NpcScript : MonoBehaviour
{
    //error en movimiento si es la primera vez y ataca ????;
    //si el optimo no tiene final que cambie a uno nuevo
    public float speed, physicalDamage, magicDamge, hp, maxHp, mana, criticalDamage, criticalRate, physicalArmor, magicalArmor, dodge, range, attackSpeed;
    const float hexDistance = 2.4f;
    public Tile currentTile;
    public int hexRange;
    public NpcScript target;
    public List<Tile> path = new List<Tile>();
    public List<Tile> previous = new List<Tile>();
    public Tile nextHex;
    public Tile startingTile;
    public NpcHud hud;
    public bool centered;
    float nextAttackTimer;
    public void Awake()
    {
        hud = transform.Find("Canvas").GetComponent<NpcHud>();
        name = gameObject.name;
        hp = maxHp;
    }
    public void Update()
    {
        if (target != null)
        {
            if (nextHex == null || path.Count == 1)
            {
                FindFullPath();
            }
            else if ((nextHex.currentNPC != null) ||
                    ((nextHex.nextNpc != null) && (nextHex.nextNpc != this)))
            {
                FindFullPath();
            }
            if (!TargetInRange() && nextHex != null && !NextHexRangeToTarget())
            {
                centered = false;
                Vector3 dir = nextHex.transform.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime * 1.5f);
                //si esta cerca del hex busca otro
                if (IsInNextHexRange(0.1f * speed, nextHex))
                {
                    currentTile.nextNpc = null;
                    currentTile.currentNPC = null;
                    transform.SetParent(nextHex.transform);
                    currentTile = nextHex;
                    nextHex.currentNPC = this;
                    nextHex.nextNpc = null;
                    path.Remove(nextHex);
                    FindFullPath();
                }
            }
            else
            {
                //cuando ya ha encontrao el hexagano de ataque
                if (nextHex != null)
                {
                    nextHex.nextNpc = null;
                    nextHex = null;
                    currentTile.currentNPC = this;
                }
                //go to center
                if (!centered)
                {
                   Vector3 dir = currentTile.transform.position - transform.position;
                   transform.Translate(dir.normalized * speed * Time.deltaTime * 1.5f);
                   if (IsInNextHexRange(0.3f * speed, currentTile))
                   {
                        centered = true;
                   }
                }
                else
                {
                    if (Vector3.Distance(transform.position,target.transform.position) <= range * 2 + 1f * speed)
                    {
                        nextAttackTimer += Time.deltaTime;
                        if (nextAttackTimer >= 1 / attackSpeed)
                        {
                            nextAttackTimer = 0;
                            AutoAttack();
                        }
                    }
                }
            }
        }
    }
    public virtual void AutoAttack()
    {
        Debug.Log("AutoAtaque");
        target.GetDmg(physicalDamage);
        if (target.hp <= 0)
        {
            if (target.GetComponent<EnemyScript>() != null)
            {
                target.GetComponent<EnemyScript>().Drop();
            }
            target.currentTile.currentNPC = null;
            Gamecontroller.instance.board.RemoveFromBoard(target);
            Gamecontroller.instance.board.CheckEndStage();
            target.gameObject.SetActive(false);
            target = null;
            Gamecontroller.instance.board.checkTargets();
            previous = new List<Tile>();
            FindFullPath();
        }
    }
    public virtual void UseSkill()
    {
        Debug.Log("Lanzar Skill");
    }
    public virtual void SelectTarget()
    {
        Debug.Log("Target Seleccionado");
    }
    public float GetDistance(NpcScript target)
    {
        return Mathf.Abs(Vector3.Distance(target.transform.position, transform.position));
    }
    public int GetDistanceInHexes(NpcScript target)
    {
        return (int)(target.transform.position - transform.position).magnitude / (int)Gamecontroller.instance.hexSize;
    }
    public bool TargetInRange()
    {
        return Vector3.Distance(target.currentTile.transform.position, currentTile.transform.position) <= range * 2f;
    }
    public bool IsInNextHexRange(float minRange, Tile hex)
    {
        return Vector3.Distance(hex.transform.position, transform.position) <= minRange;
    }
    public bool NextHexRangeToTarget()
    {
        foreach (Tile hex in currentTile.adyacentHex)
        {
            if (target.nextHex == hex)
                return true;
        }
        return false;
    }
    public void SwitchTile(Tile tile)
    {
        if (tile.currentNPC == null)
        {
            tile.nextNpc = null;
            currentTile.currentNPC = null;
            transform.SetParent(tile.transform);
            currentTile = tile;
            tile.currentNPC = this;
            transform.localPosition = Vector3.zero;
        }
        else if (tile.currentNPC.GetComponent<EnemyScript>() != null)
        {
            transform.localPosition = Vector3.zero;
        }
        else if (tile.currentNPC.GetComponent<Heroe>() != null)
        {
            //ponemos champ de su tile a la nuestra
            tile.currentNPC.currentTile = currentTile;
            tile.currentNPC.transform.SetParent(currentTile.gameObject.transform);
            tile.currentNPC.transform.localPosition = Vector3.zero;


            transform.SetParent(tile.gameObject.transform);
            currentTile = tile;
            tile.currentNPC = this;
            transform.localPosition = Vector3.zero;
        }
        if (tile.type == TileType.bench)
        {
            Gamecontroller.instance.synergieManager.RemoveHeroSynergies((Heroe)this);
        }
        if (tile.type == TileType.board)
        {
            Gamecontroller.instance.synergieManager.AddHeroSynergies((Heroe)this);
        }
    }
    public Tile GetNextHexPath(Tile pathtile)
    {
        if (pathtile == null)
            return null;
        float distance = 1000;
        Tile newHex = new Tile();
        foreach (Tile hex in pathtile.adyacentHex)
        {
            if (hex.currentNPC == target)
                return null;
            if ((hex.currentNPC == null) &&
                (hex.nextNpc == null) &&
                (!path.Contains(hex)))
            {
                float newDitance;
                if (target.nextHex != null)
                    newDitance = Vector3.Distance(hex.transform.position, target.nextHex.transform.position);
                else
                    newDitance = Vector3.Distance(hex.transform.position, target.transform.position);

                if (newDitance < distance)
                {
                    distance = newDitance;
                    newHex = hex;
                }
            }
        }
        return newHex;
    }
    public Vector3 GetTargetPoint(Transform target)
    {
        return target.transform.position;
    }
    public void FindFullPath()
    {
        previous = path;
        path = new List<Tile>();
        bool find = true;
        path.Add(GetNextHexPath(currentTile));
        while (find)
        {
            Tile newTile = GetNextHexPath(path[path.Count - 1]);
            if (newTile == null)
            {
                find = false;
                if (previous.Count > 0)
                    OptimusPath();
                SetTileWhenCalc(path[0]);
                return;
            }

            if (!path.Contains(newTile))
            {
                path.Add(newTile);
                print(newTile.gameObject);
            }
        }
    }
    public void OptimusPath()
    {
        if (!PathCollapsed(previous) && (previous.Count < path.Count) && previous[0] != null)
        {
            path = previous;
        }

    }
    public bool PathCollapsed(List<Tile> path)
    {
        foreach (Tile tile in path)
        {
            if (tile != null)
            {
                if ((tile.currentNPC != null) ||
                    ((tile.nextNpc != null) && (tile.nextNpc != this)))
                {
                    return true;
                }
                else
                    return false;
            }
        }
        return false;
    }
    public void SetTileWhenCalc(Tile newtile)
    {
        if (newtile == null)
            return;

        newtile.nextNpc = this;
        nextHex = newtile;
    }
    public void GetDmg(float dmg)
    {
        hp -= dmg;
        hud.UpdateBar(hp, maxHp);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void NpcEvent(NpcScript target);
public class NpcScript : MonoBehaviour
{
    //error en movimiento si es la primera vez y ataca ????;
    //si el optimo no tiene final que cambie a uno nuevo
    
    const float hexDistance = 2.4f;
    float nextAttackTimer;
    public int hexRange;
    public bool centered;
    public bool isInvecible;
    public Transform mesh;
    public Animator anim;
    public NpcScript target;
    public NpcHud hud;
    public List<Tile> path = new List<Tile>();
    public List<Tile> previous = new List<Tile>();
    public Tile nextHex;
    public Tile startingTile;
    public Tile currentTile;
    public NpcStats stats;
    public List<NpcEvent> onHitEvents = new List<NpcEvent>();
    public List<NpcEvent> onKillEvents = new List<NpcEvent>();
    public List<NpcEvent> onGetHitEvents = new List<NpcEvent>();
    public List<NpcEvent> onCastEvents = new List<NpcEvent>();
    public List<NpcEvent> onSpellHitEvents = new List<NpcEvent>();
    public void Awake()
    {
        stats = GetComponent<NpcStats>();
        hud = transform.parent.Find("Canvas").GetComponent<NpcHud>();
        mesh = transform.Find("Mesh");
        anim = mesh.transform.Find("Prefab").GetComponent<Animator>();
        name = gameObject.name;
        
    }
    public void Update()
    {
        if (target != null)
        {
            anim.SetBool("run", true);
            anim.speed = 1;
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
                mesh.transform.localRotation = Quaternion.Euler(0, Vector3.SignedAngle(transform.forward, dir.normalized, transform.up), 0);
                transform.Translate(dir.normalized * stats.speed.value * Time.deltaTime * 1.5f);
                //si esta cerca del hex busca otro
                if (IsInNextHexRange(0.1f * stats.speed.value, nextHex))
                {
                    currentTile.nextNpc = null;
                    currentTile.currentNPC = null;
                    transform.parent.SetParent(nextHex.transform);
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
                    mesh.transform.localRotation = Quaternion.Euler(0, Vector3.SignedAngle(transform.forward, dir.normalized, transform.up), 0);
                    transform.Translate(dir.normalized * stats.speed.value * Time.deltaTime * 1.5f);
                    if (IsInNextHexRange(0.3f * stats.speed.value, currentTile))
                    {
                        centered = true;
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, target.transform.position) <= stats.range.value * 2 + 1f * stats.speed.value)
                    {
                        if (nextAttackTimer <= 0)
                        {
                            anim.SetTrigger("attack");
                            anim.speed = stats.attackSpeed.value;
                        }
                        nextAttackTimer += Time.deltaTime;
                        Vector3 dir = target.transform.position - transform.position;
                        mesh.transform.localRotation = Quaternion.Euler(0, Vector3.SignedAngle(transform.forward, dir.normalized, transform.up), 0);
                        if (nextAttackTimer >= 1 / stats.attackSpeed.value)
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
        foreach (NpcEvent func in onHitEvents)
        {
            func(target);
        }
        DealDmg();

    }
    public void DealDmg()
    {
        target.GetDmg(stats.physicalDamage.value);
        if (target.stats.hp.value <= 0)
        {
            target.hud.bar.gameObject.SetActive(false);
            target.anim.SetTrigger("die");
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == "Die")
                {
                    target.Invoke("die", clip.length);
                }
            }
            if (target.GetComponent<EnemyScript>() != null)
            {
                target.GetComponent<EnemyScript>().Drop();
            }
            target.currentTile.currentNPC = null;
            Gamecontroller.instance.board.RemoveFromBoard(target);
            Gamecontroller.instance.board.CheckEndStage();
            target = null;
            Gamecontroller.instance.board.checkTargets();
            previous = new List<Tile>();
            FindFullPath();
        }
    }
    public void die()
    {
        if (GetComponent<EnemyScript>() != null)
            Destroy(transform.parent.gameObject);

        transform.parent.gameObject.SetActive(false);
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
        return Vector3.Distance(target.currentTile.transform.position, currentTile.transform.position) <= stats.range.value * 2f;
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
            transform.parent.SetParent(tile.transform);
            currentTile = tile;
            tile.currentNPC = this;
            transform.parent.localPosition = Vector3.zero;
        }
        else if (tile.currentNPC.GetComponent<EnemyScript>() != null)
        {
            transform.localPosition = Vector3.zero;
        }
        else if (tile.currentNPC.GetComponent<Heroe>() != null)
        {
            //ponemos champ de su tile a la nuestra
            tile.currentNPC.currentTile = currentTile;
            tile.currentNPC.transform.parent.SetParent(currentTile.gameObject.transform);
            tile.currentNPC.transform.parent.localPosition = Vector3.zero;


            transform.parent.SetParent(tile.gameObject.transform);
            currentTile = tile;
            tile.currentNPC = this;
            transform.parent.localPosition = Vector3.zero;
        }
        if (GetComponent<Heroe>() != null)
        {
            if (tile.type == TileType.bench)
            {
                Gamecontroller.instance.synergieManager.RemoveHeroSynergies((Heroe)this);
            }
            else if (tile.type == TileType.board)
            {
                Gamecontroller.instance.synergieManager.AddHeroSynergies((Heroe)this);
            }
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
        if (!isInvecible)
        {
            stats.hp.value -= dmg;
            hud.UpdateBar(stats.hp.value, stats.maxHp.value);
        }
    }
   
}

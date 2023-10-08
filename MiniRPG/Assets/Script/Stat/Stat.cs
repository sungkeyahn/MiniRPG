using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int level;
    public int Level { get { return level; } set { level = value; } }

    [SerializeField]
    protected int hp;
    public int Hp { get { return hp; } set { hp = value; } }

    [SerializeField]
    protected int maxHp;
    public int MaxHP { get { return maxHp; } set { maxHp = value; } }

    [SerializeField]
    protected int attack;
    public int Attack { get { return attack; } set { attack = value; } }

    [SerializeField]
    protected int defense;
    public int Defense { get { return defense; } set { defense = value; } }

    [SerializeField]
    protected float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }


    private void Start()
    {
        level = 1;
        hp = 100;
        maxHp = 100;
        attack = 10;
        defense = 5;
        moveSpeed=5.0f;
    }

    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, Attack - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }
    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat ps = attacker as PlayerStat;
        if (ps !=null)
        {
            ps.Exp += 15;
        }
        Managers.Game.DeSpawn(gameObject);
    }

}

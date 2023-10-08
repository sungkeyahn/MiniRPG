using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 destPos;

    [SerializeField]
    protected GameObject lockTarget;

    [SerializeField]
    protected Define.State state = Define.State.Idle;
    public virtual Define.State State
    {
        get { return state; }
        set
        {
            state = value;
            Animator anim = GetComponent<Animator>();//애니메이션
            switch (state)
            {   
                case Define.State.Die:

                    break;
                case Define.State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case Define.State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
                default:
                    break;
            }
        }
    }

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public abstract void Init();
    void Start()
    {
        Init();
    }
    void Update()
    {
        switch (state)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
    }
    protected virtual void UpdateDie() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateSkill() { }




}

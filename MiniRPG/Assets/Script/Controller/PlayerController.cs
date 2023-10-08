using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : UnitController
{
    PlayerStat stat;

    int mask = ((1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster));

    bool stopSkill =false;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        if (gameObject.GetComponentInChildren<UI_HpBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HpBar>(transform);
    }

    protected override void UpdateMoving()
    {
        if (lockTarget != null)
        {
            destPos = lockTarget.transform.position;
            float distance = (destPos - transform.position).magnitude;
            if (distance <= 1.0f)
            {
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = destPos - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.01f)
            State = Define.State.Idle;
        else
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    State = Define.State.Idle;
                return;
            }

            float moveDist = Mathf.Clamp(stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }
    protected override void UpdateSkill()
    {
        if (lockTarget != null)
        {
            Vector3 dir = lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation,quat,20*Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if (lockTarget != null)
        {
            Stat targetStat = lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(stat);
        }

        if (stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (state)
        {
            case Define.State.Die:
                break;
            case Define.State.Moving:
                OnMouseEvent_Run_Idle(evt);
                break;
            case Define.State.Idle:
                OnMouseEvent_Run_Idle(evt);
                break;
            case Define.State.Skill:
                OnMouseEvent_Attack(evt);
                break;
            default:
                break;
        }
    }
    void OnMouseEvent_Run_Idle(Define.MouseEvent evt)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, mask);
        switch (evt)
        {
            case Define.MouseEvent.Press:
                if (lockTarget == null && raycastHit)
                {
                    destPos = hit.point;
                    destPos.y = 0;
                }
                break;
            case Define.MouseEvent.PointerDown:
                if (raycastHit)
                {
                    destPos = hit.point;
                    destPos.y = 0;
                    State = Define.State.Moving;
                    stopSkill = false;
                    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        lockTarget = hit.collider.gameObject;
                    else
                        lockTarget = null;
                }
                break;
            case Define.MouseEvent.PointerUp:
                stopSkill = true;
                break;
        }
    }
    void OnMouseEvent_Attack(Define.MouseEvent evt)
    {
        if (evt == Define.MouseEvent.PointerUp)
            stopSkill = true;
    }
}

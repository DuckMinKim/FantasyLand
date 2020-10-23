using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtl : MonoBehaviour
{
    public enum MoveType { WAY_POINT, LOOK_AT, GEAR_VR}
    public MoveType moveType = MoveType.WAY_POINT;
    public float speed = 1.0f;
    public float damping = 3.0f;

    public Transform[] points;
    Transform tr;
    CharacterController cc;
    Transform camTr;
    int nextIdx = 1;

    void Start()
    {
        tr = GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
        camTr = Camera.main.GetComponent<Transform>();

        GameObject wayPointGroup = GameObject.Find("WayPointGroup");
        if (wayPointGroup != null)
            points = wayPointGroup.GetComponentsInChildren<Transform>();

    }


    void Update()
    {
        switch (moveType)
        {
            case MoveType.WAY_POINT: MoveWayPoint(); break;
            case MoveType.LOOK_AT: MoveLookAt(1); break;
            case MoveType.GEAR_VR: break;
        }
    }

    void MoveWayPoint()
    {
        Vector3 direction = points[nextIdx].position - tr.position;
        Quaternion rot = Quaternion.LookRotation(direction);
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    void MoveLookAt(int facing)
    {
        Vector3 heading = camTr.forward;
        heading.y = 0.0f;
        Debug.DrawRay(tr.position, heading.normalized * 0.1f, Color.white);
        cc.SimpleMove(heading * speed * facing);
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "WAY_POINT")
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
    }
}

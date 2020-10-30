using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IGvrPointerHoverHandler
{
    Animator anim;
    readonly int hashOpen = Animator.StringToHash("IsOpen");

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnBoxOpen(bool isOpen) => anim.SetBool(hashOpen, isOpen);

    public void OnLookItemBox(bool isLookAt)
    {
        Debug.Log(isLookAt);
        MoveCtl.isStopped = isLookAt;
    }

    public void OnGvrPointerHover(PointerEventData eventData)=> Debug.Log("Reticle On");

    public void OnPointerEnter(PointerEventData eventData) => MoveCtl.isStopped = true;

    public void OnPointerExit(PointerEventData eventData) => MoveCtl.isStopped = false;

}

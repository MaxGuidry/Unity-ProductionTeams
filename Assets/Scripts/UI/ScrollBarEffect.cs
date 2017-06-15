using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarEffect : MonoBehaviour
{
    public GameObject MenuButtonsGameObject;
    public GameObject InstructionsGameObject;
    public GameObject CreditsGameObject;

    public Transform InstructionsTarget;
    public Transform CreditsTarget;
    public Transform ButtonTarget;

    public Vector3 InstructionsOriginalPosition;
    public Vector3 CreditsOriginalPosition;

    private bool IsCreditMove;
    private bool IsInstruMove;

    public void Start()
    {
        IsCreditMove = false;
        IsInstruMove = false;
        InstructionsOriginalPosition = InstructionsGameObject.transform.position;
        CreditsOriginalPosition = InstructionsGameObject.transform.position;
    }

   
    public void InstruMove()
    {
        IsInstruMove = !IsInstruMove;
        if (!IsInstruMove)
        {
            InstructionsGameObject.transform.position = InstructionsTarget.position;
            return;
        }
        InstructionsGameObject.transform.position = InstructionsOriginalPosition;
    }

    public void CreditsMove()
    {
        IsCreditMove = !IsCreditMove;
        if (!IsCreditMove)
        {
            CreditsGameObject.transform.position = CreditsTarget.position;
            return;
        }
        CreditsGameObject.transform.position = CreditsOriginalPosition;
    }

    // Update is called once per frame
    void Update()
    {


    }
}

﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/*
 * Gets information from the joystick on the screen
 * The input vector is used in moving the player
 */
public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public Image bgImg;
    public Image joystickImg;
    private Vector3 inputVector;
    public bool moving = false;



    private void start() {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnDrag(PointerEventData ped) {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform
                       , ped.position
                       , ped.pressEventCamera
                       , out pos)) {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImg.rectTransform.anchoredPosition =
             new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3)
              , inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3));


        }
    }

    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
        moving = true;
    }


    public virtual void OnPointerUp(PointerEventData ped) {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        moving = false;
    }

    public float Horizontal() {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical() {

        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}
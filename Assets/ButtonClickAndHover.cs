using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClickAndHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource clickSound;
    public AudioSource hoverSound;

    private Button button { get { return GetComponent<Button>(); } }

    void Start()
    {
        button.onClick.AddListener(() => {
            clickSound.Play(0);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverSound.Play(0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}

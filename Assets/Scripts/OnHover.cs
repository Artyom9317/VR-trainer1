using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

[RequireComponent(typeof(Image))]
public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.grey;
    public Color32 m_DownColor = Color.white;
    public Color32 m_ClickColor = Color.red;

    private Image m_Image = null;

    void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Image.color = m_HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {       
        m_Image.color = m_NormalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {        
        m_Image.color = m_DownColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {       
       // m_Image.color = m_NormalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        m_Image.color = m_ClickColor;
    }
}
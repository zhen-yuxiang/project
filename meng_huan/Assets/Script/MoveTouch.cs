using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveTouch : MonoBehaviour, IDragHandler,IEndDragHandler
{
    public Image ImageBg;
    public Image ImagePoint;
    //虚拟按键 2d节点坐标
    public Vector2 m_imagePointV;
    private float m_radius
    {
        get {
            return ImageBg.rectTransform.sizeDelta.x / 2;
        }
    }

    public Vector3 Point3DDir
    {
        get
        {
            return new Vector3(m_imagePointV.x, 0, m_imagePointV.y);
        }
    }



    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(ImageBg.rectTransform, eventData.position, eventData.pressEventCamera, out m_imagePointV))
        {
            float _destance = Vector2.Distance(m_imagePointV, Vector2.zero);
            if (_destance > m_radius)
            {
                m_imagePointV = m_radius * (m_imagePointV - Vector2.zero).normalized;
            }
            ImagePoint.rectTransform.anchoredPosition = m_imagePointV;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        ImagePoint.rectTransform.anchoredPosition = Vector2.zero;
        m_imagePointV = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


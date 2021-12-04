using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Text))]
public class UiTabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Text uiText;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;
    public Quest quest;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        tabGroup = this.GetComponentInParent<TabGroup>();
        tabGroup.Subscribe(this);
        uiText = GetComponent<Text>();
    }

    //public void Select()
    //{
    //    onTabSelected?.Invoke();
    //}

    //public void Deselect()
    //{
    //    onTabDeselected?.Invoke();
    //}
}

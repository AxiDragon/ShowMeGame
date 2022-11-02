using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image image;
    internal bool selected;

    public virtual void Select()
    {
        selected = !selected;
        image.color = selected ? Color.red : Color.green;
    }
}

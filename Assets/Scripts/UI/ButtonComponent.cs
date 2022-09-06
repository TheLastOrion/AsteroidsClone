using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public abstract class ButtonComponent : MonoBehaviour
{
    protected Button _button;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(
            delegate
            {
                ButtonClick();
            }
        );

    }


    protected abstract void ButtonClick();

}


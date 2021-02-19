using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActionController : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject _hoverIndicator;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
      

    public void ButtonDownAnimation()
    {
        Debug.Log("Button pressed");
        _anim.SetTrigger("ButtonDown");
    }

    public void ShowHoverIndicator()
    {
        _hoverIndicator.SetActive(true);
    }
    public void HideHoverIndicator()
    {
        _hoverIndicator.SetActive(false);
    }
}

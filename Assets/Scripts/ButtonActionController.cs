using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActionController : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject _button;
    private Renderer objRenderer;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

        objRenderer = _button.GetComponent<Renderer>();
        startColor = objRenderer.material.color;
    }
      

    /// <summary>
    /// Starts an animation (Shade in)
    /// </summary>
    public void ButtonDownAnimation()
    {
        Debug.Log("Activate Button pressed");
        _anim.SetTrigger("ButtonDown");
    }
    /// <summary>
    /// Starts an animation (Changing color) on Hover
    /// </summary>
    public void ShowHoverIndicator()
    {
        objRenderer.material.color = Color.white;
    }
    /// <summary> 
    /// Starts an animation (Changing color) on Hover end
    /// </summary>
    public void HideHoverIndicator()
    {
        objRenderer.material.color = startColor;
    }
}

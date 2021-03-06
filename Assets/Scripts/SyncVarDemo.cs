using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

public class SyncVarDemo : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetColor))] private Color32 _color = Color.red;
    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        StartCoroutine(RandomColor());
    }

    private void SetColor(Color32 oldColor, Color32 newColor)
    {
        _renderer.color = newColor;
    }

    private IEnumerator RandomColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            _color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f, 1f, 1f);
        }
    }
}
using Unity.VisualScripting;
using UnityEngine;

public class MovibleIngridient : MonoBehaviour
{
    private IngridientBase _ingridient;

    private SpriteRenderer _sp;

    private int _method;

    private void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _sp.sprite = _ingridient.GetSprite();
        _method = _ingridient.GetMethod();
    }

    private void Update()
    {
        
    }
}
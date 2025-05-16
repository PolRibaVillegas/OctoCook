using Unity.VisualScripting;
using UnityEngine;

public class MovibleIngridient : MonoBehaviour
{
    [SerializeField]
    private IngridientBase _ingridient;

    private SpriteRenderer _sp;
    
    private int _method;

    private bool _mouse = false;

    private int _state;
    private const int _IDLE = 0;
    private const int _MOVING = 1;

    private void Start()
    {
        _state = _IDLE;

        _sp = GetComponent<SpriteRenderer>();

        if (_ingridient != null)
        {
            _sp.sprite = _ingridient.GetSprite();
            _method = _ingridient.GetMethod();
        }
    }

    private void Update()
    {
        switch (_state)
        {
            case _IDLE:
                //TODO idle animation
                break;
            case _MOVING:
                Vector3 newPosition = Input.mousePosition;
                newPosition = Camera.main.ScreenToWorldPoint(newPosition);
                newPosition.z = this.transform.position.z;
                this.transform.position = newPosition;
                break;
        }
    }

    private void OnMouseEnter()
    {
        if (_state != _IDLE)
            return;

        _mouse = true;

        if (_sp != null)
            _sp.color = Color.gray;
        else
            _sp = GetComponent<SpriteRenderer>();
    }

    private void OnMouseExit()
    {
        if (_state != _IDLE)
            return;

        _mouse = false;

        _sp.color = Color.white;
    }

    private void OnLClickPress()
    {
        if (_mouse)
            _state = _MOVING;
    }

    private void OnLClickRelease()
    {
        if (_state == _MOVING)
            _state = _IDLE;
    }

    public IngridientBase getIngridient()
    {
        return this._ingridient;
    }

    public void setIngridient(IngridientBase ingridient)
    {
        _sp = GetComponent<SpriteRenderer>();

        this._ingridient = null;
        this._ingridient = ingridient;
        this._method = ingridient.GetMethod();
        this._sp.sprite = ingridient.GetSprite();
    }

    public void setStateMov()
    {
        _state = _MOVING;
    }

}
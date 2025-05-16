using UnityEngine;

public class OnionBox : MonoBehaviour
{
    private SpriteRenderer _sp;
    private BoxCollider2D _box;

    private bool _mouse = false;

    [SerializeField]
    private GameObject _onionPrefab;

    [SerializeField]
    private IngridientBase _onion;

    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _box = GetComponent<BoxCollider2D>();
    }

    private void OnMouseEnter()
    {
        _mouse = true;
        _sp.color = Color.gray;
    }

    private void OnMouseExit()
    {
        _mouse= false;
        _sp.color = Color.white;
    }

    private void OnLClickPress()
    {
        if (!_mouse)
            return;

        var _prefab = Instantiate(_onionPrefab, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
        _prefab.GetComponent<MovibleIngridient>().setIngridient(_onion);
        _prefab.GetComponent<MovibleIngridient>().setStateMov();

        _sp.color= Color.white;
    }
}

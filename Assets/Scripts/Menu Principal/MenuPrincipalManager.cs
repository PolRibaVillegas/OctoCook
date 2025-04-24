using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    private SpriteRenderer _sp;
    private bool _dentro = false;

    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        _sp.color = Color.gray;
        _dentro = true;
    }

    private void OnMouseExit()
    {
        _sp.color = Color.white;
        _dentro = false;
    }

    private void OnClick()
    {
        if (_dentro)
        {
            SceneManager.LoadScene("CocinaCore");
        }
    }
}

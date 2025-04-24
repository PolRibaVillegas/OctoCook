using UnityEngine;

public class Ingrediente : MonoBehaviour
{
    [SerializeField]
    private IngredienteBase _ingrediente;

    public IngredienteBase GetIngrediente()
    {
        return _ingrediente;
    }

    public void ChangeIngrediente(IngredienteBase ingrediente)
    {
        _ingrediente = ingrediente;
    }
}

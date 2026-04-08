using UnityEngine;

public class CatOrder : MonoBehaviour
{
    public GameObject orderBubble;
    public string[] recipes = { "Pescado", "Leche", "Pollo" };
    private string currentRecipe;

    void Start()
    {
        GenerateOrder();
        orderBubble.SetActive(false);
    }

    void GenerateOrder()
    {
        currentRecipe = recipes[Random.Range(0, recipes.Length)];
    }

    public void ShowOrder(bool show)
    {
        orderBubble.SetActive(show);
    }

    public string GetOrder() => currentRecipe;
}
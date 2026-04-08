using UnityEngine;
using TMPro;

public class OrderBubble : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject bubbleObject;
    public TextMeshProUGUI recipeText;
    public SpriteRenderer recipeIcon;

    [Header("Recetas disponibles")]
    public string[] recipeNames = { "Salmon", "Pollo", "Leche" };
    public Sprite[] recipeSprites;

    private int assignedRecipe = -1;

    void Awake()
    {
        if (bubbleObject != null)
            bubbleObject.SetActive(false);
    }

    public void ShowOrder()
    {
        assignedRecipe = Random.Range(0, recipeNames.Length);

        if (recipeText != null)
            recipeText.text = recipeNames[assignedRecipe];

        if (recipeIcon != null && recipeSprites != null && assignedRecipe < recipeSprites.Length)
            recipeIcon.sprite = recipeSprites[assignedRecipe];

        if (bubbleObject != null)
            bubbleObject.SetActive(true);

        StartCoroutine(FaceCamera());
    }

    public void HideOrder()
    {
        if (bubbleObject != null)
            bubbleObject.SetActive(false);
    }

    public int GetRecipeIndex() => assignedRecipe;

    System.Collections.IEnumerator FaceCamera()
    {
        Camera cam = Camera.main;
        while (bubbleObject != null && bubbleObject.activeSelf)
        {
            if (cam != null)
                bubbleObject.transform.LookAt(
                    bubbleObject.transform.position + cam.transform.rotation * Vector3.forward,
                    cam.transform.rotation * Vector3.up);
            yield return null;
        }
    }
}

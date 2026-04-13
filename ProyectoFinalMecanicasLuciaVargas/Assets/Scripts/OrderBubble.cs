using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderBubble : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject bubbleObject;
    public TextMeshProUGUI recipeText;
    public Image recipeIcon;

    [Header("Recetas disponibles")]
    public string[] recipeNames = { "Salmon", "Pollo", "Leche" };
    public Sprite[] recipeSprites;

    private int assignedRecipe = -1;
    private QueueManager queueManager;
    private CatMover catMover;

    void Awake()
    {

        if (bubbleObject != null)
            bubbleObject.SetActive(false);

        queueManager = FindObjectOfType<QueueManager>();
        catMover = GetComponent<CatMover>();
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

    private void OnCollisionEnter(Collider other)
    {
        FoodItem food = other.GetComponent<FoodItem>();

        if (food != null)
        {
            if (food.foodIndex == assignedRecipe)
            {
                Debug.Log("¡Pedido correcto! Cliente feliz 😺");

                HideOrder();
                GameManager.Instance.AddMoney(food.value);
                // Destruir la comida
                Destroy(other.gameObject);

                // 👉 Avisar al QueueManager y eliminar de la cola
                if (queueManager != null && catMover != null)
                {
                    queueManager.LeaveQueue(catMover);
                }

                // 👉 Destruir el gato (puedes retrasarlo si quieres animación)
                Destroy(gameObject, 0.2f);
            }
            else
            {
                Debug.Log("Pedido incorrecto 😾");
                GameManager.Instance.AddMoney(-5);
                Destroy(other.gameObject);
            }
        }
    }


}
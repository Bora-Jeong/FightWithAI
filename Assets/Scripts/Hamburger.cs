using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hamburger : MonoBehaviour
{
    private Queue<Ingredient> _ingredients = new Queue<Ingredient>(); // 재료 큐
    public Queue<Ingredient> ingredients => _ingredients;

    public void StackIngredient(Ingredient ingredient)
    {
        GameObject go = new GameObject();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Hamburger";
        sr.sprite = GameManager.instance.GetIngredientSprite(ingredient);
        float height = sr.size.y;

        Vector3 pos = Vector3.zero;
        if (_ingredients.Count > 0) // 아래에 다른 재료가 있다면
        {
            Transform under = transform.GetChild(transform.childCount - 1);
            SpriteRenderer underSR = under.GetComponent<SpriteRenderer>();
            float underHeight = underSR.size.y;
            if (ingredient == Ingredient.Cheese)
                pos = underSR.transform.position + new Vector3(0, underHeight * 0.2f, 0);
            else
                pos = under.transform.position + new Vector3(0, underHeight * 0.2f + height * 0.25f, 0);
            sr.sortingOrder = underSR.sortingOrder + 1;
        }
        go.transform.SetParent(transform);
        go.transform.position = pos;

        _ingredients.Enqueue(ingredient); // 큐에 추가
    }

    public void StackIngredientUI(Ingredient ingredient)
    {
        GameObject go = new GameObject();
        Image image = go.AddComponent<Image>();
        image.sprite = GameManager.instance.GetIngredientSprite(ingredient);
        image.preserveAspect = true;
        float height = image.sprite.rect.height;

        Vector3 pos = Vector3.zero;
        if (_ingredients.Count > 0) // 아래에 다른 재료가 있다면
        {
            Transform under = transform.GetChild(transform.childCount - 1);
            Image underSR = under.GetComponent<Image>();
            float underHeight = underSR.sprite.rect.height;
            float value = 0.15f;
            if (ingredient == Ingredient.Cheese)
                pos = underSR.transform.position + new Vector3(0, underHeight * 0.2f * value, 0);
            else
                pos = under.transform.position + new Vector3(0, underHeight * 0.2f * value + height * 0.3f * value, 0);
        }
        go.transform.SetParent(transform);
        go.transform.position = pos;

        _ingredients.Enqueue(ingredient); // 큐에 추가
    }
}

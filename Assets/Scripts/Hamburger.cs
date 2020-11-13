using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hamburger : MonoBehaviour
{
    public Queue<Ingredient> ingredients = new Queue<Ingredient>(); // 재료 큐

    public void StackIngredient(Ingredient ingredient)
    {
        GameObject go = new GameObject();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Hamburger";
        sr.sprite = GameManager.instance.GetIngredientSprite(ingredient);

        float y = sr.bounds.size.y;
        go.transform.SetParent(transform);
        go.transform.localScale = Vector3.one;

        Vector3 pos = transform.position;
        if (ingredients.Count > 0) // 아래에 다른 재료가 있다면
        {
            Transform under = transform.GetChild(transform.childCount - 2);
            SpriteRenderer underSR = under.GetComponent<SpriteRenderer>();
            float underHeight = underSR.bounds.size.y;

            if (ingredient == Ingredient.Cheese)
                pos = under.transform.position + new Vector3(0, underHeight * 0.2f, 0);
            else if (ingredient == Ingredient.TopBread)
                pos = under.transform.position + new Vector3(0, underHeight * 0.2f + y * 0.1f, 0);
            else
                pos = under.transform.position + new Vector3(0, underHeight * 0.5f, 0);
            sr.sortingOrder = underSR.sortingOrder + 1;
        }

        go.transform.position = pos;
        ingredients.Enqueue(ingredient); // 큐에 추가
    }

    public void StackIngredientUI(Ingredient ingredient)
    {
        GameObject go = new GameObject();
        Image image = go.AddComponent<Image>();
        image.sprite = GameManager.instance.GetIngredientSprite(ingredient);
        image.preserveAspect = true;
        float height = image.sprite.rect.height;

        Vector3 pos = Vector3.zero;
        if (ingredients.Count > 0) // 아래에 다른 재료가 있다면
        {
            Transform under = transform.GetChild(transform.childCount - 1);
            Image underSR = under.GetComponent<Image>();
            float underHeight = underSR.sprite.rect.height;
            float value = 0.15f;
            if (ingredient == Ingredient.Cheese)
                pos = underSR.transform.position + new Vector3(0, underHeight * 0.2f * value, 0);
            else
                pos = under.transform.position + new Vector3(0, underHeight * 0.3f * value + height * 0.35f * value, 0);
        }
        go.transform.SetParent(transform);
        go.transform.position = pos;

        ingredients.Enqueue(ingredient); // 큐에 추가
    }

    public void Discard() //현재 햄버거 재료 모두 destroy
    {
        for (int i = transform.childCount - 1; i >= 0; i--) // 쟁반 클리어
            Destroy(transform.GetChild(i).gameObject);
        ingredients.Clear();
    }
}

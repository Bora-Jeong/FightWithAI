using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public Ingredient ingredient;
    [SerializeField]
    Transform ingredientTransform;
    GameObject curIngredient;
   
    private void OnMouseDown()
    {
        /*
        print("click");
        GameObject go = new GameObject();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        go.AddComponent<DragIngredient>();
        sr.sprite = GameManager.instance.GetIngredientSprite(ingredient);
        go.AddComponent<BoxCollider2D>();
        */
        curIngredient = ingredientTransform.GetChild((int)ingredient).gameObject;
        curIngredient.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;
        Vector3 objectPostion = Camera.main.ScreenToWorldPoint(mousePosition);
        curIngredient.transform.position = new Vector3(objectPostion.x, objectPostion.y, 1);
       
  
    }

    private void OnMouseDrag()
    {
        Vector3 objectPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curIngredient.transform.position = new Vector3(objectPostion.x, objectPostion.y, 1);

    }

    private void OnMouseUp()
    {
        Vector3 objectPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curIngredient.SetActive(false);

        if (Physics2D.Raycast(objectPostion, transform.forward, 10, 1 << LayerMask.NameToLayer("Tray"))){
            GameManager.instance.playerHamburger.StackIngredient(ingredient);
            AudioManager.instance.DropSound();
           
        }
        else
        {

        }
    }

}

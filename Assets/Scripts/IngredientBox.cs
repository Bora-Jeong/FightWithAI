using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientBox : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 

    public Ingredient ingredient;
    [SerializeField]
    Transform ingredientTransform;
    GameObject curIngredient;

    /*
     private void OnMouseDown()
     {

         curIngredient = ingredientTransform.GetChild((int)ingredient).gameObject;
         curIngredient.SetActive(true);

         Vector3 mousePosition = Input.mousePosition;
         Vector3 objectPostion = Camera.main.ScreenToWorldPoint(mousePosition);
         curIngredient.transform.position = new Vector3(objectPostion.x, objectPostion.y, 1);


     }
     */

    public void OnPointerDown(PointerEventData eventData)
    {
        print("point");
        curIngredient = ingredientTransform.GetChild((int)ingredient).gameObject;
        curIngredient.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;
        Vector3 objectPostion = Camera.main.ScreenToWorldPoint(mousePosition);
        curIngredient.transform.position = new Vector3(objectPostion.x, objectPostion.y, 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 objectPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curIngredient.transform.position = new Vector3(objectPostion.x, objectPostion.y, 1);

    }
    /*
    private void OnMouseDrag()
    {
        Vector3 objectPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curIngredient.transform.position = new Vector3(objectPostion.x, objectPostion.y, 1);

    }
    */
    public void OnEndDrag(PointerEventData eventData)
    {
   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3 objectPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curIngredient.SetActive(false);

        if (Physics2D.Raycast(objectPostion, transform.forward, 10, 1 << LayerMask.NameToLayer("Tray")))
        {
            GameManager.instance.playerHamburger.StackIngredient(ingredient);
            AudioManager.instance.DropSound();

        }
    }

    /*
    private void OnMouseUp()
    {
        Vector3 objectPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curIngredient.SetActive(false);

        if (Physics2D.Raycast(objectPostion, transform.forward, 10, 1 << LayerMask.NameToLayer("Tray"))){
            GameManager.instance.playerHamburger.StackIngredient(ingredient);
            AudioManager.instance.DropSound();
           
        }
    }
    */

}

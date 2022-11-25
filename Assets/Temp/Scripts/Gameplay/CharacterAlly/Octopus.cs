using UnityEngine;

public class Octopus : Ally
{
    [Space] public int sushi = 1;

    protected override void OnActionDone()
    {
        base.OnActionDone();

        var foodObject = Pool<Food>.Get("sushi");
        
        foodObject.value = sushi;
        foodObject.transform.position = transform.position;
    }
}
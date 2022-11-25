using UnityEngine;

public class Pig : Ally
{
    [Space] public int food = 10;

    protected override void OnActionDone()
    {
        base.OnActionDone();

        var foodObject = Pool<Food>.Get("food");

        foodObject.value = this.food;
        foodObject.transform.position = transform.position;
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Ally/Frog", fileName = "frog")]
public class FrogConfig : CharacterConfig
{
    public FrogData data;

    public override CharacterData GetData()
    {
        return data;
    }
}
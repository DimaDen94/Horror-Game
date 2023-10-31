using UnityEngine;

public interface IGameFactory
{
    Hero CreateHero(Vector3 _heroStartPosition, Quaternion quaternion);
    GameObject CreateLight(Transform transform);
}
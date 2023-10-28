using UnityEngine;

public interface IImageLoader
{
    Sprite LoadFromResource(string spriteResourcePath);
}
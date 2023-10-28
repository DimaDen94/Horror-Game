using UnityEngine;

public class ImageLoader: IImageLoader
{
    public Sprite LoadFromResource(string spriteResourcePath) => Resources.Load<Sprite>(spriteResourcePath);
}
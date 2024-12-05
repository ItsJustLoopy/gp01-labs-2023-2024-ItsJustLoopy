using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class CustomTextRenderer
{
    private Dictionary<char, Texture2D> _letterTextures;

    public CustomTextRenderer(GraphicsDevice graphicsDevice)
    {
        _letterTextures = new Dictionary<char, Texture2D>();
        LoadTextures(graphicsDevice);
    }

    private void LoadTextures(GraphicsDevice graphicsDevice)
    {
        foreach (char c in "abcdefghijklmnopqrstuvwxyz0123456789-")
        {
            string texturePath = $"Content\\text\\{c}.png"; 
            Texture2D texture = Texture2D.FromFile(graphicsDevice, texturePath);
            _letterTextures[c] = texture;
        }
    }

    public void RenderText(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
    {
        float offsetX = 0;
        foreach (char c in text)
        {
            if (_letterTextures.TryGetValue(c, out Texture2D texture))
            {
                spriteBatch.Draw(texture, position + new Vector2(offsetX, 0), color);
                offsetX += texture.Width; 
            }
        }
    }
}

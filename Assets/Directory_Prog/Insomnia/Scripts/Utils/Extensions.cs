using UnityEngine;

namespace Insomnia {
    public static class TextureExtentions {
        public static Texture2D ToTexture2D(this Texture texture) {
            return Texture2D.CreateExternalTexture(
                texture.width,
                texture.height,
                TextureFormat.RGB24,
                false, false,
                texture.GetNativeTexturePtr());
        }
    }
}

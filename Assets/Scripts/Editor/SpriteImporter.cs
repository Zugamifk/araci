using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SpriteImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        var importer = assetImporter as TextureImporter;

        importer.spritePixelsPerUnit = 128;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        importer.filterMode = FilterMode.Point;
    }
}

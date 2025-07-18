import * as THREE from "three";
import terrainVS from "./shaders/terrain.vs";
import terrainFS from "./shaders/terrain.fs";
import { AssetManager, TextureAsset } from "./asset-manager";

export class TexturedTerrain extends THREE.Mesh {
  constructor(private assetManager: AssetManager) {
    const geometry = new THREE.PlaneGeometry(1, 1, 512, 512).rotateX(
      -Math.PI / 2
    );

    const heightMap = assetManager.textures.get(TextureAsset.TERRAIN_HEIGHT);
    const normalMap = assetManager.textures.get(TextureAsset.TERRAIN_NORMAL);
    if (!heightMap || !normalMap) throw new Error("Could not get maps");

    const material = new THREE.ShaderMaterial({
      glslVersion: THREE.GLSL3,
      vertexShader: terrainVS,
      fragmentShader: terrainFS,
      uniforms: {
        tHeightMap: { value: heightMap },
        tNormal: { value: normalMap },
      },
    });

    super(geometry, material);
  }
}

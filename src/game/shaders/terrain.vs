uniform sampler2D tHeightMap;

out vec2 vUv;

void main() {
  vUv = uv;

  float height = texture(tHeightMap, uv).x;
  vec3 transformed = position;
  transformed.y += height * 0.2;

  gl_Position = projectionMatrix * modelViewMatrix * vec4(transformed, 1.0);
}
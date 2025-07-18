precision highp float;
precision highp int;

layout(location = 0) out vec4 pc_fragColor;

uniform sampler2D tNormal;
uniform mat3 normalMatrix;

in vec2 vUv;

const vec3 SUN_DIRECTION = normalize(vec3(0.5, 0.5, 0.5));

void main() {
  vec3 normalTexel = texture(tNormal, vUv).xzy * 2.0 - 1.0; // -1 : 1
  vec3 normal = normalize(normalMatrix * normalTexel);
  vec3 sunDirection_view = (viewMatrix * vec4(SUN_DIRECTION, 0.0)).xyz;

  float dotP = dot(normal, sunDirection_view);

  pc_fragColor = vec4(vUv, 0.0, 1.0);
  pc_fragColor = vec4(normal, 1.0);
  pc_fragColor = vec4(vec3(dotP), 1.0);
}
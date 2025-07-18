precision highp float;
precision highp int;

layout(location = 0) out vec4 pc_fragColor;

uniform sampler2D tNormal;
uniform mat3 normalMatrix;

in vec2 vUv;

const vec3 SUN_DIRECTION = normalize(vec3(0.5, 0.5, 0.5));
const vec3 UP = vec3(0.0, 1.0, 0.0);

const vec3 grassColor = vec3(0.13, 0.46, 0.13);
const vec3 rockColor = vec3(0.12, 0.11, 0.1);

void main() {
  vec3 normal = normalize(texture(tNormal, vUv).xzy * 2.0 - 1.0); // -1 : 1
  vec3 normal_V = normalize(normalMatrix * normal);
  vec3 sunDirection_view = (viewMatrix * vec4(SUN_DIRECTION, 0.0)).xyz;

  float dotP = dot(normal_V, sunDirection_view);

  float dotPUp = clamp(dot(normal, UP), 0.0, 1.0);
  float mask = pow(dotPUp, 8.0);

  vec4 color = vec4(grassColor, 1.0);
  color.rgb = mix(rockColor, color.rgb, mask);

  pc_fragColor = vec4(vUv, 0.0, 1.0);
  pc_fragColor = vec4(normal_V, 1.0);
  pc_fragColor = vec4(vec3(dotP), 1.0);
}
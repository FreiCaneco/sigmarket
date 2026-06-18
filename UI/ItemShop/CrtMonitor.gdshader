shader_type canvas_item;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_nearest;
uniform float SCREEN_HEIGHT = 320.0;

void fragment() {
	vec2 centered_uv = SCREEN_UV - 0.5;
	float pixel_distance_to_center = distance(centered_uv,vec2(0,0));
	
	vec2 distorion = centered_uv + (centered_uv * pixel_distance_to_center * 0.03);
	vec2 uv_distortion = distorion + 0.5;
	
	vec4 tex = texture(SCREEN_TEXTURE, uv_distortion);
	
	float y_multiplied = SCREEN_UV.y * SCREEN_HEIGHT;
	float scanline = floor(mod(y_multiplied,2.0));
	float glowing = mix(0.8,1,scanline);
		
	float vignette_glowing = smoothstep(0.7,0.0,pixel_distance_to_center * pixel_distance_to_center);
		
	COLOR.rgb = tex.rgb * glowing * vignette_glowing;
}

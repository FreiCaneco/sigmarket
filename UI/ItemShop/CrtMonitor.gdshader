shader_type canvas_item;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_nearest;

void fragment() {
	vec4 tex = texture(SCREEN_TEXTURE, SCREEN_UV);
	vec4 final_color = tex * 0.5;
	COLOR = final_color;
}

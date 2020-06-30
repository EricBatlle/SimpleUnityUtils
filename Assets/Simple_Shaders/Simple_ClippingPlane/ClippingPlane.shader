Shader "Masks/ClippingPlane" 
{
	Properties
	{
	  _MainTex("Texture", 2D) = "white" {}
	  _BumpMap("Bumpmap", 2D) = "bump" {}
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Cull Off
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 worldPos;
		};
		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _Plane;

		void surf(Input IN, inout SurfaceOutput o) 
		{
			//Get world position 
			//_MyVector = mul(unity_WorldToObject, IN.worldPos);
			float distance = dot(IN.worldPos, _Plane.xyz);
			distance = distance + _Plane.w;
			//if world position.y of the object-pixel is > 0, will render
			clip(-distance);
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}
	//Use Fallback Off to avoid shadows
	Fallback "Diffuse"
}
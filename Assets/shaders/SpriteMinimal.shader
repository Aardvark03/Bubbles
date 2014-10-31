Shader "Sprites/glow" {
    // userdefined values, can be set in the unity editor
    Properties {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "Queue" = "Transparent"
        }
        Blend One OneMinusSrcAlpha

        Pass {
            CGPROGRAM

            #pragma vertex vertex
            #pragma fragment fragment
            
            // values defined in properties
            uniform sampler2D _MainTex;

            // define input and output structs for vertex function
            struct vertexInput { 
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct vertexOutput {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
                half2 texcoord : TEXCOORD0;
            };

            // vertex shader function
            vertexOutput vertex(vertexInput vIn) {
                vertexOutput vOut;
           
                vOut.pos = mul(UNITY_MATRIX_MVP, vIn.vertex);
                vOut.texcoord = vIn.texcoord;
                vOut.color = vIn.color;
            
                return vOut;
            }

            // fragment shader function
            float4 fragment(vertexOutput vOut) : SV_Target {
                fixed4 color = tex2D(_MainTex, vOut.texcoord) * vOut.color;
                color.rgb *= color.a;
                
                return color;
            }

            ENDCG
        }
    }

    //Fallback "Sprites/Default";
}

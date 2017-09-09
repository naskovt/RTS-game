
/*** Generated through Lumonix shaderFX  by: id in 3dsmax at: 28.01.2009 13:58:37  ***/ 

// This FX shader was built to support 3ds Max's standard shader compiler. 


texture TextureMap_7602
<
	string Name = "N_cobblestones_ati.dds";
	string UIName = "Parallax";
	string ResourceType = "2D";
>;
 
sampler2D TextureMap_7602Sampler = sampler_state
{
	Texture = <TextureMap_7602>;
	MinFilter = ANISOTROPIC;
	MagFilter = ANISOTROPIC;
	MipFilter = ANISOTROPIC;
};
 
float OffsetBias
<
	string UIType = "FloatSpinner";
	float UIMin = 0.0;
	float UIMax = 0.2;
	float UIStep = 0.001;
	string UIName = "Offset Bias";
> = 0.003;
 
int texcoord0 : Texcoord 
<
	int Texcoord = 0;
	int MapChannel = 1;
	string UIType = "None"; 
>;

texture TextureMap_2501
<
	string Name = "D_cobblestones.dds";
	string UIName = "Diffuse";
	string ResourceType = "2D";
>;
 
sampler2D TextureMap_2501Sampler = sampler_state
{
	Texture = <TextureMap_2501>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
};
 
texture TextureMap_4922 : SpecularMap
<
	string Name = "";
	string UIName = "Specular";
	string ResourceType = "2D";
>;
 
sampler2D TextureMap_4922Sampler = sampler_state
{
	Texture = <TextureMap_4922>;
	MinFilter = ANISOTROPIC;
	MagFilter = ANISOTROPIC;
	MipFilter = ANISOTROPIC;
};
 
float4 UIColor_5698 : Specular
<
	string UIName = "Specular Intensity";
	string UIType = "ColorSwatch";
> = {0.345098f, 0.345098f, 0.345098f, 1.0f };
 
texture TextureMap_1787 : NormalMap
<
	string Name = "N_cobblestones_ati.dds";
	string UIName = "Normal";
	string ResourceType = "2D";
>;
 
sampler2D TextureMap_1787Sampler = sampler_state
{
	Texture = <TextureMap_1787>;
	MinFilter = ANISOTROPIC;
	MagFilter = ANISOTROPIC;
	MipFilter = ANISOTROPIC;
};
 


/************** light info **************/ 

float3 light1Pos : POSITION 
< 
	string UIName = "Light 1 Position"; 
	string Object = "PointLight"; 
	string Space = "World"; 
		int refID = 1; 
> = {100.0f, 100.0f, 100.0f}; 

//---------------------------------- 

float4 light1Color : LIGHTCOLOR 
< 
		int LightRef = 1; 
		string UIType = "None"; 
> = { 1.0f, 1.0f, 1.0f, 1.0f}; 

//---------------------------------- 

float4x4 wvp : WorldViewProjection < string UIType = "None"; >;  
float4x4 worldI : WorldInverse < string UIType = "None"; >;  
float4x4 worldIT : WorldInverseTranspose < string UIType = "None"; >;  
float4x4 viewInv : ViewInverse < string UIType = "None"; >;  
float4x4 world : World < string UIType = "None"; >;  

// input from application 
	struct a2v { 
	float4 position		: POSITION; 
	float3 tangent		: TANGENT; 
	float3 binormal		: BINORMAL; 
	float3 normal		: NORMAL; 

	float2 texCoord		: TEXCOORD0; 

}; 

// output to fragment program 
struct v2f { 
        float4 position    		: POSITION; 
        float3 lightVec    		: TEXCOORD0; 
        float3 eyeVec	    	: TEXCOORD1; 

		float2 texCoord			: TEXCOORD2; 

}; 

//Diffuse and Specular Pass Vertex Shader
v2f v(a2v In, uniform float3 lightPosition) 
{ 
	v2f Out = (v2f)0; 
    Out.position = mul(In.position, wvp);				//transform vert position to homogeneous clip space 
    //this code was added by the standard material 
    float3x3 objTangentXf;								//build object to tangent space transform matrix 
    	objTangentXf[0] = In.binormal; 
    	objTangentXf[1] = -In.tangent; 
    	objTangentXf[2] = In.normal; 
    //this code was added by the standard material 
    float3 wsLPos = mul(In.position, world).xyz;			//put the vert position in world space 
    float3 wsLVec = lightPosition - wsLPos;    //cast a ray to the light 
    float3 osLVec = mul(wsLVec, worldI).xyz;  //transform the light vector to object space 
    Out.lightVec = mul(objTangentXf, osLVec);			//tangent space light vector passed out 
    //this code was added by the standard material 
    float4 osIPos = mul(viewInv[3], worldI);			//put world space eye position in object space 
    float3 osIVec = osIPos.xyz - In.position.xyz;		//object space eye vector 
    Out.eyeVec = mul(objTangentXf, osIVec);				//tangent space eye vector passed out 

	//this code was added by the texture map Node 
    Out.texCoord = In.texCoord;						//pass through texture coordinates from channel 1 

    return Out; 
} 

//Diffuse and Specular Pass Pixel Shader
float4 f(v2f In, uniform float4 lightColor) : COLOR 
{ 
	float3 ret = float3(0,0,0); 
	float3 V = normalize(In.eyeVec);		//creating the eye vector  
	float3 L = normalize(In.lightVec);		//creating the light vector  

	float4 TextureMap_7602 = tex2D(TextureMap_7602Sampler, In.texCoord.xy);
	float Two = 2.0; 
	float TimesTwo = TextureMap_7602.a * Two;
	float One = 1.0; 
	float MinusOne = TimesTwo - One;
    float3 INEyeVec_851 = In.eyeVec.xyz;		//unnormalized eye vector 
	INEyeVec_851 = normalize(INEyeVec_851); 
    //invert eye vector Y 
    	INEyeVec_851.y = -INEyeVec_851.y; 
	float2 HeightTimesV = MinusOne * INEyeVec_851;
	float2 OffsetResult = HeightTimesV * OffsetBias;
	float2 OriginalTexCoords = In.texCoord.xy; 
	float2 OffsetTexCoords = OffsetResult + OriginalTexCoords;
	float4 TextureMap_2501 = tex2D(TextureMap_2501Sampler, OffsetTexCoords.xy);
	float3 input2 = TextureMap_2501.rgb; 


	float4 TextureMap_4922 = tex2D(TextureMap_4922Sampler, In.texCoord.xy);
	float3 MathOperator_2798 = TextureMap_4922.rgb * UIColor_5698;
	float3 input3 = MathOperator_2798; 


	float4 TextureMap_1787 = tex2D(TextureMap_1787Sampler, OffsetTexCoords.xy);
	TextureMap_1787.xyz = TextureMap_1787.xyz * 2 - 1;		//expand to -1 to 1 range 
	TextureMap_1787.y = -TextureMap_1787.y; 		// green channel flipped 
	TextureMap_1787.rgb = normalize(TextureMap_1787.rgb); 		//normalized the normal vector 
	float3 input8 = TextureMap_1787.rgb; 

	float3 N = input8;						//using the Normal socket  
	float3 diffuseColor = input2;			//using the Diffuse Color socket  
	float diffuse = saturate(dot(N,L));		//calculate the diffuse  
	diffuseColor *= diffuse;				//the resulting diffuse color  
	ret += diffuseColor;					//add diffuse light to final color  
	float3 specularColor = input3;			//using the Specular Color socket 
	float glossiness = 20;					//the Glossiness socket was empty - using default value 
	float3 H = normalize(L + V);			//Compute the half angle  
	float NdotH = saturate(dot(N,H));		//Compute NdotH  
	specularColor *= pow(NdotH, glossiness);//Raise to glossiness power and compute final specular color  
	ret += specularColor;					//add specular light to final color  
	ret *= lightColor;						//multiply by the color of the light 
	float4 done = float4(ret, 1); 
	return done; 
} 

technique Complete  
{  
	pass light1  
    {		 
	VertexShader = compile vs_1_1 v(light1Pos); 
	ZEnable = true; 
	CullMode = cw; 
	ZWriteEnable = true; 
	DestBlend = One; 
	AlphaBlendEnable = false; 
	PixelShader = compile ps_2_0 f(light1Color); 
	}  

}  


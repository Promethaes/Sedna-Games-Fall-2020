
void Edge_float(float3 normal,float3 view, float3 colour,float edgeColour,float edgeStrength, out float3 Out){

    if(dot(normal,view) <= edgeStrength){

        Out = edgeColour;
    }
    else{
        Out = colour;
    }
}
function generateEffortImage(ConjuntoValor, ConjuntoQtd, precoUnitario) {
    // L(V) = Q*(V - C)
    var conjuntoLucro = [];
   
    for (var i = 0; i < ConjuntoQtd.length; i++) {
        var lucro = ConjuntoQtd[i] * (ConjuntoValor[i] - precoUnitario);
        conjuntoLucro.push(lucro);
       
    }

    return conjuntoLucro;

}

function getApicePos() {
    var maior = ImageEffort[0];
    var apicePos = 0;
    for (var i = 0; i < ImageEffort.length; i++) {
        if (ImageEffort[i] > maior) {
            maior = ImageEffort[i];
            apicePos = i;
        }
    }

    return apicePos;
}
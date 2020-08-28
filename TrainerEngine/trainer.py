import pandas as pd
import numpy as np
import pickle
import variables
from sklearn.preprocessing import StandardScaler
from sklearn.neural_network import MLPRegressor

def fit(AppId,AppFilter,RegressorId):
    AppBasePath = variables.TransformedDataPath + '\\' + AppId + '\\main.csv'
    base = pd.read_csv(AppBasePath,sep = ';')
    #aplicar filtro
    
    for key in AppFilter:
        if(AppFilter[key] is not None):
            values = AppFilter[key]
            exp = ''
            exp += key
            exp += ' == '
            exp += '(\''
            sep = '\',\''
            exp += sep.join(values)
            exp += '\')'
            base = base.query(exp)
            print(exp)
        
        
        
    
    X = base.iloc[:,0:1].values
    Y = base.iloc[:,8:9].values
    #ajuste de escala
    scaler_x = StandardScaler()
    X = scaler_x.fit_transform(X)
    scaler_y = StandardScaler()
    Y = scaler_y.fit_transform(Y)

    print(X)
    print(Y)
    
    #treinamento
    regressor = MLPRegressor()
    regressor.fit(X,Y)
    
    #salvar
    
    RegressorPath = variables.RegressorsPath + RegressorId + '.sav'
    pickle.dump(regressor,open(RegressorPath,'wb'))
    ScalerXPath = variables.ScalersPath + RegressorId + '-x.sav'
    ScalerYPath = variables.ScalersPath + RegressorId + '-y.sav'
    pickle.dump(scaler_x,open(ScalerXPath,'wb'))
    pickle.dump(scaler_y,open(ScalerYPath,'wb'))
    
    print(regressor.score(X,Y))
    prev = scaler_y.inverse_transform(regressor.predict(scaler_x.transform([[5.0],[5.5]])))
    print(prev)


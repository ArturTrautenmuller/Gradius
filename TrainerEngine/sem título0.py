# -*- coding: utf-8 -*-
"""
Created on Sat Aug 22 18:17:06 2020

@author: artur
"""

import pandas as pd
import numpy as np
import variables
from sklearn.preprocessing import StandardScaler
from sklearn.neural_network import MLPRegressor

AppId = '123456'
RegressorId = 'teste'

AppBasePath = variables.TransformedDataPath + '\\' + AppId + '\\main.csv'
base = pd.read_csv(AppBasePath,sep = ';')
    #aplicar filtro
    
X = base.iloc[:,0:1].values
Y = base.iloc[:,8:9].values
scaler_x = StandardScaler()
X = scaler_x.fit_transform(X)
scaler_y = StandardScaler()
Y = scaler_y.fit_transform(Y)

print(X)
print(Y)
    
regressor = MLPRegressor()
regressor.fit(X,Y)
    
import pickle
RegressorPath = variables.RegressorsPath + RegressorId + '.sav'
pickle.dump(regressor,open(RegressorPath,'wb'))
    
print(regressor.score(X,Y))
prev = scaler_y.inverse_transform(regressor.predict(scaler_x.transform([[5.0],[5.5]])))
print(prev)
# -*- coding: utf-8 -*-
"""
Created on Sat Aug 22 18:55:55 2020

@author: artur
"""
import variables
import pickle
import numpy as np
import json
from sklearn.preprocessing import StandardScaler
from sklearn.neural_network import MLPRegressor

def predict(RegressorId,Min,Max,Step):
    Domain = []
    for value in np.arange(float(Min),float(Max),float(Step)):
        Domain.append([round(value,2)])

    resultPrev = []
    result = {}
    RegressorPath = variables.RegressorsPath + RegressorId + '.sav'
    ScalerXPath = variables.ScalersPath + RegressorId + '-x.sav'
    ScalerYPath = variables.ScalersPath + RegressorId + '-y.sav'
    regressor = pickle.load(open(RegressorPath,'rb'))
    scaler_x =  pickle.load(open(ScalerXPath,'rb'))
    scaler_y =  pickle.load(open(ScalerYPath,'rb'))
    prev = scaler_y.inverse_transform(regressor.predict(scaler_x.transform(Domain)))
    for val in prev:
        resultPrev.append(val)
	
    result["prev"] = resultPrev
    print(json.dumps(result))
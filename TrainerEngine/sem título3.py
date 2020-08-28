# -*- coding: utf-8 -*-
"""
Created on Sat Aug 22 19:08:55 2020

@author: artur
"""
import json
import pandas as pd
import numpy as np

Domain = []
Min = '4.25'
Max = '5.60'
Step = '0.01'
for value in np.arange(float(Min),float(Max),float(Step)):
    print(value)

base = pd.read_csv('C:\\Gradius\\Data\\TransformedData\\123456\\main.csv',sep = ';')
base2020 = base.query("Valor == ('4.25')")

string = '{\"Ano\":[\"2020\"],\"Mes\":[\"1\",\"2\"]}'
mydic = json.loads(string)
for key in mydic:
    values = mydic[key]
    
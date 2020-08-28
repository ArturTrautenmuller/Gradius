# -*- coding: utf-8 -*-
"""
Created on Sat Aug 22 18:56:56 2020

@author: artur
"""
import sys
import predictor




AppId = str(sys.argv[1])
RegressorId = str(sys.argv[2])
MinValue = str(sys.argv[3])
MaxValue = str(sys.argv[4])
Step = str(sys.argv[5])

predictor.predict(RegressorId,MinValue,MaxValue,Step)
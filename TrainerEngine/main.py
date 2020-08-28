import sys
import json
import trainer

print('App: '+str(sys.argv[1]))
print('Filter: '+str(sys.argv[3]))
print('treinando')
AppId = str(sys.argv[1])
print('treinando')
RegressorId = str(sys.argv[2])
print('treinando')
AppFilter = json.loads(str(sys.argv[3]))

print('treinando')
trainer.fit(AppId,AppFilter,RegressorId)

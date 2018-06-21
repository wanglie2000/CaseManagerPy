

class Pars():
    '''
    classdocs
    '''

    map = {}

    def __init__(self):
        self.map["LASTSTEP"] = True
        self.map["BROWSER"] = "CHROME"
        
    def clearPars(self):
        lastStep = self.map["LASTSTEP"]
        browser = self.map["BROWSER"]
        self.map.clear()
        self.map["LASTSTEP"] = lastStep
        self.map["BROWSER"] = browser
    
    def get(self, key):

        if key in self.map.keys():
            return self.map[key]
        else:
            return None

    def set(self,key,value):
        self.map[key]=value
    
    def put(self, key, ValueOrKey):
        value = ""
        if ValueOrKey in self.map:
            value = self.map[ ValueOrKey ]
        else:
            value = ValueOrKey
        self.map[ key ] = value
             
    def swap(self, key1, key2):
        value1 = self.map[key1]
        value2 = self.map[key2]
        self.map[key1] = value2
        self.map[key2] = value1

    def addDic(self,dic):
        for k,v in dic.items():
            self.map[k] = v

        
if __name__ == "__main__":
    pars = Pars()
    dic={"a":"a1","b":"b1"}

    pars.addDic(dic)

    print(pars.map)


    
        
        
        
        
        
        

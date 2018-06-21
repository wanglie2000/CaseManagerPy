#encoding=utf-8

from PIL import ImageGrab
from aip import AipOcr
import pyautogui

import re

class AITool:
    client=None
    APP_ID = '11185660'
    API_KEY = 'bqnA6lGVQ4s0KG4PuYk1Hdv5'
    SECRET_KEY = 'WmzLYeVZjSn3Vx2itEl2DKkjwDyH487B'

    def __init__(self):
        self.client =  AipOcr(self.APP_ID,self.API_KEY,self.SECRET_KEY)


    def click(self,text,bbox=None,index=0):
        location = self.getLocation(text,bbox,index)
        left = location["left"]
        top = location["top"]
        width = location["width"]
        height = location["height"]

        clickLeft = int(left+ width/2)
        clickTop = int( top + height/2 )

        pyautogui.click(clickLeft,clickTop )

    def relative2Screen(self , location ,bbox):

        print(location["left"])

        print(bbox[0])

        screenLocation={}

        x = location["left"] + bbox[0]

        screenLocation["left"]=location["left"] + bbox[0]
        screenLocation["top"] = location["top"] + bbox[1]
        screenLocation["width"] = location["width"]
        screenLocation["height"] = location["height"]

        return screenLocation




    def getLocation(self,text,bbox=None,index=0):
        ImageGrab.grab(bbox).save("d:/屏幕.png")

        img=open("d:/屏幕.png","rb").read()

        message= self.client.general(img)

        print(message)

        words_result = message["words_result"]

        locations=[]

        for result in words_result:
            words=result["words"]
            if words==text:
                locations.append(result["location"])

        print( locations )

        if None==bbox:
            return locations[index]
        else:
            return self.relative2Screen(locations[index],bbox)


        # ImageGrab.grab(bbox).save(picPath)





if __name__=="__main__":
    aiTool =   AITool()

    # aiTool.click("团队管理",(8,305,214,523),0)

    aiTool.click("企业网站",(0,0,500,500), 0)









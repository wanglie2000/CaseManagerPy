import pyautogui

import time
from PIL import ImageGrab
from PIL import Image
import os


class KeyboardAndMouse:


    def keyboardClick(self,key):


        if "ENTER"== key.upper() or "回车"== key.upper():
            pyautogui.typewrite(["return"])

        elif "TAB"==key.upper():
            pyautogui.typewrite(["tab"])
        else:
            pyautogui.typewrite(key)

    def altAndKey(self,key):
         pyautogui.keyDown('altleft');
         pyautogui.press(key);
         pyautogui.keyUp('altleft')





    def mouseClickLeft(self,x,y):
        pyautogui.click(x,y)


    def mouseScroll(self,value):
        pyautogui.scroll(value)



    def mouseDraw(self , locates):
        tmps = locates.split(",")
        l = len(tmps)

        if l>=2 and l%2==0  :
            tmps0 = tmps[0::2]
            tmps1 = tmps[1::2]

            listPoint = []

            for i in range( int(l/2)):
                x=tmps0[i]
                y=tmps1[i]

                if i==0:
                    pyautogui.mouseDown( int(x),int(y) , "left", duration=0.25)
                elif i==l/2-1:
                    pyautogui.mouseUp( int(x),int(y), "left", duration=0.25)
                else:
                    pyautogui.moveTo( int(x),int(y),duration=0.25)

    def getFileFullPath(self,filePath):
        pwd = os.getcwd()
        tmp = pwd.split("src")[0]
        if (filePath[1:2] != ':'):
            # 是相对路径 要到files下面去找

            filesDir = os.path.abspath(tmp + os.path.sep + "..") + "\\files"
            picPath = filePath.replace("/", "\\")
            if (not filePath.startswith("\\")):
                filePath = "\\" + filePath
                filePath = filesDir + filePath

        return filePath




    def getPicLocalOnScreen(self,picPath):
        picPath = self.getFileFullPath(picPath)

        locals = pyautogui.locateOnScreen(picPath)

        x = locals[0]
        y = locals[1]
        w = locals[2]
        h = locals[3]

        x1=x+w
        y1=y+h

        print((x,y,x1,y1))

        return (x,y,x1,y1)

    def saveScreenshotByLocation(self,x0,y0,x1,y1,picPath):
        picPath = self.getFileFullPath(picPath)

        bbox = (x0, y0, x1, y1)

        ImageGrab.grab(bbox).save(picPath)








    def mouseClickByPic(self,picPath):
        pwd = os.getcwd()
        tmp = pwd.split("src")[0]


        if (picPath[1:2] != ':'):
            # 是相对路径 要到files下面去找

            filesDir = os.path.abspath(tmp + os.path.sep + "..") + "\\files"
            picPath = picPath.replace("/", "\\")
            if (not picPath.startswith("\\")):
                picPath = "\\" + picPath
                picPath = filesDir + picPath

        print(picPath)
        locals =  pyautogui.locateOnScreen(picPath)

        print(locals)

        x, y = pyautogui.center(locals)

        pyautogui.click(x,y)


if __name__=="__main__":
    time.sleep(6)
    KeyboardAndMouse().altAndKey("S")























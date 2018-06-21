#coding=utf-8

import datetime
import time
from selenium.webdriver.support.ui import Select
from selenium.webdriver.common.action_chains import ActionChains
import os
import pyautogui
from PIL import ImageGrab




class EleDescription(object):
    driver = None
    findType = None
    findValue = None
    eleName = None
    logger = None



    def __init__(self, driver, findType, findValue, eleName, logger):
        self.driver = driver
        self.findType = findType
        self.findValue = findValue
        self.eleName = eleName
        self.logger = logger


    def saveElementImg(self ,imgPath):
        lacation=self.getElementScreenLacationAndSize();

        left0= lacation[0]+2
        top0=lacation[1]+2
        left1=int(lacation[0]+lacation[2]-2)
        top1= int(lacation[1]+lacation[3]-2)

        im = ImageGrab.grab(( left0,top0,left1,top1 ))





        im.save(imgPath,"png")

    def getElementScreenLacationAndSize(self):
        ele = self.find(10)
        location = ele.location
        left = location['x']
        top = location['y']
        size = ele.size
        width = size['width']
        height = size['height']

        print(left)
        print(top)
        print(width)
        print(height)

        js01 = "if(null!=document.getElementById('elementmark'))\r\n"
        js02 = "{\r\n"

        js03 = "document.body.removeChild(document.getElementById('elementmark'));\r\n"
        js04 = "}\r\n"
        js05 = "var div= document.createElement('div');\r\n"
        js06 = "document.body.appendChild(div);\r\n"
        js07 = "div.id='elementmark';\r\n"
        js08 = "div.setAttribute('style','z-index:999; position: absolute; width: 4px; height: 4px; top: " + str(
            top) + "px; left: " + str(left) + "px');\r\n"

        js09 = "div.innerHTML=\"<div style='background-color: #FFFFFF; position: absolute; width: 1px; height: 1px; top: 0px; left: 0px;'></div><div style='background-color: #FFFF00; position: absolute; width: 1px; height: 1px; top: 0px; left: 1px;'></div><div style='background-color: #00FFFF; position: absolute; width: 1px; height: 1px; top: 0px; left: 2px;'></div><div style='background-color: #00FF00; position: absolute; width: 1px; height: 1px; top: 0px; left: 3px;'></div><div style='background-color: #FF00FF; position: absolute; width: 1px; height: 1px; top: 1px; left: 0px;'></div><div style='background-color: #FF0000; position: absolute; width: 1px; height: 1px; top: 1px; left: 1px;'></div><div style='background-color: #0000FF; position: absolute; width: 1px; height: 1px; top: 1px; left: 2px;'></div><div style='background-color: #000000; position: absolute; width: 1px; height: 1px; top: 1px; left: 3px;'></div><div style='background-color: #FFFFFF; position: absolute; width: 1px; height: 1px; top: 2px; left: 0px;'></div><div style='background-color: #FFFF00; position: absolute; width: 1px; height: 1px; top: 2px; left: 1px;'></div><div style='background-color: #00FFFF; position: absolute; width: 1px; height: 1px; top: 2px; left: 2px;'></div><div style='background-color: #00FF00; position: absolute; width: 1px; height: 1px; top: 2px; left: 3px;'></div><div style='background-color: #FF00FF; position: absolute; width: 1px; height: 1px; top: 3px; left: 0px;'></div><div style='background-color: #FF0000; position: absolute; width: 1px; height: 1px; top: 3px; left: 1px;'></div><div style='background-color: #0000FF; position: absolute; width: 1px; height: 1px; top: 3px; left: 2px;'></div><div style='background-color: #000000; position: absolute; width: 1px; height: 1px; top: 3px; left: 3px;'></div>\";";

        jsStr = js01 + js02 + js03 + js04 + js05 + js06 + js07 + js08 + js09

        self.driver.execute_script(jsStr)

        pwd = os.getcwd()
        tmp = pwd.split("src")[0]
        tmp = os.path.abspath(tmp + os.path.sep + "..") + "\\files"
        picPath = tmp + "\\mark.png"

        picLocate = pyautogui.locateOnScreen(picPath)

        self.driver.execute_script(js03)
        return ( picLocate[0],picLocate[1], width,height )

    def getElementScreenLacation(self):
        ele = self.find(10)
        location = ele.location
        left = location['x']
        top = location['y']
        size = ele.size
        width=size['width']
        height=size['height']

        print(left)
        print(top)
        print(width)
        print(height)

        js01 = "if(null!=document.getElementById('elementmark'))\r\n"
        js02 = "{\r\n"

        js03 = "document.body.removeChild(document.getElementById('elementmark'));\r\n"
        js04 = "}\r\n"
        js05 = "var div= document.createElement('div');\r\n"
        js06 = "document.body.appendChild(div);\r\n"
        js07 = "div.id='elementmark';\r\n"
        js08 = "div.setAttribute('style','z-index:999; position: absolute; width: 4px; height: 4px; top: " + str(top) + "px; left: " + str(left) + "px');\r\n"

        js09 = "div.innerHTML=\"<div style='background-color: #FFFFFF; position: absolute; width: 1px; height: 1px; top: 0px; left: 0px;'></div><div style='background-color: #FFFF00; position: absolute; width: 1px; height: 1px; top: 0px; left: 1px;'></div><div style='background-color: #00FFFF; position: absolute; width: 1px; height: 1px; top: 0px; left: 2px;'></div><div style='background-color: #00FF00; position: absolute; width: 1px; height: 1px; top: 0px; left: 3px;'></div><div style='background-color: #FF00FF; position: absolute; width: 1px; height: 1px; top: 1px; left: 0px;'></div><div style='background-color: #FF0000; position: absolute; width: 1px; height: 1px; top: 1px; left: 1px;'></div><div style='background-color: #0000FF; position: absolute; width: 1px; height: 1px; top: 1px; left: 2px;'></div><div style='background-color: #000000; position: absolute; width: 1px; height: 1px; top: 1px; left: 3px;'></div><div style='background-color: #FFFFFF; position: absolute; width: 1px; height: 1px; top: 2px; left: 0px;'></div><div style='background-color: #FFFF00; position: absolute; width: 1px; height: 1px; top: 2px; left: 1px;'></div><div style='background-color: #00FFFF; position: absolute; width: 1px; height: 1px; top: 2px; left: 2px;'></div><div style='background-color: #00FF00; position: absolute; width: 1px; height: 1px; top: 2px; left: 3px;'></div><div style='background-color: #FF00FF; position: absolute; width: 1px; height: 1px; top: 3px; left: 0px;'></div><div style='background-color: #FF0000; position: absolute; width: 1px; height: 1px; top: 3px; left: 1px;'></div><div style='background-color: #0000FF; position: absolute; width: 1px; height: 1px; top: 3px; left: 2px;'></div><div style='background-color: #000000; position: absolute; width: 1px; height: 1px; top: 3px; left: 3px;'></div>\";";

        jsStr = js01+js02+js03+js04+js05+js06+js07+js08+js09

        self.driver.execute_script(jsStr)

        pwd = os.getcwd()
        tmp = pwd.split("src")[0]
        tmp = os.path.abspath(tmp + os.path.sep + "..") + "\\files"
        picPath = tmp+"\\mark.png"

        picLocate = pyautogui.locateOnScreen(picPath)
        picLeft=picLocate[0]
        picTop=picLocate[1]

        eleLocate=( int(picLeft+width/2)  , int(picTop +height/2) )

        self.driver.execute_script(js03)

        return eleLocate


    def scrollIntoView(self):
        ele = self.find(10)
        self.driver.execute_script( "arguments[0].scrollIntoView();",ele )



    def getIndex(self):
        ele = self.find(10)
        if None!= ele:
            brothers = ele.find_elements_by_xpath("./../*")
             
            index=0
            for brother in brothers:
                if brother==ele:
                    return index
                index = index + 1
            return -1    
        return -1

    #等待消失
    def waitDisappear(self, timeout=10):
        startTime = datetime.datetime.now()
        if self.findType.lower() == "id":
            while len(self.driver.find_elements_by_id(self.findValue)) > 0 and (
                    datetime.datetime.now() - startTime).seconds < timeout:
                time.sleep(1)
            elelist = self.driver.find_elements_by_id(self.findValue)
            if len(elelist) == 0:
                self.logger.appendContent("根据id[" + self.findValue + "]等待到控件[" + self.eleName + "]消失");
            else:
                self.logger.appendContent("根据id[" + self.findValue + "]没有等待到控件[" + self.eleName + "]消失");
                raise Exception("根据id[" + self.findValue + "]没有等待到控件[" + self.eleName + "]消失")
        elif self.findType.lower() == "xpath":
            while len(self.driver.find_elements_by_xpath(self.findValue)) > 0 and (
                    datetime.datetime.now() - startTime).seconds < timeout:
                time.sleep(1)

            elelist = self.driver.find_elements_by_xpath(self.findValue)
            if len(elelist) == 0:
                self.logger.appendContent("根据xpath[" + self.findValue + "]等待到控件[" + self.eleName + "]消失");
            else:
                self.logger.appendContent("根据xpath[" + self.findValue + "]没有等待到控件[" + self.eleName + "]消失");
                raise Exception("根据xpath[" + self.findValue + "]没有等待到控件[" + self.eleName + "]消失")

    def isExist(self):
        if self.findType.lower() == "id":
            l = len(self.driver.find_elements_by_id(self.findValue))
            return l>0
        else:
            l = len( self.driver.find_elements_by_xpath(self.findValue ))
            return l>0

               
    def find(self, timeout=10):
        startTime = datetime.datetime.now()
        if self.findType.lower() == "id":
            while  len(self.driver.find_elements_by_id(self.findValue)) == 0 and  (datetime.datetime.now() - startTime).seconds < timeout :
                time.sleep(1)
            elelist = self.driver.find_elements_by_id(self.findValue)
            if len(elelist) > 0:
                self.logger.appendContent("根据id[" + self.findValue + "]找到控件[" + self.eleName + "]");
                self.driver.execute_script("arguments[0].style.border='2px solid red'", elelist[0])
                return elelist[0]
            else:
                self.logger.appendContent("根据id[" + self.findValue + "]没有找到控件[" + self.eleName + "]");
                raise Exception("根据id[" + self.findValue + "]没有找到控件[" + self.eleName + "]")
        elif self.findType.lower() == "xpath":
            while len( self.driver.find_elements_by_xpath(self.findValue )  )==0 and  (datetime.datetime.now() - startTime).seconds < timeout:
                time.sleep(1)
                 
            elelist = self.driver.find_elements_by_xpath(self.findValue)
            if len( elelist)>0:
                self.logger.appendContent("根据xpath[" + self.findValue + "]找到控件[" + self.eleName + "]");
                self.driver.execute_script("arguments[0].style.border='2px solid red'", elelist[0])
                return elelist[0]
            else:
                self.logger.appendContent("根据xpath[" + self.findValue + "]没有找到控件[" + self.eleName + "]");
                raise Exception("根据xpath[" + self.findValue + "]没有找到控件[" + self.eleName + "]")



    def clickByLocate(self):
        locate=self.getElementScreenLacation()
        pyautogui.click(x=locate[0],y=locate[1])

 
    def click(self):
        ele = self.find(10)
        ele.click()
        self.logger.appendContent( "点击控件[" + self.eleName + "]")

    def moveTo(self):
        ele = self.find(10)
        ActionChains(self.driver).move_to_element(ele).perform()
        self.logger.appendContent("鼠标移动到控件[" + self.eleName + "]")

    def doubleClick(self):
        ele = self.find(10)
        ActionChains(self.driver).double_click(ele).perform()
        self.logger.appendContent("鼠标双击控件[" + self.eleName + "]")

    def rightClick(self):
        ele = self.find(10)
        ActionChains(self.driver).context_click(ele).perform()
        self.logger.appendContent("鼠标右键单击控件[" + self.eleName + "]")



    def drag(self, targetEle ,targetEleName ):
        ele = self.find(10)
        ActionChains(self.driver).drag_and_drop(ele,targetEle).perform()
        self.logger.appendContent("鼠标拖拽控件[" + self.eleName + "]到控件["+ targetEleName + "]" )




                
    def text(self):
        ele = self.find(10)
        text = ele.text
        self.logger.appendContent("得到控件[" + self.eleName + "]的文本为["+ text +"]")
        return text

            
    def input(self,text):
        ele = self.find(10)
        ele.clear()
        ele.send_keys(text)
        self.logger.appendContent( "控件[" + self.eleName + "]中输入文本["+text+"]")

    def selectByIndex(self,index):
        ele = self.find(10)
        Select(ele).select_by_index(index)
        self.logger.appendContent("根据index[" + index + "]选择控件["+ self.eleName +"]")

    def selectByValue(self,value):
        ele = self.find(10)
        Select(ele).select_by_value(value)
        self.logger.appendContent("根据value[" + value + "]选择控件[" + self.eleName + "]")


    def selectByText(self, text):
        ele = self.find(10)
        Select(ele).select_by_visible_text(text)
        self.logger.appendContent("根据text[" + text + "]选择控件[" + self.eleName + "]")

    def removeReadOnly(self):
        ele = self.find(10)
        self.driver.execute_script("arguments[0].readOnly=false", ele)
        self.logger.appendContent("将控件[" + self.eleName + "]的只读属性去掉")


    #得到属性
    def getAttr(self,attrName):
        ele = self.find(10)
        attrValue = ele.get_attribute(attrName)
        self.logger.appendContent("得到控件[" + self.eleName + "]的属性[" + attrName + "]的值为[" + attrValue + "]"  )
        return attrValue

    #移除属性
    def removeAttr(self,attrName):
        ele = self.find(10)
        self.driver.execute_script("arguments[0].removeAttribute('"+ attrName +"')",ele)


    #编辑属性
    def editAttr(self,attrName,attrValue):
        ele = self.find(10)
        self.driver.execute_script("arguments[0]."+ attrName +"='"+ attrValue +"';" ,ele)
        self.logger.appendContent("将控件[" + self.eleName + "]的属性[" + attrName + "]的值修改为[" + attrValue + "]" )

    #清除文本
    def clearText(self):
        ele = self.find(10)
        ele.clear()
        self.logger.appendContent("清除控件["+ self.eleName+"]的文本")

    #得到值
    def getValue(self):
        ele=self.find(10)
        value = self.driver.execute_script("return arguments[0].value", ele)
        self.logger.appendContent("得到控件[" + self.eleName + "]的值为[" + value + "]")
        return value



    #得到select 的 option的值的列表
    def getSlecterTexts(self):
        ele = self.find(10)
        options = ele.find_elements_by_xpath("./option");
        texts = []
        for option in options:
            texts.append(option.text)
        self.logger.appendContent("得到控件[" + self.eleName + "]的所有下拉选项为数组[ " + str(texts) + " ]")
        return texts

    #单选或者复选是否选中的状态
    def isChecked(self):
        ele = self.find(10)
        return self.driver.execute_script("return arguments[0].checked", ele)











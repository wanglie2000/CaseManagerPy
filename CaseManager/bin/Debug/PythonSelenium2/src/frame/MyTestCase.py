# encoding=utf-8
import unittest
import os

from frame.Pars import Pars
from frame.Logger import Logger

class MyTestCase(unittest.TestCase):
    logger = None
    pars = Pars()
    driver = None
    
    # 日志文件夹路径
    logDirPath = None
    # 日志文件路径
    logFilePath = None
    # 截屏文件夹路径
    screenCaptureDirPath = None
    # 截屏文件路径
    screenCapture = None
    # 测试类名
    testClassName = None


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


    def replacePars(self,text):
        tmp = text
        keys = self.pars.map.keys()
        for key in keys:
            if key!="LASTSTEP":
                tmp=tmp.replace("$"+ key +"$", str(self.getPar(key))  )
        return tmp
    
    def openURL(self, url):
        url=self.replacePars(url)
        self.driver.get(url) 
        self.logger.appendContent("打开或跳转网址[" + url + "]")   
    


    def getProjectDir(self):
        currentPath = os.getcwd()
        return os.path.abspath(currentPath + "\..\..")
        
    def setUp(self):
        print("setUp-------------------------")
        self.pars = Pars()
        projectDirPath = self.getProjectDir()
        
#         print("当前目录="+ projectDirPath)
        
        self.driver = None

        
        self.logDirPath = projectDirPath + "\\log\\" + self.testClassName.replace(".", "\\")
        self.screenCaptureDirPath = self.logDirPath + "\\screenCapture"
        self.logFilePath = self.logDirPath + "\\log.txt"
        self.screenCapture = self.screenCaptureDirPath + "\\img.jpg"
        self.logger = Logger(self.logFilePath, self.screenCapture)
        if not os.path.exists(self.logDirPath):
            os.makedirs(self.logDirPath)
  
        if not os.path.exists(self.screenCaptureDirPath):
            os.makedirs(self.screenCaptureDirPath)   
            
        if os.path.exists(self.screenCapture):
            os.remove(self.screenCapture)
        
        if os.path.exists(self.logFilePath):
            os.remove(self.logFilePath)
               

       
    def tearDown(self): 
        
        
        print("tearDown-----------------")    
        self.pars.clearPars()
        
        lastStepIsSuccess = self.pars.get("LASTSTEP")
        if lastStepIsSuccess:
            print("测试结果通过")
        else:
            print("测试结果不通过")    
        
        

        self.assertTrue(lastStepIsSuccess)

    def setPar(self, key, value):

        if isinstance( value,str ):
            self.pars.put(key, self.replacePars(value))
        else:
            self.pars.put( key,value )
        self.logger.appendContent("设置参数[" + key + "]的值为[" + str(value) + "]")

    def getPar(self,key):
        value = self.pars.get(key)
        self.logger.appendContent("得到参数[" + key + "]的值为[" + str(value) + "]")
        return value

    def addDic(self,dic):
        self.pars.addDic(dic)
        








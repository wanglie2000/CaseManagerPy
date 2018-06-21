#encoding=utf-8

import os
import autoit
import time;

class UpLoadWindow(object):
    def upload(self, filePath):
        xx=filePath[1:2]
        print(xx)
        if( filePath[1:2]!=':'  ):
            #是相对路径 要到files下面去找
            pwd = os.getcwd()
            tmp = pwd.split("src")[0]
            tmp = os.path.abspath(tmp+os.path.sep + "..")+"\\files"

            filePath = filePath.replace("/", "\\")
            if ( not filePath.startswith("\\")):
                filePath = "\\" + filePath
            filePath = tmp + filePath

        dirPath = os.path.dirname(filePath)
        if (not dirPath.endswith('\\')):
            dirPath = dirPath + '\\'

        l = len(dirPath)
        fileName = filePath[l:]

        #下面开始是在上传对话框上的操作
        time.sleep(1)
        autoit.win_active("打开")
        autoit.control_set_text("打开", "Edit1",dirPath)

        time.sleep(0.5)

        autoit.control_click("打开", "Button1")

        time.sleep(3)

        autoit.control_set_text("打开", "Edit1", fileName)

        autoit.control_click("打开", "Button1")


        # currentPath = os.getcwd()
        # uploadexe= currentPath+ "\\..\\..\\tools\\upload.exe"
        # command = uploadexe + " " + '"' + dirPath + '"' + ' ' + '"' + fileName + '"'
        # os.system(command)
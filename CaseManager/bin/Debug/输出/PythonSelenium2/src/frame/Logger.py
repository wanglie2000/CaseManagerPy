#encoding=utf-8

from PIL import ImageGrab
import datetime
import time

class Logger(object):
    filePath=""
    screenCapture=""
#     driver=None

    def __init__(self, filePath ,screenCapture):
        self.filePath =filePath
        self.screenCapture=screenCapture

    def appendContent(self,text):
        f = open(self.filePath,"a")
        now=datetime.datetime.now()
        timeStr = time.strftime('%Y-%m-%d %H:%M:%S',time.localtime(time.time()))
        print(timeStr+ "    "+ text)
        f.write(timeStr+ "    "+ text + "\r\n")
        f.close()
        
    def setDriver(self,driver):    
        self.driver=driver
        
    def snap(self):
        self.appendContent("截屏")  
        im = ImageGrab.grab()
        im.save(self.screenCapture,'jpeg')  
        
 
    
if __name__=="__main__":
    Logger("d:\\a.txt").appendContent("aaaaaa")     
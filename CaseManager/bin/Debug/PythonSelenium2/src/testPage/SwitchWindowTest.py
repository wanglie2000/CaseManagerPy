#coding=utf-8

from frame.EleDescription import EleDescription
from frame.PageOfPublic import PageOfPublic
from frame.FrameSwitcher import FrameSwitcher
from frame.WindowSwitcher import WindowSwitcher


class SwitchWindowTest(PageOfPublic):
    
    def switchTest(self):
        if self.lastStepIsPass():
            try:
                
                EleDescription(self.driver,"id","link","打开新窗口的测试链接",self.logger).click()
                
                wSwitch = WindowSwitcher( self.driver,self.logger )
                
                wSwitch.switchWindow(1)
                
                EleDescription(self.driver,"id","id2","输入框",self.logger).input("随便输点什么测试一下")
                
                wSwitch.closeAllwindowsExceptFirst()
                
                
                
            
            except Exception as e :
                self.setFailed(e)    
        
     
  
            
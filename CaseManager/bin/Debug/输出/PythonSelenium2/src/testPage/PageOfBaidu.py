#coding=utf-8

from frame.EleDescription import EleDescription
from frame.PageOfPublic import PageOfPublic
from frame.FrameSwitcher import FrameSwitcher



class PageOfBaidu(PageOfPublic):
    
    def searchInFrame(self):
        if self.lastStepIsPass():
            try:
                baiduFrame = EleDescription( self.driver,"id","baidu","frame窗口",self.logger )
                fSwitch = FrameSwitcher( self.driver, baiduFrame,self.logger )
                fSwitch.switchFrame()
                
                searchKey= self.pars.get("关键字");

                EleDescription( self.driver,"id", "kw", "关键字输入框", self.logger ).input(searchKey)
                EleDescription(self.driver,"id", "su", "搜索按钮", self.logger ).click()





                fSwitch.exitFrame()
                
            
            except Exception as e :
                self.setFailed(e)    
        
     ####百度搜索####
    def search(self):
        if self.lastStepIsPass():
            try:
                searchKey= self.pars.get("关键字");

                EleDescription( self.driver,"id", "kw", "关键字输入框", self.logger ).input(searchKey)
                EleDescription(self.driver,"id", "su", "搜索按钮", self.logger ).clickByLocate()

                EleDescription(self.driver, "id", "kw", "关键字输入框", self.logger).getElementScreenLacation()

            except Exception as e :
                self.setFailed(e)	











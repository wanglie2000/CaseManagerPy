# encoding=utf-8
'''
Created on 2017年12月27日

@author: Administrator
'''

class WindowSwitcher(object):
    '''
    classdocs
    '''
    driver = None
    logger = None


    def __init__(self, driver, logger):
        self.driver = driver
        self.logger = logger
    
    def switchWindow(self, index):
        handles = self.driver.window_handles
        self.driver.switch_to.window(handles[index])
        self.logger.appendContent("切换到窗口[" + str(index) + "]")
        
    
    
    def switchEndWindow(self): 
        handles = self.driver.window_handles
        self.driver.switch_to.window(handles[-1])
        self.logger.appendContent("切换到最后一个窗口")
    
    def closeAllwindowsExceptFirst(self):
        while len(self.driver.window_handles) > 1:
            self.driver.switch_to.window(self.driver.window_handles[-1])
            self.driver.close()
            self.logger.appendContent("切换到第一个窗口并关闭其他所有窗口")
        self.driver.switch_to.window( self.driver.window_handles[0] )
            
    
    def back(self):
        self.driver.back()
        self.logger.appendContent("回退窗口")
        
    
    def forward(self):
        self.forward()
        self.logger.appendContent("前进窗口")
        
        
    def refresh(self):
        self.refresh()  
        self.logger.appendContent("刷新窗口")  
        

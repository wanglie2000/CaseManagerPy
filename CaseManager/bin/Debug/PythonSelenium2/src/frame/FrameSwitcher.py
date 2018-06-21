# encoding=utf-8

class FrameSwitcher(object):
    
    driver = None
    frame = None
    logger = None


    def __init__(self, driver, frame, logger):
        self.driver = driver
        self.frame = frame
        self.logger = logger

    def switchFrame(self):
        self.driver.switch_to.frame(self.frame.find())
        self.logger.appendContent("进入frame[" + self.frame.eleName + "]")
            
    def exitFrame(self):
        self.driver.switch_to.default_content()
        self.logger.appendContent("退出frame[" + self.frame.eleName + "]")

#encoding=utf-8

from frame.MyTestCase import MyTestCase
from frame.DriverInit import DriverInit
from frame.PageOfPublic import PageOfPublic
import time
import json
from testPage.demo.PageOfTest import PageOfTest
from testPage.walmart.WalmartQuery import WalmartQuery
from testPage.walmart.WalmartQuery2 import WalmartQuery2
from testPage.zmy.PageOfLogin import PageOfLogin
from testPage.zmy.PageOfStudent import PageOfStudent
from testPage.PageOfBaidu import PageOfBaidu
from testPage.SwitchWindowTest import SwitchWindowTest
from testPage.UpLoadTest import UpLoadTest






class TmpCase(MyTestCase):

    def test(self):
        print("test-----------------")
        pP = PageOfPublic(self.pars, None, self.logger)

        
        #步骤1生成的代码

        if self.getPar('LASTSTEP'):
            self.driver = DriverInit(self.logger).getWebDriver('chrome')
            pP=PageOfPublic(self.pars,self.driver,self.logger)

        #步骤2生成的代码

        if self.getPar('LASTSTEP'):
            self.openURL('http://publish.kokoerp.com')



        #步骤5生成的代码

        pP.oneStep('存储控件的图像','''captcha||验证码||xxx.png''')

        #步骤6生成的代码

        pP.oneStep('识别图片中的字母和数字','''xxx.png||yzm''')

        #步骤7生成的代码

        pP.oneStep('调试输出','''$yzm$''')

        #步骤8生成的代码

        time.sleep(3)

        #步骤9生成的代码

        if None!=self.driver:
            self.driver.quit()
            self.logger.appendContent("退出")


        
        
    def setUp(self):
        self.testClassName = __name__
        #print("类名=" + __name__)
        MyTestCase.setUp(self)